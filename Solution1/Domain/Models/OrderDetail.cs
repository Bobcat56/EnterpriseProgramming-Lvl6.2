using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int OrderFK { get; set; } //Forign key property
        public Order Order { get; set; }//Navigational property

        [ForeignKey("Product")]
        public int ProductFK { get; set; } //Forign key property
        public Product Product { get; set; }//Navigational property
    }
}
