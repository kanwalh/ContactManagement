using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManagement.Entity
{
    public class ContactPhone
    {
        public int Id { get; set; }
        public PhoneType Type { get; set; }

        public string Phone { get; set; }

        public string CustomName { get; set; }

        [ForeignKey("Contact")]
        public int ContactId { get; set; }

        public Contact Contact { get; set; }
    }
}