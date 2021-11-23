
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    /// <summary>
    /// 刀劍亂舞（リミテイションエッジ）
    /// </summary>
    public class EdgedSlash : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, dActor) && SkillHandler.Instance.CheckSkillCanCastForWeapon(sActor, args))
                return 0;
            else
            {
                return -14;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.0f + 0.5f * level;
            List<Actor> affected = new List<Actor>();
            affected.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SkillHandler.DefType.IgnoreDefRight, SagaLib.Elements.Neutral, 0, factor, false);

            SagaMap.Skill.Additions.Global.DefaultBuff buff = new Additions.Global.DefaultBuff(args.skill, sActor, "碎金斬", 15000);
            buff.OnAdditionStart += this.StartEventHandler;
            buff.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(sActor, buff);
        }
        void StartEventHandler(Actor actor, SagaMap.Skill.Additions.Global.DefaultBuff skill)
        {
            int[] values = { 0, 60, 96, 133, 188, 228 };
            if (skill.Variable.ContainsKey("碎金斬pf"))
                skill.Variable.Remove("碎金斬pf");
            skill.Variable.Add("碎金斬pf", values[skill.skill.Level]);

            actor.Status.def_add_skill -= (short)values[skill.skill.Level];

            actor.Buff.防御力減少 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, SagaMap.Skill.Additions.Global.DefaultBuff skill)
        {
            actor.Status.def_add_skill += (short)skill.Variable["碎金斬pf"];

            actor.Buff.防御力減少 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}