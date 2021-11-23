using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaDB.Actor;
using SagaMap.Network.Client;

namespace SagaMap.Manager
{
    public class WRPRankingManager:Singleton<WRPRankingManager>
    {
        List<ActorPC> currentRanking;
        public WRPRankingManager()
        {
        }

        public uint GetRanking(ActorPC pc)
        {
            if (currentRanking == null)
            {
                currentRanking = MapServer.charDB.GetWRPRanking();
            }
            var fit =
                from chr in currentRanking
                where chr.CharID == pc.CharID
                select chr;
            if (fit.Count() > 0)
               return fit.First().WRPRanking;
            else
               return 268;
        }

        public void UpdateRanking()
        {
            currentRanking = MapServer.charDB.GetWRPRanking();
            foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
            {
                var fit =
                    from chr in currentRanking
                    where chr.CharID == i.Character.CharID                    
                    select chr;
                if (fit.Count() > 0)
                {
                    i.Character.WRPRanking = fit.First().WRPRanking;
                }
                else
                    i.Character.WRPRanking = 268;
                if (i.Character.WRPRanking <= 10)
                {
                    if (i.map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                    {
                        if (!i.Character.Skills.ContainsKey(10500))
                        {
                            SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(10500, 1);
                            skill.NoSave = true;
                            i.Character.Skills.Add(10500, skill);
                        }
                    }
                    else
                    {
                        if (i.Character.Skills.ContainsKey(10500))
                        {
                            i.Character.Skills.Remove(10500);
                        }
                    }
                }
                else
                {
                    if (i.Character.Skills.ContainsKey(10500))
                    {
                        i.Character.Skills.Remove(10500);
                    }
                }
                i.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.WRP_RANKING_UPDATE, null, i.Character, true);
            }
        }
    }
}
