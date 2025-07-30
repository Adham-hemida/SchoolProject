using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Abstractions.Consts;

public static class DefaultRoles
{
	public partial class Admin
	{
		public const string Name = nameof(Admin);
		public const string Id = "019855ea-fef8-708d-ab80-da9e81d2325c";
		public const string ConcurrencyStamp = "019855ea-fef8-708d-ab80-da9f845c911f";
	}

	public partial class Member
	{
		public const string Name = nameof(Member);
		public const string Id = "019855ea-fef8-708d-ab80-daa013145d98";
		public const string ConcurrencyStamp = "019855ea-fef8-708d-ab80-daa156960d15";
	}	
	
	public partial class Teacher
	{
		public const string Name = nameof(Teacher);
		public const string Id = "01985a37-f9c5-7676-93c6-fd7259f4b646";
		public const string ConcurrencyStamp = "01985a37-f9c5-7676-93c6-fd73c880f710";
	}
	
	public partial class Student
	{
		public const string Name = nameof(Student);
		public const string Id = "01985a37-f9c5-7676-93c6-fd740ea964e2";
		public const string ConcurrencyStamp = "01985a37-f9c5-7676-93c6-fd75e1ce1428";
	}

}