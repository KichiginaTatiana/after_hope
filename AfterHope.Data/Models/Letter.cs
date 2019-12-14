namespace AfterHope.Data.Models
{
    public class Letter
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }

        public string Text { get; set; }

        public bool IsProcessed { get; set; }
    }
}