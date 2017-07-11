﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiangli.Models
{
    /// <summary>
    /// 案件申诉表
    /// </summary>
    public class Appeal
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 案件ID
        /// </summary>
        public int CaseID { get; set; }
        /// <summary>
        /// 投诉人
        /// </summary>
        public string Complainant { get; set; }
        /// <summary>
        /// 投诉人ID
        /// </summary>
        public int ComplainantID { get; set; }
        /// <summary>
        /// 投诉人说明
        /// </summary>
        public string ComState { get; set; }
        /// <summary>
        /// 应诉人
        /// </summary>
        public string Respondent { get; set; }
        /// <summary>
        /// 应诉人ID
        /// </summary>
        public int RespondentID { get; set; }
        /// <summary>
        /// 应诉人陈述
        /// </summary>
        public string ResState { get; set; }
        /// <summary>
        /// 申诉轮数
        /// </summary>
        public int RoundNumber { get; set; }

    }
}