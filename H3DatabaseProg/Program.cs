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
        new Task { Name = "Design database", Todos = new() { new Todo { Name = "Design tables" } } },
        new Task { Name = "Implement database", Todos = new() { new Todo { Name = "Implement tables", IsComplete = true } } }
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
        new Task { Name = "Design database", Todos = new() { new Todo { Name = "Design connection" } } },
        new Task { Name = "Implement database", Todos = new() { new Todo { Name = "Implement connection", IsComplete = true } } }
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
        new Task { Name = "Test website", Todos = new() { new Todo { Name = "Test login", IsComplete = true }, new Todo { Name = "Test logout", IsComplete = false } } },
        new Task { Name = "Test database", Todos = new() { new Todo { Name = "Test connection", IsComplete = false } } }
    };
    Testere.CurrentTaskId = Testere.Tasks.First().TaskId;

    db.Teams.Add(Frontend);
    db.Teams.Add(Backend);
    db.Teams.Add(Testere);
    db.SaveChanges();
}

//SeedWorkers();

static void PrintTeamsWithoutTasks()
{
    using var db = new BloggingContext();

    var teams = db.Teams.Include(team => team.Tasks);
    foreach (var team in teams)
    {
        if (team.Tasks.Count == 0)
        {
            Console.WriteLine($"Team: {team.Name}");
        }
    }
}

//PrintTeamsWithoutTasks();

static void PrintTeamCurrentTask()
{
    using var db = new BloggingContext();

    var teams = db.Teams.Include(team => team.Tasks);
    foreach (var team in teams)
    {
        if (team.Tasks.Count > 0)
        {
            Console.WriteLine($"Team: {team.Name}");
            Console.WriteLine($"- Current task: {team.Tasks.First().Name}");
        }
    }
}

//PrintTeamCurrentTask();

static void PrintTeamProgress()
{
    using var db = new BloggingContext();

    var teams = db.Teams.Include(team => team.Tasks).ThenInclude(task => task.Todos);
    foreach (var team in teams)
    {
        if (team.Tasks.Count > 0)
        {
            var task = team.Tasks.First();
            var todos = task.Todos;
            var completedTodos = todos.Count(todo => todo.IsComplete);
            int progress = 0;
            if(todos.Count > 0)
            {
                progress = (int)((double)completedTodos / todos.Count * 100);
            }

            Console.WriteLine($"Team: {team.Name}");
            Console.WriteLine($"- Current task: {task.Name}");
            Console.WriteLine($"- Progress: {progress}%");
        }
    }
}

PrintTeamProgress();