using Microsoft.EntityFrameworkCore;
using TodoList.Api.Models;

namespace TodoList.Api
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        {

        }
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.Id)
                .IsRequired();

                entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.IsCompleted)
                .IsRequired()
                .HasMaxLength(1);
            });

            base.OnModelCreating(builder);
        }
    }
}
