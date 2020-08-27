using API.Model;
using API.Model.Enum;
using API.Model.Model;
using AutoMapper;
using SPA.Repository.Repository;
using SPA.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.BUS.Service
{
    public class AuthorizedService
    {
        public IRepositoryHelper _repositoryHelper { get; }

        public AuthorizedService()
        {
            this._repositoryHelper = new RepositoryHelper();
        }

        /// <summary>
        /// Check authoried of therapist.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<LogicResult<User>> GetStaffAuthoried(string username, string password)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var therapistrepo = _repositoryHelper.GetRepository<ITherapistRepository>(unitofwork);

            var staff = await therapistrepo.GetAsync(x => x.userName == username && x.passWord == password);

            if (staff.Count() == 0)
                return new LogicResult<User>() { IsSuccess = false, message = Validation.FileNotFound };

            var user = new User()
            {
                UserName = username,
                Password = password,
                Type = staff.FirstOrDefault().Possition == (int)Position.therapist? Position.therapist.ToString()
                        : staff.FirstOrDefault().Possition == (int)Position.admin ? Position.admin.ToString() 
                        : Position.outletManager.ToString(),
            };
   
            return new LogicResult<User>() { IsSuccess = true, Result = user };
        }

        /// <summary>
        /// Check authoried of customer
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<LogicResult<User>> GetCustomerAuthoried(string username, string password)
        {
            var unitofwork = _repositoryHelper.GetUnitOfWork();
            var repo = _repositoryHelper.GetRepository<ICustomerRepository>(unitofwork);

            var customer = await repo.GetAsync(x => (x.Phone == username || x.Email == username) && x.passWord == password);

            if (customer.Count() == 0)
                return new LogicResult<User>() { IsSuccess = false, message = Validation.FileNotFound };

            var user = new User()
            {
                UserName = username,
                Password = password,
                Type = Position.customer.ToString(),
            };

            return new LogicResult<User>() { IsSuccess = true, Result = user };
        }

        public  string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
