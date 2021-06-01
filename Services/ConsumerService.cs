using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using EstoqueAPI.Models;

namespace EstoqueAPI.Services{
    public class ConsumerService
    {
        public static void Init()
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var order = System.Text.Json.JsonSerializer.Deserialize<Product>(message);
                    DataBaseService.Discount(order);
                    Console.WriteLine($"Product Id {order.Id }, Order Quantity: { order.Quantity}");

                };
                channel.BasicConsume(queue: "Venda",
                                    autoAck: false,
                                    consumer: consumer);
            }
        }
    }
}
