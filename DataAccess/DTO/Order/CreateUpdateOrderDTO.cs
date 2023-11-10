using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Order
{
    public class CreateUpdateOrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequireDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string Freight { get; set; }
    }
}
