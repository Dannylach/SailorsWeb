using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class HarbourRentRepository : Repository<HabourRenting>, IHarbourRentRepository
    {
        private readonly DbSet<HabourRenting> harbourRentings;

        public HarbourRentRepository(HowDatabaseEntities context) : base(context)
        {
            harbourRentings = context.HabourRenting;
        }

        #region GetFunctions
        
        public List<HabourRenting> GetHarbourRentings()
        {
            return harbourRentings.Any() ? harbourRentings.ToList() : null;
        }

        public List<HabourRenting> GetHarbourRentingsByRenter(int renterId)
        {
            return harbourRentings.Where(x => x.renterId == renterId).ToList();
        }

        public List<HabourRenting> GetHarbourRentingsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return harbourRentings.Where(x => x.rentTimeStart < startDate).Where(x => x.rentTimeEnd > endDate).ToList();
        }

        public HabourRenting GetHarbourRentingById(int rentingId)
        {
            return harbourRentings.FirstOrDefault(x => x.rentId == rentingId);
        }

        public List<int> GetAllHarbourRentIds()
        {
            return harbourRentings.Select(x => x.rentId).ToList();
        }

        #endregion

        #region AddFunctions

        public void RentHarbour(HabourRenting newRenting)
        {
            harbourRentings.Add(newRenting);
            Save();
        }

        public void RentHarbour(int userId, DateTime startDate, DateTime endDate, string rentName,
            string rendDescription)
        {
            var rented = new HabourRenting
            {
                renterId = userId,
                rentTimeStart = startDate,
                rentTimeEnd = endDate,
                rentName = rentName,
                rentDescription = rendDescription
            };
            harbourRentings.Add(rented);
            Save();
        }

        #endregion

        public void DeleteRent(int rentId)
        {
            harbourRentings.Remove(harbourRentings.Find(rentId));
        }

        public bool IsRent(int rentId)
        {
            return harbourRentings.Find(rentId) != null;
        }
    }
}