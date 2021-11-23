    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S31170: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 15f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> targets = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), 200, true).Where(tgt=> SkillHandler.Instance.CheckValidAttackTarget(sActor, tgt)).ToList();
            foreach (var item in targets)
            {
                //SkillHandler.Instance.ShowEffectOnActor(item, 5266);
                int damage = SkillHandler.Instance.MagicAttack(sActor, item, args, Elements.Fire, factor);
                if (item.Status.Additions.ContainsKey("极寒之槛"))
                    SkillHandler.RemoveAddition(item, "极寒之槛");
                else if (damage > 0)
                {
                    if (item.Status.Additions.ContainsKey("Burning"))
                        SkillHandler.RemoveAddition(item, "Burning");
                    Burning b = new Burning(null, item, 4000, damage / 2);
                    SkillHandler.ApplyAddition(item, b);
                }
            }
        }
        #endregion
    }
}
