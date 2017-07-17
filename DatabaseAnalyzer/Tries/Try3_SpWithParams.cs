using BD.DomainModel.DataTransferObjectManagers;
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
    public class Try3_SpWithParams
    {
        public static void Proceed()
        {

            //const string dbCncStr = "Data Source=sql2.bds.hindawi.com,32000;Initial Catalog=BDS_20170220;Persist Security Info=True;User ID=sa;Password=BDS@2020";
            const string dbCncStr = "Data Source=.;Initial Catalog=OnlineStoreDB;Integrated Security=True";

            var table1 = new DataTable();
            var table2 = new DataTable();
            var dataSet1 = new DataSet();

            using (var conn = new SqlConnection(dbCncStr))
            {
                conn.Open();

                //var cmd1 = new SqlCommand("select * from TeamMembers", conn);
                //var cmd1 = new SqlCommand("select * from GeoGraphJournalsDistributionReportCache", conn);

                //var cmd1 = new SqlCommand("SET FMTONLY ON; select * from GeoGraphJournalsDistributionReportCache; SET FMTONLY OFF", conn);
                //var cmd1 = new SqlCommand("select * from GeoGraphJournalsDistributionReportCache", conn);

                //var cmd1 = new SqlCommand("exec proc_LondonCampaign_GetStatuses", conn);

                //var cmd1 = new SqlCommand("select * from Products", conn);



                var cmd1 = new SqlCommand("sp_GetProductsThenUsers2", conn);
                //var cmd1 = new SqlCommand("exec sp_GetProductsThenUsers2 @MinId", conn);
                {
                    cmd1.CommandType = CommandType.StoredProcedure;

                    List<SqlParameter> parameters = new List<SqlParameter>();

                    //cmd1.Parameters.Add(new SqlParameter
                    parameters.Add(new SqlParameter
                    {
                        ParameterName = "@MinId",
                        SqlDbType = SqlDbType.Int,
                        Value = 2,
                    });

                    cmd1.Parameters.AddRange(parameters.ToArray());
                }


                SqlDataReader reader = cmd1.ExecuteReader();


                var schemaTable = reader.GetSchemaTable();
                string schmDtoStr = DTOStringMaker.MakeDTOSring("SchemaTableColDTO", schemaTable);
                var list_schemaTableDTOs = SchemaTableColDTOMgr.CreateListFromDataTable(schemaTable);


                table1.Load(reader);
                dataSet1.Tables.Add(table1);

                var schemaTable2 = reader.GetSchemaTable();
                //string schm2DtoStr = DTOStringMaker.MakeDTOSring("SchemaTableColDTO", schemaTable2);
                var list_schemaTable2DTOs = SchemaTableColDTOMgr.CreateListFromDataTable(schemaTable2);

                table2.Load(reader);
                //table2.Load(reader);
                //dataSet1.Load(reader,);

                dataSet1.Tables.Add(table2);

                //adapt1.Fill(table1);


                //string dtoStr = DTOStringMaker.MakeDTOSring("TeamMemberDTO", table1);
                //string dtoStr = DTOStringMaker.MakeDTOSring("GeoGraphJournalsDistributionReportCacheDTO", table1);
                string dtoStr = DTOStringMaker.MakeDTOSring("ProductDTO", table1);

                dtoStr = DTOStringMaker.MakeDTOSring("UserDTO", table2);

                //var list_TeamMembers = TeamMemberDTOMgr.CreateListFromDataTable(table1);
                var list_Products = ProductDTOMgr.CreateListFromDataTable(table1);
                var list_Users = UserDTOMgr.CreateListFromDataTable(table2);
            }



        }
    }
}
