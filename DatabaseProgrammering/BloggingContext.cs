using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DatabaseProgrammering.Models;
using Task = DatabaseProgrammering.Models.Task;

namespace DatabaseProgrammering;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Task> Tasks { get; set; } 
    public DbSet<Todo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .HasKey(t => t.TodoId);

        modelBuilder.Entity<Task>()
            .HasKey(t => t.TaskId);
    }

    public string DbPath { get; }

    public BloggingContext()
    {
        // Set the path to save the database in the project folder
        var projectFolder = AppContext.BaseDirectory; // This gives the path to the project folder
        DbPath = System.IO.Path.Combine(projectFolder, "blogging.db");
    }

    // Configure EF to create an SQLite database file in the project folder
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
