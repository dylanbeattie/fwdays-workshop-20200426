using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Autoweb.Website.Models;
using Autoweb.Website.Data;

namespace Autoweb.Website.Controllers {
	public class CarsController : Controller {
		private readonly CarDatabase db;
		private readonly ILogger<HomeController> _logger;

		public CarsController(ILogger<HomeController> logger, CarDatabase db) {
            this.db = db;
			_logger = logger;
		}

		public IActionResult Index() {
            var cars = db.Cars;
			return View(cars);
		}
	}
}
