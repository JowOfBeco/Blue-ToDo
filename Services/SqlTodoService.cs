using BlueToDo.Data;
using BlueToDo.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlueToDo.Services
{
    public class SqlTodoService
    {
        TodoContext _context;


        public SqlTodoService(TodoContext context)
        {
            _context = context;
        }


        public List<TodoModel> Get(string username) => _context.TodoModel.Where(u=> u.User.UserName == username).ToList();

        public List<TodoModel> GetPriority(string username) => _context.TodoModel.Where(u => u.User.UserName == username && u.Status == false).OrderBy(x=> x.Priority).ThenBy(x=> x.DeadLine).ToList();

        public TodoModel ChangeStatus(int? id)
        {
            var task = _context.TodoModel.FirstOrDefault(e => e.IdTask == id);
            if(task.Status == true)
            {
                task.Status = false;
            }
            else
            {
                task.Status = true;
            }

            return task;

        }




        public TodoModel Get(int? id) 
        {
           return _context.TodoModel.FirstOrDefault(e => e.IdTask == id);
        }

        public bool Update(TodoModel todo)
        {
            try
            {
                var exists = Get(todo.IdTask);
                if (exists == null) return false;
                if(todo.DeadLine != null)
                {
                    exists.DeadLine = todo.DeadLine;
                }
                if(todo.Priority != null)
                {
                    exists.Priority = todo.Priority;
                }
                if(todo.TaskName != null)
                {
                    exists.TaskName = todo.TaskName;
                }
                if(todo.Task != null)
                {
                    exists.Task = todo.Task;
                }
                if(todo.Status != null)
                {
                    exists.Status = todo.Status;
                }
                            
                _context.TodoModel.Update(exists);
                _context.SaveChanges();
                todo = exists;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {

                _context.TodoModel.Remove(Get(id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Create(TodoModel todo)
        {
            try
            {
                var exists = Get(todo.IdTask);
                if (exists != null) return false;
                todo.TaskDate = System.DateTime.Today;
                todo.Status = false;
                _context.TodoModel.Add(todo);
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
