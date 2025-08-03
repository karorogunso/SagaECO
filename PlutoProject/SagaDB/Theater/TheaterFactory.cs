using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actor;

namespace SagaDB.Theater
{
    public class TheaterFactory : FactoryList<TheaterFactory, Movie>
    {
        public TheaterFactory()
        {
            this.loadingTab = "Loading theater schedules";
            this.loadedTab = " schedules loaded.";
            this.databaseName = "schedule";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// 下一部要上映的电影
        /// </summary>
        /// <param name="mapID">地图ID</param>
        /// <returns>要上映的电影</returns>
        public Movie GetNextMovie(uint mapID)
        {
            if (items.ContainsKey(mapID))
            {
                DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                var query =
                   from movie in this.items[mapID]
                   where movie.StartTime > time
                   orderby movie.StartTime
                   select movie;
                if (query.Count() != 0)
                    return query.First();
                else
                {
                    query =
                        from movie in this.items[mapID]
                        orderby movie.StartTime
                        select movie;
                    if (query.Count() != 0)
                        return query.First();
                    else
                        return null;
                }
            }
            return null;
                
        }

        /// <summary>
        /// 当前放映中的电影
        /// </summary>
        /// <param name="mapID">影院地图ID</param>
        /// <returns></returns>
        public Movie GetCurrentMovie(uint mapID)
        {
            if (items.ContainsKey(mapID))
            {
                DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                var query =
                   from movie in this.items[mapID]
                   where movie.StartTime < time && ((movie.StartTime + new TimeSpan(0, movie.Duration, 0)) > time)
                   orderby movie.StartTime
                   select movie;
                if (query.Count() != 0)
                    return query.First();
                else
                    return null;
            }
            return null;
        }
         

        protected override uint GetKey(Movie item)
        {
            return item.MapID;
        }

        protected override void ParseCSV(Movie item, string[] paras)
        {
            throw new NotImplementedException();
        }

        protected override void ParseXML(XmlElement root, XmlElement current, Movie item)
        {
            switch (root.Name.ToLower())
            {                
                case "movie":
                    switch (current.Name.ToLower())
                    {
                        case "name":
                            item.Name = current.InnerText;
                            break;
                        case "mapid":
                            item.MapID = uint.Parse(current.InnerText);
                            break;
                        case "ticket":
                            item.Ticket = uint.Parse(current.InnerText);
                            break;
                        case "url":
                            item.URL = current.InnerText;
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
