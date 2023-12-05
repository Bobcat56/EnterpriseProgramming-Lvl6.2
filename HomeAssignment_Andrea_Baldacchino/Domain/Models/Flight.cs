using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /* Flight.cs should contain the following
     * Id, Rows, Columns, DepartureDate, ArrivalDate, CountryFrom, CountryTo, WholesalePrice, CommissionRate */
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public DateOnly DepartureDate { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
        public double WholeSalePrice { get; set; }
        public double ComissionRate { get; set; }
    
    }
}
