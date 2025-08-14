using System.ComponentModel.DataAnnotations;

namespace CBS.CheckManagement.Data.Dto
{
    /// <summary>
    /// Data transfer object for sending a ping request.
    /// </summary>
    public class PingDto
    {
        /// <summary>
        /// Gets or sets the message to be included in the ping.
        /// </summary>
        [Required(ErrorMessage = "Message is required")]
        [StringLength(500, ErrorMessage = "Message cannot be longer than 500 characters")]
        public string Message { get; set; }
    }
}
