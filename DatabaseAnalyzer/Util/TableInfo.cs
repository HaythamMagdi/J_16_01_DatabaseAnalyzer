using System.Collections.Generic;
using System.Data;

namespace DatabaseAnalyzer.Util
{
    public class TableInfo
    {
        public List<SchemaTableDTO> List_SchemaTableDTOs { get; set; }
        public DataTable Table { get; set; }
    }
}
