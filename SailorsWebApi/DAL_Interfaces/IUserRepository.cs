using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        List<int> GetAllIds();
        List<Users> GetAllByFunction(int functionId);
        Users GetUserById(int userId);
        Users GetUserById(int? userId);
        Users GetUserByName(string userName);
        HttpCookie Login(Users contact);
        void AddUser(Users user);
        void DeleteUser(Users user);
        bool IsUser(int userId);
        bool IsUser(string userName);

    }
}
