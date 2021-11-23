using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 重力刃 (グラヴィティフォール)
    /// </summary>
    public class GravityFall :  ISkill
    {
        bool MobUse;
        public GravityFall()
        {
            this.MobUse = false;
        }
        public GravityFall(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor =  2.5f + 0.8f * level;
            int rate = 20 + level * 6;
            int rate2 = (4 + level * 2) * 1000;
            short[] pos = new short[2];
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            ActorSkill actor = new ActorSkill(args.skill, sActor);
            //设定技能体位置
            actor.MapID = sActor.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            actor.e = new ActorEventHandlers.NullEventHandler();
            List<Actor> affected = map.GetActorsArea(actor, 250, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
                    {
                        Additions.Global.鈍足 skill = new SagaMap.Skill.Additions.Global.鈍足(args.skill, act, rate2);
                        SkillHandler.ApplyAddition(act, skill);
                        realAffected.Add(act);
                    }
                    else
                    {
                        SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Earth, factor);
                    }
                }
            }
            SkillHandler.Instance.MagicAttack (sActor, realAffected, args, SagaLib.Elements.Earth , factor);            
        }
        #endregion
    }
}
