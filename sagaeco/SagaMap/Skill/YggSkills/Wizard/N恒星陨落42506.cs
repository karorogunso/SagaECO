using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 恒星陨落：广域单段无属性魔法攻击
    /// </summary>
    public class S42506 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 6.5f;
            //if (sActor.Status.Additions.ContainsKey("属性契约")) factor = 5f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 500, false);
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    SkillHandler.DefType type = SkillHandler.DefType.MDef;
                    if (sActor.type == ActorType.PC)
                    {
                        if (((ActorPC)sActor).CInt["无属性状态"] == 1)
                            type = SkillHandler.DefType.Def;
                    }
                    SkillHandler.Instance.DoDamage(false, sActor, item, args, type, Elements.Neutral, 0, factor);
                }
            }

            sActor.EP += 300;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }

        #endregion
    }
}
