using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Subject;
public record SubjectRequest(string Name ,int CreditHours, Guid TeacherId,string? Description);

