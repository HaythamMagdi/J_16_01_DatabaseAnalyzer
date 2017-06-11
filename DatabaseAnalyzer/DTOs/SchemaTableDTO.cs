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
                res.ColumnName = dr["ColumnName"] is DBNull ? null : (string)dr["ColumnName"];
                res.ColumnOrdinal = dr["ColumnOrdinal"] is DBNull ? null : (int?)dr["ColumnOrdinal"];
                res.ColumnSize = dr["ColumnSize"] is DBNull ? null : (int?)dr["ColumnSize"];
                res.NumericPrecision = dr["NumericPrecision"] is DBNull ? null : (short?)dr["NumericPrecision"];
                res.NumericScale = dr["NumericScale"] is DBNull ? null : (short?)dr["NumericScale"];
                res.IsUnique = dr["IsUnique"] is DBNull ? null : (bool?)dr["IsUnique"];
                res.IsKey = dr["IsKey"] is DBNull ? null : (bool?)dr["IsKey"];
                res.BaseServerName = dr["BaseServerName"] is DBNull ? null : (string)dr["BaseServerName"];
                res.BaseCatalogName = dr["BaseCatalogName"] is DBNull ? null : (string)dr["BaseCatalogName"];
                res.BaseColumnName = dr["BaseColumnName"] is DBNull ? null : (string)dr["BaseColumnName"];
                res.BaseSchemaName = dr["BaseSchemaName"] is DBNull ? null : (string)dr["BaseSchemaName"];
                res.BaseTableName = dr["BaseTableName"] is DBNull ? null : (string)dr["BaseTableName"];
                res.DataType = dr["DataType"] is DBNull ? null : (Type)dr["DataType"];
                res.AllowDBNull = dr["AllowDBNull"] is DBNull ? null : (bool?)dr["AllowDBNull"];
                res.ProviderType = dr["ProviderType"] is DBNull ? null : (int?)dr["ProviderType"];
                res.IsAliased = dr["IsAliased"] is DBNull ? null : (bool?)dr["IsAliased"];
                res.IsExpression = dr["IsExpression"] is DBNull ? null : (bool?)dr["IsExpression"];
                res.IsIdentity = dr["IsIdentity"] is DBNull ? null : (bool?)dr["IsIdentity"];
                res.IsAutoIncrement = dr["IsAutoIncrement"] is DBNull ? null : (bool?)dr["IsAutoIncrement"];
                res.IsRowVersion = dr["IsRowVersion"] is DBNull ? null : (bool?)dr["IsRowVersion"];
                res.IsHidden = dr["IsHidden"] is DBNull ? null : (bool?)dr["IsHidden"];
                res.IsLong = dr["IsLong"] is DBNull ? null : (bool?)dr["IsLong"];
                res.IsReadOnly = dr["IsReadOnly"] is DBNull ? null : (bool?)dr["IsReadOnly"];
                res.ProviderSpecificDataType = dr["ProviderSpecificDataType"] is DBNull ? null : (Type)dr["ProviderSpecificDataType"];
                res.DataTypeName = dr["DataTypeName"] is DBNull ? null : (string)dr["DataTypeName"];
                res.XmlSchemaCollectionDatabase = dr["XmlSchemaCollectionDatabase"] is DBNull ? null : (string)dr["XmlSchemaCollectionDatabase"];
                res.XmlSchemaCollectionOwningSchema = dr["XmlSchemaCollectionOwningSchema"] is DBNull ? null : (string)dr["XmlSchemaCollectionOwningSchema"];
                res.XmlSchemaCollectionName = dr["XmlSchemaCollectionName"] is DBNull ? null : (string)dr["XmlSchemaCollectionName"];
                res.UdtAssemblyQualifiedName = dr["UdtAssemblyQualifiedName"] is DBNull ? null : (string)dr["UdtAssemblyQualifiedName"];
                res.NonVersionedProviderType = dr["NonVersionedProviderType"] is DBNull ? null : (int?)dr["NonVersionedProviderType"];
                res.IsColumnSet = dr["IsColumnSet"] is DBNull ? null : (bool?)dr["IsColumnSet"];

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

