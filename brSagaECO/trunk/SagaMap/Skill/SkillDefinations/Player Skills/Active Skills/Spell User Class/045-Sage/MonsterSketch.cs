
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 魔物素描（モンスタースケッチ）
    /// </summary>
    public class MonsterSketch : ISkill
    {
        uint SKETCHBOOK = 10020757;//畫板
        uint SKETCHBOOK_Finish = 10020758;//畫板（畫作完成）
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if (dActor.type == ActorType.MOB)
            {

                if (SkillHandler.Instance.CountItem(sActor, SKETCHBOOK) > 0)
                {
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -12;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.TakeItem((ActorPC)sActor, SKETCHBOOK, 1);
            List<SagaDB.Item.Item> r = SkillHandler.Instance.GiveItem((ActorPC)sActor, SKETCHBOOK_Finish, 1, true);
            ActorMob mob=(ActorMob)dActor;
            r[0].PictID = mob.MobID;
            SkillHandler.Instance.AttractMob(sActor, dActor, 1);
        }
        #endregion
    }
}