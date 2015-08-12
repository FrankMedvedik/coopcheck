using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using CoopCheck.DAL;

namespace CoopCheck.DalADO
{
    /// <summary>
    /// DAL SQL Server implementation of <see cref="IOpenBatchListDal"/>
    /// </summary>
    public partial class OpenBatchListDal : IOpenBatchListDal
    {
        /// <summary>
        /// Loads a OpenBatchList list from the database.
        /// </summary>
        /// <returns>A list of <see cref="OpenBatchListItemDto"/>.</returns>
        public List<OpenBatchListItemDto> Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("CoopCheck"))
            {
                using (var cmd = new SqlCommand("dbo.dsa_GetOpenBatch", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var dr = cmd.ExecuteReader();
                    return LoadCollection(dr);
                }
            }
        }

        private List<OpenBatchListItemDto> LoadCollection(IDataReader data)
        {
            var openBatchList = new List<OpenBatchListItemDto>();
            using (var dr = new SafeDataReader(data))
            {
                while (dr.Read())
                {
                    openBatchList.Add(Fetch(dr));
                }
            }
            return openBatchList;
        }

        private OpenBatchListItemDto Fetch(SafeDataReader dr)
        {
            var openBatchListItem = new OpenBatchListItemDto();
            openBatchListItem.Num = dr.GetInt32("batch_num");
            openBatchListItem.Label = !dr.IsDBNull("batch_label") ? dr.GetString("batch_label") : null;

            return openBatchListItem;
        }
    }
}
