using System.ComponentModel.DataAnnotations;
using WebApp.domain.Identity;

namespace WebApp.domain;

public class Rsa
{
    public int Id { get; set; }
    [Display(Name = "Number p")] 
    public int PrimeP { get; set; }
    [Display(Name = "Number q")]   
    public int PrimeQ { get; set; }
    [Display(Name = "Public exponent e")]  
    public long E  {get; set; }
    [Display(Name = "Text to encrypt")]
    public string? Message { get; set; }
    [Display(Name = "Encrypted text")] 
    public string? Base64EncryptedMessage { get; set; }
    
    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }
}