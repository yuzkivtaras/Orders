using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderCost { get; set; }

        public string ItemsDescription { get; set; }

        public string ShippingAddress { get; set; }

        public virtual User User { get; set; }
    }
}
