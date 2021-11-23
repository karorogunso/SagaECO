
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    /// <summary>
    /// 血色战刃
    /// </summary>
    public class HpLostDamUp : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 5000 + 5000 * level;
            int HPLost = new int[] { 0, 50, 100, 500 }[level];
            int DamUp = new int[] { 0, 50, 100, 500 }[level];
            HpLostDamUpBuff skill = new HpLostDamUpBuff(args.skill, dActor, lifetime, HPLost, DamUp);
            SkillHandler.ApplyAddition(dActor, skill);
        }
        #endregion
        #region HpLostDamUpBuff
        public class HpLostDamUpBuff : DefaultBuff
        {
            public HpLostDamUpBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int HPLost, int DamUp)
                : base(skill, actor, "HpLostDamUp", lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["DamUp"] = DamUp;
                this["HPLost"] = HPLost;
            }

            void StartEvent(Actor actor, DefaultBuff skill)
            {
                actor.Buff.BloodyWeapon = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }

            void EndEvent(Actor actor, DefaultBuff skill)
            {
                actor.Buff.BloodyWeapon = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
        #endregion
    }
}
