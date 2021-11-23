using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    public class ForceMaster : ISkill
    {
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = (30 + level * 30) * 1000;
            int MdefDown = 50 + level * 10;
            ForceMasterBuff skill = new ForceMasterBuff(args.skill, dActor, lifetime, MdefDown);
            SkillHandler.ApplyAddition(dActor, skill);

        }
        public class ForceMasterBuff : DefaultBuff
        {
            
            public ForceMasterBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int MdefDown)
                :base(skill,actor,"ForceMaster",lifetime)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["MdefDown"] = MdefDown;

            }
            void StartEvent(Actor actor, DefaultBuff skill)
            { 
                int level = skill.skill.Level;
                int min_matk_add = (int)(actor.Status.min_matk_ori * 1.5f);
                int max_matk_add = (int)(actor.Status.max_matk_ori * 1.5f);
                if (skill.Variable.ContainsKey("ForceMaster_min_matk"))
                    skill.Variable.Remove("ForceMaster_min_matk");
                skill.Variable.Add("ForceMaster_min_matk", min_matk_add);
                if (skill.Variable.ContainsKey("ForceMaster_max_matk"))
                    skill.Variable.Remove("ForceMaster_max_matk");
                skill.Variable.Add("ForceMaster_max_matk", max_matk_add);
                actor.Status.min_matk_skill += (short) min_matk_add;
                actor.Status.max_matk_skill += (short) max_matk_add;
                actor.Buff.MainSkillPowerUp3RD = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                actor.Status.min_matk_skill -= (short)skill.Variable["ForceMaster_min_matk"];
                actor.Status.max_matk_skill -= (short)skill.Variable["ForceMaster_max_matk"];
                actor.Buff.MainSkillPowerUp3RD = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);                
            }
        }
    }
}