using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    /// <summary>
    /// 神經衰弱（クラッター）
    /// </summary>
    public class MagIntDexDownOne:ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int rate = 10 + 10 * level;
            if (SkillHandler.Instance.CanAdditionApply(sActor, dActor, "MagIntDexDownOne", rate))
            {
                DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MagIntDexDownOne", 30000);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                SkillHandler.ApplyAddition(dActor, skill);
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
           int level=skill.skill.Level;
           //INT
           int int_add=-(7+level);
           if (skill.Variable.ContainsKey("MagIntDexDownOne_int"))
               skill.Variable.Remove("MagIntDexDownOne_int");
           skill.Variable.Add("MagIntDexDownOne_int", int_add);
           actor.Status.int_skill = (short)int_add;
        
           //MAG
           int mag_add = -(7 + level);
           if (skill.Variable.ContainsKey("MagIntDexDownOne_mag"))
               skill.Variable.Remove("MagIntDexDownOne_mag");
           skill.Variable.Add("MagIntDexDownOne_mag", mag_add);
           actor.Status.mag_skill = (short)mag_add;
        
           //DEX
           int dex_add = -(11 + level);
           if (skill.Variable.ContainsKey("MagIntDexDownOne_dex"))
               skill.Variable.Remove("MagIntDexDownOne_dex");
           skill.Variable.Add("MagIntDexDownOne_dex", dex_add);
           actor.Status.dex_skill = (short)dex_add;
     
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //INT
            actor.Status.int_skill -= (short)skill.Variable["MagIntDexDownOne_int"];

            //MAG
            actor.Status.mag_skill -= (short)skill.Variable["MagIntDexDownOne_mag"];

            //DEX
            actor.Status.dex_skill -= (short)skill.Variable["MagIntDexDownOne_dex"];
              
        }
        #endregion
    }
}
