using MessageModelNamesapce;
using MessageServiceNamespace;
using Microsoft.AspNetCore.Mvc;

namespace TestMessagingService
{
	[ApiController]
	[Route("api/v1/test")]
	public class TestContoller:ControllerBase
	{
		private readonly MessageService _messageService;
		public TestContoller(MessageService messageService) { 
			_messageService = messageService;
		
		
		}

		[HttpPost("send/message")]
		public async Task<ActionResult> SendMessage([FromBody] MessageModel message)
		{
			try
			{
				await _messageService.SendMessageAsync(message);
				return Ok("Message sent successfully");
			}
			catch (Exception ex)
			{
				// Log the exception or handle it according to your application's needs
				return StatusCode(500, $"Internal Server Error: {ex.Message}");
			}
		}
	}

}