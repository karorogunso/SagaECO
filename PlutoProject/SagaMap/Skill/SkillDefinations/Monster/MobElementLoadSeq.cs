using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 燃燒的路
    /// </summary>
    public class MobElementLoadSeq : ISkill
    {
        private Elements Element;
        public MobElementLoadSeq(Elements e)
        {
            Element = e;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor=1.8f;
            //ClientManager.EnterCriticalArea();
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            List<Actor> affected = map.GetActorsArea(actor, 100, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    switch(Element)
                    {
                        case Elements.Earth:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stone, 20))
                            {
                                Additions.Global.Stone skill2 = new SagaMap.Skill.Additions.Global.Stone(args.skill, act, 3000);
                                SkillHandler.ApplyAddition(act, skill2);
                            }
                            break;
                        case Elements.Water:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Frosen, 20))
                            {
                                Additions.Global.Freeze skill2 = new SagaMap.Skill.Additions.Global.Freeze(args.skill, act, 3000);
                                SkillHandler.ApplyAddition(act, skill2);
                            }
                            break;
                        case Elements.Fire:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.Stun, 20))
                            {
                                Additions.Global.Stun skill2 = new SagaMap.Skill.Additions.Global.Stun(args.skill, act, 3000);
                                SkillHandler.ApplyAddition(act, skill2);
                            }
                            break;
                        case Elements.Wind:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, act, SkillHandler.DefaultAdditions.鈍足, 20))
                            {
                                Additions.Global.鈍足 skill2 = new SagaMap.Skill.Additions.Global.鈍足(args.skill, act, 3000);
                                SkillHandler.ApplyAddition(act, skill2);
                            }
                            break;
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, Element, factor);
            //ClientManager.LeaveCriticalArea();
            args.dActor = 0xffffffff;
        }
        #endregion
    }
}