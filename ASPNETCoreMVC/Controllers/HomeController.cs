using ASPNETCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Task = ASPNETCoreMVC.Domain.Task;

namespace ASPNETCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskRepository _taskRepository;
        public HomeController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IActionResult Index()
        {
            var model = _taskRepository.GetTasks();
            return View(model);
        }

        public IActionResult TaskEdit(int id)
        {
            Task model = id == default ? new Task() : _taskRepository.GetTasksById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult TaskEdit(Task model)
        {
            if (ModelState.IsValid)
            {
                _taskRepository.SaveTask(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult TaskDelete(int id)
        {
            _taskRepository.DeleteTask(new Task() { _id = id });
            return RedirectToAction("Index");
        }

    }
}