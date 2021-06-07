using System;
using System.Collections.Generic;
using EstoqueAPI.Models;
using EstoqueAPI.Controllers;
using EstoqueAPI.Services;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace EstoqueAPI.Services
{
    public static class PublisherService
    {
        public static void Publish (Product order)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Venda",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                var message = System.Text.Json.JsonSerializer.Serialize(order);
                var body = Encoding.UTF8.GetBytes(message.ToString());

                channel.BasicPublish(exchange: "",
                                    routingKey: "Venda",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" [x] Sent {0}", order.Id);
            }
        }
    }
}