using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Abstractions.Consts;
using SchoolProject.Application.Contracts.Student;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IAuthentication;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class UserService(UserManager<ApplicationUser> userManager,
	IRoleService roleService,
	IUnitOfWork unitOfWork) : IUserService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly IRoleService _roleService = roleService;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<StudentUserResponse>> CreateStudentWithUserAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
		if (emailIsExist)
			return Result.Failure<StudentUserResponse>(UserErrors.DuplicatedEmail);

		var departmentExists = await _unitOfWork.Repository<Department>()
		  .AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);

		if (!departmentExists)
			return Result.Failure<StudentUserResponse>(DepartmentErrors.DepartmentNotFound);

		var studentIsExists = await _unitOfWork.Repository<Student>().AnyAsync(x =>
		   x.FirstName.Trim().ToLower() == request.FirstName.Trim().ToLower()
		   && x.LastName.Trim().ToLower() == request.LastName.Trim().ToLower()
		   && x.Phone == request.Phone
		   && x.DepartmentId == request.DepartmentId,
		   cancellationToken);

		if (studentIsExists)
			return Result.Failure<StudentUserResponse>(StudentErrors.DuplicatedStudent);


		var user = request.Adapt<ApplicationUser>();

		var result = await _userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
		{
			var error = result.Errors.First();
			return Result.Failure<StudentUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}

		await _userManager.AddToRoleAsync(user, DefaultRoles.Student.Name);

		var student = new Student
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Phone = request.Phone,
			Address = request.Address,
			DepartmentId = request.DepartmentId,
			UserId = user.Id
		};

		await _unitOfWork.Repository<Student>().CreateAsync(student, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);


		var response = new StudentUserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.IsDisabled, DefaultRoles.Student.Name);
		return Result.Success(response);

	}

	public async Task<Result<StudentUserResponse>> AssignUserToStudentAsync(CreateUserRequest request, Guid studentId, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
		if (emailIsExist)
			return Result.Failure<StudentUserResponse>(UserErrors.DuplicatedEmail);

		var student = await _unitOfWork.Repository<Student>()
				.GetAsQueryable()
				.SingleOrDefaultAsync(x => x.Id == studentId, cancellationToken);

		if (student is null)
			return Result.Failure<StudentUserResponse>(StudentErrors.StudentNotFound);

		if (!string.IsNullOrWhiteSpace(student.UserId))
			return Result.Failure<StudentUserResponse>(StudentErrors.AlreadyHasUser);

		var user = request.Adapt<ApplicationUser>();

		var result = await _userManager.CreateAsync(user, request.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, DefaultRoles.Student.Name);

			student.UserId = user.Id;
			_unitOfWork.Repository<Student>().Update(student);
			await _unitOfWork.CompleteAsync(cancellationToken);

			var response = new StudentUserResponse(user.Id, user.FirstName, user.LastName, user.Email!, user.IsDisabled, DefaultRoles.Student.Name);
			return Result.Success(response);
		}
		else
		{
			var error = result.Errors.First();
			return Result.Failure<StudentUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
	}

	public async Task<Result<TeacherUserResponse>> CreateTeacherWithUserAsync(TeacherUserRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

		if (emailIsExist)
			return Result.Failure<TeacherUserResponse>(UserErrors.DuplicatedEmail);

		var teacherIsExists = await _unitOfWork.Repository<Teacher>().AnyAsync(x => x.Email == request.Email && x.Phone == request.Phone, cancellationToken);

		if (teacherIsExists)
			return Result.Failure<TeacherUserResponse>(TeacherErrors.DuplicatedTeacher);

		var user = request.Adapt<ApplicationUser>();
		var result = await _userManager.CreateAsync(user, request.Password);
		if (!result.Succeeded)
		{
			var error = result.Errors.First();
			return Result.Failure<TeacherUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
		await _userManager.AddToRoleAsync(user, DefaultRoles.Teacher.Name);
		var teacher = new Teacher
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Phone = request.Phone,
			Email = request.Email,
			UserId = user.Id
		};
		await _unitOfWork.Repository<Teacher>().CreateAsync(teacher, cancellationToken);
		await _unitOfWork.CompleteAsync(cancellationToken);
		var response = new TeacherUserResponse(user.Id, user.FirstName, user.LastName, user.Email!, teacher.Phone, user.IsDisabled, DefaultRoles.Teacher.Name);
		return Result.Success(response);

	}


	public async Task<Result<TeacherUserResponse>> AssignUserToTeacherAsync(CreateUserRequest request, Guid teacherId, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
		
		if (emailIsExist)
			return Result.Failure<TeacherUserResponse>(UserErrors.DuplicatedEmail);
		
		var teacher = await _unitOfWork.Repository<Teacher>()
				.GetAsQueryable()
				.SingleOrDefaultAsync(x => x.Id == teacherId, cancellationToken);
	
		if (teacher is null)
			return Result.Failure<TeacherUserResponse>(TeacherErrors.TeacherNotFound);
		
		if (!string.IsNullOrWhiteSpace(teacher.UserId))
			return Result.Failure<TeacherUserResponse>(TeacherErrors.AlreadyHasUser);
		
		var user = request.Adapt<ApplicationUser>();
		var result = await _userManager.CreateAsync(user, request.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, DefaultRoles.Teacher.Name);
			teacher.UserId = user.Id;
		    _unitOfWork.Repository<Teacher>().Update(teacher);
			await _unitOfWork.CompleteAsync(cancellationToken);
			var response = new TeacherUserResponse(user.Id, user.FirstName, user.LastName, user.Email!, teacher.Phone, user.IsDisabled, DefaultRoles.Teacher.Name);
			return Result.Success(response);
		}
		else
		{
			var error = result.Errors.First();
			return Result.Failure<TeacherUserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
	}

	public async Task<Result<UserResponse>>CreateAsync(CreateUserWithRolesRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
		if (emailIsExist)
			return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

		var allowRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

		if (request.Roles.Except(allowRoles.Select(x => x.Name)).Any())
			return Result.Failure<UserResponse>(UserErrors.InvalidRoles);

		var user = request.Adapt<ApplicationUser>();
		user.UserName = request.Email;
		user.EmailConfirmed = true; 

		var result = await _userManager.CreateAsync(user, request.Password);
		if (result.Succeeded)
		{
			await _userManager.AddToRolesAsync(user, request.Roles);
			var response = (user, request.Roles).Adapt<UserResponse>();
			return Result.Success(response);
		}
		else
		{
			var error = result.Errors.First();
			return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
	}

	public async Task<Result> UpdateAsync(string userId, UpdateUserRequest request, CancellationToken cancellationToken = default)
	{
		var emailIsExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != userId, cancellationToken);
		if (emailIsExist)
			return Result.Failure(UserErrors.DuplicatedEmail);

		var allowRoles = await _roleService.GetAllAsync(cancellationToken: cancellationToken);

		if (request.Roles.Except(allowRoles.Select(x => x.Name)).Any())
			return Result.Failure(UserErrors.InvalidRoles);

		if (await _userManager.FindByIdAsync(userId) is not { } user)
			return Result.Failure(UserErrors.UserNotFound);

		user = request.Adapt(user);
		var result = await _userManager.UpdateAsync(user);
		if (result.Succeeded)
		{
			var currentRoles = await _userManager.GetRolesAsync(user);
			var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
			if (!removeRolesResult.Succeeded)
			{
				var error = removeRolesResult.Errors.First();
				return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
			}

			await _userManager.AddToRolesAsync(user, request.Roles);
			return Result.Success();
		}
		else
		{
			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
	}

	public async Task<Result<UserResponse>> GetAsync(string userId)
	{
		if (await _userManager.FindByIdAsync(userId) is not { } user)
			return Result.Failure<UserResponse>(UserErrors.UserNotFound);

		var userRoles = await _userManager.GetRolesAsync(user);
		//	var response = new UserResponse(user.Id, user.FirstName, user.LastName, user.Email, user.IsDisabled, roles);
		var response = (user, userRoles).Adapt<UserResponse>();
		return Result.Success(response);
	}


	public async Task<Result> UnlockAsync(string userId)
	{
		var user = await _userManager.FindByIdAsync(userId);
		if (user is null)
			return Result.Failure(UserErrors.UserNotFound);

		var result = await _userManager.SetLockoutEndDateAsync(user, null);

		if (result.Succeeded)
			return Result.Success();

		var error = result.Errors.First();
		return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
	}
}