
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
    public class MoveUp3 : ISkill
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
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MoveUp3", lifetime);
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
            int Speed_add = 110;
            if (skill.Variable.ContainsKey("MoveUp3_Speed"))
                skill.Variable.Remove("MoveUp3_Speed");
            skill.Variable.Add("MoveUp3_Speed", Speed_add);

            actor.Status.speed_skill += (ushort)Speed_add;
            actor.Buff.PapaIgintion = true;
            actor.Buff.MoveSpeedUp = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);

            //移動速度
            actor.Status.speed_skill -= (ushort)skill.Variable["MoveUp3_Speed"];
            actor.Buff.PapaIgintion = false;
            actor.Buff.MoveSpeedUp = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        #endregion
    }
}
