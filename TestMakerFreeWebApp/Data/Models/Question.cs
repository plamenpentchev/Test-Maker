﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TestMakerFreeWebApp.Data.Models
{
    public class Question
    {
        #region Constructor
        public Question()
        {

        }
        #endregion


        #region Properties
        [Key]
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string Text { get; set; }

        public string Notes { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }
        [DefaultValue(0)]
        public int Flags { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion

        #region Lazy-loading Properties
        /// <summary>
        /// the parent quiz
        /// </summary>
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
        /// <summary>
        /// a list containing all the realted answer to the question.
        /// </summary>
        public virtual List<Answer> Answers { get; set; }
        
        #endregion
    }

}
