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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
