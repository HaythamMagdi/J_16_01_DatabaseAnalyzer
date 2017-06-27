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
    public class Try5
    {
        public static void Proceed()
        {

            //const string dbCncStr = "Data Source=sql2.bds.hindawi.com,32000;Initial Catalog=BDS_20170220;Persist Security Info=True;User ID=sa;Password=BDS@2020";
            const string dbCncStr = "Data Source=.;Initial Catalog=OnlineStoreDB;Integrated Security=True";

            var dataSet1 = new DataSet();

            using (var conn = new SqlConnection(dbCncStr))
            {
                conn.Open();




                //var cmd1 = new SqlCommand("select * from Products", conn);
                //var cmd1 = new SqlCommand("exec sp_GetProductsThenUsers", conn);
                var cmd1 = new SqlCommand("exec sp_GetProductsThenUsers2 2", conn);
                //var cmd1 = new SqlCommand("exec sp_GetProductsThenUsers3", conn);


                var list_TableInfos = DbHelper.ExecuteCommand(cmd1);

                string dtoStr = DTOStringMaker.MakeDTOSring("ProductDTO", list_TableInfos[0].Table);

                //dtoStr = DTOStringMaker.MakeDTOSring("UserDTO", table2);

                //var list_TeamMembers = TeamMemberDTOMgr.CreateListFromDataTable(table1);
                var list_Products = ProductDTOMgr.CreateListFromDataTable(list_TableInfos[0].Table);



                var list_StoredProcParamDTOs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3");

                string spRepoFuncStr = Misc.GetSpRepoFuncString(conn, "sp_GetProductsThenUsers3");



            }



        }
    }
}
