using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Models;

public class Comment
{
    /* userId and productid will not be doplicated */
    public int Id { get; set; }
    public int UserId { get; set; }
    //public User Users { get; set; }
    public int ProductId { get; set; }
    //public Product Products { get; set; }
    [Required,MaxLength(120)]
    public string CommentText { get; set; }
    public bool Status{ get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

}
