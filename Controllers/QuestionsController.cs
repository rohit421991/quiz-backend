using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz.Models;
using Microsoft.EntityFrameworkCore;

namespace Quiz.Controllers
{
    [Produces("application/json")]
    [Route("api/Questions")]
    public class QuestionsController : Controller
    {
        private QuizContext context = null;

        public QuestionsController(QuizContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            try
            {
                return context.Questions;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{quizId}")]
        public IEnumerable<Question> Get([FromRoute] int quizId)
        {
            return context.Questions.Where(m => m.QuizId == quizId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            var quiz = context.Quiz.SingleOrDefault(q => q.Id == question.QuizId);
            if (quiz == null)
                return NotFound();

            context.Questions.Add(question);
            await context.SaveChangesAsync();

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            if (id != question.Id)
                return BadRequest();

            context.Entry(question).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok(question);
        }
    }
}