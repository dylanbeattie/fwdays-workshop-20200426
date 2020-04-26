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
				yield return new Car { 
                    Model = prius,
                    Color = "Blue",
                    RegistrationNumber = "AB1234CD",
                    Year = 2016
                };
			}
		}
	}
}