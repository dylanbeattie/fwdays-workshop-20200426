using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autoweb.Messages;
using Autoweb.Website.Data;
using Autoweb.Website.Models;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;

namespace Autoweb.Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase {
        private readonly CarDatabase db;
        private readonly IBus bus;

        public CarsController(CarDatabase db, IBus bus) {
            this.db = db;
            this.bus = bus;
        }

        [HttpGet]
        public object Get() {
            var cars = db.Cars;
            var jsonCars = cars.Select(c => new {
                _links = new {
                    model = new {
                        href = $"/api/models/{c.Model.Code}"
                    }
                },
                registration = c.RegistrationNumber,
                color = c.Color,
                year = c.Year
            }).ToList();

            var result = new {
                _actions = new {
                    create = new {
                        href = "/cars",
                        method = "POST",
                        name = "Create a new car",
                        contentType = "application/json",
                        schema = "/schemas/car.json"
                    }
                },
                items = jsonCars
            };
            return result;
        }

        [HttpGet("{id}", Name = "Get")]
        public Car Get(string id) {
            return db.FindCar(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car) {
            try {
                db.AddCar(car) ;
                await PublishNewCarMessage(car);
                return Redirect($"/api/cars/{car.RegistrationNumber}");
            }
            catch (DuplicateCarException ex) {
                return Conflict( $"There is already a car with registration {ex.Car.RegistrationNumber} in the database!");
            }
        }

        private async Task PublishNewCarMessage(Car car) {
            var message = new NewCarMessage {
                RegistrationNumber = car.RegistrationNumber,
                Year = car.Year,
                Color = car.Color,
                Make = car.Model.Make,
                NewCarAddedAtUtc = DateTime.UtcNow,
                Model = car.Model.Name
            };
            await bus.PublishAsync<NewCarMessage>(message);
        }

        //// PUT: api/CarsApi/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value) {
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
    }
}
