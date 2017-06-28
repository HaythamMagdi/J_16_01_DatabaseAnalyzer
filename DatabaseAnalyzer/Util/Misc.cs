using System;
using System.Collections.Generic;
using System.Data;
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
            public string ClrTypeName { get; set; }
            public SqlDbType SqlDbType { get; set; }
            public string CamelParamName { get; set; }

            
        }

        public static string RemoveStartAt(string input)
        {
            if (input.Substring(0, 1) != "@")
                return input;

            //var input = "hello";
            //var output = input.Substring(1, input.Length - 1) + input.Substring(0, 1);
            var output = input.Substring(1, input.Length - 1);

            return output;
        }

        public static string ConvertToCamelName(string name)
        {
            var chars = name.ToCharArray();

            if (Char.IsUpper(chars[0]) && chars.Length >= 2 && Char.IsLower(chars[1]))
                chars[0] = Char.ToLower(chars[0]);

            return new String(chars);
            //throw new NotImplementedException();
        }

        public static string GetSpRepoFuncString(SqlConnection conn, string spName)
        {
            //var list_StoredProcParamDTOs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3");
            var list_StoredProcParamExs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3").Select(x => new StoredProcParamEx(x)).ToList();

            list_StoredProcParamExs.ForEach(x =>
            {
                x.ClrTypeName = DbHelper.ConvertToClrType(x.Type);
                x.SqlDbType = DbHelper.ConvertToSqlDbType(x.Type);
                x.CamelParamName = Misc.ConvertToCamelName(Misc.RemoveStartAt(x.Parameter_name));
            });


            string template =

                @"

    <^ReturnType^> <^SpName^>(<^FuncArguments^>)
    {

        <^SqlParamDefs^>



    }

                ";

            string funcStr = template.Replace("<^SpName^>", spName);


            string sqlParamDefStr = "";
            {
                foreach(var param in list_StoredProcParamExs)
                {
                    if(param.IsOutput)
                    {
                        string paramTemplate = @"

                        ";

                    }
                    else
                    {
                        string paramTemplate = @"
                            var <^camelName^>Parameter = new SqlParameter(""<^^>"", SqlDbType.DateTime) { Value = (object)from ?? DBNull.Value, };

                        ";

                    }
                }

            }


            throw new NotImplementedException();
        }


    }
}
