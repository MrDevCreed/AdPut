﻿namespace AppService.Dto.Requests.User
{
    public class ChangePasswordRequest
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
