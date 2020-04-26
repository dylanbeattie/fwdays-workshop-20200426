using System.Collections.Generic;
using Autoweb.Website.Models;

namespace Autoweb.Website.Data {
	public class CarDatabase {
		public IEnumerable<Car> Cars {

			get {
                var prius = new CarModel {
                    Make = "Toyota",
                    Name = "Prius",                
                };
                var bmw5 = new CarModel {
                    Make = "BMW",
                    Name = "5 Series"
                };

				yield return new Car { 
                    Model = prius,
                    Color = "Blue",
                    RegistrationNumber = "AB1234CD",
                    Year = 2016
                };
                yield return new Car {
                    Model = bmw5,
                    Color = "Black",
                    RegistrationNumber = "ABC123",
                    Year = 2009                    
                };
			}
		}
	}
}