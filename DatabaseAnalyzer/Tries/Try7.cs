using BD.DomainModel.DataTransferObjectManagers;
using DatabaseAnalyzer.Misc;
using DatabaseAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer.Tries
{
    public class Try7
    {
        public static void Proceed()
        {

            //const string dbCncStr = "Data Source=sql2.bds.hindawi.com,32000;Initial Catalog=BDS_20170220;Persist Security Info=True;User ID=sa;Password=BDS@2020";
            //const string dbCncStr = "Data Source=beta.dbs.hindawi.com,32000;Initial Catalog=mtsv2_20161115;Persist Security Info=True;User ID=MTSDev;Password=G@nd--e";
            const string dbCncStr = "Data Source=.;Initial Catalog=OnlineStoreDB;Integrated Security=True";

            //var dataSet1 = new DataSet();

            using (var conn = new SqlConnection(dbCncStr))
            {
                conn.Open();

                string spRepoFuncStr = Util.MiscUtil.GetSpRepoFuncString(conn, "proc_GetGeoGraphJournalsDistributionReportCache");

                StoredProcFuncs.proc_GetGeoGraphJournalsDistributionReportCache(conn);
            }



        }
    }
}
