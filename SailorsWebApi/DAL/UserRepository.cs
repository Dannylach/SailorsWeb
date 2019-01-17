using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly DbSet<Users> users;

        public UserRepository(HowDatabaseEntities context) : base(context)
        {
            users = context.Users;
        }

        #region GetFunctions

        public List<int> GetAllIds()
        {
            return users.Select(x => x.userId).ToList();
        }

        public List<Users> GetAllByFunction(int functionId)
        {
            var fromDatabase = users.Where(x => x.functionId == functionId).Select(x => x).ToList();
            return fromDatabase;
        }

        public Users GetUserById(int userId)
        {
            return users.FirstOrDefault(x => x.userId == userId);
        }

        public Users GetUserById(int? userId)
        {
            if (userId != null) return users.FirstOrDefault(x => x.userId == userId);
            throw new Exception("Nie podano informacji o rezerwującym.");
        }

        public Users GetUserByName(string userName)
        {
            return users.FirstOrDefault(x => x.userName == userName);
        }

        #endregion

        public void AddUser(Users user)
        {
            users.Add(user);
        }

        public void DeleteUser(string userName)
        {
            users.Remove(users.Find(userName));
        }
        
        public bool IsUser(int userId)
        {
            return users.Find(userId) != null;
        }

        public bool IsUser(string userName)
        {
            return users.Find(userName) != null;
        }
    }
}