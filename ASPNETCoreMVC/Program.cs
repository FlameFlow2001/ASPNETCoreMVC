using ASPNETCoreMVC.Domain;
using ASPNETCoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Task = ASPNETCoreMVC.Domain.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer
("Data Source=(local); Database=Task; Persist Security Info=False; MultipleActiveResultSets=True; Trusted_Connection=True; Encrypt = False"));
builder.Services.AddTransient<TaskRepository>();
//builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Task"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/api/task/", async (AppDbContext db) =>
    await db._tasks.Select(t => new { _title = t._title, _created_at = t._created_At }).ToListAsync());

app.MapGet("/api/task/{task_id}", async (int task_id, AppDbContext db) =>
    await db._tasks.FindAsync(task_id)
    is Task task
    ? Results.Ok(new {title = task._title, content = task._content, created_at = task._created_At })
    : Results.NotFound());

app.MapPost("/api/task/", async (Task task, AppDbContext db) =>
{
    db._tasks.Add(task);
    await db.SaveChangesAsync();
    return Results.Created($"/api/task/{task._id}", task);
});


app.MapPut("/api/task/{task_id}", async (int task_id, Task inputTask, AppDbContext db) =>
{
    var task = await db._tasks.FindAsync(task_id);

    if (task is null) return Results.NotFound();

    task._title = inputTask._title;
    task._content = inputTask._content;
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/api/task/{task_id}", async (int task_id, AppDbContext db) =>
{
    if (await db._tasks.FindAsync(task_id) is  Task task)
    {
        db._tasks.Remove(task);
        await db.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();