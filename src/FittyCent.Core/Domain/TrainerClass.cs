using System.Collections.Generic;

namespace FittyCent.Domain {
    public class TrainerClass {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string Keywords { get; set; }
        public ICollection<Session> Sessions { get; private set; }

        public TrainerClass() {
            Sessions = new List<Session>();
        }
    }
}