using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.BLL_Interfaces
{
    interface IRentingServices
    {
        List<HabourRenting> GetHarbourRentings();
        List<RentingEquipment> GetEquipmentRentings();
        void RentHarbour(int userId, DateTime startDate, DateTime endDate, string rentName,
            string rendDescription);
        void RentEquipment(int userId, DateTime startDate, DateTime endDate, int equipmentId);
    }
}
