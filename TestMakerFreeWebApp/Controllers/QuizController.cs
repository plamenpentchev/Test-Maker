using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.ViewModels;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Data.Models;
using Newtonsoft.Json;
using Mapster;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller, IBaseAPIController
    {
        

        private ApplicationDbContext DbContext;
        private readonly JsonSerializerSettings JsonSettings;

        ApplicationDbContext IBaseAPIController.DBContext => DbContext;

        JsonSerializerSettings IBaseAPIController.JsonSettings =>JsonSettings;

        #region Constructor
        public QuizController(ApplicationDbContext ctxt)
        {
            DbContext = ctxt;
            this.JsonSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
        }
        
        #endregion


        #region RESTfull convention methods
        /// <summary>
        /// GET: api/quiz/{id}
        /// Retrieves the quiz with the given id.
        /// </summary>
        /// <param name="id">The id of an existing quiz</param>
        /// <returns></returns>
        /// =================================================
        /// OLD IMPLEMENTATION
        /// =================================================
        /// [HttpGet("{id}")]
        /// public IActionResult Get(int id)
        /// {
        /// var v = new QuizViewModel()
        ///             {
        /// Id = id,
        /// Title = String.Format("Sample quiz with id {0}", id),
        /// Description = "Not a real quiz: its just a smaple.",
        /// CreatedDate = DateTime.Now,
        /// LastModifiedDate = DateTime.Now
        /// };
        /// 
        /// return new JsonResult(v, new JsonSerializerSettings()
        /// {
        /// Formatting = Formatting.Indented
        /// });
        /// }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = DbContext.Quizzes.Where(q => q.QuizId == id).FirstOrDefault();
            if (null == quiz)
            {
                return NotFound(new { Error = $"Quiz ID: {id} has not been found." });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(), this.JsonSettings);
        }
        

        // POST api/<controller>
        /// <summary>
        /// Edit the Quiz with the given id.
        /// </summary>
        /// <param name="m">The QuizViewModel containint the data to update.</param>
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel m)
        {
            if (null == m) return new StatusCodeResult(500);

            //DbContext.Quizzes.Add(m.Adapt<Quiz>());

            //.. retrieve the quiz to edit
            var quiz = DbContext.Quizzes.Where(q => q.QuizId == m.QuizId).FirstOrDefault();
            if (null == quiz)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID {m.QuizId} has not been found. "
                });
            }

            quiz.Title = m.Title;
            quiz.Description = m.Description;
            quiz.Text = m.Text;
            quiz.Notes = m.Notes;
            quiz.LastModifiedDate = quiz.CreatedDate;

            DbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuestionViewModel>(), this.JsonSettings);
           
        }

        // PUT api/<controller>
        /// <summary>
        /// Adds a new answer to the data base.
        /// </summary>
        /// <param name="m">The QuizViewModel containing the data to insert.</param>
        [HttpPut]
        public IActionResult Put([FromBody]QuizViewModel m)
        {
            DateTime created = new DateTime(2018, 12, 23, 14, 50, 00);
            DateTime modified = DateTime.Now;
            if (null == m) return new StatusCodeResult(500);
            if ( m.Title == null || m.Description == null || m.Text == null)
            {
                return new StatusCodeResult(500);
            }

            var quiz = new Quiz()
            {
                QuizId = m.QuizId,
                Title = m.Title,
                Description = m.Description,
                Text = m.Text,
                Notes = m.Notes,
                CreatedDate = created,
                LastModifiedDate = modified,
                UserId = DbContext.Users.Where(u => u.UserName == "Admin").FirstOrDefault().UserId
            };
            DbContext.Quizzes.Add(quiz);
            DbContext.SaveChanges();

            return new JsonResult(quiz.Adapt<QuizViewModel>(), this.JsonSettings);
           
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the quiz with the given id from the data base.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quiz = DbContext.Quizzes.Where(q => q.QuizId == id).FirstOrDefault();
            if (null == quiz)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID: {id} has not been found."
                });
            }
            DbContext.Quizzes.Remove(quiz);
            DbContext.SaveChanges();
            return new OkResult();   
        }
        #endregion


        #region attribute-based routing methds
        //... api/quiz/Latest
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var quizzes = DbContext.Quizzes.OrderByDescending(q => q.CreatedDate).Take(num).ToArray();

            return new JsonResult(quizzes.Adapt<QuizViewModel[]>(), this.JsonSettings);
        }

        /// <summary>
        /// GET: api/quiz/ByTitle
        /// Retrieves the {num} Quizzes sorted by Title (A to Z)
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} Quizzes sorted by Title</returns>
        /// 
        /// =================================================
        /// OLD IMPLEMENTATION
        /// =================================================
        /// var sampleQuizzes = ((JsonResult)Latest(num)).Value
        ///     as List<QuizViewModel>;
        ///     return new JsonResult(
        /// sampleQuizzes.OrderBy(t => t.Title),
        /// new JsonSerializerSettings()
        /// {
        /// Formatting = Formatting.Indented
        /// });
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var quizzes = DbContext.Quizzes.OrderBy(q => q.Title).Take(num).ToArray();
            return new JsonResult( quizzes.Adapt<QuizViewModel[]>(), this.JsonSettings);
        }

        /// <summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} random Quizzes</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => 
            new JsonResult(DbContext.Quizzes.OrderBy(q => Guid.NewGuid())
                                .Take(num)
                                .ToArray()
                                .Adapt<QuizViewModel[]>(), this.JsonSettings);
        
        #endregion
    }
}
