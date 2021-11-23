using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    class FireStorm:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.5f + 0.5f * level;
            ActorSkill actorS = new ActorSkill(args.skill, sActor);
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, null);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    Skill.Additions.Global.Firing1 firing = new Additions.Global.Firing1(args.skill, sActor, i, 10000, SkillHandler.Instance.CalcDamage(false, sActor, dActor, args, SkillHandler.DefType.MDef, Elements.Fire, 50, 0.2f));
                    SkillHandler.ApplyAddition(i, firing);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Fire, factor);
        }
        #endregion
    }
}
