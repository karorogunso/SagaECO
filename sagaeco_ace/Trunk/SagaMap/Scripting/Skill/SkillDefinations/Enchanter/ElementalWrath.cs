using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 精靈之怒 (エレメンタルラース)
    /// </summary>
    public class ElementalWrath: ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (sActor.Status.Additions.ContainsKey("ElementalWrath"))
                return -30;
            else
                return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "ElementalWrath", 75000 - level * 15000);
            SkillHandler.ApplyAddition(sActor, skill);
            float factor =0;
            switch(level)
            {
                case 1:
                    factor = 5.6f;
                    break;
                case 2:
                    factor = 7.4f;
                    break;
                case 3:
                    factor = 9.3f;
                    break;
                case 4:
                    factor = 11.0f;
                    break;
                case 5:
                    factor = 13.0f;
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            List<Actor> affected = map.GetActorsArea(actor, 300, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
