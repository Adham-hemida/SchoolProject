using Mapster;
using SchoolProject.Application.Abstractions;
using SchoolProject.Application.Contracts.Subject;
using SchoolProject.Application.ErrorHandler;
using SchoolProject.Application.Interfaces.IServices;
using SchoolProject.Application.Interfaces.IUnitOfWork;
using SchoolProject.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Implementation.Services;
public  class SubjectService(IUnitOfWork unitOfWork, ApplicationDbContext context) : ISubjectService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly ApplicationDbContext _context = context;

	public async Task<Result<SubjectResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		var subject =await _unitOfWork.Repository<Subject>().GetByIdAsync(id, cancellationToken);

		if (subject is null)
			return Result.Failure<SubjectResponse>(SubjectErrors.SubjectNotFound);

		return Result.Success(subject.Adapt<SubjectResponse>());
	}
}
