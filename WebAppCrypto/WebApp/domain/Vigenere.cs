using System.ComponentModel.DataAnnotations;
using WebApp.domain.Identity;

namespace WebApp.domain;

public class Vigenere
{
    public int Id { get; set; }
    [Display(Name = "Text to Encrypt")]
    public string? PlainText { get; set; }
    [Display(Name = "Key")]
    public string KeyPass { get; set; } = default!;
    [Display(Name = "Encrypted text")]
    public string? Encryption { get; set; }
    
    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }

}