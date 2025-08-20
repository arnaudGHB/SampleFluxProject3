using System.ComponentModel.DataAnnotations;

namespace CBS.CheckManagementManagement.Data.Entity
{
    public class Ping : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
