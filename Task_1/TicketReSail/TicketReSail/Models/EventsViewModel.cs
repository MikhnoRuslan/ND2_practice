using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketReSail.DAL.Model;

namespace TicketReSail.Models
{
    public class EventsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public Venue[] Venues { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public Event Event { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public IFormFile Banner { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<City> Cities { get; set; }
        public SelectList SortOrder { get; set; }
        public SelectList SortBy { get; set; }
        public string searchText { get; set; }
    }
}