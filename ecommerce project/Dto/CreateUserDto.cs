
using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Dto;

public class CreateUserDto
{
    public string Username { get; set; }

    [DataType(DataType.Password)]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Password is must be 6 characters long")]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "The password must contain a lowercase upper case and Special characters")]
    public string Password { get; set; }
    [EmailAddress(ErrorMessage = "The Email should be email format")]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [MaxLength(60, ErrorMessage = "The FirstName should be not more 60 char")]
    public string FristName { get; set; }
    [MaxLength(60, ErrorMessage = "The LastName should be not more 60 char")]
    public string LastName { get; set; }
    public int TypeId { get; set; }
    public string? Address { get; set; }
}
