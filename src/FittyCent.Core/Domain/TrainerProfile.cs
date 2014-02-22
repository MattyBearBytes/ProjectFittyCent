namespace FittyCent.Domain {
    public class TrainerProfile {
        public virtual string Summary { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual bool? IsInsured { get; set; }
        public virtual string Registrations { get; set; }
        public virtual string Specialisations { get; set; }
        public virtual string Qualifications { get; set; }
        public virtual bool? HasMobileServiceAvailable { get; set; }
    }
}