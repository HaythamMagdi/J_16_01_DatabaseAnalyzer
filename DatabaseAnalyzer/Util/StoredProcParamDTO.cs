

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DatabaseAnalyzer.Util;


namespace DatabaseAnalyzer.Util
{
    public class StoredProcParamDTO
    {
        public string ParameterName { get; set; }
        public string Type { get; set; }
        public short Length { get; set; }
        public int? Prec { get; set; }
        public int? Scale { get; set; }
        public int Param_order { get; set; }
        public string Collation { get; set; }
        public bool IsOutput { get; set; }

    }
}



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_StoredProcParamDTO](    
        [ParameterName] [nvarchar](300),
[Type] [nvarchar](300),
[Length] int16,
[Prec] int,
[Scale] int,
[Param_order] int,
[Collation] [nvarchar](300),
[IsOutput] boolean

    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace DatabaseAnalyzer.Util
{

    public class StoredProcParamDTOMgr
    {

        public static StoredProcParamDTO FromDataRow(DataRow dr)
        {
            StoredProcParamDTO res = new StoredProcParamDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.ParameterName = dr["ParameterName"] is DBNull ? null : (string)dr["ParameterName"];
                res.Type = dr["Type"] is DBNull ? null : (string)dr["Type"];
                res.Length = (short)dr["Length"];
                res.Prec = dr["Prec"] is DBNull ? null : (int?)dr["Prec"];
                res.Scale = dr["Scale"] is DBNull ? null : (int?)dr["Scale"];
                res.Param_order = (int)dr["Param_order"];
                res.Collation = dr["Collation"] is DBNull ? null : (string)dr["Collation"];
                res.IsOutput = (bool)dr["IsOutput"];

            }

            return res;
        }

        public static List<StoredProcParamDTO> CreateListFromDataTable(DataTable table)
        {
            List<StoredProcParamDTO> list_DTOs = new List<StoredProcParamDTO>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(StoredProcParamDTOMgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

