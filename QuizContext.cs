using Microsoft.EntityFrameworkCore;
using Quiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }


        public DbSet<Question> Questions { get; set; }


        public DbSet<Quiz.Models.Quiz> Quiz { get; set; }
    }
}
