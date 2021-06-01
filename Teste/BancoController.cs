﻿// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using EstoqueAPI.Models;
// using EstoqueAPI.Services;


// namespace EstoqueAPI.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class BancoController : ControllerBase
//     {
//         //Get todos os produtos


//         [HttpGet("{id}")]
//         public ActionResult<Product> Get(int id){
//             return DataBaseService.Get(id);
//         }

//         [HttpGet]
//         public ActionResult<List<Product>> GetAll(){
//             return  DataBaseService.GetAll();
//         }

//         //Add produto
//         [HttpPost]
//         public IActionResult Create(Product product){
//             DataBaseService.Add(product);
//             return NoContent();
//         }

//         //Atualizar um produto
//         [HttpPut("{id}")]
//         public IActionResult Update(int id, Product product){
//             if (id != product.Id){
//                 return BadRequest();
//             }
//             DataBaseService.Update(product);
//             return CreatedAtAction(nameof(Update),new {id = product.Id }, product);
//         }

//         //Deletar um produto
//         [HttpDelete("{id}")]
//         public IActionResult Delete(int id){
//             if (DataBaseService.Get(id) is null){
//                 return NotFound();
//             }
//             DataBaseService.Delete(id);
//             return NoContent();
//         }
//     }
// }