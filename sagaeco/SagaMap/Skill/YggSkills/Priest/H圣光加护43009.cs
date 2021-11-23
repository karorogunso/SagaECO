using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    public class S43009 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(pc.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    return 0;
                }
                return -2;
            }
            else
            {
                return -2;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            float factors = 2.3f;
            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Holy)
                {
                    int damage = SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factors);
                    SkillHandler.Instance.ShowEffectOnActor(dActor, 4295);
                    dActor.TInt["圣光加护次数"] = 0;
                    dActor.TInt["圣光加护治疗量"] = damage;
                    if(!dActor.Status.Additions.ContainsKey("圣光加护"))
                    {
                        OtherAddition skill = new OtherAddition(null, dActor, "圣光加护", 20000);
                        skill.OnAdditionStart += (s, e) =>
                        {
                            s.Buff.三转ブロッキング = true;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                        };
                        skill.OnAdditionEnd += (s, e) =>
                        {
                            s.Buff.三转ブロッキング = false;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                        };
                        SkillHandler.ApplyAddition(dActor, skill);
                    }
                }
            }
        }
        #endregion
    }
}

