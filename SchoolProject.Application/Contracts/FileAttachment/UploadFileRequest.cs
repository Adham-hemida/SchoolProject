using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.FileAttachment;
public record UploadFileRequest(
	IFormFile File
	);
