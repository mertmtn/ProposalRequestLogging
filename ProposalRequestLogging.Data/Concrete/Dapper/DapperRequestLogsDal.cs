using Dapper;
using ProposalRequestLogging.Data.Abstract;
using ProposalRequestLogging.Models.Concrete;
using System.Data;
using System.Linq;
namespace ProposalRequestLogging.Data.Concrete.Dapper
{
    public class DapperRequestLogsDal : IRequestLogsDal
    {
        public int Add(RequestLogs entity)
        {
            using (var connection = DbConnect.Connection)
            {
                var query = "spAddRequestLog"; 
                
                var parametreList = new DynamicParameters();
                parametreList.Add("@EndorsNo", dbType: DbType.Int32, value: entity.EndorsNo, direction: ParameterDirection.Input);
                parametreList.Add("@RenewalNo", dbType: DbType.Int32, value: entity.RenewalNo, direction: ParameterDirection.Input);
                parametreList.Add("@ProductNo", dbType: DbType.String, value: entity.ProductNo, direction: ParameterDirection.Input);
                parametreList.Add("@ProposalNo", dbType: DbType.Int64, value: entity.ProposalNo, direction: ParameterDirection.Input);
                parametreList.Add("@Response", dbType: DbType.String, value: entity.Response, direction: ParameterDirection.Input);
                parametreList.Add("@RequestLogId", dbType: DbType.Int32,  direction: ParameterDirection.Output);
                connection.Query<int>(query, parametreList, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return parametreList.Get<int>("RequestLogId");                
            }
        }

        public int UpdateResponseRequestLog(RequestLogs entity)
        {
            using (var connection = DbConnect.Connection)
            {
                var query = "spUpdateResponseRequestLog";
                var parametreList = new
                {
                    Response = entity.Response,
                    LogId= entity.LogId
                };
                return connection.Query<int>(query, parametreList, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }             
        }
    } 
}
