using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 祷告（Ragnarok）
    /// </summary>
    public class Oratio : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {


            int lifetime = 300000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 1500, true);
             
            foreach (Actor act in affected)
            {
                int numd = SagaLib.Global.Random.Next(1, 100);
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act) && numd <= 60)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, act, "Oratio", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(act, skill);
                    SkillHandler.ApplyAddition(act, skill);
                    EffectArg arg = new EffectArg();
                    arg.effectID = 5445;
                    arg.actorID = act.ActorID;
                    arg.x = args.x;
                    arg.y = args.y;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, sActor, true);
                    
                        

                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("光属性抗性下降");
            }
            actor.Buff.BodyLightElementDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);



        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
            {
                MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("光属性抗性恢复正常");
            }
            actor.Buff.BodyLightElementDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        public void RemoveAddition(Actor actor, String additionName)
        {

        }
        #endregion
    }
}