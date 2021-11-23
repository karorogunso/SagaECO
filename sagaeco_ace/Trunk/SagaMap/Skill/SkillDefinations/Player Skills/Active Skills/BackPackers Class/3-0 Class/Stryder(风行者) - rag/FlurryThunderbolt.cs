
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Stryder
{
    public class FlurryThunderbolt : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 200, true);
            List<Actor> affected = new List<Actor>();
            foreach (Actor i in actors)
            {
                if (i.type == ActorType.PC)
                {
                    DefaultBuff skill = new DefaultBuff(args.skill, i, "FlurryThunderbolt", 180000);
                    skill.OnAdditionStart += this.StartEventHandler;
                    skill.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(i, skill);
                }
            }
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int add = 7 + skill.skill.Level;
            if (skill.Variable.ContainsKey("FlurryThunderbolt_DEX"))
                skill.Variable.Remove("FlurryThunderbolt_DEX");
            skill.Variable.Add("FlurryThunderbolt_DEX", add);
            actor.Status.dex_skill += (short)add;
            actor.Status.agi_skill += (short)add;

            actor.Buff.DEXUp = true;
            actor.Buff.AGIUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.dex_skill -= (short)skill.Variable["FlurryThunderbolt_DEX"];
            actor.Status.agi_skill -= (short)skill.Variable["FlurryThunderbolt_DEX"];
            actor.Buff.DEXUp = false;
            actor.Buff.AGIUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}