using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.SunFlowerAdditions
{
    /// <summary>
    /// 霸邪之阵（Ragnarok）
    /// </summary>
    public class KyrieEleison : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //float factor = -14.3f;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);//实测7*7范围内怪物互补情况太差,更改为11*11
            List<Actor> realAffected = new List<Actor>();
            Actor ActorlowHP = sActor;
            foreach (Actor act in affected)
            {
                if ((float)(act.HP / act.MaxHP) < (float)(ActorlowHP.HP / ActorlowHP.MaxHP) && !SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    int lifetime = 120000;
                    DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MobKyrie", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }

        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            
            skill["MobKyrie"] = 10;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

        }
        #endregion
    }
}
