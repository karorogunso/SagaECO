using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    public class SoulTaker : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (!dActor.Status.Additions.ContainsKey("SoulTaker"))
            {
                int lifetime = (30 + level * 30) * 1000;
                int rate = 175 + level * 25;
                SoulTakerBuff skill = new SoulTakerBuff(args.skill, sActor, lifetime, rate);
                //skill.OnUpdate += this.UpdateEventHandler;
                SkillHandler.ApplyAddition(sActor, skill);
            }
            else
            {
                sActor.Status.Additions["SoulTaker"].OnTimerEnd();
            }
                
        }
        //void UpdateEventHandler(Actor actor, DefaultBuff skill)
        //{
        //    //SagaMap.Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("当前速度为" + actor.Speed);
        //    if (actor.Speed != 310)
        //    {
        //        actor.Status.speed_skill = -100;
        //        actor.Status.speed_item = 0;
        //    }
        //}
        public class SoulTakerBuff : DefaultBuff
        {
            public SoulTakerBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int rate)
                : base(skill, actor, "SoulTaker", lifetime, 1000)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["rate"] = rate;
            }
            void StartEvent(Actor actor, DefaultBuff skill)
            {
                int level = skill.skill.Level;
                int rate = 175 + level * 25;


                if (skill.Variable.ContainsKey("SoulTaker"))

                    skill.Variable.Remove("SoulTaker");

                skill.Variable.Add("SoulTaker", rate);
                //Speed Limit 
                int speed_add = 100;
                if (skill.Variable.ContainsKey("SoulTaker_speed"))
                    skill.Variable.Remove("SoulTaker_speed");
                skill.Variable.Add("SoulTaker_speed", speed_add);
                actor.Status.speed_skill -= (ushort)speed_add;
                actor.Buff.MainSkillPowerUp3RD = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                if (skill.Variable.ContainsKey("SoulTaker"))
                    skill.Variable.Remove("SoulTaker");
                actor.Status.speed_skill += (ushort)skill.Variable["SoulTaker_speed"];
                if (skill.Variable.ContainsKey("SoulTaker_speed"))
                    skill.Variable.Remove("SoulTaker_speed");
                actor.Buff.MainSkillPowerUp3RD = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
    }
}