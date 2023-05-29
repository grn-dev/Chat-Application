namespace Chat.Application.ViewModels
{
    public partial class TicketCategoryViewModel
    {
        public class TicketCategoryExpose
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }

        }

        public class RegisterTicketCategory
        {
            public string Description { get; set; }
            public string Name { get; set; }

        }

        public class UpdateTicketCategory : RegisterTicketCategory
        {
            public int Id { get; set; }
        }
    }
}

