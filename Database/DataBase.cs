using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using EstoqueAPI.Models;
using EstoqueAPI.Controllers;
using EstoqueAPI.Services;
using System.Linq;

public class DataBase
{
	//static List<Product> Products { get; set; }
	private static MongoClient client = new MongoClient();
    public static MongoClient Client
    {
        get { return client; }
    }
    
	public static int Id(){
		var collection = Client.GetDatabase("test").GetCollection<BsonDocument>("estoque");
		var product = collection.Find(bson => true).SortBy(bson => bson["id"]).ThenByDescending(bson => bson["id"]).ToList();
		return product[0].GetValue("id").ToInt32();
	}

	//Get um produto
	public static Product Get(int id)
    {
		var collection = Client.GetDatabase("test").GetCollection<BsonDocument>("estoque");
		var filter = Builders<BsonDocument>.Filter.Eq("id", id);
		var p = collection.Find(filter).ToList()[0];
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

		var collection = Client.GetDatabase("test").GetCollection<BsonDocument>("estoque");
		var prod = collection.Find(new BsonDocument()).ToList();
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
	public static void Update(Product prod){
		var collection = Client.GetDatabase("test").GetCollection<BsonDocument>("estoque");
		var product = new BsonDocument
			{
				{ "id", Id()+1 },
				{ "name", prod.Name },
				{ "sku", prod.Sku },
				{ "price", prod.Price },
				{ "quantity", prod.Quantity}
			};
		collection.InsertOne(product);


	}
	//

}	
