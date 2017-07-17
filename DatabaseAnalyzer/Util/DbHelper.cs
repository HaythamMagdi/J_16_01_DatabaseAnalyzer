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
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                var list_TableInfos = new List<TableInfo>();

                if (!reader.IsClosed && !reader.HasRows)
                {
                    var schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                    {
                        var tableInfo = new TableInfo
                        {
                            List_SchemaTableColDTOs = SchemaTableColDTOMgr.CreateListFromDataTable(schemaTable),
                            Table = new DataTable(),
                        };
                        tableInfo.Table.Load(reader);

                        list_TableInfos.Add(tableInfo);
                    }
                }

                while (!reader.IsClosed && reader.HasRows)
                {
                    var tableInfo = new TableInfo
                    {
                        List_SchemaTableColDTOs = SchemaTableColDTOMgr.CreateListFromDataTable(reader.GetSchemaTable()),
                        Table = new DataTable(),
                    };
                    tableInfo.Table.Load(reader);

                    list_TableInfos.Add(tableInfo);
                }

                return list_TableInfos;
            }
        }

        public static List<StoredProcParamDTO> GetSpParameterDTOs(SqlConnection conn, string spName)
        {


            string cmdText = @"

                    select  
                       'ParameterName' = name,  
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

            List<StoredProcParamDTO> list_StoredProcParamDTOs = new List<StoredProcParamDTO>();

            if (list_TableInfos.Any())
            {
                list_StoredProcParamDTOs = StoredProcParamDTOMgr.CreateListFromDataTable(list_TableInfos[0].Table);
            }
            return list_StoredProcParamDTOs;
        }

        public static string ConvertToClrType(string sqlType)
        {
            switch (sqlType.ToLower())
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
                case "time":
                case "datetime2":
                    return "DateTime";

                case "decimal":
                case "money":
                case "smallmoney":
                    return "decimal";

                case "float":
                    return "double";

                case "int":
                    return "int";

                case "real":
                    return "float";

                case "uniqueidentifier":
                    return "Guid";

                case "smallint":
                    return "short";

                case "tinyint":
                    return "byte";


                default:
                    throw new InvalidOperationException();
            }

        }

        public static SqlDbType ConvertToSqlDbType(string sqlType)
        {

            switch (sqlType.ToLower())
            {
                case "bigint":
                    return SqlDbType.BigInt;

                case "binary":
                    return SqlDbType.Binary;

                case "image":
                    return SqlDbType.Image;

                case "timestamp":
                    return SqlDbType.Timestamp;

                case "varbinary":
                    return SqlDbType.VarBinary;

                case "bit":
                    return SqlDbType.Bit;

                case "char":
                    return SqlDbType.Char;

                case "nchar":
                    return SqlDbType.NChar;

                case "ntext":
                    return SqlDbType.NText;

                case "nvarchar":
                    return SqlDbType.NVarChar;

                case "text":
                    return SqlDbType.Text;

                case "varchar":
                    return SqlDbType.VarChar;

                case "xml":
                    return SqlDbType.Xml;

                case "datetime":
                    return SqlDbType.DateTime;

                case "smalldatetime":
                    return SqlDbType.SmallDateTime;

                case "date":
                    return SqlDbType.Date;

                case "time":
                    return SqlDbType.Time;

                case "datetime2":
                    return SqlDbType.DateTime2;

                case "decimal":
                    return SqlDbType.Decimal;

                case "money":
                    return SqlDbType.Money;

                case "smallmoney":
                    return SqlDbType.SmallMoney;

                case "float":
                    return SqlDbType.Float;

                case "int":
                    return SqlDbType.Int;

                case "real":
                    return SqlDbType.Real;

                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;

                case "smallint":
                    return SqlDbType.SmallInt;

                case "tinyint":
                    return SqlDbType.TinyInt;


                default:
                    throw new InvalidOperationException();
            }

        }


    }
}
