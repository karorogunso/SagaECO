using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    /// <summary>
    /// 火焰之心（フレイムハート）
    /// </summary>
    public class FrameHart : ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
           return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000 + 10000 * level;
            ActorPC realDActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff skill = new DefaultBuff(args.skill, realDActor, "FrameHart", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realDActor, skill);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            int max_atk1_add = (int)(actor.Status.max_atk_bs  * (0.04 + 0.1 * level));
            int min_atk1_add = (int)(actor.Status.min_atk_bs  * (0.04 + 0.1 * level));
            int max_atk2_add = (int)(actor.Status.max_atk_bs * (0.04 + 0.1 * level));
            int min_atk2_add = (int)(actor.Status.min_atk_bs * (0.04 + 0.1 * level));
            int max_atk3_add = (int)(actor.Status.max_atk_bs * (0.04 + 0.1 * level));
            int min_atk3_add = (int)(actor.Status.min_atk_bs * (0.04 + 0.1 * level));

            //大傷
            if (skill.Variable.ContainsKey("PoisonReate1_Max"))
                skill.Variable.Remove("PoisonReate1_Max");
            skill.Variable.Add("PoisonReate1_Max", max_atk1_add);
            actor.Status.max_atk1_skill += (short)max_atk1_add;

            if (skill.Variable.ContainsKey("PoisonReate2_Max"))
                skill.Variable.Remove("PoisonReate2_Max");
            skill.Variable.Add("PoisonReate2_Max", max_atk2_add);
            actor.Status.max_atk2_skill += (short)max_atk2_add;

            if (skill.Variable.ContainsKey("PoisonReate3_Max"))
                skill.Variable.Remove("PoisonReate3_Max");
            skill.Variable.Add("PoisonReate3_Max", max_atk3_add);
            actor.Status.max_atk3_skill += (short)max_atk3_add;
            //小傷
            if (skill.Variable.ContainsKey("PoisonReate1_Min"))
                skill.Variable.Remove("PoisonReate1_Min");
            skill.Variable.Add("PoisonReate1_Min", min_atk1_add);
            actor.Status.min_atk1_skill += (short)min_atk1_add;

            if (skill.Variable.ContainsKey("PoisonReate2_Min"))
                skill.Variable.Remove("PoisonReate2_Min");
            skill.Variable.Add("PoisonReate2_Min", min_atk2_add);
            actor.Status.min_atk2_skill += (short)min_atk2_add;

            if (skill.Variable.ContainsKey("PoisonReate3_Min"))
                skill.Variable.Remove("PoisonReate3_Min");
            skill.Variable.Add("PoisonReate3_Min", min_atk3_add);
            actor.Status.min_atk3_skill += (short)min_atk3_add;
            
            actor.Buff.AtkMinUp = true;
            actor.Buff.AtkMaxUp = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            //ReCalcStatus((ActorPC)actor);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            //大傷
            actor.Status.max_atk1_skill -= (short)skill.Variable["PoisonReate1_Max"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["PoisonReate2_Max"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["PoisonReate3_Max"];
            //小傷
            actor.Status.min_atk1_skill -= (short)skill.Variable["PoisonReate1_Min"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["PoisonReate2_Min"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["PoisonReate3_Min"];

            actor.Buff.AtkMinUp = false;
            actor.Buff.AtkMaxUp = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            //ReCalcStatus((ActorPC)actor);
        }

        //public void ReCalcStatus(ActorPC actor)
        //{
        //    PC.StatusFactory.Instance.CalcStatus(actor);
        //    MapClient.FromActorPC(actor).SendPlayerInfo();

        //    ActorPC dActor = SkillHandler.Instance.GetPossesionedActor(actor);
        //    if (dActor != actor)
        //    {
        //        PC.StatusFactory.Instance.CalcStatus(dActor);
        //        MapClient.FromActorPC(dActor).SendPlayerInfo();
        //    }
        //}
         #endregion
    }
}
