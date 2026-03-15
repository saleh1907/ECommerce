using ECommerce.Common;

namespace ECommerce.Models;

public class AppUser:BaseEntity
{
    public string FullName { get; set; }=string.Empty;
    public string UserName { get; set; }=string.Empty;
    public string Email { get; set; }=string.Empty;
    public string Password { get; set; }=string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}
