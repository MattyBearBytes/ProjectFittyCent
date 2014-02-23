using System;
using FittyCent.Domain;

namespace FittyCent.Web.Models {
    public class SessionModel {
        public int Id { get; set; }
        public int TrainerClassId { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public int ClassLimit { get; set; }
        public VenueType VenueType { get; set; }
        public Audience Audience { get; set; }
    }
}