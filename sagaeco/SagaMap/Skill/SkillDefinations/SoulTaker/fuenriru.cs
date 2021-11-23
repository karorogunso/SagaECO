using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations.SoulTaker
{
    class fuenriru : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 300, true);
            List<Actor> actors2 = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 300, null);
            List<Actor> affected = new List<Actor>();
            switch (level)
            {
                case 1:
                    foreach (Actor i in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            affected.Add(i);
                    }
                    SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Earth, 11.0f);
                    break;
                case 2:

                    break;
                case 3:
                    foreach (Actor i in actors2)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            affected.Add(i);
                    }
                    SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Earth, 20.0f);
                    break;

            }
        }
        #endregion
    }
}
