using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestMakerFreeWebApp.Data.Models;

namespace TestMakerFreeWebApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions options) :
            base(options)
        {

        } 
        #endregion
       
        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Quizzes).WithOne(i => i.User);

            // ... Quizzes db table
            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<Quiz>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Quiz>().HasOne<ApplicationUser>().WithMany(u => u.Quizzes);
            modelBuilder.Entity<Quiz>().HasMany(i => i.Questions).WithOne(c => c.Quiz);

            // ... Questions db table
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Question>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Question>().HasOne(i => i.Quiz).WithMany(u => u.Questions);
            modelBuilder.Entity<Question>().HasMany(i => i.Answers).WithOne(a => a.Question);


            // ... Answers db table
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Answer>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Answer>().HasOne(i => i.Question).WithMany(u => u.Answers);

            //.. Results db table
            modelBuilder.Entity<Result>().ToTable("Results");
            modelBuilder.Entity<Result>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Result>().HasOne(i => i.Quiz).WithMany(u => u.Results);

        }     
        #endregion
    }
}
