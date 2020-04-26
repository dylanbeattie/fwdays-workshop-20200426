using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Autoweb.Website.Models;

namespace Autoweb.Website.Controllers {
	public class CarsController : Controller {
		private readonly ILogger<HomeController> _logger;

		public CarsController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		public IActionResult Index() {
            
			return View();
		}
	}
}
