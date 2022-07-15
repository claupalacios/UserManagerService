using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagerService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}
