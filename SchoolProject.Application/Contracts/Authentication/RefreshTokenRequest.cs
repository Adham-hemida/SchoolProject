using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Application.Contracts.Authentication;
public record RefreshTokenRequest(
	string token,
	string refreshToken
	);
