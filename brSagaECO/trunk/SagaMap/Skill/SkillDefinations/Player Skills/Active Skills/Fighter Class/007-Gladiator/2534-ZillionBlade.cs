using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;

namespace SagaMap.Skill.SkillDefinations.Gladiator
{
    /// <summary>
    /// ジリオンブレイド
    /// </summary>
    public class ZillionBlade : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = new float[] { 0, 10f, 20f, 22.4f, 35f, 42f }[level];
            if (sActor is ActorPC)
            {
                ActorPC pc = sActor as ActorPC;
                if (pc.Skills3.ContainsKey(1117))
                {
                    //神速斩
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2527);
                    //一閃
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2400);
                    //居合
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2115);
                    //居合2
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2201);
                    //居合3
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2202);
                    //百鬼哭
                    SkillHandler.Instance.SetNextComboSkill(sActor, 2235);
                }
            }
            List<Actor> actors = Manager.MapManager.Instance.GetMap(sActor.MapID).GetActorsArea(dActor, 100, true);
            List<Actor> affected = new List<Actor>();
            if (level % 2 == 1)
                foreach (var item in actors)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                        affected.Add(item);
                }
            else
                affected.Add(dActor);
            SkillHandler.Instance.PhysicalAttack(sActor, affected, args, SagaLib.Elements.Neutral, factor);
        }
    }
}
