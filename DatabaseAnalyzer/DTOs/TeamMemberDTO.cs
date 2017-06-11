

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
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Position { get; set; }
        public String Team { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean Active { get; set; }
        public Boolean IsConcurrentAccount { get; set; }
        public DateTime OriginalEndDate { get; set; }

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
                res.FirstName = (String)dr["FirstName"];
                res.LastName = (String)dr["LastName"];
                res.Password = (String)dr["Password"];
                res.Email = (String)dr["Email"];
                res.Position = (String)dr["Position"];
                res.Team = (String)dr["Team"];
                res.StartDate = (DateTime)dr["StartDate"];
                res.EndDate = (DateTime)dr["EndDate"];
                res.Active = (Boolean)dr["Active"];
                res.IsConcurrentAccount = (Boolean)dr["IsConcurrentAccount"];
                res.OriginalEndDate = (DateTime)dr["OriginalEndDate"];

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

