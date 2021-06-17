using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace blog.DAL
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, JsonIgnore]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public bool Gender { get; set; }
        public List<Post> Posts { get; set;} = new List<Post>();
    }
}