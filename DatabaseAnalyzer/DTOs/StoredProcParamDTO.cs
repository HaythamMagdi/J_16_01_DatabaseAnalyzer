

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class StoredProcParamDTO
    {
        public string Parameter_name { get; set; }
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
        [Parameter_name] [nvarchar](300),
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



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class StoredProcParamDTOMgr
    {

        public static StoredProcParamDTO FromDataRow(DataRow dr)
        {
            StoredProcParamDTO res = new StoredProcParamDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.Parameter_name = dr["Parameter_name"] is DBNull ? null : (string)dr["Parameter_name"];
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

