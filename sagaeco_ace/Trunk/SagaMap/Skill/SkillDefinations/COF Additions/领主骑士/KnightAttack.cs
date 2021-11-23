using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.X
{
    public class KnightAttack : MobISkill
    {
        #region ISkill Members

        public void BeforeCast(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(dActor, 100, true, true);
            List<Actor> realAffected = new List<Actor>();
            foreach (Actor act in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, act))
                {
                    realAffected.Add(act);
                    if (act.Status.Additions.ContainsKey("Parry") && SagaLib.Global.Random.Next(0, 10) < 5)
                    {
                        act.Status.Additions.Remove("Parry");
                        DefaultBuff skill = new DefaultBuff(args.skill, act, "KnightAttack", 5000);
                        skill.OnAdditionStart += this.StartEventHandler;
                        skill.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(act, skill);
                    }
                }
            }
            if (SagaLib.Global.Random.Next(0, 100) < 8)
            {
                float HpRate = 0.005f * realAffected.Count;
                int hpadd = (int)(sActor.MaxHP * HpRate);

                SkillHandler.Instance.ShowVessel(sActor, -hpadd);

                SkillArg arg2 = new SkillArg();
                arg2 = args.Clone();
                SkillHandler.Instance.FixAttack(sActor, sActor, arg2, SagaLib.Elements.Holy, -hpadd);
            }
            SkillHandler.Instance.PhysicalAttack(sActor, realAffected, args, SagaLib.Elements.Neutral, 1f);
        }
        #endregion
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int value = actor.Status.def;
            if (skill.Variable.ContainsKey("KnightAttackDownDef"))
                skill.Variable.Remove("KnightAttackDownDef");
            skill.Variable.Add("KnightAttackDownDef", value);
            actor.Status.def_add_skill -= (short)value;
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_add_skill += (short)skill.Variable["KnightAttackDownDef"];
        }
    }
}
