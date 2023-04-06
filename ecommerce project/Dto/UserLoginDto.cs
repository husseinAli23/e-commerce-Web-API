using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Dto;

/// <summary>
/// A DTO for a login user
/// </summary>
public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(int.MaxValue, MinimumLength = 6, ErrorMessage = "Password is must be 6 characters long")]
    [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "The password must contain a lowercase upper case and Special characters")]
    public string Password { get; set; }
}
