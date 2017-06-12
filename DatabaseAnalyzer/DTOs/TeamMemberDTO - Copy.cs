using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BD.DomainModel.DataTransferObjects;


namespace BD.DomainModel.DataTransferObjects
{
    public class TeamMemberDTO
    {
        public int TeamMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }
        public bool IsConcurrentAccount { get; set; }
        public DateTime? OriginalEndDate { get; set; }

    }
}



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



/*
    -- Create Table Script

    CREATE TABLE [dbo].[Tbl_TeamMemberDTO](    
        [TeamMemberId] int,
[FirstName] [nvarchar](300),
[LastName] [nvarchar](300),
[Password] [nvarchar](300),
[Email] [nvarchar](300),
[Position] [nvarchar](300),
[Team] [nvarchar](300),
[StartDate] datetime,
[EndDate] datetime,
[Active] boolean,
[IsConcurrentAccount] boolean,
[OriginalEndDate] datetime

    )


*/



//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------



namespace BD.DomainModel.DataTransferObjectManagers
{

    public class TeamMemberDTOMgr
    {

        public static TeamMemberDTO FromDataRow(DataRow dr)
        {
            TeamMemberDTO res = new TeamMemberDTO();

            DataTable table = dr.Table;

            foreach (DataColumn col in table.Columns)
            {
                res.TeamMemberId = (int)dr["TeamMemberId"];
                res.FirstName = (string)dr["FirstName"];
                res.LastName = (string)dr["LastName"];
                res.Password = (string)dr["Password"];
                res.Email = (string)dr["Email"];
                res.Position = (string)dr["Position"];
                res.Team = (string)dr["Team"];
                res.StartDate = (DateTime)dr["StartDate"];
                res.EndDate = dr["EndDate"] is DBNull ? null : (DateTime?)dr["EndDate"];
                res.Active = (bool)dr["Active"];
                res.IsConcurrentAccount = (bool)dr["IsConcurrentAccount"];
                res.OriginalEndDate = dr["OriginalEndDate"] is DBNull ? null : (DateTime?)dr["OriginalEndDate"];

            }

            return res;
        }

        public static List<TeamMemberDTO> CreateListFromDataTable(DataTable table)
        {
            List<TeamMemberDTO> list_DTOs = new List<TeamMemberDTO>();

            foreach (DataRow dr in table.Rows)
            {
                list_DTOs.Add(TeamMemberDTOMgr.FromDataRow(dr));
            }

            return list_DTOs;
        }

    }

}

