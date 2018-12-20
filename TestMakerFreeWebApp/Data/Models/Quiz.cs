using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TestMakerFreeWebApp.Data.Models
{
    public class Quiz
    {
        #region Constructor
        public Quiz()
        {

        }
        #endregion

                
        #region Properties
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }

        [DefaultValue(0)]
        public int Type { get; set; }

        [DefaultValue(0)]
        public int Flags { get; set; }

        /// <summary>
        /// foreign key, points to the user who created the quiz
        /// </summary>
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ViewCount { get; set;
 }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion


        #region Lazy-Load Properties
        /// <summary>
        /// the quiz  autor: it will be loaded
        /// on first use thanks to the EF Lazy-Loading feature.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

       /// <summary>
       /// a list containing all the questions realtede to this quiz.
       /// It will be populated on first use thanks to the EF lazy-loadiing feature.
       /// </summary>
        public virtual List<Question> Questions { get; set; }
        /// <summary>
        /// A list containing all the results related to this quiz.
        /// It will be populated on first use thanks to the EF lazy-loading feature. 
        /// </summary>
        public virtual List<Result> Results { get; set; }
        #endregion
    }
}
