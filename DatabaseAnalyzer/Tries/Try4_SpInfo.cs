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
    public class Try4_SpInfo
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


                string cmdText = @"


                    select  
                       'Parameter_name' = name,  
                       'Type'   = type_name(user_type_id),  
                       'Length'   = max_length,  
                       'Prec'   = case when type_name(system_type_id) = 'uniqueidentifier' 
                                  then precision  
                                  else OdbcPrec(system_type_id, max_length, precision) end,  
                       'Scale'   = OdbcScale(system_type_id, scale),  
                       'Param_order'  = parameter_id,  
                       'Collation'   = convert(sysname, 
                                       case when system_type_id in (35, 99, 167, 175, 231, 239)  
                                       then ServerProperty('collation') end),
	                   'IsOutput' = is_output    

                      from sys.parameters where object_id = object_id('<^PROC_NAME^>')
                ";
                cmdText = cmdText.Replace("<^PROC_NAME^>", "sp_GetProductsThenUsers2");


                var cmd1 = new SqlCommand(cmdText, conn);

                //var cmd1 = new SqlCommand("sp_GetProductsThenUsers2", conn);
                //{
                //    cmd1.CommandType = CommandType.StoredProcedure;

                //    cmd1.Parameters.Add(new SqlParameter
                //    {
                //        ParameterName = "@MinId",
                //        SqlDbType = SqlDbType.Int,
                //        Value = 2,
                //    });
                //}

                var adapt1 = new SqlDataAdapter(cmd1);

                //adapt1.get



                SqlDataReader reader = cmd1.ExecuteReader();


                var schemaTable = reader.GetSchemaTable();
                //string schmDtoStr = DTOStringMaker.MakeDTOSring("SchemaTableDTO", schemaTable);
                var list_schemaTableDTOs = SchemaTableDTOMgr.CreateListFromDataTable(schemaTable);

                table1.Load(reader);
                dataSet1.Tables.Add(table1);
                string dtoStr = DTOStringMaker.MakeDTOSring("StoredProcParamDTO", table1);
                var list_StoredProcParams = StoredProcParamDTOMgr.CreateListFromDataTable(table1);


            }



        }
    }
}
