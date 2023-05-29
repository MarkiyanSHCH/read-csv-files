using System;
using System.Collections.Generic;

namespace Domain.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<TSource> Yield<TSource>(this TSource item)
        {
            yield return item;
        }

        public static TSource Apply<TSource>(this TSource source, Func<TSource, TSource> functor)
            => functor(source);

        public static TOut Bind<TSource, TOut>(this TSource _, TOut returnValue)
            => returnValue;

        public static TSource Switch<TSource>(
            this TSource source,
            Func<TSource, bool> predicate,
            Func<TSource, TSource> success,
            Func<TSource, TSource> failure)
            => predicate(source) ? success(source) : failure(source);
    }
}