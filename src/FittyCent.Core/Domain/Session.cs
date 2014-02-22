﻿using System;

namespace FittyCent.Domain {
    public class Session {
        public virtual int Id { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual double Price { get; set; }
        public virtual int ClassLimit { get; set; }
        public virtual VenueType VenueType { get; set; }
        public virtual Audience Audience { get; set; }
        public virtual TrainerClass Class { get; set; }
    }
}