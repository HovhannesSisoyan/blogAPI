using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
 namespace blog.DAL
 {
      public class Image
        {
            [Key]
            public int ImageId { get; set; }
            public string Url { get; set; }

            [ForeignKey(nameof(PostId))]
            public int PostId { get; set; }
            public Post Post { get; set; }
        }
 }