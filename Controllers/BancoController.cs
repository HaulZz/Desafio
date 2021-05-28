using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EstoqueAPI.Models;
using EstoqueAPI.Services;


namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        //Get todos os produtos


        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id){
            return DataBase.Get(id);
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll(){
            return  DataBase.GetAll();
        }

        //Add produto
        [HttpPost]
        public IActionResult Create(Product product){
            DataBase.Add(product);
            return NoContent();
        }
    }
}