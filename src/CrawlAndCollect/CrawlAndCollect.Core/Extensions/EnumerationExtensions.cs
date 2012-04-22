using System;
using System.ComponentModel;

namespace CrawlAndCollect.Core.Extensions {
    public static class EnumerationExtensions {

        public static string GetDescription(this Enum instance)
        {
            var type = instance.GetType();

            var memInfo = type.GetMember(instance.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return instance.ToString();
        }
    }
}