using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFreeWebApp.Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace TestMakerFreeWebApp.Data
{
    public static class DBSeeder
    {
        #region Public Methods
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
                CreateUsers(context);

            if (!context.Quizzes.Any())
                CreateQuizzes(context);
        }
        #endregion


        #region Seed Methods
        private static void CreateUsers(ApplicationDbContext context)
        {
            //... create admin user
            DateTime dateCreated = new DateTime(2018, 12, 22, 21, 30, 00);
            DateTime dateModified = DateTime.Now;
            var admin = new ApplicationUser()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "admin@testmakerfree.com",
                CreatedDate = dateCreated,
                LastModifiedDate = dateModified
            };
            //... add admin to the data base
            context.Users.Add(admin);

#if DEBUG
            // Create some sample registered user accounts (if they don't exist already)
            var user_Ryan = new ApplicationUser()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = "Ryan",
                Email = "ryan@testmakerfree.com",
                CreatedDate = dateCreated,
                LastModifiedDate = dateModified
            };

            var user_Solice = new ApplicationUser()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = "Solice",
                Email = "solice@testmakerfree.com",
                CreatedDate = dateCreated,
                LastModifiedDate = dateModified
            };
            var user_Vodan = new ApplicationUser()
            {
                UserId = Guid.NewGuid().ToString(),
                UserName = "Vodan",
                Email = "vodan@testmakerfree.com",
                CreatedDate = dateCreated,
                LastModifiedDate = dateModified
            };
            context.Users.AddRange(user_Ryan, user_Solice, user_Vodan);
#endif

            context.SaveChanges();
        }



        private static void CreateQuizzes(ApplicationDbContext context)
        {
            DateTime createdDate = new DateTime(2018, 12, 22, 22, 02, 00);
            DateTime modifiedDate = DateTime.Now;
            var adminUser = context.Users
                .Where(u => u.UserName == "Admin")
                .FirstOrDefault();
#if DEBUG
            int num = 47;
            for (int i = 0; i < 47; i++)
            {
                CreateSampleQuiz(context,
                                i,
                                adminUser.UserId,
                                num - i,
                                3,
                                3,
                                3,
                                createdDate.AddDays(-num));

            }
#endif

            EntityEntry<Quiz> e1 = context.Quizzes.Add(new Quiz()
            {
                UserId = adminUser.UserId,
                Title = "Are you more Light or Dark side of the Force?",
                Description = "Star Wars personality test",
                Text = @"Choose wisely you must, young padawan: this test will prove if your will is strong enough to adhere to the principles of the light side of the Force or if you're fated to embrace the dark side. No you want to become a true JEDI, you can't possibly miss this!",
                ViewCount = 2343,
                CreatedDate = createdDate,
                LastModifiedDate = modifiedDate
            });

            EntityEntry<Quiz> e2 = context.Quizzes.Add(new Quiz()
            {
                UserId = adminUser.UserId,
                Title = "GenX, GenY or Genz?",
                Description = "Find out what decade most represents you",
                Text = @"Do you feel confortable in your generation? What year should you have been born in? Here's a bunch of questions that will help you to find out!",
                ViewCount = 4180,
                CreatedDate = createdDate,
                LastModifiedDate = modifiedDate
            });

            EntityEntry<Quiz> e3 = context.Quizzes.Add(new Quiz()
            {
                UserId = adminUser.UserId,
                Title = "Which Shingeki No Kyojin character are you?",
                Description = "Attack On Titan personality test",
                Text = @"Do you relentlessly seek revenge like Eren? Are you willing to put your like on the stake to protect your friends like Mikasa ? Would you trust your fighting skills like Levi or rely on your strategies and tactics like Arwin? Unveil your true self with this Attack On Titan personality test!",
                ViewCount = 5203,
                CreatedDate = createdDate,
                LastModifiedDate = modifiedDate
            });
            context.SaveChanges();
        }

        #endregion

        #region Utility Methods
        /// <summary>
        /// Creates a sample quiz and add it to the Database
        /// together with a sample set of questions, answers & results.
        /// </summary>
        /// <param name="userId">the author ID</param>
        /// <param name="id">the quiz ID</param>
        /// <param name="createdDate">the quiz CreatedDate</param>
        private static void CreateSampleQuiz(
                                            ApplicationDbContext dbContext,
                                            int num,
                                            string authorId,
                                            int viewCount,
                                            int numberOfQuestions,
                                            int numberOfAnswersPerQuestion,
                                            int numberOfResults,
                                            DateTime createdDate)
        {
            var quiz = new Quiz()
            {
                UserId = authorId,
                Title = $"Quiz {num} Title",
                Description = $"This is a sample description for quiz { num }.",
                Text = "This is a sample quiz created by the DbSeeder class for All the questions, answers & results are autogeneratedas well.",
                ViewCount = viewCount,
                CreatedDate = createdDate,
                LastModifiedDate = createdDate
            };
            dbContext.Quizzes.Add(quiz);
            dbContext.SaveChanges();

            for (int i = 0; i < numberOfQuestions; i++)
            {
                var question = new Question()
                {
                    QuizId = quiz.QuizId,
                    Text = "This is a sample question created by the DbSeeder class for testing purposes. All the child answers are auto-generated as well.",
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                };

                dbContext.Questions.Add(question);
                dbContext.SaveChanges();

                for (int i2 = 0; i2 < numberOfAnswersPerQuestion; i2++)
                {
                    var e2 = dbContext.Answers.Add(new Answer()
                    {
                        QuestionId = question.QuestionId,
                        Text = "This is a sample answer created by the DbSeeder class for testing purposes. ",
                        Value = i2,
                        CreatedDate = createdDate,
                        LastModifiedDate = createdDate
                    });
                }
            }

            for (int i = 0; i < numberOfResults; i++)
            {
                dbContext.Results.Add(new Result()
                {
                    QuizId = quiz.QuizId,
                    Text = "This is a sample result created by the DbSeeder class for testing purposes. ",
                    MinValue = 0,
                    // max value should be equal to answers number * max answer value
                    MaxValue = numberOfAnswersPerQuestion * 2,
                    CreatedDate = createdDate,
                    LastModifiedDate = createdDate
                });
            }
            dbContext.SaveChanges();
        }
        #endregion
    }
}