using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EstoqueAPI.Models;
using EstoqueAPI.Services;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase{

        //Add Order
        [HttpPost]
        public IActionResult Create(Product product){
            PublisherService.Publish(product);
            return Accepted(product);
        }
    }
}