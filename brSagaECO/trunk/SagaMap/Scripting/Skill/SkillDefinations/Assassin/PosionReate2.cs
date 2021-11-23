using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    ///  硬化毒（硬化毒）
    /// </summary>
    public class PosionReate2 : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            uint itemID=10000354;//刺客的內服藥（硬化毒）
            if (SkillHandler.Instance.CountItem(pc, itemID) >0)
            {
                SkillHandler.Instance.TakeItem(pc, itemID, 1);
                return 0;
            }
            return -57;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifeTime = (100-10*level ) *1000;
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "PoisonReate2", lifeTime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level, rate=50;
            float[] lDown = {0f, 0.05f, 0.05f, 0.08f, 0.08f, 0.11f };
            float[] rDown = {0f, 0.1f,0.12f, 0.14f, 0.16f, 0.18f };
            //左防禦
            int def_add = (int)( actor.Status.def * lDown[level]);
            if (skill.Variable.ContainsKey("PosionReate2_def"))
                skill.Variable.Remove("PosionReate2_def");
            skill.Variable.Add("PosionReate2_def", def_add);
            actor.Status.def_skill = (short)def_add;

            //右防禦
            int def_add_add =(int)( actor.Status.def_add * rDown[level]);
            if (skill.Variable.ContainsKey("PosionReate2_def_add"))
                skill.Variable.Remove("PosionReate2_def_add");
            skill.Variable.Add("PosionReate2_def_add", def_add_add);
            actor.Status.def_add_skill = (short)def_add_add;
            
            //中毒?
            if (SkillHandler.Instance.CanAdditionApply(actor,actor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Additions.Global.Poison nskill = new SagaMap.Skill.Additions.Global.Poison(skill.skill , actor, 7000);
                SkillHandler.ApplyAddition(actor, nskill);
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //左防禦
            actor.Status.def_skill -= (short)skill.Variable["PosionReate2_def"];

            //右防禦
            actor.Status.def_add_skill -= (short)skill.Variable["PosionReate2_def_add"];
       
        }
        #endregion
    }
}



                                          