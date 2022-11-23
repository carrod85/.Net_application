using System.ComponentModel.DataAnnotations;
using WebApp.domain.Identity;

namespace WebApp.domain;

public class DHellman
{
    public int Id { get; set; }
    [Display(Name = "Order of group")] 
    public int Modulo { get; set; }
    [Display(Name = "Generator")] 
    public int Group { get; set; }
    [Display(Name = "Private Key A person")] 
    public int PrivateX{ get; set; }
    [Display(Name = "Private Key B person")] 
    public int PrivateY{ get; set; }
    [Display(Name = "Shared key")] 
    public int? SharedSecret { get; set; }
    
    public string AppUserId { get; set; } = default!;
    public AppUser? AppUser { get; set; }
}