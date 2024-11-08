namespace scheapp.app.Models.API
{
    public class CustomerConfirmationRQ
    {
        public string VoiceApiConversationId { get; set; } = "";
        public bool CustomerConfirmed { get; set; }
    }
}
