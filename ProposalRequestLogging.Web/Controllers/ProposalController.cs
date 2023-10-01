using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProposalRequestLogging.Business.Abstract;
using ProposalRequestLogging.Models.Concrete;
using ProposalRequestLogging.Web.Models.ViewModels;
namespace ProposalRequestLogging.Web.Controllers
{
    public class ProposalController : Controller
    {
        private IRequestLogService _requestLogService;
        private IConfiguration _configuration;
        public ProposalController(IRequestLogService requestLogService, IConfiguration configuration)
        {
            _configuration = configuration;
            _requestLogService = requestLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ProposalRequest request)
        {
            var resultViewModel = new ResultViewModel();
            try
            {
                request.Authentication.Key = _configuration.GetValue<string>("ApiInformations:Authorization:Key");
                request.Authentication.Source = _configuration.GetValue<string>("ApiInformations:Authorization:Source");

                var resultList = _requestLogService.AddRequestLog(request).GetAwaiter().GetResult();

                if (resultList.Results != null)
                {
                    foreach (var result in resultList.Results)
                    {
                        if (result.Status.Value == ((int)(Models.Status.Olumlu)).ToString()) resultViewModel.PositiveResultList.Add(new Models.ViewModels.Result { Code = result.Code, Description = result.Description });
                        else if (result.Status.Value == ((int)(Models.Status.Olumsuz)).ToString()) resultViewModel.NegativeResultList.Add(new Models.ViewModels.Result { Code = result.Code, Description = result.Description });
                        else if (result.Status.Value == ((int)(Models.Status.Bilgi)).ToString()) resultViewModel.InformativeResultList.Add(new Models.ViewModels.Result { Code = result.Code, Description = result.Description });
                    }
                    resultViewModel.IsSuccess = true;
                    return Json(resultViewModel);
                }
                resultViewModel.ErrorMessage = "Sorgu herhangi bir sonuç döndürmedi.";
            }
            catch
            {
                //TODO: Exception loglama sistemi yapılacak.
                resultViewModel.ErrorMessage = "İşlem sırasında hata oluştu!";
            }

            return Json(resultViewModel);
        }
    }
}
