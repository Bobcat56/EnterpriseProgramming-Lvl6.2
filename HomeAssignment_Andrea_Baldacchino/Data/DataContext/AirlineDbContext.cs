﻿using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContext
{
    //Remember this class is a representation of the database. An abstraction of it.
    public class AirlineDbContext: IdentityDbContext <IdentityUser>
    {
        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
            : base(options)
        {

        }//Close constructor

        //Representation of the Flight and Ticket tables
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }//Close class
}
