using jiangli.UI.Attribute.ModelValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiangli.UI.Models.test
{
    public class account
    {
    }
    public class LogInModal
    {
        //[Required(ErrorMessage = "账户你能为空的呢！亲")]
        [Account]
        [Display(Name = "账户")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [MinLength(6,ErrorMessage = "密码长度最少为6为呢亲!")]
        public string PassWord { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get;set; }
    }
    public class Register
    {
        [Required(ErrorMessage = "请输入邮箱")]
        [Display(Name = "邮箱")]
        [RegisterEmail]
        [EmailAddress(ErrorMessage = "您输入的不是有效的邮箱地址呦亲")]
        [Remote("GetEmail","login",ErrorMessage ="邮箱已经存在")]//远程服务端验证
        public string Email { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        [MinLength(6)]
        public string Password1 { get; set; }
        [Required(ErrorMessage = "请确认密码")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [System.ComponentModel.DataAnnotations.Compare("Password1")]
        public string Password2 { get; set; }
    }
}