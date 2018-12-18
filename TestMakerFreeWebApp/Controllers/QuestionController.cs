using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.ViewModels;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        // GET: api/question/all/7
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId)
        {
            var sampleQuestions = new List<QuestionViewModel>();
            sampleQuestions.Add(new QuestionViewModel() {
                Id = 1,
                QuizId = quizId,
                Text = "What do you value most in your life?",
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i < 5; i++)
            {
                sampleQuestions.Add(new QuestionViewModel()
                {
                    Id = i,
                    QuizId = quizId,
                    Text = String.Format("Sample question {0}", i),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(
                    sampleQuestions,
                    new JsonSerializerSettings() {
                        Formatting = Formatting.Indented
                    });
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
            return Content("Not implemented yet.");
        }

        // POST api/<controller>
        /// <summary>
        /// Edit the Question with the given id.
        /// </summary>
        /// <param name="m">The QuestionViewModel containing the data to update.</param>
        [HttpPost]
        public void Post(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Adds a new question to the data base.
        /// </summary>
        /// <param name="m">The question view model containing the data to insert.</param>
        [HttpPut("{id}")]
        public void Put(QuestionViewModel m)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the question with the given id from the data base.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
