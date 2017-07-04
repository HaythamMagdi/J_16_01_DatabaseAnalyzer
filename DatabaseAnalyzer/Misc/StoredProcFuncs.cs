using DatabaseAnalyzer.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer.Misc
{
    public class StoredProcFuncs
    {









        public static void sp_GetProductsThenUsers3(int minId, string anyStr, out long someLong, SqlConnection conn)
        {
            var minIdParameter = new SqlParameter("@MinId", SqlDbType.Int) { Value = (object)minId ?? DBNull.Value, };
            var anyStrParameter = new SqlParameter("@AnyStr", SqlDbType.NVarChar) { Value = (object)anyStr ?? DBNull.Value, };
            var someLongParameter = new SqlParameter("@SomeLong", SqlDbType.BigInt) { Direction = System.Data.ParameterDirection.Output, };


            //((IBDMainUnitOfWork)UnitOfWork).ExecuteCommand("EXEC sp_GetProductsThenUsers3 @MinId, @AnyStr, out @SomeLong", minIdParameter, anyStrParameter, someLongParameter);

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(minIdParameter);
            parameters.Add(anyStrParameter);
            parameters.Add(someLongParameter);


            var cmd1 = new SqlCommand("sp_GetProductsThenUsers3", conn);
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddRange(parameters.ToArray());
            }

            var list_TableInfos = DbHelper.ExecuteCommand(cmd1);


            someLong = (long)someLongParameter.Value;


            var list_rowDTOStrs = new List<string>();

            for (int i = 0; i < list_TableInfos.Count; i++)
            {
                list_rowDTOStrs.Add(DTOStringMaker.MakeDTOSring("RowDTO_<^I^>".Replace("<^I^>", i.ToString()), list_TableInfos[i].Table));
            }

        }

                
                



        public static void proc_GetChinesePercentageForBD(string isEiCInsteadEditor, string year, out int referenceId, SqlConnection conn)
        {

            List<SqlParameter> parameters = new List<SqlParameter>();

            var isEiCInsteadEditorParameter = new SqlParameter("@IsEiCInsteadEditor", SqlDbType.VarChar) { Value = (object)isEiCInsteadEditor ?? DBNull.Value, };
            parameters.Add(isEiCInsteadEditorParameter);

            var yearParameter = new SqlParameter("@Year", SqlDbType.VarChar) { Value = (object)year ?? DBNull.Value, };
            parameters.Add(yearParameter);

            var referenceIdParameter = new SqlParameter("@ReferenceId", SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output, };
            parameters.Add(referenceIdParameter);



            var cmd1 = new SqlCommand("proc_GetChinesePercentageForBD", conn);
            {
                cmd1.CommandTimeout = 3600000;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddRange(parameters.ToArray());
            }

            var list_TableInfos = DbHelper.ExecuteCommand(cmd1);

            referenceId = (int)referenceIdParameter.Value;

            string dtoStr = DTOStringMaker.MakeDTOSring("GeoGraphJournalsDistribDTO", list_TableInfos[0].Table);

        }




    }
}
