using System.Text;
using System.Threading.Tasks;
using ImageRecognizer.Web.Models;

namespace ImageRecognizer.Web.Services
{
    internal class RecognitionReporter : IRecognitionReporter
    {
        public Task<string> ReportAsync(OperationResultModel operationResult)
        {
            var stringBuilder = new StringBuilder();

            foreach (var recognitionResult in operationResult.RecognitionResultCollection)
            {
                foreach (var line in recognitionResult.LineCollection)
                {
                    stringBuilder.AppendLine(line.Text);
                }
            }

            return Task.FromResult( stringBuilder.ToString() );
        }
    }
}