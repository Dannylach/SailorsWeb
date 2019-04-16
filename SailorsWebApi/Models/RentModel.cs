using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SailorsWebApi.Models
{
    public class RentModel
    {
        public int renterId { get; set; }
        public int equipmentId { get; set; }
        public DateTime rentTimeStart { get; set; }
        public DateTime rentTimeEnd { get; set; }
        public string rentName { get; set; }
        public string rentDescription { get; set; }
    }
}