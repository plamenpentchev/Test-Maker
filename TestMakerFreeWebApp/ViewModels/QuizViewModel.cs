﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;

namespace TestMakerFreeWebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class QuizViewModel
    {
        #region ctor
        public QuizViewModel()
        {

        }
        #endregion


        #region Properties
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public string Notes { get; set; }
        [DefaultValue(0)]
        public int Type { get; set; }
        [DefaultValue(0)]
        public int Flags { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public int ViewCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        #endregion
    }
}
