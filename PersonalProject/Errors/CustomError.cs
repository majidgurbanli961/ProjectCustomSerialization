using System;

namespace PersonalProject.Errors
{
    public class CustomError : Exception
    {
        public CustomError(string errorMessage)
            : base(errorMessage)
        {


        }
    }
}
