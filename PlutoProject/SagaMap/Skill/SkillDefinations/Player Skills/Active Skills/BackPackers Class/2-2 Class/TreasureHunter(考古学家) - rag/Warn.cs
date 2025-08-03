using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.TreasureHunter
{
    /// <summary>
    /// 警戒（警戒）
    /// </summary>
    public class Warn : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }

        bool CheckPossible(Actor sActor)
        {
            return true;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] totals = new int[] { 0, 40, 60, 80, 100, 120 };
            int lifetime = 1000 * (totals[level]);
            args.dActor = 0;
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            if (CheckPossible(realdActor))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "Warn", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(realdActor, skill);
            }

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("ST_LEFT_DEF"))
                skill.Variable.Remove("ST_LEFT_DEF");
            skill.Variable.Add("ST_LEFT_DEF", 7 + skill.skill.Level);
            actor.Status.def_skill += (short)(7 + skill.skill.Level);

            //if (skill.Variable.ContainsKey("ST_CTI_AVOID"))
            //    skill.Variable.Remove("ST_CTI_AVOID");
            //skill.Variable.Add("ST_CTI_AVOID", 255);
            //actor.Status.cri_avoid_tit += (short)(255);

            //actor.Buff.DefUp = true;
            actor.Buff.Warning = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["ST_LEFT_DEF"];
            //actor.Status.cri_avoid_tit -= (short)skill.Variable["ST_CTI_AVOID"];

            actor.Buff.Warning = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}