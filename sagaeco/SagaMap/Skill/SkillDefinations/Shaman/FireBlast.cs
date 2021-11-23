using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Shaman
{
    public class FireBlast:ISkill
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
                    if (sActor.type == ActorType.PC)
                    {
                        if (((ActorPC)sActor).Skills.ContainsKey(3006))
                        {
                            SkillArg add = new SkillArg();
                            add = args.Clone();
                            SkillHandler.Instance.MagicAttack(sActor, i, add, SagaLib.Elements.Fire, 0.18f * ((ActorPC)sActor).Skills[3006].Level);
                            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, add, sActor, true);
                            EffectArg arg = new EffectArg();
                            arg.effectID = 5115;
                            arg.actorID = i.ActorID;
                            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, (ActorPC)sActor, true);
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Fire, factor);
        }

        #endregion
    }
}
