using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PersonalProject.Data.Spec
{
    public interface ISpesification
    {
        public interface ISpecification<T>
        {
            Expression<Func<T, bool>> Criteria { get; }
            List<Expression<Func<T, object>>> Includes { get; }
            List<string> IncludeStrings { get; }
        }
    }
}
