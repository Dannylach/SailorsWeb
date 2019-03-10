using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        public HttpCookie Login(Users contact)
        {
            bool validEmail = users.Any(x => x.email == contact.email);

            if (!validEmail)
            {
                return null;
            }

            string password = users.Where(x => x.email == contact.email)
                .Select(x => x.passwordHash)
                .Single();

            bool passwordMatches = Crypto.VerifyHashedPassword(password, contact.passwordHash);

            if (!passwordMatches)
            {
                return null;
            }

            string authId = Guid.NewGuid().ToString();
            
            var cookie = new HttpCookie("AuthID");
            cookie.Value = authId;

            return cookie;
        }

        public void AddUser(Users user)
        {
            users.Add(user);
        }

        public void DeleteUser(Users user)
        {
            users.Remove(user);
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