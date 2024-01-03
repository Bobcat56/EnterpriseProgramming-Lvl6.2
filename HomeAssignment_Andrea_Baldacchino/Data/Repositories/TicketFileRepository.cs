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
            if (File.Exists(_ticketFile))
            {
                try
                {
                    string allText = "";

                    using (StreamReader sr = File.OpenText(_ticketFile))
                    {
                        allText = sr.ReadToEnd();
                        sr.Close();

                    }   
                    
                    if (string.IsNullOrEmpty(allText))
                    {
                        //return new List<Ticket>();
                    }

                    List<Ticket> listTickets = JsonSerializer.Deserialize<List<Ticket>>(allText);
                
                    return listTickets.AsQueryable();
                }
                catch
                {
                    throw new Exception("Error encountered while opening file");
                }

            }
            else throw new Exception("Error occurd while fecthing data");
        }
    }
}
