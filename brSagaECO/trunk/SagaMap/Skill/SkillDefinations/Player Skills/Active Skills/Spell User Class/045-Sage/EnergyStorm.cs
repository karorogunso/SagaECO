using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sage
{
    /// <summary>
    /// 魔法暴風雨（エナジーストーム）
    /// </summary>
    public class EnergyStorm : ISkill
    {
        bool MobUse;
        public EnergyStorm()
        {
            this.MobUse = false;
        }
        public EnergyStorm(bool MobUse)
        {
            this.MobUse = MobUse;
        }
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (MobUse)
            {
                level = 5;
            }
            float factor = new float[] { 0, 4.2f, 4.5f, 4.8f, 5.1f, 5.6f }[level];

            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                    affected.Add(i);
            }

            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
