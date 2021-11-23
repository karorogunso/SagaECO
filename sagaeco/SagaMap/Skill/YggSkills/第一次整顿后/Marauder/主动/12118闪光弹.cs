using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12118 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("闪光弹CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            StableAddition cd = new StableAddition(null, sActor, "闪光弹CD", 150000);
            cd.OnAdditionEnd += (s, e) =>
            {
                SkillHandler.Instance.ShowEffectOnActor(sActor, 8008, sActor);
                SkillHandler.SendSystemMessage(sActor, "『闪光弹』可以再次使用了。");
            };
            SkillHandler.ApplyAddition(sActor, cd);
            float factor = 6f + 2f * level;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(dActor, 400, true);
            List<Actor> dest = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if (item.HP > 0 && !item.Status.Additions.ContainsKey("闪光弹免疫"))
                    {
                        Stun stun = new Stun(null, item, 8000);
                        SkillHandler.ApplyAddition(item, stun);
                        OtherAddition skill = new OtherAddition(null, item, "闪光弹免疫", 60000);
                        SkillHandler.ApplyAddition(item, skill);

                        SkillHandler.Instance.CancelSkillCast(item);
                    }
                    dest.Add(item);
                }
            }
            SkillHandler.Instance.ShowEffectOnActor(dActor, 5622, sActor);
            SkillHandler.Instance.PhysicalAttack(sActor, dest, args, Elements.Holy, factor);
        }
    }
}
