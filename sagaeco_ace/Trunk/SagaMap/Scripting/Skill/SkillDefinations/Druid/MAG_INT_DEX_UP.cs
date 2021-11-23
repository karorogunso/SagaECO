using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 心靈豐足（ラウズメンタル）
    /// </summary>
    public class MAG_INT_DEX_UP : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime ={15, 20, 25, 27, 30};
            DefaultBuff skill = new DefaultBuff(args.skill, dActor, "MAG_INT_DEX_UP", lifetime[level - 1]*1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(dActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            short[] DEX={6, 8, 10, 12, 14}; 
            short[] INT={6, 7, 8, 10, 11};
            short[] MAG={5, 6, 7, 9, 10}; 

            //DEX
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_DEX"))
                skill.Variable.Remove("MAG_INT_DEX_UP_DEX");
            skill.Variable.Add("MAG_INT_DEX_UP_DEX", DEX[level-1]);
            actor.Status.dex_skill += DEX[level - 1];
            //INT
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_INT"))
                skill.Variable.Remove("MAG_INT_DEX_UP_INT");
            skill.Variable.Add("MAG_INT_DEX_UP_INT", INT[level - 1]);
            actor.Status.int_skill += INT[level - 1];
            //MAG
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_MAG"))
                skill.Variable.Remove("MAG_INT_DEX_UP_MAG");
            skill.Variable.Add("MAG_INT_DEX_UP_MAG", MAG[level - 1]);
            actor.Status.mag_skill += MAG[level - 1];
            actor.Buff.MAG上昇 = true;
            actor.Buff.INT上昇 = true;
            actor.Buff.DEX上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

            actor.Status.dex_skill -= (short)skill.Variable["MAG_INT_DEX_UP_DEX"];
            actor.Status.int_skill -= (short)skill.Variable["MAG_INT_DEX_UP_INT"];
            actor.Status.mag_skill -= (short)skill.Variable["MAG_INT_DEX_UP_MAG"];
            actor.Buff.MAG上昇 = false;
            actor.Buff.INT上昇 = false;
            actor.Buff.DEX上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
