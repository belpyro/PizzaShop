using System;
using System.ComponentModel;
using System.Globalization;

namespace RestSampleNew.Controllers
{
    public class FilterConverter :TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string)
            {
                var data = value.ToString();
                var items = data.Split(',');
                if(items.Length != 2)
                {
                    return base.ConvertFrom(context, culture, value);
                }

                return new Filter
                {
                    GroupBy = items[0],
                    SortedByAsc = bool.Parse(items[1])
                };
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}