using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class EquipmentRepository : Repository<SailingEquipment>, IEquipmentRepository
    {
        private readonly DbSet<SailingEquipment> equipment;

        public EquipmentRepository(HowDatabaseEntities context) : base(context)
        {
            equipment = context.SailingEquipment;
        }

        public string GetById(int? id)
        {
            var equipmentName = id != null
                ? equipment.Where(x => x.equipmentId == id).Select(x => x.equipmentName).ToList()
                : null;
            return equipmentName?[0].Trim(' ') ?? "brak funkcji";
        }
        
        public bool IsId(int id)
        {
            var getAll = GetAll();
            var idsList = new List<int>();
            foreach (var equipment in getAll)
            {
                idsList.Add(equipment.equipmentId);
            }

            return idsList.Contains(id);
        }

        public List<int> GetAllIds()
        {
            return equipment.Select(x => x.equipmentId).ToList();
        }

        public void AddEquipment(SailingEquipment newEquipment)
        {
            equipment.Add(newEquipment);
            Save();
        }

        public void DeleteEquipment(int deleteId)
        {
            equipment.Remove(equipment.Find(deleteId));
        }
    }
}