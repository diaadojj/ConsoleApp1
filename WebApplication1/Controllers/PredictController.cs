using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using WebApplication1.DataModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        public static IWebHostEnvironment _environment;


        private readonly PredictionEnginePool<HazulnutData, HazulnutPrediction> _predictionEnginePool;

        public PredictController(PredictionEnginePool<HazulnutData, HazulnutPrediction> predictionEnginePool
            )
        {
            _predictionEnginePool = predictionEnginePool;
            //_environment = environment;
        }


        [HttpPost]
        public ActionResult<string> Post([FromBody] ConsoleApp1.MLModel1.ModelInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ConsoleApp1.MLModel1.ModelInput hazulnutData = new ConsoleApp1.MLModel1.ModelInput
            {
                ImageSource = input.ImageSource,
                //    Label = input.Label

            };


            //Console.WriteLine("image::" + input.Label);

            // HazulnutPrediction prediction = _predictionEnginePool.Predict(modelName: "MLModel",example: input);

            ////string sentiment = Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative";
            ///

            var predictionResult = ConsoleApp1.MLModel1.Predict(input);

            // string Predict = prediction.Prediction;
            // float []score = prediction.Score;
            //foreach(float i in score)
            //{
            //    return Ok(Convert.ToString(i));

            //}
            string pre = predictionResult.Prediction;

            return Ok(pre);
        }
    }
}

