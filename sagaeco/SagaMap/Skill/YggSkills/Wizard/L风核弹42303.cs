using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 风核弹：大范围风属性单段魔法攻击
    /// </summary>
    public class S42303
        : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 5.0f;

            if (sActor.Status.Additions.ContainsKey("属性契约"))
            {
                if (((OtherAddition)(sActor.Status.Additions["属性契约"])).Variable["属性契约"] == (int)Elements.Wind)
                {
                    factor = 13f;
                    sActor.EP += 300;
                }
            }
            if (sActor.Status.Additions.ContainsKey("元素解放"))
            {
                factor = 20.0f;
            }

            sActor.EP += 500;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            List<Actor> targets = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x,map.Width),SagaLib.Global.PosY8to16(args.y,map.Height), 300, true);
            List<Actor> dactors = new List<Actor>();
            foreach (var item in targets)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    dactors.Add(item);
                }
            }
            if (dactors.Count < 1) return;
            SkillHandler.Instance.MagicAttack(sActor, dactors, args, Elements.Wind, factor);
        }
        #endregion
    }
}
