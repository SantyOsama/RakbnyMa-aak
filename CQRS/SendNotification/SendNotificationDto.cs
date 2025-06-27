namespace RakbnyMa_aak.CQRS.SendNotificationForDriver
{
    public class SendNotificationDto
    {
        public string ReceiverId { get; set; }
        public string SenderUserId { get; set; }
        public string Message { get; set; }
    }
}
