using Azure.Messaging.ServiceBus;
using MessageModelNamesapce;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MessageServiceNamespace
{
	public class MessageService
	{
		private readonly IConfiguration _configuration;

		public MessageService(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}
		public async Task<ActionResult> SendMessageAsync(MessageModel userMessage)
		{
			string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			string queueName = _configuration.GetSection("AzureServices:qName").Value;
			string messageBody = userMessage.messageBody;

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a sender for the queue
				ServiceBusSender sender = client.CreateSender(queueName);

				// Create a message
				ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));

				// Send the message
				await sender.SendMessageAsync(message);

				Console.WriteLine($"Sent a message to the queue: {messageBody}");

				return new OkObjectResult("Message sent successfully");
			}
		}

		public async Task<string> ReceiveMessage()
		{
			string connectionString = _configuration.GetSection("AzureServices:connectionString").Value;
			string queueName = _configuration.GetSection("AzureServices:qName").Value;

			await using (ServiceBusClient client = new ServiceBusClient(connectionString))
			{
				// Create a receiver for the queue
				ServiceBusReceiver receiver = client.CreateReceiver(queueName);

				// Receive messages
				ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

				if (receivedMessage != null)
				{
					string messageBody = Encoding.UTF8.GetString(receivedMessage.Body);
					Console.WriteLine($"Received a message from the queue: {messageBody}");

					// Complete the message to remove it from the queue
					await receiver.CompleteMessageAsync(receivedMessage);
					return messageBody;
				}
				else
				{
					Console.WriteLine("No messages available in the queue.");
					return null;
				}
			}
		}
	}
}
