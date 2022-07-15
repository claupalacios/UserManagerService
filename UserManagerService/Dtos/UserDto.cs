using System;

namespace UserManagerService.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active = true;
    }
}
