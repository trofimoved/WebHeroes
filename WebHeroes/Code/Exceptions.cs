using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHeroes.Exceptions
{
    public class CustomException : Exception
    {
        public string ErrorMessage { get; set; }

        public CustomException() { }

        public CustomException(string message)
        {
            ErrorMessage = message;
        }
    }

    public class NeedTargetEntityException : CustomException
    {
        public NeedTargetEntityException()
        {
            ErrorMessage = "Необхадимо указать цель";
        }
    }
    public class NeedTargetAreaException : CustomException
    {
        public NeedTargetAreaException()
        {
            ErrorMessage = "Необхадимо указать цель";
        }
    }

}