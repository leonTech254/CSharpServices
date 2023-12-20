using Azure.Messaging.ServiceBus;
using MessageModelNamesapce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Amqp.Framing;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceNamespace
{
	public class MessageService
	{
		private readonly IConfiguration _configuration; 

		public MessageService(IConfiguration configuration) {
		
			_configuration = configuration;
		}

		

		public async Task<ActionResult> SendMessageAsync(MessageModel usermessage)
		{
			 string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			 string queueName = _configuration.GetSection("AzureServices:qName").Value;
			String messageBody = usermessage.messageBody;

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a sender for the queue
				ServiceBusSender sender = client.CreateSender(queueName);

				// Create a message
				ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

				// Send the message
				await sender.SendMessageAsync(message);

				Console.WriteLine($"Sent a message to the queue: {messageBody}");

				return new OkObjectResult("message Set Successfully");
			}
		}

	}
}
