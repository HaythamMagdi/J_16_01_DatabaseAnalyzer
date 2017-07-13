using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer.Util
{
    public class MiscUtil
    {
        class StoredProcParamEx : StoredProcParamDTO
        {
            public StoredProcParamEx() { }
            public StoredProcParamEx(StoredProcParamDTO src) 
            { 
                this.ParameterName = src.ParameterName;
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
            public string ParamCamelName { get; set; }

            
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
            //var list_StoredProcParamExs = DbHelper.GetSpParameterDTOs(conn, "sp_GetProductsThenUsers3").Select(x => new StoredProcParamEx(x)).ToList();
            var list_StoredProcParamExs = DbHelper.GetSpParameterDTOs(conn, spName).Select(x => new StoredProcParamEx(x)).ToList();

            list_StoredProcParamExs.ForEach(x =>
            {
                x.ClrTypeName = DbHelper.ConvertToClrType(x.Type);
                x.SqlDbType = DbHelper.ConvertToSqlDbType(x.Type);
                x.ParamCamelName = MiscUtil.ConvertToCamelName(MiscUtil.RemoveStartAt(x.ParameterName));
            });


            string template =

                @"

    public static void <^SpName^>(<^FuncArguments^>, SqlConnection conn)
    {
        <^SqlParamDefs^>
        
            //((IBDMainUnitOfWork)UnitOfWork).ExecuteCommand(""EXEC <^SpName^> <^InlineParamNames^>"", <^InlineParamVars^>);

        <^SqlParamList^>

                var cmd1 = new SqlCommand(""<^SpName^>"", conn);
                {
                    cmd1.CommandTimeout = 3600000;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddRange(parameters.ToArray());
                }

            var list_TableInfos = DbHelper.ExecuteCommand(cmd1);

        <^AssignOutParams^>        

        <^RowDTOStrs^>
    }

                ";

            string funcStr = template.Replace("<^SpName^>", spName);

            {
                string funcArgStr = "";
                string paramTemplate_Out = @"out <^ClrTypeName^> <^ParamCamelName^>";
                string paramTemplate_In = @"<^ClrTypeName^> <^ParamCamelName^>";

                foreach (var param in list_StoredProcParamExs)
                {
                    string paramTemplate = null;
                    if (param.IsOutput)
                    {
                        paramTemplate = paramTemplate_Out;
                    }
                    else
                    {
                        paramTemplate = paramTemplate_In;
                    }

                    string paramStr = paramTemplate;

                    paramStr = paramStr.Replace("<^ClrTypeName^>", param.ClrTypeName);
                    paramStr = paramStr.Replace("<^ParamCamelName^>", param.ParamCamelName);

                    funcArgStr += paramStr + ", ";
                }
                funcArgStr = funcArgStr.TrimEnd().TrimEnd(',');

                funcStr = funcStr.Replace("<^FuncArguments^>", funcArgStr);
            }

            {
                string sqlParamDefStr = "";

                foreach(var param in list_StoredProcParamExs)
                {
                    string paramTemplate = null;
                    if(param.IsOutput)
                    {
                        paramTemplate = @"var <^ParamCamelName^>Parameter = new SqlParameter(""<^ParamName^>"", SqlDbType.<^SqlDbTypeName^>) { Direction = System.Data.ParameterDirection.Output, };";
                    }
                    else
                    {
                        paramTemplate = @"var <^ParamCamelName^>Parameter = new SqlParameter(""<^ParamName^>"", SqlDbType.<^SqlDbTypeName^>) { Value = (object)<^ParamCamelName^> ?? DBNull.Value, };";
                    }

                    string paramStr = paramTemplate;

                    paramStr = paramStr.Replace("<^ParamCamelName^>", param.ParamCamelName);
                    paramStr = paramStr.Replace("<^ParamName^>", param.ParameterName);
                    paramStr = paramStr.Replace("<^SqlDbTypeName^>", param.SqlDbType.ToString());

                    sqlParamDefStr += paramStr + "\r\n";
                }

                funcStr = funcStr.Replace("<^SqlParamDefs^>", sqlParamDefStr);
            }

            {
                string inlineParamNameStr = "";
                string paramTemplate = @"<^ParameterName^>";

                bool isFirst = true;
                foreach (var param in list_StoredProcParamExs)
                {
                    string paramStr = (isFirst ? "" : ", ") + (param.IsOutput ? "out " : "") + paramTemplate;

                    paramStr = paramStr.Replace("<^ParameterName^>", param.ParameterName);

                    inlineParamNameStr += paramStr;

                    isFirst = false;
                }

                funcStr = funcStr.Replace("<^InlineParamNames^>", inlineParamNameStr);
            }

            {
                string inlineParamVarStr = "";
                string paramTemplate = @"<^ParamCamelName^>Parameter";

                bool isFirst = true;
                foreach (var param in list_StoredProcParamExs)
                {
                    string paramStr = (isFirst ? "" : ", ") + paramTemplate;

                    paramStr = paramStr.Replace("<^ParamCamelName^>", param.ParamCamelName);

                    inlineParamVarStr += paramStr;

                    isFirst = false;
                }

                funcStr = funcStr.Replace("<^InlineParamVars^>", inlineParamVarStr);
            }

            {
                string sqlParamListStr = "";
                sqlParamListStr += "List<SqlParameter> parameters = new List<SqlParameter>();" + "\r\n\r\n";

                foreach (var param in list_StoredProcParamExs)
                {
                    string paramStr = "parameters.Add(<^ParamCamelName^>Parameter);";

                    paramStr = paramStr.Replace("<^ParamCamelName^>", param.ParamCamelName);

                    sqlParamListStr += paramStr + "\r\n";
                }

                funcStr = funcStr.Replace("<^SqlParamList^>", sqlParamListStr);
            }

            {
                string assignOutParamStr = "";
                string paramTemplate = @"<^ParamCamelName^> = (<^ParamClrTypeName^>)<^ParamCamelName^>Parameter.Value;";

                foreach (var param in list_StoredProcParamExs.Where(x => x.IsOutput))
                {
                    string paramStr = paramTemplate;

                    paramStr = paramStr.Replace("<^ParamCamelName^>", param.ParamCamelName);
                    paramStr = paramStr.Replace("<^ParamClrTypeName^>", param.ClrTypeName);

                    assignOutParamStr += paramStr + "\r\n";
                }

                funcStr = funcStr.Replace("<^AssignOutParams^>", assignOutParamStr);
            }

            {
                string rowDTOStrs = 
@"
    var list_rowDTOStrs = new List<string>();

    for(int i=0; i < list_TableInfos.Count; i++)
    {
        list_rowDTOStrs.Add(DTOStringMaker.MakeDTOSring(""RowDTO_<^I^>"".Replace(""<^I^>"", i.ToString()), list_TableInfos[i].Table));
    }
";
                funcStr = funcStr.Replace("<^RowDTOStrs^>", rowDTOStrs);
            }

            return funcStr;
        }


    }
}
