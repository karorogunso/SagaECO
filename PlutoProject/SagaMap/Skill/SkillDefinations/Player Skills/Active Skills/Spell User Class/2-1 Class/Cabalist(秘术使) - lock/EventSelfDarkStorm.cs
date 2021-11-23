
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    /// <summary>
    /// 黑暗火焰（ダークブレイズ）
    /// </summary>
    public class EventSelfDarkStorm : ISkill
    {
        bool MobUse;
        public EventSelfDarkStorm()
        {
            this.MobUse = false;
        }
        public EventSelfDarkStorm(bool MobUse)
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
                level = 1;
            }
            float factor = 2.6f + 1.6f * level;

            short range = (short)((100 * level) + 50);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> affected = map.GetActorsArea(sActor, range, false);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in affected)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, realAffected, args, SagaLib.Elements.Dark, factor);
        }
        #endregion
    }
}