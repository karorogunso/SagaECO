using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 魔动剑：单体单段无属性魔法攻击
    /// </summary>
    public class S42505 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 8.5f;
            //if (sActor.Status.Additions.ContainsKey("属性契约")) factor = 6.5f;
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.CInt["无属性状态"] == 1)
                {
                    SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.Def, Elements.Neutral, factor);
                }
                else
                    SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, factor);
            }
            sActor.EP += 500;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        #endregion
    }
}
