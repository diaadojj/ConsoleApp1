using Newtonsoft.Json;
using System;
using RestSharp;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static ConsoleApp1.MLModel1;
using System.Net;
using Google.Protobuf;
using System.IO;
using System.Threading.Tasks;
using System.Security.Policy;
using ServiceStack;
using ServiceStack.Web;
using System.Drawing.Design;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Xamarin.Forms.PlatformConfiguration;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string filePath;

        private HttpClient client = new HttpClient();
        public Form1()
        {

            client.BaseAddress = new Uri("http://localhost:44302/api/predict");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }



       





        [Obsolete]
        private async void button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select an image!", "Image Selection Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Create single instance of sample data from first line of dataset for model input
                ModelInput sampleData = new ModelInput()
                {
                    ImageSource = filePath,
                };

                // Make a single prediction on the sample data and print results
                var predictionResult = ConsoleApp1.MLModel1.Predict(sampleData);

                var stringBuilder = new StringBuilder();

                var tt = predictionResult.ToString();

              
                stringBuilder.AppendLine("Hazelnut type is:");
                stringBuilder.AppendLine(predictionResult.Prediction);
                predictTxt.Text = stringBuilder.ToString();

                var client = new HttpClient();


             


                using (var multipartFormContent = new MultipartFormDataContent())
                {
                    //Add other fields
                    multipartFormContent.Add(new StringContent(predictionResult.Prediction), name: "type");
                    

                    //Add the file
                    var fileStreamContent = new StreamContent(File.OpenRead(filePath));
                    fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                    multipartFormContent.Add(fileStreamContent, name: "files", fileName: filePath);

                    //Send it
                    var response = await client.PostAsync("http://localhost:7655/api/image/", multipartFormContent);
                    response.EnsureSuccessStatusCode();
                    var re =  await response.Content.ReadAsStringAsync();   

                    resp.Text = re.ToString();
                }



         







            }

       

        }



        private void btnSelect_Click(object sender, EventArgs e)
        {

            predictTxt.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.JPEG;*.JPG;*.PNG)|*.JPEG;*.JPG;*.PNG|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    picBox.ImageLocation = filePath;
                    txtUrl.Text = new FileInfo(filePath).Name;
                }
            }



          

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void predictTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
