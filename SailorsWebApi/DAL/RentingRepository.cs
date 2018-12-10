using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class RentingRepository : Repository<HowDatabaseEntities>, IRentingRepository
    {
        public DbSet<HabourRenting> HarbourRentings;
        public DbSet<RentingEquipment> EquipmentRentings;

        public RentingRepository(HowDatabaseEntities context) : base(context)
        {
            HarbourRentings = context.HabourRenting;
            EquipmentRentings = context.RentingEquipment;
        }

        public List<HabourRenting> GetHarbourRentings()
        {
            try
            {
                return HarbourRentings.Any() ? HarbourRentings.ToList() : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    

        public List<RentingEquipment> GetEquipmentRentings()
        {
            try
            {
                return EquipmentRentings.Any() ? EquipmentRentings.ToList() : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RentHarbour(int userId, DateTime startDate, DateTime endDate, string rentName, string rendDescription)
        {
            try
            {
                var rented = new HabourRenting
                {
                    renterId = userId,
                    rentTimeStart = startDate,
                    rentTimeEnd = endDate,
                    rentName = rentName,
                    rentDescription = rendDescription
                };
                HarbourRentings.Add(rented);
                Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RentEquipment(int userId, DateTime startDate, DateTime endDate, int equipmentId)
        {
            try
            {
                var rented = new RentingEquipment
                {
                    renterId = userId,
                    equipmentId = equipmentId,
                    rentTimeStart = startDate,
                    rentTimeEnd = endDate
                };
                EquipmentRentings.Add(rented);
                Save();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}