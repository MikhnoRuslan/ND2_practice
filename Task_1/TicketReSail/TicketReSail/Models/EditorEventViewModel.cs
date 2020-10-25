using System;
using Microsoft.AspNetCore.Http;

namespace TicketReSail.Models
{
    public class EditorEventViewModel
    {
        public string Name { get; set; }
        
        public DateTime Date { get; set; }
        
        public int VenueId { get; set; }
        public IFormFile Banner { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
