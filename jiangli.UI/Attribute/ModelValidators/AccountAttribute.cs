using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jiangli.UI.Attribute.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class AccountAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                ErrorMessage = "用户不能为空呢！";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class RegisterEmailAttribute : ValidationAttribute
    {
        private static readonly string RegisterErrorMessage = "注册失败";
        public RegisterEmailAttribute() : base(RegisterErrorMessage) { }
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                ErrorMessage = "邮箱不能为空";
                return false;
            }
            else
            {
                if (Repository.WebSite.Repository.DbUser.m_checkEmail((string)value))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "邮箱已经存在";
                    return false;
                }
            }
        }
    }
}