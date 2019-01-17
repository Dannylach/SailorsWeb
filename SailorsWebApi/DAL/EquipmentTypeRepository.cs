using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SailorsWebApi.DAL_Interfaces;
using SailorsWebApi.Models;

namespace SailorsWebApi.DAL
{
    public class EquipmentTypeRepository : Repository<EquipmentTypes>, IEquipmentTypeRepository
    {
        private readonly DbSet<EquipmentTypes> equipmentTypeses;

        public EquipmentTypeRepository(HowDatabaseEntities context) : base(context)
        {
            equipmentTypeses = context.EquipmentTypes;
        }

        public string GetById(int? id)
        {
            var functionName = id != null ? equipmentTypeses.Where(x => x.typeId == id).Select(x => x.typeName).ToList() : null;
            return functionName?[0].Trim(' ') ?? "brak takiego typu";
        }

        public bool IsId(int id)
        {
            var getAll = GetAll();
            var idsList = new List<int>();
            foreach (var type in getAll)
            {
                idsList.Add(type.typeId);
            }

            return idsList.Contains(id);
        }

        public List<int> GetAllIds()
        {
            return equipmentTypeses.Select(x => x.typeId).ToList();
        }

        public void AddEquipmentType(EquipmentTypes newEquipment)
        {
            equipmentTypeses.Add(newEquipment);
        }

        public void DeleteEquipmentType(int deleteId)
        {
            equipmentTypeses.Remove(equipmentTypeses.Find(deleteId));
        }
    }
}