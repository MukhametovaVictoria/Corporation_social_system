using MassTransit;

namespace WebApi.RmConsumer
{
	public class EventConsumer : IConsumer<MessageDto>
	{
		public async Task Consume(ConsumeContext<MessageDto> context)
		{
			await Task.Delay(TimeSpan.FromSeconds(2));
			Console.WriteLine("Value: {0}", context.Message.Content);
		}
	}
}
