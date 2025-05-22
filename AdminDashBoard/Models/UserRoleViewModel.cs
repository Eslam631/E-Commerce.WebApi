using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class UserRoleViewModel
    {
       

        public string UserName {  get; set; } = default!;

        public List<UpdateRoleViewModel> Roles { get; set; } = [];

    }



}
