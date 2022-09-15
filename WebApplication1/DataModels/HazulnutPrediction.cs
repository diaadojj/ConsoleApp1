using Microsoft.ML.Data;
using System;

namespace WebApplication1.DataModels
{
    public class HazulnutPrediction
    {

        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }
        public float[] Score { get; set; }

    }
}
