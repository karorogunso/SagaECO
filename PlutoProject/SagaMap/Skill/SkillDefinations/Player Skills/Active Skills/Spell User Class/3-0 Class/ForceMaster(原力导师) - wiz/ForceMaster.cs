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
            if (sActor.Status.Additions.ContainsKey("Sacrifice"))
            {
                return -12;
            }
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            int lifetime = (30 + level * 30) * 1000;
            int MdefDown = 50 + level * 10;
            ForceMasterBuff skill = new ForceMasterBuff(args.skill, sActor, lifetime, MdefDown);
            skill.OnUpdate += this.UpdateEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);

        }

        void UpdateEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.X != (short)skill.Variable["Save_X"] || actor.Y != (short)skill.Variable["Save_Y"])
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                actor.Status.Additions["ForceMaster"].AdditionEnd();
                actor.Status.Additions.Remove("ForceMaster");
                actor.Buff.MainSkillPowerUp3RD = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
            }
        }
        public class ForceMasterBuff : DefaultBuff
        {

            public ForceMasterBuff(SagaDB.Skill.Skill skill, Actor actor, int lifetime, int MdefDown)
                : base(skill, actor, "ForceMaster", lifetime, 1000)
            {
                this.OnAdditionStart += this.StartEvent;
                this.OnAdditionEnd += this.EndEvent;
                this["MdefDown"] = MdefDown;

            }
            void StartEvent(Actor actor, DefaultBuff skill)
            {
                //X
                if (skill.Variable.ContainsKey("Save_X"))
                    skill.Variable.Remove("Save_X");
                skill.Variable.Add("Save_X", actor.X);

                //Y
                if (skill.Variable.ContainsKey("Save_Y"))
                    skill.Variable.Remove("Save_Y");
                skill.Variable.Add("Save_Y", actor.Y);
                int level = skill.skill.Level;
                int min_matk_add = (int)(actor.Status.min_matk_bs * 1.5f);
                int max_matk_add = (int)(actor.Status.max_matk_bs * 1.5f);
                if (skill.Variable.ContainsKey("ForceMaster_min_matk"))
                    skill.Variable.Remove("ForceMaster_min_matk");
                skill.Variable.Add("ForceMaster_min_matk", min_matk_add);
                if (skill.Variable.ContainsKey("ForceMaster_max_matk"))
                    skill.Variable.Remove("ForceMaster_max_matk");
                skill.Variable.Add("ForceMaster_max_matk", max_matk_add);
                actor.Status.min_matk_skill += (short)min_matk_add;
                actor.Status.max_matk_skill += (short)max_matk_add;
                actor.Status.ForceMaster_Lv = skill.skill.Level;
                actor.Buff.MainSkillPowerUp3RD = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
            void EndEvent(Actor actor, DefaultBuff skill)
            {
                actor.Status.min_matk_skill -= (short)skill.Variable["ForceMaster_min_matk"];
                actor.Status.max_matk_skill -= (short)skill.Variable["ForceMaster_max_matk"];
                actor.Status.ForceMaster_Lv = 0;
                actor.Buff.MainSkillPowerUp3RD = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            }
        }
    }
}