using System.Collections.Generic;
using System.Data;

namespace DatabaseAnalyzer.Util
{
    public class TableInfo
    {
        public List<SchemaTableColDTO> List_SchemaTableColDTOs { get; set; }
        public DataTable Table { get; set; }
    }
}
