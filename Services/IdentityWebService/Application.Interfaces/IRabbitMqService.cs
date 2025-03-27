namespace Application.Interfaces
{
    public interface IRabbitMqService
    {
        void SendMessage(object obj, string hostName, string queue);
        void SendMessage(string message, string hostName, string queue);
    }
}
