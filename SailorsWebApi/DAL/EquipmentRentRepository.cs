using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class EquipmentRentRepository : Repository<RentingEquipment>, IEquipmentRentRepository
    {
        private readonly DbSet<RentingEquipment> equipmentRentings;

        public EquipmentRentRepository(HowDatabaseEntities context) : base(context)
        {
            equipmentRentings = context.RentingEquipment;
        }

        #region GetFunctions

        public List<RentingEquipment> GetEquipmentRentings()
        {
            return equipmentRentings.Any() ? equipmentRentings.ToList() : null;
        }

        public List<RentingEquipment> GetEquipmentRentingsByRenter(int renterId)
        {
            return equipmentRentings.Where(x => x.renterId == renterId).Select(x => x).ToList();
        }

        public List<RentingEquipment> GetEquipmentRentingsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return equipmentRentings.Where(x => x.rentTimeStart < startDate).Where(x => x.rentTimeEnd > endDate)
                .ToList();
        }

        public RentingEquipment GetEquipmentRentingById(int rentingId)
        {
            return equipmentRentings.FirstOrDefault(x => x.rentId == rentingId);
        }

        public List<int> GetAllEquipmentRentIds()
        {
            return equipmentRentings.Select(x => x.rentId).ToList();
        }

        #endregion

        public void RentEquipment(RentingEquipment newRenting)
        {
            equipmentRentings.Add(newRenting);
            Save();
        }

        public void RentEquipment(int userId, DateTime startDate, DateTime endDate, int equipmentId)
        {
            var rented = new RentingEquipment
            {
                renterId = userId,
                equipmentId = equipmentId,
                rentTimeStart = startDate,
                rentTimeEnd = endDate
            };
            equipmentRentings.Add(rented);
            Save();
        }

        public void DeleteRent(RentingEquipment delete)
        {
            equipmentRentings.Remove(delete);
        }
        
        public bool IsRent(int rentId)
        {
            return equipmentRentings.Find(rentId) != null;
        }
    }
}