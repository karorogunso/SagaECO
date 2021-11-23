using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Party;
using SagaDB.Actor;
using SagaDB.FFarden;

using SagaMap.Network.Client;

namespace SagaMap.Manager
{
    public class FFardenManager : Singleton<FFardenManager>
    {
        List<FFarden> items = new List<FFarden>();
        public List<SagaDB.FFarden.FFarden> GetFFList(int page, out int maxPage)
        {
            var res =
            from r in MapServer.charDB.GetFFList()
            select r;
            List<SagaDB.FFarden.FFarden> list = MapServer.charDB.GetFFList();
            if (list.Count % 15 == 0)
                maxPage = list.Count / 15;
            else
                maxPage = (list.Count / 15) + 1;
            res =
                from r in list
                where (list.IndexOf(r) >= (page) * 15) && (list.IndexOf(r) < ((page + 1) * 15))
                select r;
            list = res.ToList();
            return list;
        }

    }
}
