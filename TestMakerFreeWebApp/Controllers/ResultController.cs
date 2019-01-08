using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMakerFreeWebApp.ViewModels;
using TestMakerFreeWebApp.Data;
using TestMakerFreeWebApp.Data.Models;
using Mapster;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestMakerFreeWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : Controller, IBaseAPIController
    {
        private readonly ApplicationDbContext DBContext;
        private readonly JsonSerializerSettings JsonSettings;

        ApplicationDbContext IBaseAPIController.DBContext => DBContext;

        JsonSerializerSettings IBaseAPIController.JsonSettings => JsonSettings;

        public ResultController(ApplicationDbContext context)
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
            var results = this.DBContext.Results.Where(r => r.QuizId == quizId).ToArray();
            return new JsonResult(results.Adapt<ResultViewModel[]>(), this.JsonSettings);
        }

        #region RESTful based convention methods
        // GET api/<controller>/5
        /// <summary>
        /// Retrieves the result with given id
        /// </summary>
        /// <param name="id">The id of an existsing result</param>
        /// <returns>The result to the gievn id.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = this.DBContext.Results.Where(r => r.ResultId == id).FirstOrDefault();
            if (null == result)
                return NotFound(new { Error = $"Result with id [{ id }] not found." });
            
            return new JsonResult(result.Adapt<ResultViewModel>(), this.JsonSettings);
        }

        // POST api/<controller>
        /// <summary>
        /// Edit the Result with the given id.
        /// </summary>
        /// <param name="m">The ResultViewModel containint the data to update.</param>
        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel m)
        {
            if (m == null)
                return new StatusCodeResult(500);
            
            var result = this.DBContext.Results.Where(r => r.ResultId == m.ResultId).FirstOrDefault();

            if(result == null)
                return NotFound(new { Error = $"Results to quiz with id [{ m.QuizId }] were not found." });
            

            // handle the update (without object-mapping)
            // by manually assigning the properties
            // we want to accept from the request
            result.QuizId = m.QuizId;
            result.Text = m.Text;
            result.MinValue = m.MinValue;
            result.MaxValue = m.MaxValue;
            result.Notes = m.Notes;

            // properties set from server-side
            result.LastModifiedDate = result.CreatedDate;

            this.DBContext.SaveChanges();

            return new JsonResult(result.Adapt<ResultViewModel>(), this.JsonSettings);
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Adds a new result to the data base.
        /// </summary>
        /// <param name="m">The ResultViewModel containing the data to insert.</param>
        [HttpPut]
        public IActionResult Put([FromBody]ResultViewModel m)
        {
            if (null == m)
                return new StatusCodeResult(500);
            
            var result = m.Adapt<Result>();


            // override those properties
            // that should be set from the server-side only
            result.CreatedDate = DateTime.Now;
            result.LastModifiedDate = result.CreatedDate;

            this.DBContext.Results.Add(result);
            this.DBContext.SaveChanges();

            return new JsonResult( result.Adapt<ResultViewModel>(), this.JsonSettings);

        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Deletes the result with the given id from the data base.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = this.DBContext.Results.Where(r => r.ResultId == id).FirstOrDefault();
            if (null == result)
                return NotFound(new {Error = $"Result with the id [{ id }] was not found."});

            this.DBContext.Results.Remove(result);
            this.DBContext.SaveChanges();
            return new OkResult();

        }
        #endregion
    }
}
