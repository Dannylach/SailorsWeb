using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IRentingRepository : IRepository<HowDatabaseEntities>
    {
        IEnumerable<HowDatabaseEntities> GetAll();
        void Save();
        void Update(HowDatabaseEntities item);
        List<HabourRenting> GetHarbourRentings();
        List<RentingEquipment> GetEquipmentRentings();
        void RentHarbour(int userId, DateTime startDate, DateTime endDate, string rentName, string rendDescription);
        void RentEquipment(int userId, DateTime startDate, DateTime endDate, int equipmentId);
    }
}