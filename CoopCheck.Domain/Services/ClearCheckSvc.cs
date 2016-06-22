using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoopCheck.DAL;
using CoopCheck.Library;
using CoopCheck.Repository;


namespace CoopCheck.Domain.Services
{
    public  class ClearCheckSvc
    {
        private IDbConnection _db;
        public IDbConnection DbConnection
        {
            get
            {
                if(_db == null)
                    _db = new DBConnection();
                return _db;
            }
            set
            {
                _db = value;

            }
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

