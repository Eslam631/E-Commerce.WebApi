﻿namespace Shared.DataTransferObject.IdentityDto
{
   public class UserDto
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; }=default!;
        public string DisplayName {  get; set; } = default!;
    }
}
