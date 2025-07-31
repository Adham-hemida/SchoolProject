using Mapster;
using SchoolProject.Application.Contracts.User;
using SchoolProject.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Mapping;
public class MappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateUserRequest, ApplicationUser>()
         .Map(dest => dest.UserName, src => src.Email)
         .Map(dest => dest.EmailConfirmed, src => true);


		config.NewConfig<CreateStudentRequest, ApplicationUser>()
         .Map(dest => dest.UserName, src => src.Email)
         .Map(dest => dest.EmailConfirmed, src => true);

	config.NewConfig<TeacherUserRequest, ApplicationUser>()
         .Map(dest => dest.UserName, src => src.Email)
         .Map(dest => dest.EmailConfirmed, src => true);

	}
}
