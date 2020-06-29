using Elmah.ContentSyndication;

namespace RestSampleNew.Controllers
{
    //[TypeConverter(typeof(FilterConverter))]
    public class Filter
    {
        public string GroupBy { get; set; } //groupby = value

        public bool SortedByAsc { get; set; } // sortedby = value
    }
}