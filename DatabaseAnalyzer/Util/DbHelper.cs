using BD.DomainModel.DataTransferObjectManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalyzer.Util
{
    public class DbHelper
    {
        public static List<TableInfo> ExecuteCommand(SqlCommand cmd)
        {
            SqlDataReader reader = cmd.ExecuteReader();
            
            var list_TableInfos = new List<TableInfo>();

            while (!reader.IsClosed && reader.HasRows)
            {
                var tableInfo = new TableInfo
                {
                    List_SchemaTableDTOs = SchemaTableDTOMgr.CreateListFromDataTable(reader.GetSchemaTable()),
                    Table = new DataTable(),
                };
                tableInfo.Table.Load(reader);

                list_TableInfos.Add(tableInfo);
            }

            return list_TableInfos;
        }

        public static List<StoredProcParamDTO> GetSpParameterDTOs(SqlConnection conn, string spName)
        {


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
            cmdText = cmdText.Replace("<^PROC_NAME^>", spName);
            
            var cmd1 = new SqlCommand(cmdText, conn);

            var list_TableInfos = DbHelper.ExecuteCommand(cmd1);

            var list_StoredProcParamDTOs = StoredProcParamDTOMgr.CreateListFromDataTable(list_TableInfos[0].Table);

            return list_StoredProcParamDTOs;
        }

        public static string GetClrTypeFromSqlType(string sqlType)
        {
            switch(sqlType.ToLower())
            {
                case "bigint":
                    return "long";

                case "binary":
                case "image":
                case "timestamp":
                case "varbinary":
                    return "byte[]";

                case "bit":
                    return "bool";

                case "char":
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    return "string";

                case "datetime":
                case "smalldatetime":
                case "date":
                case "Time":
                case "DateTime2":
                    return "DateTime";

                case "Decimal":
                case "Money":
                case "SmallMoney":
                    return "decimal";

                case "Float":
                    return "double";

                case "Int":
                    return "int";

                case "Real":
                    return "float";

                case "UniqueIdentifier":
                    return "Guid";

                case "SmallInt":
                    return "short";

                case "TinyInt":
                    return "byte";


                //case "int":
                //    return "int";

                //case "bigint":
                //    return "long";

                //case "nvarchar":
                //case "varchar":
                //    return "long";

                //case "bit":
                //    return "bool";

                //case "datetime":
                //    return "DateTime";
                    





                default:
                    throw new InvalidOperationException();
            }

        }


    }
}
