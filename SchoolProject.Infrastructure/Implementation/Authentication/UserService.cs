using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Authentication;
public class UserService(UserManager<ApplicationUser> userManager,
	IRoleService roleService,
	IUnitOfWork unitOfWork)  :IUserService
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly IRoleService _roleService = roleService;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<StudentUserResponse>> CreateStudentUserAsync(CreateStudentRequest request, CancellationToken cancellationToken = default)
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
		user.UserName = request.Email;
		user.EmailConfirmed = true; 

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

}

