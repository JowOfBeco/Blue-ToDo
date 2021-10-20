using Microsoft.AspNetCore.Identity;

namespace BlueToDo.Models
{
    public class Users: IdentityUser
    {

        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        


    }
}
