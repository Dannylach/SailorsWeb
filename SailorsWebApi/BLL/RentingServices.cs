using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using SailorsWebApi.BLL_Interfaces;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.BLL
{
    public class RentingServices : IRentingServices
    {
        private readonly IHarbourRentRepository harbourRentRepository;
        private readonly IEquipmentRentRepository equipmentRentRepository;
        private readonly IUserRepository userRepository;
        private readonly IEquipmentTypeRepository equipmentTypeRepository;
        private readonly IEquipmentRepository equipmentRepository;
        
        public RentingServices(IHarbourRentRepository harbourRentRepository, IEquipmentRentRepository equipmentRentRepository, IUserRepository userRepository, IEquipmentTypeRepository equipmentType, IEquipmentRepository equipmentRepository)
        {
            this.harbourRentRepository = harbourRentRepository;
            this.equipmentRentRepository = equipmentRentRepository;
            this.userRepository = userRepository;
            equipmentTypeRepository = equipmentType;
            this.equipmentRepository = equipmentRepository;
        }

        #region GetFunctions

        public ResponseWrapper<object> GetAllRentings()
        {
            try
            {
                var getAllHarbourRentings = harbourRentRepository.GetHarbourRentings();
                var getAllEquipmentRentings = equipmentRentRepository.GetEquipmentRentings();
                var resultList = new List<object>();

                if(getAllHarbourRentings != null)
                foreach (var rentings in getAllHarbourRentings)
                {
                    var instance = new
                    {
                        rentings.rentId,
                        rentingName = rentings.rentName.Trim(' '),
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                    };
                    resultList.Add(instance);
                }

                if(getAllEquipmentRentings != null)
                foreach (var rentings in getAllEquipmentRentings)
                {
                    var instance = new
                    {
                        renter = rentings.renterId != null ? userRepository.GetUserById(rentings.renterId).userName.Trim(' ') : null,
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                        equipmentType = equipmentTypeRepository.GetById(rentings.equipmentId).Trim(' ')
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #region HarbourRent

        public ResponseWrapper<object> GetAllHarbourRentings()
        {
            try
            {
                var getAll = harbourRentRepository.GetHarbourRentings();
                var resultList = new List<object>();

                if(getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var rentings in getAll)
                {
                    var instance = new
                    {
                        rentings.rentId,
                        rentingName = rentings.rentName.Trim(' '),
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetHarbourRentingById(int rentingId)
        {
            try
            {
                var renting = harbourRentRepository.GetHarbourRentingById(rentingId);

                if (renting == null) return new ResponseWrapper<object>("Error 404: No such record", false);
                var instance = new
                {
                    rentingName = renting.rentName.Trim(' '),
                    renter = renting.renterId != null ? userRepository.GetUserById(renting.renterId).userName.Trim(' ') : null,
                    rentStart = renting.rentTimeStart.ToString(),
                    rentEnd = renting.rentTimeEnd.ToString(),
                    rentDescription = renting.rentDescription.Replace("  ", "")
                };

                return new ResponseWrapper<object>(instance, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetHarbourRentingsForUser(string userName)
        {
            try
            {
                var getAll = harbourRentRepository.GetHarbourRentingsByRenter(userRepository.GetUserByName(userName).userId);
                var resultList = new List<object>();

                if (getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var rentings in getAll)
                {
                    var instance = new
                    {
                        rentingName = rentings.rentName.Trim(' '),
                        renter = rentings.renterId != null ? userRepository.GetUserById(rentings.renterId).userName.Trim(' ') : null,
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                        rentDescription = rentings.rentDescription.Replace("  ", "")
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region EquipmentRent
      
        public ResponseWrapper<object> GetAllEquipmentRentings()
        {
            try
            {
                var getAll = equipmentRentRepository.GetEquipmentRentings();
                var resultList = new List<object>();

                if (getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var rentings in getAll)
                {
                    var instance = new
                    {
                        renter = rentings.renterId != null ? userRepository.GetUserById(rentings.renterId).userName.Trim(' ') : null,
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                        equipmentType = equipmentRepository.GetById(rentings.equipmentId).Trim(' ')
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetEquipmentRentingById(int rentingId)
        {
            try
            {
                var renting = equipmentRentRepository.GetEquipmentRentingById(rentingId);

                if (renting == null) return new ResponseWrapper<object>("Error 404: No such record", false);
                var instance = new
                {
                    renter = renting.renterId != null
                        ? userRepository.GetUserById(renting.renterId).userName.Trim(' ')
                        : null,
                    rentStart = renting.rentTimeStart.ToString(),
                    rentEnd = renting.rentTimeEnd.ToString(),
                    equipmentType = equipmentRepository.GetById(renting.equipmentId).Trim(' ')
                };
            
                return new ResponseWrapper<object>(instance, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetEquipmentRentingsForUser(string userName)
        {
            try
            {
                var getAll = equipmentRentRepository.GetEquipmentRentingsByRenter(userRepository.GetUserByName(userName).userId);
                var resultList = new List<object>();

                if (getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var rentings in getAll)
                {
                    var instance = new
                    {
                        renter = rentings.renterId != null ? userRepository.GetUserById(rentings.renterId).userName.Trim(' ') : null,
                        rentStart = rentings.rentTimeStart.ToString(),
                        rentEnd = rentings.rentTimeEnd.ToString(),
                        equipmentType = equipmentRepository.GetById(rentings.equipmentId).Trim(' ')
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region Equipment

        public ResponseWrapper<object> GetAllEquipment()
        {
            try
            {
                var getAll = equipmentRepository.GetAll();
                var resultList = new List<object>();

                if (getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var equipment in getAll)
                {
                    var instance = new
                    {
                        equipment.equipmentId,
                        equipmentName = equipment.equipmentName.Trim(' '),
                        typeName = equipment.EquipmentTypes.typeName.Trim(' ')
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> GetAllEquipmentTypes()
        {
            try
            {
                var getAll = equipmentTypeRepository.GetAll();
                var resultList = new List<object>();

                if (getAll.Count == 0) return new ResponseWrapper<object>("Error 404: No such record", false);
                foreach (var type in getAll)
                {
                    var instance = new
                    {
                        type.typeId,
                        typeName = type.typeName.Trim(' ')
                    };
                    resultList.Add(instance);
                }

                return new ResponseWrapper<object>(resultList, true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #endregion

        #region AddFunctions

        public ResponseWrapper<object> RentHarbour(string userName, string startDate, string endDate, string rentName,
            string rendDescription)
        {
            try
            {
                var rentStartTime = DateTime.Parse(startDate);
                var rentEndTime = DateTime.Parse(endDate);
                var renterId = userRepository.GetUserByName(userName).userId;
                var renting = new HabourRenting
                {
                    renterId = renterId,
                    rentId = GetHarbourRentFreeId(),
                    rentTimeStart = rentStartTime,
                    rentTimeEnd = rentEndTime,
                    rentName = rentName,
                    rentDescription = rendDescription
                };

                harbourRentRepository.RentHarbour(renting);
                harbourRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> RentEquipment(string userName, string startDate, string endDate, int equipmentId)
        {
            try
            {
                var rentStartTime = DateTime.Parse(startDate);
                var rentEndTime = DateTime.Parse(endDate);
                var renterId = userRepository.GetUserByName(userName).userId;
                var rentId = GetEquipmentRentFreeId();
                var isEquipmentId = equipmentTypeRepository.IsId(equipmentId);
                if (!isEquipmentId) return new ResponseWrapper<object>("Error 404: No such record", false);
                var renting = new RentingEquipment
                {
                    renterId = renterId,
                    rentTimeStart = rentStartTime,
                    rentTimeEnd = rentEndTime,
                    equipmentId = equipmentId,
                    rentId = rentId
                };

                equipmentRentRepository.RentEquipment(renting);
                equipmentRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> AddEquipment(string equipmentName, int typeId)
        {
            try
            {
                var freeId = GetEquipmentFreeId();
                var newEquipment = new SailingEquipment()
                {
                    equipmentId = freeId,
                    equipmentName = equipmentName,
                    equipmentType = typeId
                };

                equipmentRepository.AddEquipment(newEquipment);
                equipmentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> AddEquipmentType(string equipmentName)
        {
            try
            {
                var freeId = GetEquipmentTypeFreeId();
                var newEquipment = new EquipmentTypes
                {
                    typeId = freeId,
                    typeName = equipmentName
                };

                equipmentTypeRepository.AddEquipmentType(newEquipment);
                equipmentTypeRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region UpdateFunctions

        public ResponseWrapper<object> UpdateHarbourRent(int rentId, string userName, string startDate, string endDate, string rentName,
            string rendDescription)
        {
            try
            {
                var userId = userRepository.GetUserByName(userName).userId;
                var updateData = new HabourRenting
                {
                    rentId = rentId,
                    renterId = userId,
                    rentTimeStart = DateTime.Parse(startDate),
                    rentTimeEnd = DateTime.Parse(endDate),
                    rentName = rentName,
                    rentDescription = rendDescription
                };
                harbourRentRepository.Update(updateData);
                harbourRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> UpdateEquipmentRent(int rentId, string userName, string startDate, string endDate, int equipmentId)
        {
            try
            {
                var renting = new RentingEquipment
                {
                    renterId = userRepository.GetUserByName(userName).userId,
                    rentTimeStart = DateTime.Parse(startDate),
                    rentTimeEnd = DateTime.Parse(endDate),
                    equipmentId = equipmentId,
                    rentId = GetEquipmentRentFreeId()
                };

                equipmentRentRepository.Update(renting);
                equipmentRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> UpdateEquipment(int equipmentId, string equipmentName, int typeId)
        {
            try
            {
                var newEquipment = new SailingEquipment
                {
                    equipmentId = equipmentId,
                    equipmentName = equipmentName,
                    equipmentType = typeId
                };

                equipmentRepository.Update(newEquipment);
                equipmentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> UpdateEquipmentType(int typeId, string typeName)
        {
            try
            {
                var newEquipment = new EquipmentTypes
                {
                    typeId = typeId,
                    typeName = typeName
                };

                equipmentTypeRepository.Update(newEquipment);
                equipmentTypeRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region DeleteFunctions

        public ResponseWrapper<object> DeleteHarbourRent(int rentId)
        {
            try
            {
                if(!harbourRentRepository.IsRent(rentId)) return new ResponseWrapper<object>("Error 404: No such record", false);
                harbourRentRepository.DeleteRent(rentId);
                harbourRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> DeleteEquipmentRent(int rentId)
        {
            try
            {
                if (!equipmentRentRepository.IsRent(rentId)) return new ResponseWrapper<object>("Error 404: No such record", false);
                harbourRentRepository.DeleteRent(rentId);
                harbourRentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> DeleteEquipment(int equipmentId)
        {
            try
            {
                if (!equipmentRepository.IsId(equipmentId)) return new ResponseWrapper<object>("Error 404: No such record", false);
                equipmentRepository.DeleteEquipment(equipmentId);
                equipmentRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        public ResponseWrapper<object> DeleteEquipmentType(int typeId)
        {
            try
            {
                if (!equipmentTypeRepository.IsId(typeId)) return new ResponseWrapper<object>("Error 404: No such record", false);
                equipmentTypeRepository.DeleteEquipmentType(typeId);
                equipmentTypeRepository.Save();
                return new ResponseWrapper<object>("OK", true);
            }
            catch (Exception e)
            {
                return new ResponseWrapper<object>(e.Message, false);
            }
        }

        #endregion

        #region HelperFunctions

        private int GetHarbourRentFreeId()
        {
            var addIds = harbourRentRepository.GetAllHarbourRentIds();
            if (addIds.Count == 0) return 0;
            addIds.Sort();
            var lastId = addIds.Last();
            if (addIds.Count != lastId)
                for (var i = 0; i < lastId; i++)
                {
                    if (addIds.Contains(i)) continue;
                    return i;
                }
            return lastId + 1;
        }

        private int GetEquipmentRentFreeId()
        {
            var addIds = equipmentRentRepository.GetAllEquipmentRentIds();
            if (addIds.Count == 0) return 0;
            addIds.Sort();
            var lastId = addIds.Last();
            if (addIds.Count != lastId)
                for (var i = 0; i < lastId; i++)
                {
                    if (addIds.Contains(i)) continue;
                    return i;
                }
            return lastId + 1;
        }

        private int GetEquipmentFreeId()
        {
            var addIds = equipmentRepository.GetAllIds();
            if (addIds.Count == 0) return 0;
            addIds.Sort();
            var lastId = addIds.Last();
            if (addIds.Count != lastId)
                for (var i = 0; i < lastId; i++)
                {
                    if (addIds.Contains(i)) continue;
                    return i;
                }
            return lastId + 1;
        }

        private int GetEquipmentTypeFreeId()
        {
            var addIds = equipmentTypeRepository.GetAllIds();
            if (addIds.Count == 0) return 0;
            addIds.Sort();
            var lastId = addIds.Last();
            if (addIds.Count != lastId)
                for (var i = 0; i < lastId; i++)
                {
                    if (addIds.Contains(i)) continue;
                    return i;
                }
            return lastId + 1;
        }
        #endregion
    }
}