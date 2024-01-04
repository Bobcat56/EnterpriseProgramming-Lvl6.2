using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Data.Repositories
{
    public class TicketFileRepository : ITicketRepository
    {
        private string _ticketFile;
        public TicketFileRepository(string ticketFile)
        {
            _ticketFile = ticketFile;
        }

        public void Book(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public void Cancel(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Ticket> GetTickets(Guid id)
        {
            if (!File.Exists(_ticketFile))
            {
                //More dynamic error handling. SHowing which file it is mapped to.
                throw new Exception($"Error: File '{_ticketFile}' doesn't exist");
            }
            try
            {
                string allText = "";
                using (StreamReader sr = File.OpenText(_ticketFile))
                {
                    allText = sr.ReadToEnd();
                    //sr.Close(); //Not needed since it closes itself
                }   
                    
                if (string.IsNullOrWhiteSpace(allText))
                {
                    return new List<Ticket>().AsQueryable();
                }

                List<Ticket> listTickets = JsonSerializer.Deserialize<List<Ticket>>(allText);
                var flightTickets = listTickets.Where(ticket => ticket.FlightIdFK == id);

                return flightTickets.AsQueryable();
            }
            catch (JsonException ex) //Show Json errors for debugging 
            {
                throw new Exception("Error encountered while transforming data from Json file", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error encountered while opening file");
                //throw new Exception("Error encountered while opening file", ex);
            } 
        }//Close GetTickets()
    }
}
