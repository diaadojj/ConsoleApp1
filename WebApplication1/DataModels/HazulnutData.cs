using Microsoft.ML.Data;

namespace WebApplication1.DataModels
{
    public class HazulnutData
    {
        [ColumnName("ImageSource"), LoadColumn(1)]
        public string ImageSource { get; set; }

        [ColumnName("Label"), LoadColumn(0)]
        public string Label { get; set; }

    }
}
