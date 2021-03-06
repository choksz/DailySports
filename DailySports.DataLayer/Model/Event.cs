﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DailySports.DataLayer.Model
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        public string Tag { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        private string ImageEvent;
        public string EventImage
        {
            get
         ;
            set
         ;
        }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<EventImage> Images { get; set; }

        [ForeignKey("ticketid")]
        public virtual Ticket ticket { get; set; }
        public int ticketid { get; set; }

    }
}
