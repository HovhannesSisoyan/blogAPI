using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace blog.DAL
{
      public class Post
      {
            [Key, JsonProperty("PostId:")]
            public int PostId { get; set; }
            [JsonProperty("Title:")]
            public string Title { get; set; }
            [JsonProperty("Category:")]
            public String Category { get; set; }
            [JsonProperty("Body:")]
            public string Body { get; set; }

            [ForeignKey(nameof(UserId)), JsonProperty("UserId:")]
            public int UserId { get; set; }
            public User User { get; set; }
            public List<Image> Image { get; set; }
      }
}