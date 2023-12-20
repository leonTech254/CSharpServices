using MessageModelNamesapce;
using Microsoft.AspNetCore.Mvc;

namespace TestMessagingService
{
	[ApiController]
	[Route("api/v1/test")]
	public class TestContoller:ControllerBase
	{
		public TestContoller() { 
		
		
		}

		[HttpPost("send/message")]
		public void SendMessage([FromBody] MessageModel message)
		{

			

		}
	}

}