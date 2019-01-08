using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestMakerFreeWebApp.ViewModels;
using Newtonsoft.Json;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Data.Models;
using Mapster;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller, IBaseAPIController
    {
        private ApplicationDbContext DBContext;
        private readonly JsonSerializerSettings JsonSettings;

        ApplicationDbContext IBaseAPIController.DBContext => this.DBContext;

        JsonSerializerSettings IBaseAPIController.JsonSettings => this.JsonSettings;

        public QuestionController(ApplicationDbContext context)
        {
            this.DBContext = context;
            this.JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };
        }
        // GET: api/question/all/7
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var questions = DBContext.Questions.Where(q => q.QuizId == quizId).ToArray();
            return new JsonResult(questions.Adapt<QuestionViewModel[]>(), this.JsonSettings);
        }

        #region RESTful based convention methods
        // GET api/<controller>/5
        /// <summary>
        /// Retrieves the question with given id
        /// </summary>
        /// <param name="id">The id of an existsing answer</param>
        /// <returns>The answer to the gievn id.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = this.DBContext.Questions.Where(q => q.QuestionId == id).FirstOrDefault();
            if (null == question)
                return NotFound(new { Error = $"Question with the id [{ id }] has not been found." });
            
            return new JsonResult(question.Adapt<QuestionViewModel>(), this.JsonSettings);
        }

        // POST api/<controller>
        /// <summary>
        /// Edit the Question with the given id.
        /// </summary>
        /// <param name="m">The QuestionViewModel containing the data to update.</param>
        [HttpPost]
        public IActionResult Post([FromBody]QuestionViewModel m)
        {
            if (null == m)
                return new StatusCodeResult(500);

            var question = this.DBContext.Questions.Where(q => q.QuestionId == m.QuestionId).FirstOrDefault();

            if (null == question)
                return NotFound(new {
                    Error = $"No question with id [{ m.QuestionId }] was found."
                });

            // handle the update (without object-mapping)
            // by manually assigning the properties
            // we want to accept from the request
            question.QuizId = m.QuizId;
            question.Text = m.Text;
            question.Notes = m.Notes;
            // properties set from server-side
            question.LastModifiedDate = question.CreatedDate;

            this.DBContext.SaveChanges();
            return new JsonResult(question.Adapt<QuestionViewModel>(), this.JsonSettings);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Adds a new question to the data base.
        /// </summary>
        /// <param name="m">The question view model containing the data to insert.</param>
        [HttpPut]
        public IActionResult Put([FromBody] QuestionViewModel m)
        {
            if (null == m)
                return new StatusCodeResult(500);

            var question = m.Adapt<Question>();
            // override those properties
            // that should be set from the server-side only
            question.QuizId = m.QuizId;
            question.Text = m.Text;
            question.Notes = m.Notes;
            // properties set from server-side
            question.CreatedDate = DateTime.Now;
            question.LastModifiedDate = question.CreatedDate;

            this.DBContext.Questions.Add(question);
            this.DBContext.SaveChanges();
            return new JsonResult(question.Adapt<QuestionViewModel>(), this.JsonSettings);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the question with the given id from the data base.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{QuestionId}")]
        public IActionResult Delete(int QuestionId)
        {
            var question = this.DBContext.Questions.Where(q => q.QuestionId == QuestionId).FirstOrDefault();
            if ( null == question)
                return NotFound(
                        new { Error = $"Question with id [{ QuestionId }] was not found."}
                    );

            this.DBContext.Questions.Remove(question);
            this.DBContext.SaveChanges();

            return new OkResult();
        }
        #endregion
    }
}
