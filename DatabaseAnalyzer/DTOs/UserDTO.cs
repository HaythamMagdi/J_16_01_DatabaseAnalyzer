

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string SecurityRole { get; set; }
        public string GeneralOrderComment { get; set; }

    }
}



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_UserDTO](    
        [Id] int,
[Name] [nvarchar](300),
[Email] [nvarchar](300),
[Password] [nvarchar](300),
[Phone] [nvarchar](300),
[Address] [nvarchar](300),
[SecurityRole] [nvarchar](300),
[GeneralOrderComment] [nvarchar](300)

    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class UserDTOMgr
    {

        public static UserDTO FromDataRow(DataRow dr)
        {
            UserDTO res = new UserDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.Id = (int)dr["Id"];
                res.Name = (string)dr["Name"];
                res.Email = (string)dr["Email"];
                res.Password = (string)dr["Password"];
                res.Phone = (string)dr["Phone"];
                res.Address = (string)dr["Address"];
                res.SecurityRole = (string)dr["SecurityRole"];
                res.GeneralOrderComment = dr["GeneralOrderComment"] is DBNull ? null : (string)dr["GeneralOrderComment"];

            }

            return res;
        }

        public static List<UserDTO> CreateListFromDataTable(DataTable table)
        {
            List<UserDTO> list_DTOs = new List<UserDTO>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(UserDTOMgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

