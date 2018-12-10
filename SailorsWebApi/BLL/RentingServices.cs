using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.BLL
{
    public class RentingServices : IRentingServices 
    {
        private readonly IRentingRepository rentingRepository;
        private readonly HowDatabaseEntities dataContext;

        public RentingServices(IRentingRepository rentingRepository)
        {
            this.rentingRepository = rentingRepository;
            dataContext = new HowDatabaseEntities();
        }

        public List<HabourRenting> GetHarbourRentings()
        {
            try
            {
                return rentingRepository.GetHarbourRentings();
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
                return rentingRepository.GetEquipmentRentings();
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
                //TODO Validate input
                rentingRepository.RentHarbour(userId, startDate, endDate, rentName, rendDescription);
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
                //TODO Validate input
                rentingRepository.RentEquipment(userId, startDate, endDate, equipmentId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}