using BD.DomainModel.DataTransferObjectManagers;
using DatabaseAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbCncStr = "Data Source=sql2.bds.hindawi.com,32000;Initial Catalog=BDS_20170220;Persist Security Info=True;User ID=sa;Password=BDS@2020";
            var table1 = new DataTable();

            using (var conn = new SqlConnection(dbCncStr))
            {
                conn.Open();

                var cmd1 = new SqlCommand("select * from TeamMembers", conn);
                //var cmd1 = new SqlCommand("select * from GeoGraphJournalsDistributionReportCache", conn);

                //var cmd1 = new SqlCommand("SET FMTONLY ON; select * from GeoGraphJournalsDistributionReportCache; SET FMTONLY OFF", conn);
                //var cmd1 = new SqlCommand("select * from GeoGraphJournalsDistributionReportCache", conn);

                //var cmd1 = new SqlCommand("exec proc_LondonCampaign_GetStatuses", conn);


                SqlDataReader reader = cmd1.ExecuteReader();


                var schemaTable = reader.GetSchemaTable();
                string schmDtoStr = DTOStringMaker.MakeDTOSring("SchemaTableDTO", schemaTable);
                var list_schemaTableDTOs = SchemaTableDTOMgr.CreateListFromDataTable(schemaTable);


                //var schmCols = reader.GetSchemaTable().Columns;
                //var rows = reader.GetSchemaTable().Rows;
                //var row0 = reader.GetSchemaTable().Rows[0];

                //SqlDbType type = (SqlDbType)(int)reader.GetSchemaTable().Rows[0]["ProviderType"];

                table1.Load(reader);


                var adapt1 = new SqlDataAdapter(cmd1);

                adapt1.Fill(table1);

                string dtoStr = DTOStringMaker.MakeDTOSring("TeamMemberDTO", table1);
                //string dtoStr = DTOStringMaker.MakeDTOSring("GeoGraphJournalsDistributionReportCacheDTO", table1);


                var list_TeamMembers = TeamMemberDTOMgr.CreateListFromDataTable(table1);
            }

        }
    }
}
