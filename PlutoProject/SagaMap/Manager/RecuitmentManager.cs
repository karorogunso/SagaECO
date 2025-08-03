using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Party;
using SagaDB.Actor;

using SagaMap.Network.Client;

namespace SagaMap.Manager
{
    public enum RecruitmentType
    {
        Party = 1,
        Item,
        Info,
        Team
    }

    public class Recruitment
    {
        ActorPC creator;
        string title;
        string content;
        RecruitmentType type;

        public ActorPC Creator { get { return this.creator; } set { this.creator = value; } }

        public string Title { get { return this.title; } set { this.title = value; } }

        public string Content { get { return this.content; } set { this.content = value; } }

        public RecruitmentType Type { get { return this.type; } set { this.type = value; } }
    }

    public class RecruitmentManager:Singleton<RecruitmentManager>
    {
        List<Recruitment> items = new List<Recruitment>();
        public RecruitmentManager()
        {

        }

        public void CreateRecruiment(Recruitment rec)
        {
            var res =
                from r in items
                where r.Creator == rec.Creator
                select r;

            if (res.Count() != 0)
            {
                items.Remove(res.First());
                items.Add(rec);
            }
            else
            {
                items.Add(rec);
            }
        }

        public void DeleteRecruitment(ActorPC creator)
        {
            var res =
                from r in items
                where r.Creator == creator
                select r;

            if (res.Count() != 0)
            {
                items.Remove(res.First());
            }
        }

        public List<Recruitment> GetRecruitments(RecruitmentType type, int page, out int maxPage)
        {
            var res =
                from r in items
                where r.Type == type
                select r;
            List<Recruitment> list = res.ToList();
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
        public List<Recruitment> GetRecruitments(RecruitmentType type)
        {
            var res =
                from r in items
                where r.Type == type
                select r;
            List<Recruitment> list = res.ToList();
            return list;
        }
    }
}
