using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        [System.ComponentModel.DataAnnotations.Key]//This will set the below as the Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//This will make the below auto increment
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
