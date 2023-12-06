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
        public DbSet<FlightSeating> FlightSeatings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Set the ID's to auto generate a GUID
            builder.Entity<Flight>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<Ticket>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Entity<FlightSeating>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }//Close class
 }