using Student.Data;
using Student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Student.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly StudentDbContext _context;

        public AccountRepository(StudentDbContext context)
        {
            _context = context;
        }
        public async Task SignUpAsyn(SignUpModel signUpModel)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(signUpModel.password));
            string _stringuUnicode = Encoding.UTF8.GetString(hashValue);

            //string str = hashValue.ToString();
            var signup = new SignUp()
            {
                name = signUpModel.name,
                email = signUpModel.email,
                password=Convert.ToBase64String(hashValue)

            };

            await _context.signup.AddAsync(signup);
            await _context.SaveChangesAsync();

          


        }

        public async Task<IEnumerable<SignUpModel>> Loginasync(LoginModel login)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(login.password));
           // string _stringuUnicode = Encoding.UTF8.GetString(hashValue);

            var auth =  _context.signup
                .Where(x => x.email==(login.email))
                .Where(x => x.password==(Convert.ToBase64String(hashValue)))
                .ToList();

            var record = auth.Select(x => new SignUpModel
            {
                name = x.name,
                email = x.email,
                password = x.password
            });

            return record;
        }
    }
}
