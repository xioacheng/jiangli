using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jiangli.UI.Attribute.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class CustomModelValidatorAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                ErrorMessage = "账户不能为空的，亲！";
                return false;
            }
            else if(string.Compare((string)value, "admin", true)== 0)
            {
                ErrorMessage = "账户名称不合法呢！";
                return false;
            }
            else return true;
        }
    }
}