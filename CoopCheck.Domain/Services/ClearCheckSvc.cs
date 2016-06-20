using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoopCheck.DAL;
using CoopCheck.Library;


namespace CoopCheck.Domain.Services
{
    public  class ClearCheckSvc
    {
        private readonly IDbConnection _db;
        public ClearCheckSvc(IDbConnection db)
        {
            _db = db;
        }
        public  void ClearChecks(List<CheckInfoDto> checks)
        {
                var bulkCopy = new SqlBulkCopy((SqlConnection)(_db));
                bulkCopy.DestinationTableName = "clear_check_work";
                bulkCopy.ColumnMappings.Add("Id", "tran_id");
                bulkCopy.ColumnMappings.Add("ClearedDate", "cleared_date");
                bulkCopy.ColumnMappings.Add("ClearedAmount", "cleared_amount");
                _db.Open();
                using (var dataReader = new ObjectDataReader<CheckInfoDto>(checks))
                {
                    bulkCopy.WriteToServer(dataReader);
                }
            ClearCheckBatchCommand.Execute();
        }
            
        }
    }

