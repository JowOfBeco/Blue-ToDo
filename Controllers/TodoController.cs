using BlueToDo.Models;
using BlueToDo.Roles;
using BlueToDo.Services;
using CodeBlue.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlueToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController: ControllerBase
    {
        SqlTodoService _sqlTodoService;
        SqlUsersService _sqlUsersService;

        public TodoController(SqlTodoService sqlTodoService, SqlUsersService sqlUsersService)
        {
            _sqlTodoService = sqlTodoService;
            _sqlUsersService = sqlUsersService;
        }

        /// <summary>
        /// Cria tarefas
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult Create([FromBody] TodoModel todoModel)
        {
            var userlogged = User.Identity.Name;
            var UserTask = _sqlUsersService.Get().ToList();
            todoModel.User = UserTask.FirstOrDefault(u => u.UserName == userlogged);
            return _sqlTodoService.Create(todoModel) ? Ok(_sqlTodoService.Get(todoModel.IdTask)) : NotFound("Erro ao Criar Tarefa");
        }

        /// <summary>
        /// Busca tarefas criadas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]

        public IActionResult GetAll()
        {
            var userlogged = User.Identity.Name;
            return Ok(_sqlTodoService.Get(userlogged));
        }

        /// <summary>
        /// Buscar tarefas por ordem de prioridade
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        [Route("GetPriority")]
        public IActionResult GetByPriority()
        {
            var userlogged = User.Identity.Name;
            return Ok(_sqlTodoService.GetPriority(userlogged));
        }


        /// <summary>
        /// Busca tarefas recebendo id como parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult Get(int id) => _sqlTodoService.Get(id) != null ? Ok(_sqlTodoService.Get(id)) : NotFound("Tarefa não existe");

        /// <summary>
        /// Endpoint responsável por mudar o Status da tarefa com o ID como parâmetro se o Status for true a tarefa foi concluída.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("status/{id}")]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult ChangeStatus(int id) => _sqlTodoService.ChangeStatus(id) != null ? Ok(_sqlTodoService.ChangeStatus(id)) : NotFound("Tarefa não existe");

        /// <summary>
        /// Editar tarefas
        /// </summary>
        /// <param name="todoModel"></param>
        /// <returns></returns>
        [HttpPut]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]
        public IActionResult Update([FromBody] TodoModel todoModel)
        {
           
            return _sqlTodoService.Update(todoModel) ? Ok(_sqlTodoService.Get(todoModel.IdTask)) : NotFound("Erro ao atualizar Tarefa");
        }

        /// <summary>
        /// Delete de tarefas, passar id como parametro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [RoleAuthorize(RoleTypes.SuperAdmin, RoleTypes.Usuario)]

        public IActionResult Delete(int id) => _sqlTodoService.Delete(id) ? Ok() : NotFound("Tarefa não existe");


    }
}
