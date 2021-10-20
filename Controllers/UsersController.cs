using BlueToDo.Models;
using BlueToDo.Roles;
using CodeBlue.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlueToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        SqlUsersService _sqlUsersService;

        public UsersController(SqlUsersService sqlUsersService)
        {
            _sqlUsersService = sqlUsersService;
        }

        [HttpGet]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult GetAll() => Ok(_sqlUsersService.Get());

        [HttpGet]
        [Route("{id}")]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult Get(string id) => _sqlUsersService.Get(id) != null ? Ok(_sqlUsersService.Get(id)) : NotFound("Usuário não existe");

        [HttpPut]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult Update([FromBody] Users u)
        {
            return _sqlUsersService.Update(u) ? Ok(_sqlUsersService.Get(u.Id)) : NotFound("Erro ao atualizar Usuário");
        }

        [HttpDelete]
        [Route("{id}")]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]

        public IActionResult Delete(string id) => _sqlUsersService.Delete(id) ? Ok() : NotFound("Usuário não existe");


    }
}
