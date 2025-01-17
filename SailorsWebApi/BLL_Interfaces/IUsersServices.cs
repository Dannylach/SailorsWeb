﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.BLL_Interfaces
{
    public interface IUsersServices
    {
        ResponseWrapper<object> GetAllUsersData();
        ResponseWrapper<object> GetAllUserNames();
        ResponseWrapper<object> GetAllByFunction(int functionId);
        ResponseWrapper<object> GetUserById(int userId);
        ResponseWrapper<object> GetUserById(int? userId);
        ResponseWrapper<object> GetUserDataByName(string userName);
        ResponseWrapper<object> GetUserDataById(int userId);
        ResponseWrapper<object> AddUser(string userName, string userPassword, string userEmail, string phoneNumber,int functionId, string name, string surname);
        ResponseWrapper<object> AddUser(Users user);
        ResponseWrapper<object> UpdateUser(int userId, string userName, string userPassword, string userEmail, string phoneNumber, int functionId, string name, string surname);
        ResponseWrapper<object> DeleteUser(string userName);
        ResponseWrapper<object> GetAllFunctions();
        string GetFunctionById(int functionId);
        ResponseWrapper<object> AddFunction(string functionName);
        ResponseWrapper<object> UpdateFunction(int functionId, string functionName);
        ResponseWrapper<object> DeleteFunction(string functionName);
        ResponseWrapper<object> Login(Users contact);
    }
}
