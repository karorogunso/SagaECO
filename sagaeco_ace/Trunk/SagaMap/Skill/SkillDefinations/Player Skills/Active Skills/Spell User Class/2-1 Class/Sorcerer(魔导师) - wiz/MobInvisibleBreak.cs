
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 透明化感知（インビジブルブレイク）
    /// </summary>
    public class MobInvisibleBreak : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            List<Actor> affected1 = map.GetActorsArea(actor, 300, false);
            List<Actor> affected2 = map.GetActorsArea(sActor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected1)
            {
                if (act.Buff.Transparent)
                {
                    SetVisible(act,map);
                }
            }
            foreach (Actor act in affected2)
            {
                if (act.Buff.Transparent)
                {
                    SetVisible(act,map);
                }
            }
        }
        public void SetVisible(Actor act, Map map)
        {
            act.Buff.Transparent = false;
            if (act.Status.Additions.ContainsKey("Hiding"))
                act.Status.Additions["Hiding"].AdditionEnd();
            if (act.Status.Additions.ContainsKey("Cloaking"))
                act.Status.Additions["Cloaking"].AdditionEnd();
            if (act.Status.Additions.ContainsKey("Invisible"))
                act.Status.Additions["Invisible"].AdditionEnd();
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, act, true);
        }
        #endregion
    }
}