using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Settings;
public static class FileSettings
{
	public const int MaxFileSizeInMB = 1;
	public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024; // 1MB in bytes

	public static readonly string[] BlockSignatures = ["2F-2A", "4D-5A", "D0-CF"];
	public static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png"];
}
