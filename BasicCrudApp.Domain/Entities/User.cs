using BasicCrudApp.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicCrudApp.Domain.Entities
{
    public class User : AuditableEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }

    }
}
