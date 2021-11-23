using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 地裂术：3×3地属性魔法单段攻击
    /// </summary>
    public class S42401 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("地裂术CD")) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 4.5f;
            sActor.EP += 500;
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Earth)
                {
                    factor = 7.5f;
                    sActor.EP += 300;
                }
            }
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            if (sActor.Status.Additions.ContainsKey("元素解放"))
            {
                factor = 14.0f;
                DefaultBuff cd = new DefaultBuff(sActor, "地裂术CD", 5000);
                SkillHandler.ApplyAddition(sActor, cd);
            }
            List<Actor> targets = map.GetActorsArea(dActor, 100, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dactors.Add(item);
                    if (factor >= 10f)
                    {
                        硬直 yz = new 硬直(args.skill, dActor, 1500);
                        SkillHandler.ApplyAddition(dActor, yz);
                    }
                }
            }
            if (dactors.Count < 1) return;
            SkillHandler.Instance.MagicAttack(sActor, dactors, args, Elements.Earth, factor);
        }
        #endregion
    }
}
