using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IEquipmentRentRepository : IRepository<RentingEquipment>
    {
        List<RentingEquipment> GetEquipmentRentings();
        List<RentingEquipment> GetEquipmentRentingsByRenter(int renterId);
        List<RentingEquipment> GetEquipmentRentingsBetweenDates(DateTime startDate, DateTime endDate);
        RentingEquipment GetEquipmentRentingById(int rentingId);
        List<int> GetAllEquipmentRentIds();
        bool IsRent(int rentId);
        void RentEquipment(RentingEquipment newRenting);
        void RentEquipment(int userId, DateTime startDate, DateTime endDate, int equipmentId);
        void DeleteRent(int deleteId);
    }
}
