
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    /// <summary>
    /// 銅牆鐵壁（亀の構え）
    /// </summary>
    public class PosturetorToise : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 100000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PosturetorToise", lifetime, 1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnUpdate += this.UpdateEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        void UpdateEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.X != (short)skill.Variable["Save_X"] || actor.Y != (short)skill.Variable["Save_Y"])
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                actor.Status.Additions["PosturetorToise"].AdditionEnd();
                actor.Status.Additions.Remove("PosturetorToise");
                skill.Variable.Remove("Save_X");
                skill.Variable.Remove("Save_Y");
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //X
            if (skill.Variable.ContainsKey("Save_X"))
                skill.Variable.Remove("Save_X");
            skill.Variable.Add("Save_X", actor.X);

            //Y
            if (skill.Variable.ContainsKey("Save_Y"))
                skill.Variable.Remove("Save_Y");
            skill.Variable.Add("Save_Y", actor.Y);

            if (skill.Variable.ContainsKey("PosturetorToiseDefUp"))
                skill.Variable.Remove("PosturetorToiseDefUp");
            skill.Variable.Add("PosturetorToiseDefUp", 10 * skill.skill.Level);
            actor.Status.def_skill += (short)(10 * skill.skill.Level);
            actor.Buff.DefUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)(skill.Variable["PosturetorToiseDefUp"]);
            actor.Buff.DefUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
