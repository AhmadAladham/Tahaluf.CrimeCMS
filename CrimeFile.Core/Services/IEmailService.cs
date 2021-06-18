using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrimeFile.Core.Services
{
    public interface IEmailService : IBaseService
    {
        Task<string> SendVerificationCode(string Email, int Code);
    }
}
