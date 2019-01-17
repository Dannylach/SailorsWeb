using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL_Interfaces
{
    public interface IEquipmentRepository : IRepository<SailingEquipment>
    {
        string GetById(int? id);
        bool IsId(int id);
        List<int> GetAllIds();
        void AddEquipment(SailingEquipment newEquipment);
        void DeleteEquipment(int deleteId);
    }
}
