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
    public class AnswerController : Controller
    {
        // GET: api/<controller>
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId)
        {
            var sampleAnswers = new List<AnswerViewModel>();
            sampleAnswers.Add(new AnswerViewModel() {
                Id = 1,
                QuestionId = questionId,
                Text = "Friends and Family",
                Value = new Random().Next(1, 10),
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            });

            for (int i = 2; i < 5; i++)
            {
                sampleAnswers.Add(new AnswerViewModel()
                {
                    Id = i,
                    QuestionId = questionId,
                    Text = string.Format("Sample Answer {0}", i),
                    Value = new Random().Next(1,10),
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });
            }

            return new JsonResult(
                sampleAnswers,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                }
                );
        }

        #region RESTful based convention methods
        // GET api/<controller>/5
        /// <summary>
        /// Retrieves the answer with given id
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
        /// Edit the Answer with the given id.
        /// </summary>
        /// <param name="m">The AnswerViewModel containint the data to update.</param>
        [HttpPost]
        public void Post(AnswerViewModel m)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Adds a new answer to the data base.
        /// </summary>
        /// <param name="m">The answer view model containing the data to insert.</param>
        [HttpPut("{id}")]
        public void Put(AnswerViewModel m)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the answer with the given id from the data base.
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
