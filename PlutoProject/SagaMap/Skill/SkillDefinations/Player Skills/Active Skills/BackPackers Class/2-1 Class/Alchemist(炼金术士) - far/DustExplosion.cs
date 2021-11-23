
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    /// <summary>
    /// 塵土爆發（ダストエクスプロージョン）
    /// </summary>
    public class DustExplosion : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            float factor = 4.0f + 0.5f * level;
            int lifetime = 3000 + 2000 * level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, item, "DustExplosion", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(item, skill);
                    affected.Add(item);
                }
                
            }
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, sActor.WeaponElement, factor);
            //float factor = 0.5f + 0.5f * level;
            //int lifetime = 2500 + 1500 * level;
            //int rate = 25 + 5 * level;
            //Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //ActorSkill actor = new ActorSkill(args.skill, sActor);
            ////设定技能体位置
            //actor.MapID = sActor.MapID;
            //actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            //actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            ////设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
            //actor.e = new ActorEventHandlers.NullEventHandler();
            //List<Actor> affected = map.GetActorsArea(actor, 350, false);
            //List<Actor> realAffected = new List<Actor>();
            //foreach (Actor act in affected)
            //{
            //    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
            //    {
            //        if (SagaLib.Global.Random.Next(0, 99) < rate)
            //        {
            //            DefaultBuff skill = new DefaultBuff(args.skill, act, "DustExplosion", lifetime);
            //            skill.OnAdditionStart += this.StartEventHandler;
            //            skill.OnAdditionEnd += this.EndEventHandler;
            //            SkillHandler.ApplyAddition(act, skill);
            //        }
            //        realAffected.Add(act);
            //    }
            //}
            //SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, sActor.WeaponElement, factor);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            //近命中
            int hit_melee_add = (int)(actor.Status.hit_melee * (0.2f + 0.05f * level));
            if (skill.Variable.ContainsKey("DustExplosion_hit_melee"))
                skill.Variable.Remove("DustExplosion_hit_melee");
            skill.Variable.Add("DustExplosion_hit_melee", hit_melee_add);
            actor.Buff.ShortHitDown = true;
            actor.Status.hit_melee_skill -= (short)hit_melee_add;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //近命中
            actor.Status.hit_melee_skill += (short)skill.Variable["DustExplosion_hit_melee"];
            actor.Buff.ShortHitDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
