using System.Collections.Generic;
using System.Data;

namespace DatabaseAnalyzer.Util
{
    public class SqlTableStringMaker
    {
        class DTOProperty
        {
            public string Name { get; set; }
            public string TableColName { get; set; }
            public string TypeName { get; set; }
            public string SqlTypeName { get; set; }
            public bool AllowDBNull { get; set; }
        }


        //public static string MakeSring(string tableName, DataTable table)
        public static string MakeSring(string tableName, List<SchemaTableColDTO> list_SchemaTableColDTOs)
        {
            List<DTOProperty> list_Props = new List<DTOProperty>();

            foreach (DataColumn col in table.Columns)
            {
                DTOProperty prop = new DTOProperty
                {
                    //Name = col.ColumnName.Trim().Replace(" ", "_"),
                    Name = col.ColumnName.Trim().Replace(" ", ""),
                    TableColName = col.ColumnName,

                    TypeName = col.DataType.Name
                        .Replace("String", "string")
                        .Replace("Boolean", "bool")

                        .Replace("Int16", "short")
                        .Replace("Int32", "int")
                        .Replace("Int64", "long"),

                    SqlTypeName = col.DataType.Name.ToLower()
                        .Replace("int64", "bigint")
                        .Replace("int32", "int")
                        .Replace("string", "[nvarchar](300)"),

                    AllowDBNull = col.AllowDBNull,
                };
                prop.TypeName += ((prop.AllowDBNull && prop.TypeName != "string" && prop.TypeName != "Type") ? "?" : "");

                list_Props.Add(prop);
            }



            string dtoFileTemplate = @"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class <^DTOClassName^>
    {
        <^DEF_PROPS^>
    }
}


                
//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_<^DTOClassName^>](    
        <^TableColDef^>
    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class <^DTOClassName^>Mgr
    {

        public static <^DTOClassName^> FromDataRow(DataRow dr)
        {
            <^DTOClassName^> res = new <^DTOClassName^>();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                <^COPY_PROPS_FROM_ROW^>                       
            }

            return res;
        }

        public static List<<^DTOClassName^>> CreateListFromDataTable(DataTable table)
        {
            List<<^DTOClassName^>> list_DTOs = new List<<^DTOClassName^>>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(<^DTOClassName^>Mgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

                ";
            string dtoFileStr = dtoFileTemplate.Replace("<^DTOClassName^>", tableName);

            {
                string tableColDef = "";
                for (int i = 0; i < list_Props.Count; i++)
                {
                    DTOProperty prop = list_Props[i];
                    //tableColDef += "[" + prop.TableColName + "] " + prop.SqlTypeName + "<^CMA^>" + "\r\n";
                    tableColDef += "[" + prop.Name + "] " + prop.SqlTypeName + "<^CMA^>" + "\r\n";

                    if (i < list_Props.Count - 1)
                    {
                        tableColDef = tableColDef.Replace("<^CMA^>", ",");
                    }
                    else
                    {
                        tableColDef = tableColDef.Replace("<^CMA^>", "");
                    }
                }
                dtoFileStr = dtoFileStr.Replace("<^TableColDef^>", tableColDef);
            }

            {
                string propDef = "";
                foreach (DTOProperty prop in list_Props)
                {
                    propDef += "public " + prop.TypeName + " " + prop.Name + " { get; set; }" + "\r\n";
                }
                dtoFileStr = dtoFileStr.Replace("<^DEF_PROPS^>", propDef);
            }

            {
                string propFromColsStr = "";
                foreach (DTOProperty prop in list_Props)
                {
                    propFromColsStr += "res." + prop.Name + " = " + (prop.AllowDBNull ? " dr[\"" + prop.TableColName + "\"] is DBNull ? null : " : "") + " (" + prop.TypeName + ")dr[\"" + prop.TableColName + "\"];" + "\r\n";
                }
                dtoFileStr = dtoFileStr.Replace("<^COPY_PROPS_FROM_ROW^>", propFromColsStr);
            }


            return dtoFileStr;
        }




    }
}
