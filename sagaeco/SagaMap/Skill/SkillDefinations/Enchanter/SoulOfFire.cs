using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    /// <summary>
    /// 火焰勢力（ファイアオーラ）
    /// </summary>
    public class SoulOfFire : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "SoulOfFire", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short MinAtk = 0, MaxAtk = 0;
            int level = skill.skill.Level;
            switch (level)
            {
                case 1:
                    MinAtk = 10;
                    break;
                case 2:
                    MinAtk = 20;
                    break;
                case 3:
                    MinAtk = 30;
                    MaxAtk = 40;
                    break;
            }

            //最大攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk1"))
                skill.Variable.Remove("SoulOfFire_max_atk1");
            skill.Variable.Add("SoulOfFire_max_atk1", MinAtk);
            actor.Status.max_atk1_skill += (short)MinAtk;

            //最大攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk2"))
                skill.Variable.Remove("SoulOfFire_max_atk2");
            skill.Variable.Add("SoulOfFire_max_atk2", MinAtk);
            actor.Status.max_atk2_skill += (short)MinAtk;

            //最大攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk3"))
                skill.Variable.Remove("SoulOfFire_max_atk3");
            skill.Variable.Add("SoulOfFire_max_atk3", MinAtk);
            actor.Status.max_atk3_skill += (short)MinAtk;

            //最小攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk1"))
                skill.Variable.Remove("SoulOfFire_min_atk1");
            skill.Variable.Add("SoulOfFire_min_atk1", MinAtk);
            actor.Status.min_atk1_skill += (short)MinAtk;

            //最小攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk2"))
                skill.Variable.Remove("SoulOfFire_min_atk2");
            skill.Variable.Add("SoulOfFire_min_atk2", MinAtk);
            actor.Status.min_atk2_skill += (short)MinAtk;

            //最小攻擊
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk3"))
                skill.Variable.Remove("SoulOfFire_min_atk3");
            skill.Variable.Add("SoulOfFire_min_atk3", MinAtk);
            actor.Status.min_atk3_skill = (short)MinAtk;
                                        

            actor.Buff.AtkMinUp = true;
            actor.Buff.AtkMaxUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //最大攻擊
            actor.Status.max_atk1_skill -= (short)skill.Variable["SoulOfFire_max_atk1"];

            //最大攻擊
            actor.Status.max_atk2_skill -= (short)skill.Variable["SoulOfFire_max_atk2"];

            //最大攻擊
            actor.Status.max_atk3_skill -= (short)skill.Variable["SoulOfFire_max_atk3"];

            //最小攻擊
            actor.Status.min_atk1_skill -= (short)skill.Variable["SoulOfFire_min_atk1"];

            //最小攻擊
            actor.Status.min_atk2_skill -= (short)skill.Variable["SoulOfFire_min_atk2"];

            //最小攻擊
            actor.Status.min_atk3_skill -= (short)skill.Variable["SoulOfFire_min_atk3"];

            actor.Buff.AtkMinUp = false;
            actor.Buff.AtkMaxUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
