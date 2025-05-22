using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role Is Required")]
        [StringLength(256, ErrorMessage = "Role Length Should Be less Than 256 char ")]
        public string Name { get; set; } = default!;
    }
}
