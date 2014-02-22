using System.Collections.Generic;

namespace FittyCent.Domain {
    public class TrainerClass {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Type { get; set; }
        public virtual string Keywords { get; set; }
        public virtual UserAccount User { get; set; }
        public virtual ICollection<Session> Sessions { get; private set; }

        public TrainerClass() {
            Sessions = new List<Session>();
        }
    }
}