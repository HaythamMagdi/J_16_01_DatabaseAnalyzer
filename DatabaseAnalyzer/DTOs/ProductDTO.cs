

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Decimal UnitPrice { get; set; }

    }
}



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_ProductDTO](    
        [Id] int,
[Name] [nvarchar](300),
[UnitPrice] decimal

    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class ProductDTOMgr
    {

        public static ProductDTO FromDataRow(DataRow dr)
        {
            ProductDTO res = new ProductDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.Id = (int)dr["Id"];
                res.Name = (string)dr["Name"];
                res.UnitPrice = (Decimal)dr["UnitPrice"];

            }

            return res;
        }

        public static List<ProductDTO> CreateListFromDataTable(DataTable table)
        {
            List<ProductDTO> list_DTOs = new List<ProductDTO>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(ProductDTOMgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

