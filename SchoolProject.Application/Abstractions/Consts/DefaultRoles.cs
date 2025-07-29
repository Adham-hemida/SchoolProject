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

}