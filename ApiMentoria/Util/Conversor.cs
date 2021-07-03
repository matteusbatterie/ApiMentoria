using System;
using System.Collections.Generic;
using System.Reflection;

namespace ApiMentoria.Util
{
    public static class Conversor
    {
        public static TOut Convert<TOut, TIn>(this TIn model)
            where TIn : class
        {
            TOut instance = (TOut)Activator.CreateInstance(typeof(TOut));
            foreach (PropertyInfo propTOut in instance.GetType().GetProperties())
            {
                PropertyInfo propTIn = model.GetType().GetProperty(propTOut.Name);
                if (propTIn != null)
                    propTOut.SetValue(instance, propTIn.GetValue(model, null));
            }

            return instance;
        }

        public static List<TOut> ConvertList<TOut, TIn>(this List<TIn> list)
            where TOut : class
            where TIn : class
        {
            List<TOut> convertedList = new List<TOut>();
            foreach (var item in list)
            {
                convertedList.Add(item.Convert<TOut, TIn>());
            }

            return convertedList;
        }
    }
}
