using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketReSail.DAL.Model
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public int VenueId { get; set; }
        [Required]
        public Venue Venue { get; set; }

        [Required]
        public byte[] Banner { get; set; }
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
