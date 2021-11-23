using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.Maestro
{
    class WeaponAtkUp : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime = {0,120000,120000,180000,180000,210000};
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "WeaponAtkUp", lifetime[level]);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            ushort up = (ushort)(5 + 10 * skill.skill.Level);
            if (skill.Variable.ContainsKey("WeaponAtkUp"))
                skill.Variable.Remove("WeaponAtkUp");
            skill.Variable.Add("WeaponAtkUp", (int)up);
            actor.Status.max_atk1 += up;
            actor.Status.max_atk2 += up;
            actor.Status.max_atk3 += up;
            actor.Status.min_atk1 += up;
            actor.Status.min_atk2 += up;
            actor.Status.min_atk3 += up;
            actor.Buff.三转红锤子ウェポンエンハンス = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Status.max_atk2 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Status.max_atk3 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Status.min_atk1 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Status.min_atk2 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Status.min_atk3 -= (ushort)skill.Variable["WeaponAtkUp"];
            actor.Buff.三转红锤子ウェポンエンハンス = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
//if (i.Status.Additions.ContainsKey("イレイザー") 