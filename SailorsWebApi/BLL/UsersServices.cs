using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;
using Newtonsoft.Json;


namespace SailorsWebApi.BLL
{
    public class UsersServices : IUsersServices
    {
        private readonly IUserRepository userRepository;
        private readonly IFunctionsRepository functionsRepository;

        public UsersServices(IUserRepository userRepository, IFunctionsRepository functionsRepository)
        {
            this.functionsRepository = functionsRepository;
            this.userRepository = userRepository;
        }

        #region GetFunctions

        #region Users

        public ResponseWrapper<object> GetAllUsersData()
        {
            try
            {
                var getAll = userRepository.GetAll();
                var resultList = new List<object>();

                foreach (var user in getAll)
                {
                    var function = functionsRepository.GetById(user.functionId);
                    var instance = new
                    {
                        name = user.Name?.Trim(' '),
                        surname = user.Surname?.Trim(' '),
                        userName = user.userName.Trim(' '),
                        userEmail = user.email?.Trim(' '),
                        user.phoneNumber,
                        function
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }
        
        public ResponseWrapper<object> GetAllUserNames()
        {
            try
            {
                var getAll = userRepository.GetAll();
                var resultList = new List<object>();

                foreach (var user in getAll)
                {
                    resultList.Add(user.userName.Trim(' '));
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetAllByFunction(int functionId)
        {
            try
            {
                if (!functionsRepository.IsId(functionId))
                    return new ResponseWrapper<object>("Brak takiej funkcji na liście", false);
                var getAllByFunction = userRepository.GetAllByFunction(functionId);
                var resultList = new List<string>();
                if (getAllByFunction.Count == 0)
                    return new ResponseWrapper<object>("Brak użytkowników z taką funkcją", false);

                foreach (var user in getAllByFunction)
                {
                    resultList.Add(user.userName.Trim(' '));
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetUserById(int userId)
        {
            try
            {
                if (!userRepository.IsUser(userId))
                    return new ResponseWrapper<object>("Brak użytkownika z takim numerem ID", false);
                var getById = userRepository.GetUserById(userId);

                return new ResponseWrapper<object>(getById.userName.Trim(' '), true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetUserById(int? usersId)
        {
            try
            {
                if (usersId == null)
                    return new ResponseWrapper<object>("None", true);
                var userId = (int) usersId;
                if (!userRepository.IsUser(userId))
                    return new ResponseWrapper<object>("None", false);
                var getById = userRepository.GetUserById(userId);

                return new ResponseWrapper<object>(getById.userName.Trim(' '), true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetUserDataByName(string userName)
        {
            try
            {
                var user = userRepository.GetUserByName(userName);
                var function = functionsRepository.GetById(user.functionId);
                var instance = new
                {
                    name = user.Name?.Trim(' '),
                    surname = user.Surname?.Trim(' '),
                    userName = user.userName.Trim(' '),
                    userEmail = user.email?.Trim(' '),
                    user.phoneNumber,
                    function
                };

                return new ResponseWrapper<object>(instance, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetUserDataById(int userId)
        {
            try
            {
                var user = userRepository.GetUserById(userId);
                var function = functionsRepository.GetById(user.functionId);
                var instance = new
                {
                    name = user.Name?.Trim(' '),
                    surname = user.Surname?.Trim(' '),
                    userName = user.userName.Trim(' '),
                    userEmail = user.email?.Trim(' '),
                    user.phoneNumber,
                    function
                };

                return new ResponseWrapper<object>(instance, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region Functions

        public ResponseWrapper<object> GetAllFunctions()
        {
            try
            {
                var getAll = functionsRepository.GetAll();
                var resultList = new List<object>();

                foreach (var function in getAll)
                    resultList.Add(function.functionName.Trim(' '));

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #endregion

        #region AddFunctions

        public ResponseWrapper<object> AddUser(string userName, string userPassword, string userEmail, string phoneNumber, int functionId, string name, string surname)
        {
            try
            {
                var user = new Users
                {
                    userId = GetFreeId(),
                    userName = userName,
                    passwordHash = userPassword,
                    email = userEmail,
                    phoneNumber = phoneNumber,
                    functionId = functionId,
                    Name = name,
                    Surname = surname
                };
                userRepository.AddUser(user);
                userRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> AddFunction(string functionName)
        {
            try
            {
                functionsRepository.AddFunction(functionName);
                functionsRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region UpdateFunctions

        public ResponseWrapper<object> UpdateUser(int userId, string userName, string userEmail, string phoneNumber, int functionId, string name, string surname)
        {
            try
            {
                var updateData = new Users
                {
                    userId = userId,
                    userName = userName,
                    email = userEmail,
                    phoneNumber = phoneNumber,
                    functionId = functionId,
                    Name = name,
                    Surname = surname
                };
                userRepository.Update(updateData);
                userRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> UpdateFunction(int functionId, string functionName)
        {
            try
            {
                var function = new Functions
                {
                    functionId = functionId,
                    functionName = functionName
                };
                functionsRepository.Update(function);
                functionsRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region DeleteFunctions

        public ResponseWrapper<object> DeleteUser(string userName)
        {
            try
            {
                var userToDel = userRepository.GetUserByName(userName);
                userRepository.DeleteUser(userToDel);
                userRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }
        
        public ResponseWrapper<object> DeleteFunction(string functionName)
        {
            try
            {
                functionsRepository.DeleteFunction(functionName);
                functionsRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region HelperFunctions
        
        private int GetFreeId()
        {
            var user = userRepository.GetAllIds();
            if (user == null) return 0;
            user.Sort();
            var lastId = user.Last();
            if (user.Count != lastId)
            for (var i = 0; i < lastId; i++)
            {
                if(user.Contains(i)) continue;
                return i;
            }
            return lastId + 1;
        }

        #endregion

        #region Authorization

        public ResponseWrapper<object> Login(Users contact)
        {
            var cookie = userRepository.Login(contact);
            return cookie != null? new ResponseWrapper<object>(cookie, true) : new ResponseWrapper<object>("Wrong user name or password.", false);
        }

        #endregion
    }
}