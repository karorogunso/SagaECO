using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// フリークブラスト
    /// </summary>
    public class ThunderSpray : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 2.0f + 1.0f * level;
            int[] lifetime = { 0, 4000, 6000, 8000, 10000, 12000 };
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills.ContainsKey(3123))
                    factor += 0.7f;
            }
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 200, true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                {
                    affected.Add(i);
                    if (!SkillHandler.Instance.isBossMob(i))
                    {
                        switch (SagaLib.Global.Random.Next(1, 3))
                        {
                            case 1:
                                Additions.Global.Stone Stone = new SagaMap.Skill.Additions.Global.Stone(null, i, lifetime[level]);
                                SkillHandler.ApplyAddition(i, Stone);
                                break;
                            case 2:
                                Additions.Global.Poison Poison = new SagaMap.Skill.Additions.Global.Poison(null, i, lifetime[level]);
                                SkillHandler.ApplyAddition(i, Poison);
                                break;
                            case 3:
                                Additions.Global.MoveSpeedDown Sleep = new SagaMap.Skill.Additions.Global.MoveSpeedDown(null, i, lifetime[level]);
                                SkillHandler.ApplyAddition(i, Sleep);
                                break;
                        }
                    }
                }
            }
            SkillHandler.Instance.MagicAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
        }
        #endregion
    }
}
