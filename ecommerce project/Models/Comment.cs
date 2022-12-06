using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Models
{
    public class Comment
    {
        /* userId and productid will not be doplicated */
         int Id { get; set; }
         int UserId { get; set; }
        //public User Users { get; set; }
         int ProductId { get; set; }
        //public Product Products { get; set; }
        [Required,MaxLength(120)]
         string CommentText { get; set; }
         bool Status{ get; set; }
         DateTime CreatedAt { get; set; }
         DateTime ModifiedAt { get; set; }

    }
}
