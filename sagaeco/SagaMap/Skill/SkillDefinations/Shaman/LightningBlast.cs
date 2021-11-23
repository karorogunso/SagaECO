using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class LightningBlast:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 1.3f + 0.2f*level;
            List<Actor> actors = Manager.MapManager.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            //取得有效Actor（即怪物）
            foreach (Actor i in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,i))
                    affected.Add(i);
            }
            if (sActor.type == ActorType.PC)
            {
                if (SagaLib.Global.Random.Next(0, 100) < 60)
                {
                    SkillArg add = new SkillArg();
                    add = args.Clone();
                    SkillHandler.Instance.MagicAttack(sActor, dActor, add, SagaLib.Elements.Wind, 0.9f+0.1f*level);
                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, add, sActor, true);
                    EffectArg arg = new EffectArg();
                    arg.effectID = 5113;
                    arg.actorID = dActor.ActorID;
                    if (sActor.type == ActorType.PC)
                        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Wind, factor);

        }

        #endregion
    }
}
