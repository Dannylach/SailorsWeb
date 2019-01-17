using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class FunctionsRepository : Repository<Functions>, IFunctionsRepository
    {
        private readonly DbSet<Functions> functions;

        public FunctionsRepository(HowDatabaseEntities context) : base(context)
        {
            functions = context.Functions;
        }

        public string GetById(int? id)
        {
            var functionName = id != null ? functions.Where(x => x.functionId == id).Select(x => x.functionName).ToList() : null;
            return functionName?[0].Trim(' ') ?? "brak funkcji";
        }

        public bool IsId(int id)
        {
            var getAll = GetAll();
            var idsList = new List<int>();
            foreach (var function in getAll)
            {
                idsList.Add(function.functionId);
            }

            return idsList.Contains(id);
        }

        public void AddFunction(string functionName)
        {
            var allFunctions = functions.Select(x => x.functionId).ToList();
            var lastId = allFunctions.Max();
            var function = new Functions
            {
                functionId = lastId + 1,
                functionName = functionName
            };
            functions.Add(function);
        }

        public void UpdateFunction(Functions function)
        {
            var dataFromDb = functions.Find(function);
            dataFromDb = function;
        }

        public void DeleteFunction(string functionName)
        {
            functions.Remove(GetEntity(functionName));
        }

        private Functions GetEntity(string functionName)
        {
            return functions.FirstOrDefault(x => x.functionName == functionName);
        }
    }
}