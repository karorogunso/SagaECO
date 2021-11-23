using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations.Traveler
{
    public class Silva : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //创建设置型技能技能体
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            //在指定地图注册技能体Actor
            map.RegisterActor(actor);
            //设置Actor隐身属性为非
            actor.invisble = false;
            //广播隐身属性改变事件，以便让玩家看到技能体
            map.SendVisibleActorsToActor(actor);
            map.OnActorVisibilityChange(actor);
            //設置系
            //actor.Stackable = false;
            map.SendActorToMap(actor, map, actor.X, actor.Y);
            //List<Actor> actors = map.GetRoundAreaActors(args.x, args.y, 150);
            //List<Actor> affected = new List<Actor>();
            //affected.Add(actor);
            /*
            args.affectedActors = new List<Actor>();
            args.affectedActors.Add(actor);
            args.Init();
            */
            
            List<Actor> actors = map.GetRoundAreaActors(SagaLib.Global.PosX8to16(args.x,map.Width), SagaLib.Global.PosY8to16(args.y,map.Height),500,true);
            args.affectedActors = actors;
            args.Init();
            
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, args, actor, true);

            //map.DeleteActor(actor);
            
        }

        #endregion

    }
}
