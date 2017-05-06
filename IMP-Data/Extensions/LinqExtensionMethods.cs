using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Extensions
{
    public static class LinqExtensionMethods
    {
        public static IQueryable<T> ConditionalWhere<T>(
        this IQueryable<T> source,
        Func<bool> condition,
        Expression<Func<T, bool>> predicate)
        {
            if (condition())
            {
                return source.Where(predicate);
            }

            return source;
        }
    }
}
