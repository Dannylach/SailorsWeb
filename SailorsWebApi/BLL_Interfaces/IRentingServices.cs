using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.BLL_Interfaces
{
    public interface IRentingServices
    {
        #region GetFunctions

        ResponseWrapper<object> GetAllRentings();
        ResponseWrapper<object> GetAllHarbourRentings();
        ResponseWrapper<object> GetHarbourRentingById(int rentingId);
        ResponseWrapper<object> GetHarbourRentingsForUser(string username);
        ResponseWrapper<object> GetAllEquipmentRentings();
        ResponseWrapper<object> GetEquipmentRentingById(int rentingId);
        ResponseWrapper<object> GetEquipmentRentingsForUser(string username);
        ResponseWrapper<object> GetAllEquipment();
        ResponseWrapper<object> GetAllEquipmentTypes();

        #endregion

        #region AddFunctions

        ResponseWrapper<object> RentHarbour(string userName, string startDate, string endDate, string rentName,
            string rendDescription);

        ResponseWrapper<object> RentEquipment(string userName, string startDate, string endDate, int equipmentId);

        ResponseWrapper<object> AddEquipment(string equipmentName, int typeId);

        ResponseWrapper<object> AddEquipmentType(string typeName);

        #endregion

        #region UpdateFunctions

        ResponseWrapper<object> UpdateHarbourRent(int rentId, string userName, string startDate, string endDate, string rentName,
            string rendDescription);

        ResponseWrapper<object> UpdateEquipmentRent(int rentId, string userName, string startDate, string endDate, int equipmentId);

        ResponseWrapper<object> UpdateEquipment(int equipmentId, string equipmentName, int typeId);

        ResponseWrapper<object> UpdateEquipmentType(int typeId, string typeName);

        #endregion

        #region DeleteFunctions

        ResponseWrapper<object> DeleteHarbourRent(int rentId);

        ResponseWrapper<object> DeleteEquipmentRent(int rentIt);

        ResponseWrapper<object> DeleteEquipment(int equipmentId);

        ResponseWrapper<object> DeleteEquipmentType(int typeId);

        #endregion
    }
}
