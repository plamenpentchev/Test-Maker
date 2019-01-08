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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller, IBaseAPIController
    {
        private readonly ApplicationDbContext DBContext;
        private readonly JsonSerializerSettings JsonSettings;

        ApplicationDbContext IBaseAPIController.DBContext => DBContext;

        JsonSerializerSettings IBaseAPIController.JsonSettings => JsonSettings;
        public AnswerController(ApplicationDbContext ctxt)
        {
            this.DBContext = ctxt;
            this.JsonSettings = new JsonSerializerSettings() { Formatting = Formatting.Indented };
        }

        // GET: api/<controller>
        [HttpGet("All/{id}")]
        public IActionResult All(int id)
        {
            var answers = this.DBContext.Answers.Where(a => a.QuestionId == id).ToArray();
            if (null == answers)
                return NotFound(new  { Error= $"No answers found to quetsion id [{ id }]"});
            
            return new JsonResult(answers.Adapt<AnswerViewModel[]>(), this.JsonSettings);
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
            var answer = this.DBContext.Answers.Where(a => a.AnswerId == id).FirstOrDefault();
            if (null == answer)
                return NotFound(new { Error = $"Answer with id [{ id }] not found."});

            return new JsonResult(answer.Adapt<AnswerViewModel>(), this.JsonSettings);
        }

        // POST api/<controller>
        /// <summary>
        /// Edit the Answer with the given id.
        /// </summary>
        /// <param name="m">The AnswerViewModel containint the data to update.</param>
        [HttpPost]
        public IActionResult Post([FromBody]AnswerViewModel m)
        {
            if(null == m)
                return new StatusCodeResult(500);

            //var answer = m.Adapt<Answer>();

            var answer = this.DBContext.Answers.Where(a => a.AnswerId == m.AnswerId).FirstOrDefault();

            if (null == answer)
                return NotFound(new
                {
                    Error = $"No answer with id [{ m.AnswerId }] was found."
                });
            // handle the update (without object-mapping)
            // by manually assigning the properties
            // we want to accept from the request
            answer.QuestionId = m.QuestionId;
            answer.Text = m.Text;
            answer.Value = m.Value;
            answer.Notes = m.Notes;
            // properties set from server-side
            answer.LastModifiedDate = answer.CreatedDate;

            this.DBContext.SaveChanges();
            return new JsonResult( answer.Adapt<AnswerViewModel>(), this.JsonSettings);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Adds a new answer to the data base.
        /// </summary>
        /// <param name="m">The answer view model containing the data to insert.</param>
        [HttpPut]
        public IActionResult Put([FromBody] AnswerViewModel m)
        {
            if (m == null)
                return new  StatusCodeResult(500);
            
            var answer = m.Adapt<Answer>();
            // override those properties
            // that should be set from the server-side only
            answer.QuestionId = m.QuestionId;
            answer.Text = m.Text;
            answer.Notes = m.Notes;
            // properties set from server-side
            answer.CreatedDate = DateTime.Now;
            answer.LastModifiedDate = answer.CreatedDate;

            this.DBContext.Answers.Add(answer);
            this.DBContext.SaveChanges();
            return new JsonResult(answer.Adapt<AnswerViewModel>(), this.JsonSettings);

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the answer with the given id from the data base.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var answer = this.DBContext.Answers.Where(a => a.AnswerId == id).FirstOrDefault();
            if (null == answer)
                return NotFound(new { Error = $"Answer with id [{ id }]" });
            
            this.DBContext.Answers.Remove(answer);
            this.DBContext.SaveChanges();
            
            return new OkResult();
        } 
        #endregion
    }
}
