using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace blog.DAL
{
  public class Comment
  {
    [Key]
    public int CommentId { get; set; }
    [ForeignKey(nameof(PostId))]
    public int PostId { get; set; }
    public Post Post { get; set; }
     [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
  }
}