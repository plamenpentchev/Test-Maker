﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace TestMakerFreeWebApp.Data.Models
{
    public class ApplicationUser
    {
        #region Constructor
        public ApplicationUser()
        {

        }
        #endregion

        #region Properties
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string DisplayName { get; set; }
        public string Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion

        #region Lazy-load Properties
        /// <summary>
        /// a list of all the quizzes, cretaed by this user.
        /// </summary>
        public virtual List<Quiz> Quizzes { get; set; }
        #endregion
    }
}
