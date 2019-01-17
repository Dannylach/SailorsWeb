using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IEquipmentTypeRepository : IRepository<EquipmentTypes>
    {
        string GetById(int? id);
        bool IsId(int id);
        List<int> GetAllIds();
        void AddEquipmentType(EquipmentTypes newEquipment);
        void DeleteEquipmentType(int deleteId);
    }
}
