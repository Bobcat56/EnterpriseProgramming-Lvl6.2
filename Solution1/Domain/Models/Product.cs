using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [System.ComponentModel.DataAnnotations.Key]//This will set the below as the Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//This will make the below auto increment
        public Guid Id { get; set; }    //Generated a unique ID 

        [System.ComponentModel.DataAnnotations.Required] //This will not allow the value to be set as NULL
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double Price { get; set; }
        
        public int Stock { get; set; }
        
        public string Image { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryFK { get; set; } //Forign key property

        public Category Category { get; set; } //Navigational property
        //This allows the use of Category.[Property]
    }
}
