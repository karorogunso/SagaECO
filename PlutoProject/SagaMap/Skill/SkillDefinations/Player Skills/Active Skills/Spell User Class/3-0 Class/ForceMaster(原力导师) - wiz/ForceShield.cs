using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    /// <summary>
    /// フォースシールド
    /// </summary>
    public class ForceShield : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.type == ActorType.PC && dActor.HP < dActor.MaxHP * 0.4)
            {
                int lifetime = 15000 + 3000 * level;
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "ForceShield", lifetime);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short left_def_add = 15;
            short right_def_add = 80;
            short left_mdef_add = 15;
            short right_mdef_add = 80;
            ActorPC Me = (ActorPC)actor;
            if (Me.Skills.ContainsKey(3113))
            {
                switch (Me.Skills[3113].BaseData.lv)
                {
                    case 1:
                        left_def_add += 3;
                        right_def_add += 5;
                        break;
                    case 2:
                        left_def_add += 3;
                        right_def_add += 10;
                        break;
                    case 3:
                        left_def_add += 6;
                        right_def_add += 10;
                        break;
                    case 4:
                        left_def_add += 6;
                        right_def_add += 15;
                        break;
                    case 5:
                        left_def_add += 9;
                        right_def_add += 15;
                        break;
                }
            }
            if (Me.Skills.ContainsKey(3114))
            {
                switch (Me.Skills[3114].BaseData.lv)
                {
                    case 1:
                        left_mdef_add += 15;
                        right_mdef_add += 5;
                        break;
                    case 2:
                        left_mdef_add += 20;
                        right_mdef_add += 10;
                        break;
                    case 3:
                        left_mdef_add += 25;
                        right_mdef_add += 10;
                        break;
                    case 4:
                        left_mdef_add += 30;
                        right_mdef_add += 15;
                        break;
                    case 5:
                        left_mdef_add += 35;
                        right_mdef_add += 15;
                        break;
                }
            }

            if (skill.Variable.ContainsKey("ForceShield_Left_Def"))
                skill.Variable.Remove("ForceShield_Left_Def");
            skill.Variable.Add("ForceShield_Left_Def", left_def_add);
            actor.Status.def_skill += (short)left_def_add;

            if (skill.Variable.ContainsKey("ForceShield_Right_Def"))
                skill.Variable.Remove("ForceShield_Right_Def");
            skill.Variable.Add("ForceShield_Right_Def", right_def_add);
            actor.Status.def_add_skill += (short)right_def_add;

            if (skill.Variable.ContainsKey("ForceShield_Left_MDef"))
                skill.Variable.Remove("ForceShield_Left_MDef");
            skill.Variable.Add("ForceShield_Left_MDef", left_mdef_add);
            actor.Status.mdef_skill += (short)left_mdef_add;

            if (skill.Variable.ContainsKey("ForceShield_Right_MDef"))
                skill.Variable.Remove("ForceShield_Right_MDef");
            skill.Variable.Add("ForceShield_Right_MDef", right_mdef_add);
            actor.Status.mdef_add_skill += (short)right_mdef_add;
            ActorPC pc = (ActorPC)actor;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("フォースシールド被触发！");
        }

        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["ForceShield_Left_Def"];
            actor.Status.def_add_skill -= (short)skill.Variable["ForceShield_Right_Def"];
            actor.Status.mdef_skill -= (short)skill.Variable["ForceShield_Left_MDef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["ForceShield_Right_MDef"];
            ActorPC pc = (ActorPC)actor;
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("フォースシールド消失了！");
        }


        #endregion
    }
}
