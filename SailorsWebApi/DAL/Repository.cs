using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected HowDatabaseEntities Context;

        public Repository(HowDatabaseEntities context)
        {
            this.Context = context;
        }

        protected Repository()
        {
            Context = new HowDatabaseEntities();
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public void Save()
        {
                Context.SaveChanges();
        }

        public void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }
    }
}