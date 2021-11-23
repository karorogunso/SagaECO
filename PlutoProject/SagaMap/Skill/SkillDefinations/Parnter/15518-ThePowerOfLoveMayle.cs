
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 想いの力・メイリー
    /// </summary>
    public class ThePowerOfLoveMayle : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            int lifetime = 0;
            Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
            if (sActor.type == ActorType.PARTNER)
            {
                lifetime = 12000;
                List<Actor> affected = map.GetActorsArea(sActor, 500, false);
                Actor ActorlowHP = sActor;
                foreach (Actor act in affected)
                {
                    if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                    {
                        int a = SagaLib.Global.Random.Next(0, 99);
                        if (a < 40)
                        {
                            dActor = act;
                        }
                    }

                }

            }

            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            skill["MobKyrie"] = 12;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            skill["MobKyrie"] = 0;
        }
        #endregion
    }
}
