using System.Threading.Tasks;
using ImageRecognizer.Web.Models;

namespace ImageRecognizer.Web.Services
{
    public interface IRecognitionReporter
    {
        Task<string> ReportAsync(OperationResultModel operationResult);
    }
}