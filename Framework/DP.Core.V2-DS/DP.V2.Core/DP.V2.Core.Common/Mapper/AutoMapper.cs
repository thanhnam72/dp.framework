using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DP.V2.Core.Common.Mapper
{
    public class AutoMapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            TTarget target = Activator.CreateInstance<TTarget>();

            PropertyInfo[] propsSource = source.GetType().GetProperties();

            PropertyInfo[] propsTarget = target.GetType().GetProperties();

            foreach (PropertyInfo propSrc in propsSource)
            {
                PropertyInfo propTag = propsTarget.Where(
                    p => p.Name.Equals(propSrc.Name) &&
                    p.GetType().Name.Equals(propSrc.GetType().Name)
                ).FirstOrDefault();

                if (propTag == null)
                    continue;

                object propValueSrc = propSrc.GetValue(source);

                propTag.SetValue(target, propValueSrc);
            }

            return target;
        }

        public static IEnumerable<TTarget> MapList<TSource, TTarget>(IEnumerable<TSource> source)
        {
            return source.Select(x => Map<TSource, TTarget>(x));
        }
    }
}
