using System;

namespace FittyCent.Domain {
    public class Session {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Price { get; set; }
        public int ClassLimit { get; set; }
        public VenueType VenueType { get; set; }
        public Audience Audience { get; set; }
        public TrainerClass Class { get; set; }
    }
}