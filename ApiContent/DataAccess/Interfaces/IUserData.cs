using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiContent.Models;

namespace ApiContent.DataAccess
{
    interface IUserData
    {
        Task<List<User>> GetUsers(string filter);
        Task<int> AddOrUpdateUser(User user);
        Task<User> GetUser(string email);
    }
}
