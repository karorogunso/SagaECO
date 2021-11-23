using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace GMTool.bin.Debug.QueryScripts
{
    public class QueryItemCountByItem : Query 
    {

        public override object Initial()
        {
            this.QueryTitle = "統計各道具數量";
            this.QueryDescription = "依照各道具來統計\n分別計算每個道具的數量\n已群組的方式表示";
            object o = new object();
            return o;
        }
        public override System.Collections.IEnumerable Run(DataTable Items, Object arg)
        {
            var ret = from r in Items.AsEnumerable()
                      group r by new { Key = r.Field<int>("ItemID"), Name = r.Field<string>("ItemName") } into g
                      select new { g.Key.Key ,g.Key.Name,  count = g.Count() };
            return ret;
        }
    }
}
