
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Event
{
    /// <summary>
    /// 點火
    /// </summary>
    public class MoveUp5 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 90000;
            dActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MoveUp5", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //Clear All MoveUp
            if (skill.Variable.ContainsKey("MoveUp2_Speed"))
                actor.Status.Additions["MoveUp2_Speed"].AdditionEnd();


            if (skill.Variable.ContainsKey("MoveUp3_Speed"))
                actor.Status.Additions["MoveUp3_Speed"].AdditionEnd();

            if (skill.Variable.ContainsKey("MoveUp4_Speed"))
                actor.Status.Additions["MoveUp4_Speed"].AdditionEnd();

            if (skill.Variable.ContainsKey("MoveUp5_Speed"))
                actor.Status.Additions["MoveUp5_Speed"].AdditionEnd();

            //移動速度
            int Speed_add = 160;
            if (skill.Variable.ContainsKey("MoveUp5_Speed"))
                skill.Variable.Remove("MoveUp5_Speed");
            skill.Variable.Add("MoveUp5_Speed", Speed_add);

            actor.Status.speed_skill += (ushort)Speed_add;
            actor.Buff.点火紫火 = true;
            actor.Buff.MoveSpeedUp = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //移動速度
            actor.Status.speed_skill -= (ushort)skill.Variable["MoveUp5_Speed"];
            actor.Buff.点火紫火 = false;
            actor.Buff.MoveSpeedUp = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
