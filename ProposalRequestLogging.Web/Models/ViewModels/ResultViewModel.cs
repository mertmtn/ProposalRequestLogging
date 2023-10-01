 
using System.Collections.Generic;

namespace ProposalRequestLogging.Web.Models.ViewModels
{
    public class ResultViewModel
    {
        public ResultViewModel()
        {
            PositiveResultList = new List<Result>();
            NegativeResultList = new List<Result>();
            InformativeResultList = new List<Result>();
        }

        public List<Result> PositiveResultList { get; set; }
        public List<Result> NegativeResultList { get; set; }
        public List<Result> InformativeResultList { get; set; }

        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class Result
    {        
        public string Code { get; set; }       
        public string Description { get; set; } 
    }


}
