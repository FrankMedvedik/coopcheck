using System;
using System.Collections.Generic;
using DataClean.Contracts.Interfaces;
using DataClean.Models;
using DataClean.Repository.Contracts.Interfaces;
using log4net;
using Newtonsoft.Json;

namespace DataClean.Repository.Mgr
{
   public class DataCleanRespository : IDataCleanRepository
    {
        private DataCleanEntities _ctx;
        private static readonly ILog Logger =
            LogManager.GetLogger(typeof(DataCleanRespository));

        public DataCleanRespository()
        {
            _ctx = new DataCleanEntities();
        }
        public IDataCleanEvent GetEvent(int id)
        {

            // get archive from database if exists if not return null
            // handle json to object conversion
            var row = _ctx.DataCleanEventLogs.FindAsync(id).Result;
            if (row == null) return null;
            var logEntry = JsonConvert.DeserializeObject<DataCleanEvent>(row.DataCleanEventJson);
            return logEntry;
        }

        // save the results of the dataclean operation to the database 
        // convert the event and its related addresses and parse  results to json and store em 
        // if a datacleanevent exists the contents are updated 
        public void SaveEvent(IDataCleanEvent e)
        {
            try
            {
                if (e == null) throw new Exception("null DataClean event cant be saved ");
                if (e.DataCleanDate == DateTime.MinValue) throw new Exception("Bad DataClean date - only clean events can be saved");
                if (e.Input == null) throw new Exception("Bad DataClean input - input address needed");
                if (e.Output == null) throw new Exception("Bad DataClean output - dataclean output needed for event to be saved");
                if (e.Output.Results== null) throw new Exception("Bad DataClean output results - dataclean output must contain results to be saved. was DataClener run on the event? ");
                if (e.Output.ID != e.Input.ID) throw new Exception("Bad address match - dataclean output id must match input id");

                var a = new DataCleanEventLog()
                {
                    ID = e.ID,
                    DataCleanDate = e.DataCleanDate,
                    DataCleanEventJson = JsonConvert.SerializeObject(e)
                };

                var v = _ctx.DataCleanEventLogs.Find(a.ID);
                if (v != null)
                {
                    v.DataCleanDate = a.DataCleanDate;
                    v.DataCleanEventJson = a.DataCleanEventJson;
                }
                else
                {
                    _ctx.DataCleanEventLogs.Add(a);
                }
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                throw;
            }

            
        }

        public List<IParseResult> GetMelissaReference()
        {

            var r = new List<IParseResult>();
                foreach (var p in _ctx.MelissaResultReferences)
                {
                    r.Add(new ParseResult()
                    {
                        Code = p.Code.Trim(),
                        AlternateAddressExists = p.AlternateAddressExists.GetValueOrDefault(false),
                        LongDescription = p.LongDescription,
                        ShortDescription = p.ShortDescription,
                        Type = p.Type
                    });
                }
            return r;
        }

       public void SaveStats(IDataCleanStat d)
       {
           _ctx.DataCleanStats.Add((DataCleanStat) d);
           _ctx.SaveChanges();
       }

    }
}
