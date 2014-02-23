using System;
using System.ComponentModel.DataAnnotations;
using FittyCent.Domain;

namespace FittyCent.Web.Models {
    public class SessionModel {
        public int Id { get; set; }
        public int TrainerClassId { get; set; }
        public string TrainerClassTitle { get; set; }

        public string Address { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required, Display(Name = "Date and Time")]
        public DateTime DateTime { get; set; }

        [Required, Display(Name = "Cost")]
        public double Price { get; set; }

        [Required, Display(Name = "Max. Participants")]
        public int ClassLimit { get; set; }

        [Required, Display(Name = "Venue Type")]
        public VenueType? VenueType { get; set; }

        [Required, Display(Name = "Target Audience")]
        public Audience Audience { get; set; }
    }
}