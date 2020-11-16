using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TicketReSail.DAL.Model;

namespace TicketReSail.Models
{
    public class EventsViewModel
    {
        public List<Category> Categories { get; set; }
        public Event[] Events { get; set; }
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public string SellerNote { get; set; }
        public IFormFile Banner { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public City[] Cities { get; set; }
    }
}