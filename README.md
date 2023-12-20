# MessageService

The `MessageService` class in this project facilitates sending and receiving messages from an Azure Service Bus queue. It leverages the Azure.Messaging.ServiceBus library for interacting with the Azure Service Bus service.

## Configuration

Ensure that the required configuration settings are present in your `appsettings.json` file under the `"AzureServices"` section:



```sh

    {
      "AzureServices": {
        "connectionString": "YOUR_SERVICE_BUS_CONNECTION_STRING",
        "qName": "YOUR_QUEUE_NAME"
      }
    }
   ```

## Usage

### Sending a Message

```sh 
    // Instantiate MessageService with IConfiguration
    var messageService = new MessageService(configuration);

    // Create a MessageModel with the desired message content
    var userMessage = new MessageModel { MessageBody = "Hello, Service Bus!" };

    // Send the message asynchronously
    await messageService.SendMessageAsync(userMessage);
```

### Receiving a Message

```sh
    // Instantiate MessageService with IConfiguration
var messageService = new MessageService(configuration);

// Receive a message asynchronously
string receivedMessage = await messageService.ReceiveMessage();

// Use the received message as needed
Console.WriteLine($"Received message: {receivedMessage}");

```

## Dependencies

1. Azure.Messaging.ServiceBus for Azure Service Bus interaction
2. MessageModelNamespace for the MessageModel class
3. Microsoft.Extensions.Configuration for configuration access


1. 
### TestController


# TestController

The `TestController` class in this project serves as an API controller for testing message sending and receiving using the `MessageService`.

## Dependencies

- `MessageModelNamespace` for the `MessageModel` class
- `MessageServiceNamespace` for the `MessageService` class
- `Microsoft.AspNetCore.Mvc` for ASP.NET Core MVC

## Endpoints

### `POST /api/v1/test/send/message`

Send a message to the Azure Service Bus queue.

**Request Body:**

```sh
{
  "MessageBody": "Your message content here"
}
```

