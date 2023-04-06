using System.ComponentModel.DataAnnotations;

namespace ecommerce_project.Dto;
/// <summary>
/// A user DTO without a password
/// </summary>
public class UserDto
{
    public int ID { get; set; }

    public string Username { get; set; }
   

    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public string FristName { get; set; }

    public string LastName { get; set; }
    public int TypeId { get; set; }
    public string? Address { get; set; }
}
