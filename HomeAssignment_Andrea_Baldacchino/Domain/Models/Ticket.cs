using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /* Ticket.cs should contain the following
     * Id, Row, Column, FlightIdFK, Passport, PricePaid, Cancelled */
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Row { get; set; }
        public char Column { get; set; }

        [ForeignKey(nameof(Flight))]
        public int FlightIdFK { get; set; }
        public Flight Flight { get; set; }
        public string Passport { get; set; }
        public double PricePaid { get; set; }
        public Boolean Canelled { get; set; }

    }
}
