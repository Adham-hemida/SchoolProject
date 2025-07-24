using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Settings;
public  class JwtOptions
{
	public static string sectionName = "Jwt";
	[Required]
	public string Key { get; set; } = string.Empty;
	[Required]
	public string Issuer { get; set; } = string.Empty;
	[Required]
	public string Audience { get; set; } = string.Empty;
	[Required]
	[Range(1, int.MaxValue)]
	public int ExpiresInMinutes { get; set; }
}
