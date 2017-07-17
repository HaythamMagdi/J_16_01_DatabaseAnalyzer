using System.Collections.Generic;
using System.Data;

namespace DatabaseAnalyzer.Util
{
    public class SqlTableStringMaker
    {
        //class DTOProperty
        //{
        //    public string Name { get; set; }
        //    public string TableColName { get; set; }
        //    public string TypeName { get; set; }
        //    public string SqlTypeName { get; set; }
        //    public bool AllowDBNull { get; set; }
        //}


        //public static string MakeSring(string tableName, DataTable table)
        public static string MakeSring(string tableName, List<SchemaTableColDTO> list_SchemaTableColDTOs)
        {
            //List<DTOProperty> list_Props = new List<DTOProperty>();

            string allColsStr = "";
            foreach (var col in list_SchemaTableColDTOs)
            {
                string template = "[<^^>] [int] IDENTITY(1,1) NOT NULL";

                string colStr = "";

                colStr +



                allColsStr += colStr + "\r\n";
            }



            string finalTemplate = @"


/*
    -- Create Table Script

    CREATE TABLE [dbo].[<^TableName^>](    
        <^TableColDef^>
    )

*/

                ";

            string dtoFileStr = finalTemplate.Replace("<^DTOClassName^>", tableName);

            {
                //string tableColDef = "";
                //for (int i = 0; i < list_Props.Count; i++)
                //{
                //    DTOProperty prop = list_Props[i];
                //    //tableColDef += "[" + prop.TableColName + "] " + prop.SqlTypeName + "<^CMA^>" + "\r\n";
                //    tableColDef += "[" + prop.Name + "] " + prop.SqlTypeName + "<^CMA^>" + "\r\n";

                //    if (i < list_Props.Count - 1)
                //    {
                //        tableColDef = tableColDef.Replace("<^CMA^>", ",");
                //    }
                //    else
                //    {
                //        tableColDef = tableColDef.Replace("<^CMA^>", "");
                //    }
                //}
                //dtoFileStr = dtoFileStr.Replace("<^TableColDef^>", tableColDef);
            }

            return dtoFileStr;
        }




    }
}
