using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EstoqueAPI.Models;
using EstoqueAPI.Services;

namespace EstoqueAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase{
        //Get todos os produtos
        [HttpGet]
        public ActionResult<List<Product>> GetAll(){
            //return ProductService.GetAll();
            return DataBase.GetAll();
        }

        //Get Um produto
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id){
            //var product = ProductService.Get(id);
            var product = DataBase.Get(id);
            if (product is null){
                return NotFound();
            }
            return product;
        }

        //Add produto
        [HttpPost]
        public IActionResult Create(Product product){
            //ProductService.Add(product);
            DataBase.Add(product);
            return NoContent();
        }

        //Atualizar produto
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product){
            if (DataBase.Get(id) is null){
                return NotFound();
            }
            if (id != product.Id){
                return BadRequest();
            }
            //ProductService.Update(product);
            DataBase.Update(product);
            return CreatedAtAction(nameof(Update),new {id = product.Id }, product);
        }
        //Deletar produto
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            if (DataBase.Get(id) is null){
                return NotFound();
            }
            DataBase.Delete(id);
            return NoContent();
        }
    }
}