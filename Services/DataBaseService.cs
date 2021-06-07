using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using EstoqueAPI.Models;
using EstoqueAPI.Controllers;
using EstoqueAPI.Services;
using System.Linq;

namespace EstoqueAPI.Services{
	/// ARMAZENAMENTO DOS DADOS EM MONGODB
	public static class DataBaseService
	{
		//static List<Product> Products { get; set; }
		private static MongoClient client = new MongoClient();
		public static IMongoCollection<MongoDB.Bson.BsonDocument> Collection
		{
			get { return client.GetDatabase("test").GetCollection<BsonDocument>("estoque"); }
		}
		
		//Criar novo ID
		public static int Id(){
			var product = Collection.Find(bson => true).SortBy(bson => bson["id"]).ThenByDescending(bson => bson["id"]).ToList();
			return (product[0].GetValue("id").ToInt32()+1);
		}

		//Get um produto
		public static Product Get(int id)
		{
			var filter = Builders<BsonDocument>.Filter.Eq("id", id);
			var p = Collection.Find(filter).ToList()[0];
			var product = new Product();	
			
			product.Id = p.GetValue("id").ToInt32();
			product.Name = p.GetValue("name").ToString();
			product.Sku = p.GetValue("sku").ToString();
			product.Quantity = p.GetValue("quantity").ToInt32();
			product.Price = p.GetValue("price").ToDecimal();
			
			return product;
		}


		//Get todos os produtos
		public static List<Product> GetAll()
		{	
			var prod = Collection.Find(new BsonDocument()).ToList();
			List<Product> Products = new List<Product>();
			foreach (var p in prod){
				var product = new Product();
				product.Id = p.GetValue("id").ToInt32();
				product.Name = p.GetValue("name").ToString();
				product.Sku = p.GetValue("sku").ToString();
				product.Quantity = p.GetValue("quantity").ToInt32();
				product.Price = p.GetValue("price").ToDecimal();
				Products.Add(product);
			}
			//Console.WriteLine(Products.ToString());
			return Products;
		}

		//Add um produto
		public static void Add(Product product){
			var prod = new BsonDocument
				{
					{ "id", Id() },
					{ "name", product.Name },
					{ "sku", product.Sku },
					{ "price", product.Price },
					{ "quantity", product.Quantity}
				};
			Collection.InsertOne(prod);
		}

		//Update um produto
		public static void Update(Product product){
		
			var update = Builders<BsonDocument>.Update.Set("price", product.Price)
											.Set("name", product.Name)
											.Set("sku", product.Sku)
											.Set("quantity", product.Quantity);

			var filter = Builders<BsonDocument>.Filter.Eq("id", product.Id);
			Collection.UpdateOne(filter, update);
		}

		//Deletar um produto
		public static void Delete(int id){

			var filter = Builders<BsonDocument>.Filter.Eq("id", id);
			Collection.DeleteOne(filter);
		}


		//Desconta produtos vendidos na loja
		public static void Discount(Product order){
			var product = Get(order.Id);
			var update = Builders<BsonDocument>.Update.Set("quantity", (product.Quantity - order.Quantity));

			var filter = Builders<BsonDocument>.Filter.Eq("id", product.Id);
			Collection.UpdateOne(filter, update);
		}
	}
}
