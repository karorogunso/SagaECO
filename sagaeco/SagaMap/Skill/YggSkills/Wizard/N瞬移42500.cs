using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42500 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //if (pc.Status.Additions.ContainsKey("属性契约")) return -2;
            //if (pc.Status.Additions.ContainsKey("Teleport")) return -30;
            if (pc.EP < 1000) return -2;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            sActor.EP -= 1000;
            if (sActor.EP > sActor.MaxEP) sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);


            map.TeleportActor(sActor, SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height));

            SkillHandler.Instance.ShowEffectByActor(sActor, 4336);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "Teleport", 5000);
            skill.OnAdditionStart += this.StartEvent;
            skill.OnAdditionEnd += this.EndEvent;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }
        void EndEvent(Actor actor, DefaultBuff skill)
        {
        }
        #endregion
    }
}
