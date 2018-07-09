using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApiContent.Models;
using ApiContent.DataAccess;

namespace ApiContent.DataAccess
{
    public class UserData : IUserData
    {
        private DataContext _dataContext;

        public UserData()
        {
            _dataContext = new DataContext();
        }

        public async Task<List<User>> GetUsers(string filter)
        {
            UserFilter uFilter = new UserFilter(filter);
            IQueryable<User> users = _dataContext.Users;
            if (!string.IsNullOrWhiteSpace(uFilter.Email)) users = users.Where(u => u.Email.Contains(uFilter.Email));
            if (!string.IsNullOrWhiteSpace(uFilter.Name)) users = users.Where(u => u.FirstName.Contains(uFilter.Name) || u.LastName.Contains(uFilter.Name) );
            return await users.ToListAsync();
        }

        public async Task<User> GetUser(string email)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<int> AddOrUpdateUser(User user)
        {
            _dataContext.Entry(user).State = user.Id == 0 ?
                         EntityState.Added :
                         EntityState.Modified;

            await _dataContext.SaveChangesAsync();
            return user.Id;
        }

    }
}