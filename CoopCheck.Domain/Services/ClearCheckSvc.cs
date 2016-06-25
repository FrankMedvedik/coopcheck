using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using CoopCheck.DAL;
using CoopCheck.Library;
using CoopCheck.Repository;


namespace CoopCheck.Domain.Services
{
    public  class ClearCheckSvc
    {
        private ICoopCheckEntities _ctx;
        private IDbConnection _conn;
        public ICoopCheckEntities  DbConnection
        {
            get
            {
                if (_ctx == null)
                {
                    _ctx = new CoopCheckEntities();
                    _conn = ((EntityConnection) ((IObjectContextAdapter) _ctx).ObjectContext.Connection).StoreConnection;
                }
                return _ctx;
            }
            set
            {
                _ctx = value;

            }
        }

        public  void ClearChecks(List<CheckInfoDto> checks)
        {
                var bulkCopy = new SqlBulkCopy((SqlConnection) _conn);
                bulkCopy.DestinationTableName = "clear_check_work";
                bulkCopy.ColumnMappings.Add("Id", "tran_id");
                bulkCopy.ColumnMappings.Add("ClearedDate", "cleared_date");
                bulkCopy.ColumnMappings.Add("ClearedAmount", "cleared_amount");
                 _conn.Open();
                using (var dataReader = new ObjectDataReader<CheckInfoDto>(checks))
                {
                    bulkCopy.WriteToServer(dataReader);
                }
            ClearCheckBatchCommand.Execute();
        }
            
        }
    }

