using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Abstractions.Consts;
public static class RateLimiters
{
	public const string IpLimiter = "ipLimit";
	public const string UserLimiter = "userLimit";
	public const string Concurrency = "concurrency";
}
