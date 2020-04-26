using System;

namespace Autoweb.Website.Models {
    public class Car {
        // What make/model of car is this?
        public CarModel Model { get; set; }
        public string Color { get; set; }
        public string RegistrationNumber { get; set; }
        public int Year { get; set; }
    }

    public class CarModel {
        public string Code { get; set; }
        // Make, eg. Toyota, Ford, Bogdan
        public string Make { get; set; }
        // Model name, e.g. 3 Series, Corolla, Focus, 
        public string Name { get; set; }
    }
}

