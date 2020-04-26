using Autoweb.Messages;

namespace Autoweb.EmailRenderer {
    public class Customer {
        public string Name { get; set; }
        public string Email { get; set; }

        public int MinimumYear { get; set; }
        //        public int MaximumPrice { get; set; }

        public bool WantsEmailAbout(NewCarMessage newCar) {
            return newCar.Year > MinimumYear;
        }
    }
}