using API.Model.Enum;
using API.Model.Model;
using SPA.BUS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPA.API.Authentication
{
    public class UserSevice
    {
        private readonly AuthorizedService authorizedService;

        public UserSevice()
        {
            authorizedService = new AuthorizedService();
        }

        public User GetUserByCredentials(string username, string password, string role)
        { 
            //therapist
            if (role == Position.therapist.ToString())
            {
                var user = authorizedService.GetStaffAuthoried(username, authorizedService.CreateMD5(password));
                if (!user.Result.IsSuccess)
                {
                    return null;
                }
                if (user.Result.Result.Type != Position.therapist.ToString())
                {
                    return null;
                }
                return user.Result.Result;
            }
            //customer
            if (role == Position.customer.ToString())
            {
                var user = authorizedService.GetCustomerAuthoried(username, authorizedService.CreateMD5(password));
                if (!user.Result.IsSuccess)
                {
                    return null;
                }
                return user.Result.Result;
            }
            return null;
        }
    }
}