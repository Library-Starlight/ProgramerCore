using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Linq();
        }

        /// <summary>
        /// 表达式树简单例子
        /// </summary>
        private static void Simple()
        {
            Expression<Func<int, int, int>> expr = (int i, int j) => i + j;
            var fun = expr.Compile();
            var sum = fun(1, 5);
            Console.WriteLine(sum);
        }

        /// <summary>
        /// 使用表达式树执行Linq查询
        /// </summary>
        private static void Linq()
        {
            var lst = new List<Apple>
            {
                new Apple{ Id = 1, Name = "红苹果", ProductCountry = "澳大利亚", Value = 15.05m},
                new Apple{ Id = 1, Name = "绿苹果", ProductCountry = "中国", Value = 16.05m},
            };

            var q1 = lst.Where(apple => apple.ProductCountry == "中国");
            var results1 = q1.ToList();
            Write(results1);

            IQueryable<Apple> queryableData = lst.AsQueryable();
            //IQueryable queryableData = lst.AsQueryable();
            ParameterExpression pe = Expression.Parameter(queryableData.ElementType, "apple");
            Expression left = Expression.Property(pe, "ProductCountry");
            Expression right = Expression.Constant("中国");
            Expression e1 = Expression.Equal(left, right);

            // 调用public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);                )
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryableData.ElementType },
                queryableData.Expression,
                Expression.Lambda<Func<Apple, bool>>(e1, pe));

            var results2 = queryableData.Provider.CreateQuery<Apple>(whereCallExpression).ToList();
            Write(results2);
        }

        private static void Write(object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
    }
}
