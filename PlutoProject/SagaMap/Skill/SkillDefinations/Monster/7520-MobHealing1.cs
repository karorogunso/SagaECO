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
    /// 治愈术(强)
    /// </summary>
    public class MobHealing1 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            if (SkillHandler.Instance.isBossMob(sActor))
            {
                factor = -35.8f;
            }
            else
            {
                factor = -3.58f;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, 500, false);//实测7*7范围内怪物互补情况太差,更改为11*11
            List<Actor> realAffected = new List<Actor>();
            Actor ActorlowHP = sActor;
            foreach (Actor act in affected)
            {
                if (act.type == ActorType.MOB)
                {
                    if ((float)(act.HP / act.MaxHP) < (float)(ActorlowHP.HP / ActorlowHP.MaxHP) && act.type == ActorType.MOB)
                    {
                        ActorlowHP = act;
                    }
                }
                    
            }
            SkillHandler.Instance.MagicAttack(sActor, ActorlowHP, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factor);
            //SkillHandler.Instance.FixAttack(sActor, ActorlowHP, args, SagaLib.Elements.Holy, factor);
        }
        #endregion
    }
}