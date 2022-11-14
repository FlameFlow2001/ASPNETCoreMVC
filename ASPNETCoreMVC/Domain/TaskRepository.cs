using ASPNETCoreMVC.Domain;
using Microsoft.EntityFrameworkCore;
using Task = ASPNETCoreMVC.Domain.Task;

namespace ASPNETCoreMVC.Models
{
    public class TaskRepository
    {
        private readonly AppDbContext context;
        public TaskRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Task> GetTasks()
        {
            return context._tasks.OrderBy(x => x._id);
        }

        public Task GetTasksById(int id)
        {
            return context._tasks.Single(x => x._id == id);
        }

        public int SaveTask(Task entity)
        {
            if (entity._id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();

            return entity._id;
        }

        public void DeleteTask(Task entity)
        {
            context._tasks.Remove(entity);
            context.SaveChanges();
        }
    }
}
