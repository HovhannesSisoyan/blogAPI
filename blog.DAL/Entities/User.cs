using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog.DAL
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required,EmailAddress, JsonProperty("Email:")]
        public string Email { get; set; }
        [Required, JsonProperty("Username:")]
        public string Username { get; set; }
        [Required, JsonProperty("Password:")]
        public string Password { get; set; }
        [Required, JsonProperty("FirstName:")]
        public string FirstName { get; set; }
        [Required, JsonProperty("LastName:")]
        public string LastName { get; set; }
        [Required, JsonProperty("BirthDate:")]
        public DateTime BirthDate { get; set; }
        [Required, JsonProperty("Gender:")]
        public bool Gender { get; set; }
        public List<Post> Posts { get; set;} = new List<Post>();
    }
}