using System;
using System.Collections.Generic;
using System.Linq;
using Autoweb.Website.Models;

namespace Autoweb.Website.Data {
	public class CarDatabase {
        private List<Car> cars = new List<Car>();

		public IEnumerable<Car> Cars => cars;

        /// <summary>
        /// Find the car with the specified registration. If the registration is not found, this method returns null.
        /// </summary>
        /// <param name="reg">The reg no of the car we want to find.</param>
        /// <returns></returns>
        public Car FindCar(string reg) {
            return cars.FirstOrDefault(car => car.RegistrationNumber == reg);
        }

        public void AddCar(Car car) {
            cars.Add(car);
        }

        public CarDatabase() {
            var prius = new CarModel {
                Code = "toy-pri",
                Make = "Toyota",
                Name = "Prius",
            };
            var bmw5 = new CarModel {
                Code = "bmw-5sr",
                Make = "BMW",
                Name = "5 Series"
            };
            cars.Add(new Car {
                RegistrationNumber = "AB1234CD",
                Model = prius,
                Color = "Blue",
                Year = 2016
            });
            cars.Add(new Car {
                RegistrationNumber = "ABC123",
                Model = bmw5,
                Color = "Black",
                Year = 2009
            });
        }
    }
}