using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Monster
{
    /// <summary>
    /// 應援
    /// </summary>
    public class MobAtkupOne : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 60000;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.MOB)
                {
                    realAffected.Add(act);
                }
            }
            realAffected.Add(sActor);
            foreach (Actor i in realAffected)
            {
                if (!i.Status.Additions.ContainsKey("MobAtkupOne"))
                {
                    DefaultBuff skill1 = new DefaultBuff(args.skill, i, "MobAtkupOne", lifetime);
                    skill1.OnAdditionStart += this.StartEventHandler;
                    skill1.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(i, skill1);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill += (short)10;

            //最大攻擊
            actor.Status.max_atk2_skill += (short)10;

            //最大攻擊
            actor.Status.max_atk3_skill += (short)10;

            //最小攻擊
            actor.Status.min_atk1_skill += (short)10;

            //最小攻擊
            actor.Status.min_atk2_skill += (short)10;

            //最小攻擊
            actor.Status.min_atk3_skill += (short)10;

            //最大魔攻
            actor.Status.max_matk_skill += (short)10;
               
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)10;

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)10;

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)10;

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)10;

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)10;

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)10;

            //最大魔攻
            actor.Status.max_matk_skill -= (short)10;
        }
        #endregion
    }
}
