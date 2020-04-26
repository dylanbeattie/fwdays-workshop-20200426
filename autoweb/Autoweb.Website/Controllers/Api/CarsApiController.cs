using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autoweb.Website.Data;
using Autoweb.Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;

namespace Autoweb.Website.Controllers.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase {
        private readonly CarDatabase db;

        public CarsController(CarDatabase db) {
            this.db = db;

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
        public Car Post([FromBody] Car car) {
            db.AddCar(car);
            return car;
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
