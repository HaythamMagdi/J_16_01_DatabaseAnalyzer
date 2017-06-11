

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class SchemaTableDTO
    {
        public string ColumnName { get; set; }
        public int? ColumnOrdinal { get; set; }
        public int? ColumnSize { get; set; }
        public short? NumericPrecision { get; set; }
        public short? NumericScale { get; set; }
        public bool? IsUnique { get; set; }
        public bool? IsKey { get; set; }
        public string BaseServerName { get; set; }
        public string BaseCatalogName { get; set; }
        public string BaseColumnName { get; set; }
        public string BaseSchemaName { get; set; }
        public string BaseTableName { get; set; }
        public Type DataType { get; set; }
        public bool? AllowDBNull { get; set; }
        public int? ProviderType { get; set; }
        public bool? IsAliased { get; set; }
        public bool? IsExpression { get; set; }
        public bool? IsIdentity { get; set; }
        public bool? IsAutoIncrement { get; set; }
        public bool? IsRowVersion { get; set; }
        public bool? IsHidden { get; set; }
        public bool? IsLong { get; set; }
        public bool? IsReadOnly { get; set; }
        public Type ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }
        public string XmlSchemaCollectionDatabase { get; set; }
        public string XmlSchemaCollectionOwningSchema { get; set; }
        public string XmlSchemaCollectionName { get; set; }
        public string UdtAssemblyQualifiedName { get; set; }
        public int? NonVersionedProviderType { get; set; }
        public bool? IsColumnSet { get; set; }

    }
}



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_SchemaTableDTO](    
        [ColumnName] [nvarchar](300),
[ColumnOrdinal] int,
[ColumnSize] int,
[NumericPrecision] int16,
[NumericScale] int16,
[IsUnique] boolean,
[IsKey] boolean,
[BaseServerName] [nvarchar](300),
[BaseCatalogName] [nvarchar](300),
[BaseColumnName] [nvarchar](300),
[BaseSchemaName] [nvarchar](300),
[BaseTableName] [nvarchar](300),
[DataType] type,
[AllowDBNull] boolean,
[ProviderType] int,
[IsAliased] boolean,
[IsExpression] boolean,
[IsIdentity] boolean,
[IsAutoIncrement] boolean,
[IsRowVersion] boolean,
[IsHidden] boolean,
[IsLong] boolean,
[IsReadOnly] boolean,
[ProviderSpecificDataType] type,
[DataTypeName] [nvarchar](300),
[XmlSchemaCollectionDatabase] [nvarchar](300),
[XmlSchemaCollectionOwningSchema] [nvarchar](300),
[XmlSchemaCollectionName] [nvarchar](300),
[UdtAssemblyQualifiedName] [nvarchar](300),
[NonVersionedProviderType] int,
[IsColumnSet] boolean

    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class SchemaTableDTOMgr
    {

        public static SchemaTableDTO FromDataRow(DataRow dr)
        {
            SchemaTableDTO res = new SchemaTableDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.ColumnName = (string)dr["ColumnName"];
                res.ColumnOrdinal = (int)dr["ColumnOrdinal"];
                res.ColumnSize = (int)dr["ColumnSize"];
                res.NumericPrecision = (short)dr["NumericPrecision"];
                res.NumericScale = (short)dr["NumericScale"];
                res.IsUnique = (bool)dr["IsUnique"];
                res.IsKey = (bool)dr["IsKey"];
                res.BaseServerName = (string)dr["BaseServerName"];
                res.BaseCatalogName = (string)dr["BaseCatalogName"];
                res.BaseColumnName = (string)dr["BaseColumnName"];
                res.BaseSchemaName = (string)dr["BaseSchemaName"];
                res.BaseTableName = (string)dr["BaseTableName"];
                res.DataType = (Type)dr["DataType"];
                res.AllowDBNull = (bool)dr["AllowDBNull"];
                res.ProviderType = (int)dr["ProviderType"];
                res.IsAliased = (bool)dr["IsAliased"];
                res.IsExpression = (bool)dr["IsExpression"];
                res.IsIdentity = (bool)dr["IsIdentity"];
                res.IsAutoIncrement = (bool)dr["IsAutoIncrement"];
                res.IsRowVersion = (bool)dr["IsRowVersion"];
                res.IsHidden = (bool)dr["IsHidden"];
                res.IsLong = (bool)dr["IsLong"];
                res.IsReadOnly = (bool)dr["IsReadOnly"];
                res.ProviderSpecificDataType = (Type)dr["ProviderSpecificDataType"];
                res.DataTypeName = (string)dr["DataTypeName"];
                res.XmlSchemaCollectionDatabase = (string)dr["XmlSchemaCollectionDatabase"];
                res.XmlSchemaCollectionOwningSchema = (string)dr["XmlSchemaCollectionOwningSchema"];
                res.XmlSchemaCollectionName = (string)dr["XmlSchemaCollectionName"];
                res.UdtAssemblyQualifiedName = (string)dr["UdtAssemblyQualifiedName"];
                res.NonVersionedProviderType = (int)dr["NonVersionedProviderType"];
                res.IsColumnSet = (bool)dr["IsColumnSet"];

            }

            return res;
        }

        public static List<SchemaTableDTO> CreateListFromDataTable(DataTable table)
        {
            List<SchemaTableDTO> list_DTOs = new List<SchemaTableDTO>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(SchemaTableDTOMgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

