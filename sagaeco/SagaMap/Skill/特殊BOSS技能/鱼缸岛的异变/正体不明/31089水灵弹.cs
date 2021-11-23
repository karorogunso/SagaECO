using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31089 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 300, true);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);

                    if (!item.Status.Additions.ContainsKey("BOSS_水灵弹"))
                    {
                        OtherAddition skill = new OtherAddition(null, item, "BOSS_水灵弹", 60000);
                        skill.OnAdditionEnd += (i, e) =>
                        {
                            i.TInt["BOSS_水灵弹层数"] = 0;
                            Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("【水灵弹】层数清空了。");
                        };
                        SkillHandler.ApplyAddition(item, skill);
                    }
                    if (item.TInt["BOSS_水灵弹层数"] < 2)
                    {
                        //SkillHandler.Instance.ShowEffectOnActor(j, 5041);
                        string s = "";
                        if (item.TInt["BOSS_水灵弹层数"] == 1)
                            s = "请尽快转移仇恨！！";
                        item.TInt["BOSS_水灵弹层数"]++;
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("你被水灵弹击中了！"+s+":[层数:" + item.TInt["BOSS_水灵弹层数"].ToString() + "/3]");
                    }
                    else
                    {
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("汹涌的波涛将你吞噬了。");
                        SkillHandler.Instance.ShowEffectOnActor(item, 4488);
                        List<Actor> actors3 = map.GetRoundAreaActors(item.X, item.Y, 5000);
                        foreach (var item2 in actors3)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item2))
                            {
                                SkillHandler.Instance.CauseDamage(sActor, item2, (int)item2.MaxHP);
                                SkillHandler.Instance.ShowVessel(item2, (int)item2.MaxHP);
                                SkillHandler.Instance.ShowEffectOnActor(item2, 4489);
                                if (item2 != item)
                                    Network.Client.MapClient.FromActorPC((ActorPC)item2).SendSystemMessage("汹涌的波涛将你吞噬了。");
                            }
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, targets, args, Elements.Water, 2f);
        }

        #endregion
    }
}
