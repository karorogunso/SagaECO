using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30017 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ActorSpeak(sActor, "灵魂炸裂！！");
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 400, false);
            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(sActor.MapID), sActor, 5320);

            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if (item.HP < (item.MaxHP / 2))
                    {
                        SkillHandler.Instance.CauseDamage(sActor, item, 99999);
                        SkillHandler.Instance.ShowVessel(item, 99999);
                        SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(item.MapID), item, 5396);
                    }
                    else
                    {
                        Stun s = new Stun(null, item, 3000);
                        SkillHandler.ApplyAddition(item, s);
                        MaxHPDown hpd = new MaxHPDown(args.skill, item, 10000);//死亡毒素
                        SkillHandler.ApplyAddition(item, hpd);
                    }
                }
            }
        }
    }
}
