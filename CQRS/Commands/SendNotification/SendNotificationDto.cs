namespace RakbnyMa_aak.CQRS.Commands.SendNotification
{
    public class SendNotificationDto
    {
        public string ReceiverId { get; set; }
        public string SenderUserId { get; set; }
        public string Message { get; set; }
    }
}
