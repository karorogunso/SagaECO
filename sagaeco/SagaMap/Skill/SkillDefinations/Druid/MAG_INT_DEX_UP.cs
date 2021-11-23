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
            if (pc.Status.Additions.ContainsKey("EvilSoul"))
            {
                return -7;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int[] lifetime ={15, 20, 25, 27, 30};
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, true);
            ActorPC pc = (ActorPC)sActor;
            foreach (var item in actors)
            {
                if (item.type == ActorType.PC)
                {
                    ActorPC target = (ActorPC)item;
                    DefaultBuff skill = new DefaultBuff(args.skill, item, "MAG_INT_DEX_UP", 60000);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            short[] DEX = { 8, 10, 13, 16, 88 };
            short[] INT = { 7, 9, 11, 13, 88 };
            short[] MAG = { 10, 12, 14, 17, 88 };

            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID), actor, 5178);
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
