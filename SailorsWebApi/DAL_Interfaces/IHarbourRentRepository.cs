using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IHarbourRentRepository : IRepository<HabourRenting>
    {
        List<HabourRenting> GetHarbourRentings();
        List<HabourRenting> GetHarbourRentingsByRenter(int renterId);
        List<HabourRenting> GetHarbourRentingsBetweenDates(DateTime startDate, DateTime endDate);
        HabourRenting GetHarbourRentingById(int rentingId);
        List<int> GetAllHarbourRentIds();
        void RentHarbour(HabourRenting newRenting);
        void RentHarbour(int userId, DateTime startDate, DateTime endDate, string rentName,
            string rendDescription);
        void DeleteRent(int deleteId);
        bool IsRent(int rentId);
    }
}
