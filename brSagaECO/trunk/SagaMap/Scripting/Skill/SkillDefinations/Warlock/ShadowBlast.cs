using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Warlock
{
    public class ShadowBlast:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = 1.8f;
                    break;
                case 2:
                    factor = 2.1f;
                    break;
                case 3:
                    factor = 2.4f;
                    break;
                case 4:
                    factor = 2.7f;
                    break;
                case 5:
                    factor = 3.0f;
                    break;
            }
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    if (dActor.Darks == 1)
                    {
                        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEffect(dActor, 5202);
                        SkillArg add = new SkillArg();
                        add.argType = SkillArg.ArgType.Actor_Active;
                        add.skill = args.skill;
                        SkillHandler.Instance.MagicAttack(sActor, i, add, SagaLib.Elements.Dark, 1.1f + 0.1f * level);
                    }
                }
            }
                dActor.Darks = 0;
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Dark, factor);
        }

        #endregion
    }
}
