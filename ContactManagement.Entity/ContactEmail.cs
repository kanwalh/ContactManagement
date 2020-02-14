using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagement.Entity
{
    public class ContactEmail
    {
        public int Id { get; set; }

        public EmailType Type { get; set; }

        public string Email { get; set; }

        public string CustomName { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public Contact Contact { get; set; }
    }
}