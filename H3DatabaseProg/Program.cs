using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

static void SeedTasks()
{
    using var db = new BloggingContext();

    // Note: This sample requires the database to be created before running.
    Console.WriteLine($"Database path: {db.DbPath}.");

    Task ProduceSoftwareTask = new();
    ProduceSoftwareTask.Name = "Produce Software";
    ProduceSoftwareTask.Todos = new()
    {
        new Todo { Name = "Write code", IsComplete = true },
        new Todo { Name = "Compile source", IsComplete = true },
        new Todo { Name = "Test program", IsComplete = false },
    };

    Task BrewCoffeeTask = new();
    BrewCoffeeTask.Name = "Brew Coffee";
    BrewCoffeeTask.Todos = new()
    {
        new Todo { Name = "Pour water", IsComplete = true },
        new Todo { Name = "Pour coffee", IsComplete = true },
        new Todo { Name = "Turn on", IsComplete = true },
    };

    db.Tasks.Add(ProduceSoftwareTask);
    db.Tasks.Add(BrewCoffeeTask);
    db.SaveChanges();

    var tasks = db.Tasks.Include(task => task.Todos);
    foreach (var task in tasks)
    {
        Console.WriteLine($"Task: {task.Name}");
        foreach (var todo in task.Todos)
        {
            Console.WriteLine($"- {todo.Name} is {(todo.IsComplete ? "complete" : "incomplete")}");
        }
    }
}

//SeedTasks();

static void PrintIncompleteTasks()
{
    using var db = new BloggingContext();

    var tasks = db.Tasks.Include(task => task.Todos);
    foreach (var task in tasks)
    {
        if (task.Todos.Any(todo => !todo.IsComplete))
        {
            Console.WriteLine($"Task: {task.Name}");
            foreach (var todo in task.Todos)
            {
                if (!todo.IsComplete)
                {
                    Console.WriteLine($"- {todo.Name}");
                }
            }
        }
    }
}

//PrintIncompleteTasks();

static void SeedWorkers()
{
    var db = new BloggingContext();

    Team Frontend = new();
    Frontend.Name = "Frontend";
    Frontend.Workers = new()
    {
        new TeamWorker { Worker = new Worker { Name = "Steen Secher" } },
        new TeamWorker { Worker = new Worker { Name = "Ejvind Møller" } },
        new TeamWorker { Worker = new Worker { Name = "Konrad Sommer" } },
    };
    Frontend.Tasks = new()
    {
        new Task { Name = "Design database" },
        new Task { Name = "Implement database" },
    };
    Frontend.CurrentTaskId = Frontend.Tasks.First().TaskId;

    Team Backend = new();
    Backend.Name = "Backend";
    Backend.Workers = new()
    {
        new TeamWorker { Worker = new Worker { Name = "Konrad Sommer" } },
        new TeamWorker { Worker = new Worker { Name = "Sofus Lotus" } },
        new TeamWorker { Worker = new Worker { Name = "Remo Lademann" } },
    };
    Backend.Tasks = new()
    {
        new Task { Name = "Design database" },
        new Task { Name = "Implement database" },
    };
    Backend.CurrentTaskId = Backend.Tasks.First().TaskId;

    Team Testere = new();
    Testere.Name = "Testere";
    Testere.Workers = new()
    {
        new TeamWorker { Worker = new Worker { Name = "Ella Fanth" } },
        new TeamWorker { Worker = new Worker { Name = "Anne Dam" } },
        new TeamWorker { Worker = new Worker { Name = "Steen Secher" } },
    };
    Testere.Tasks = new()
    {
        new Task { Name = "Test website" },
        new Task { Name = "Test database" },
    };
    Testere.CurrentTaskId = Testere.Tasks.First().TaskId;

    db.Teams.Add(Frontend);
    db.Teams.Add(Backend);
    db.Teams.Add(Testere);
    db.SaveChanges();
}

SeedWorkers();