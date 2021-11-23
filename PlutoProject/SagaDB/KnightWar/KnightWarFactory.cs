using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.KnightWar
{
    public class KnightWarFactory : Factory<KnightWarFactory, KnightWar>
    {
        public KnightWarFactory()
        {
            this.loadingTab = "Loading KnightWar database";
            this.loadedTab = " KightWars loaded.";
            this.databaseName = "KnightWaw";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// 下一场要进行的骑士团演习
        /// </summary>
        /// <returns>要上映的电影</returns>
        public KnightWar GetNextKnightWar()
        {
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var query =
               from movie in this.items.Values
               where movie.StartTime > time
               orderby movie.StartTime
               select movie;
            if (query.Count() != 0)
                return query.First();
            else
            {
                query =
                    from movie in this.items.Values
                    orderby movie.StartTime
                    select movie;
                if (query.Count() != 0)
                    return query.First();
                else
                    return null;
            }
        }

        /// <summary>
        /// 当前正在进行的骑士团演习
        /// </summary>
        /// <returns></returns>
        public KnightWar GetCurrentMovie()
        {
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            var query =
               from movie in this.items.Values
               where movie.StartTime < time && ((movie.StartTime + new TimeSpan(0, movie.Duration, 0)) > time)
               orderby movie.StartTime
               select movie;
            if (query.Count() != 0)
                return query.First();
            else
                return null;
        }


        protected override uint GetKey(KnightWar item)
        {
            return item.ID;
        }

        protected override void ParseCSV(KnightWar item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, KnightWar item)
        {
            switch (root.Name.ToLower())
            {
                case "movie":
                    switch (current.Name.ToLower())
                    {
                       case "id":
                            item.ID = uint.Parse(current.InnerText);
                            break;
                        case "starttime":
                            string[] buf = current.InnerText.Split(':');
                            DateTime time = new DateTime(1970, 1, 1, int.Parse(buf[0]), int.Parse(buf[1]), 0);
                            item.StartTime = time;
                            break;
                        case "duration":
                            item.Duration = int.Parse(current.InnerText);
                            break;
                    }
                    break;
            }
        }
    }
}
