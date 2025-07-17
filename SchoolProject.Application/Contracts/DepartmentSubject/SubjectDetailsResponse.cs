using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.DepartmentSubject;
public record SubjectDetailsResponse(int Id, string Name, bool IsActive);
