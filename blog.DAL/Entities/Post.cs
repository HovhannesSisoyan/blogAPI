using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace blog.DAL
{
      public class Post
      {
            [Key]
            public int PostId { get; set; }
            public string Title { get; set; }
            public String Category { get; set; }
            public string Body { get; set; }

            [ForeignKey(nameof(UserId))]
            public int UserId { get; set; }
            public User User { get; set; }
            public List<Image> Image { get; set; }
      }
}