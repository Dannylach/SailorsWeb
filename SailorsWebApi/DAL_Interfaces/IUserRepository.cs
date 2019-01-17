using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        void AddUser(Users user);
        void DeleteUser(string userNsame);
        bool IsUser(int userId);
        bool IsUser(string userName);

    }
}
