using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IFunctionsRepository : IRepository<Functions>
    {
        string GetById(int? id);
        bool IsId(int id);
        void AddFunction(string functionName);
        void DeleteFunction(string functionName);
        void UpdateFunction(Functions function);
    }
}
