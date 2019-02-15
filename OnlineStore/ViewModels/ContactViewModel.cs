using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore.ViewModels
{
    public class ContactViewModel 
    {
       [Required(ErrorMessage = "Acest camp este obligatoriu")]
       [MinLength(5, ErrorMessage = "Numele trebuie sa contina cel putin 5 caractere")]
       public string Name { get; set;}
       [MinLength(5, ErrorMessage = "Parola trebuie sa contina cel putin 5 caractere")]
       [Required(ErrorMessage = "Acest camp este obligatoriu")]
       public string Password { get; set; }
       [Required(ErrorMessage = "Acest camp este obligatoriu")]
       [EmailAddress(ErrorMessage = "Adresa de email nu este valida")]
       public string Email { get; set; }
       [Required(ErrorMessage = "Acest camp este obligatoriu")]
       public string Subject { get; set; }
       [Required(ErrorMessage = "Acest camp este obligatoriu")]
       [MaxLength(250, ErrorMessage = "Prea lung")]
       public string Review { get; set; }
    }
}
