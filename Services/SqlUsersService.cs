using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using BlueToDo.Data;
using BlueToDo.Models;

namespace CodeBlue.Services
{
    public class SqlUsersService
    {
        TodoContext _context;
        

        public SqlUsersService(TodoContext context, UserManager<Users> userManager)
        {
            _context = context;
            
        }

        
        public List<Users> Get() => _context.User.ToList();
        public Users Get(string? id)=> _context.User.FirstOrDefault(u => u.Id == id);
          

        public bool Update(Users u)
        {
            try
            {
                var exists = Get(u.Id);
                if (exists == null) return false;
                if(u.Nome != null)
                {
                    exists.Nome = u.Nome;
                }
                if (u.Sobrenome != null)
                {
                    exists.Sobrenome = u.Sobrenome;
                }
               
                _context.User.Update(exists);
                _context.SaveChanges();
                u = exists;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool Delete(string? id) 
        {
            try
            {
                
                _context.User.Remove(Get(id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Create(Users u)
        {
            try
            {
                var exists = Get(u.Id);
                if (exists != null) return false;
                _context.User.Add(u);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
            
    }
}
