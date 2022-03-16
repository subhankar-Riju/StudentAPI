using Student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Repository
{
    public interface IAccountRepository
    {
       Task SignUpAsyn(SignUpModel signUpModel);
        Task<IEnumerable<SignUpModel>> Loginasync(LoginModel login);
    }
}
