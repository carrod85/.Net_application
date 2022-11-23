using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebApp.domain.Identity;

namespace WebApp.domain;

public class Cesar
{
        public int Id { get; set; }
        
        [Display(Name = "Alphabet (Optional)-> default: A..Z")]
        public string? Alphabet { get; set; }
        
       
        [Display(Name = "Text to Encrypt")] 
        public string PlainText { get; set; } = default!;
        
        [Display(Name = "Key")]
        public int Key { get; set; }
        [Display(Name = "Encryption")]
        public string? CypherText { get; set; }
        public string AppUserId { get; set; } = default!;
        //foreign key
        public AppUser? AppUser { get; set; }
        //navigator property

        // we want to get all the Cesars from User so we need to add in users table (predefined) the collection of Cesars


}
    
