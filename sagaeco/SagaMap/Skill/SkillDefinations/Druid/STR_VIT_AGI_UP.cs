using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Druid
{
    /// <summary>
    /// 強身健體（ラウズボディ）
    /// </summary>
    public class STR_VIT_AGI_UP : ISkill 
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
            int[] lifetime = { 15, 20, 25, 27, 30 };
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 700, true);
            ActorPC pc = (ActorPC)sActor;
            foreach (var item in actors)
            {
                if (item.type == ActorType.PC)
                {
                    ActorPC target = (ActorPC)item;
                    DefaultBuff skill = new DefaultBuff(args.skill, item, "STR_VIT_AGI_UP", 60000);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(item, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            short[] STR={10, 12, 14, 17, 88 }; 
            short[] VIT={7, 9, 11, 13, 88 };
            short[] AGI={8, 10, 13, 16, 88 };

            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(actor.MapID), actor, 5177);
            //STR
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_STR"))
                skill.Variable.Remove("STR_VIT_AGI_UP_STR");
            skill.Variable.Add("STR_VIT_AGI_UP_STR", STR[level - 1]);
            actor.Status.str_skill  += STR[level - 1];
            //VIT
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_VIT"))
                skill.Variable.Remove("STR_VIT_AGI_UP_VIT");
            skill.Variable.Add("STR_VIT_AGI_UP_VIT", VIT[level - 1]);
            actor.Status.vit_skill  += VIT[level - 1];
            //AGI
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_AGI"))
                skill.Variable.Remove("STR_VIT_AGI_UP_AGI");
            skill.Variable.Add("STR_VIT_AGI_UP_AGI", AGI[level - 1]);
            actor.Status.agi_skill  += AGI[level - 1];
            actor.Buff.STR上昇 = true;
            actor.Buff.AGI上昇 = true;
            actor.Buff.VIT上昇 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {

            actor.Status.str_skill -= (short)skill.Variable["STR_VIT_AGI_UP_STR"];
            actor.Status.vit_skill -= (short)skill.Variable["STR_VIT_AGI_UP_VIT"];
            actor.Status.agi_skill -= (short)skill.Variable["STR_VIT_AGI_UP_AGI"];
            actor.Buff.STR上昇 = false;
            actor.Buff.AGI上昇 = false;
            actor.Buff.VIT上昇 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}

