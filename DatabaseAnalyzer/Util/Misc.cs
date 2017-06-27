using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer.Util
{
    public class Misc
    {
        class StoredProcParamEx : StoredProcParamDTO
        {
            public StoredProcParamEx() { }
            public StoredProcParamEx(StoredProcParamDTO src) 
            { 
                this.Parameter_name = src.Parameter_name;
                this.Type = src.Type;
                this.Length = src.Length;
                this.Prec = src.Prec;
                this.Scale = src.Scale;
                this.Param_order = src.Param_order;
                this.Collation = src.Collation;
                this.IsOutput = src.IsOutput;                
            }
            public string ClrName { get; set; }
        }

        public static string GetSpRepoFuncString(SqlConnection conn, string spName)
        {
            //var list_StoredProcParamDTOs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3");
            var list_StoredProcParamExs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3").Select(x => new StoredProcParamEx(x)).ToList();

            list_StoredProcParamExs.ForEach(x =>
            {
                x.ClrName = DbHelper.GetClrTypeFromSqlType(x.Type);

            });

            throw new NotImplementedException();
        }


    }
}
