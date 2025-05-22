using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = default!;

        [Display(Name ="Display Name")]
        public string DisplayName { get; set; }=default!;

        public string Email { get; set; } = default!;
        public string UserName {  get; set; } = default!;
        public IEnumerable<string> Roles { get; set; }
    }
}
