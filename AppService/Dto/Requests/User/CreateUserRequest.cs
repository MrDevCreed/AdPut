﻿namespace AppService.Dto.Requests.User
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }
    }
}
