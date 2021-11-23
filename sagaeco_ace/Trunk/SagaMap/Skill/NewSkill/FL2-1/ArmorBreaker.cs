using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.FL2_1
{

    public class ArmorBreaker : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            float factor = 0;
            args.type = ATTACK_TYPE.BLOW;
            factor = 1.4f;
            SkillHandler.Instance.PhysicalAttack(sActor, dActor, args, SagaLib.Elements.Neutral, factor);
            if ((args.flag[0] & SagaLib.AttackFlag.HP_DAMAGE) != 0)
            {
                int rate = 100;
                int lifetime = 0;
                switch (level)
                {
                    case 1:
                        rate = 100;
                        lifetime = 10000;
                        break;
                    case 2:
                        rate = 150;
                        lifetime = 12000;
                        break;
                    case 3:
                        rate = 200;
                        lifetime = 14000;
                        break;
                    case 4:
                        rate = 250;
                        lifetime = 16000;
                        break;
                    case 5:
                        rate = 300;
                        lifetime = 18000;
                        break;
                }
                lifetime=SkillHandler.Instance.AdditionApply(sActor, dActor, rate, lifetime, SkillHandler.异常状态.无);
                if (lifetime > 0)
                {
                    Logger.ShowError(dActor.Status.def_add_skill.ToString());
                    DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ArmorBreaker", lifetime);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(dActor, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;

            int def_add = -(10 * level);
            if (skill.Variable.ContainsKey("ArmorBreaker"))
                skill.Variable.Remove("ArmorBreaker");
            skill.Variable.Add("ArmorBreaker", def_add);
            actor.Status.def_add_skill += (short)def_add;
            actor.Buff.DefDown = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

            actor.Status.def_add_skill -= (short)skill.Variable["ArmorBreaker"];
            actor.Buff.DefDown = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
