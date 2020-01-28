using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace TimeSheet.Utils
{
    public class ReflectionUtils
    {
        public static T CopyShallow<T>(object source)
        {
            T result = (T)Activator.CreateInstance(typeof(T));

            if (source == null) return result;

            foreach (System.Reflection.PropertyInfo sourcePropInfo in source.GetType().GetProperties())
            {
                if (!sourcePropInfo.CanRead) continue;

                System.Reflection.PropertyInfo resultPropInfo = result.GetType().GetProperty(sourcePropInfo.Name);

                if (resultPropInfo == null) continue;

                if (!resultPropInfo.CanWrite) continue;

                resultPropInfo.SetValue(result, sourcePropInfo.GetValue(source, null), null);
            }
            return result;
        }

        public static T[] CopyShallowArray<T>(object[] sourceArray)
        {
            T[] resultArray = new T[sourceArray.Length];
            for (int i = 0; i < sourceArray.Length; ++i)
            {
                resultArray[i] = CopyShallow<T>(sourceArray[i]);
            }
            return resultArray;
        }
    }
}