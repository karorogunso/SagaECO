using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;


namespace SagaMap.Skill.SkillDefinations.Royaldealer
{
    class RoyalDealer : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorPC pc = (ActorPC)sActor;
            if (pc.Skills3.ContainsKey(989) || pc.DualJobSkill.Exists(x => x.ID == 989))
            {
                var duallv = 0;
                if (pc.DualJobSkill.Exists(x => x.ID == 989))
                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 989).Level;

                var mainlv = 0;
                if (pc.Skills3.ContainsKey(989))
                    mainlv = pc.Skills3[989].Level;

                int maxlv = Math.Max(duallv, mainlv);
                pc.Gold -= new int[] { 0, 0, (int)(100.0f * (1.0f - 0.03f * maxlv)), (int)(250.0f * (1.0f - 0.03f * maxlv)), (int)(500.0f * (1.0f - 0.03f * maxlv)), (int)(1000.0f * (1.0f - 0.03f * maxlv)) }[level];
            }
            else
            {
                pc.Gold -= new int[] { 0, 0, 100, 250, 500, 1000 }[level];
            }


            int lifetime = 30000 + 30000 * level;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "RoyalDealer", lifetime, 1000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnUpdate += this.UpdateEventHandler;
            SkillHandler.ApplyAddition(sActor, skill);
        }


        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            //X
            if (skill.Variable.ContainsKey("Save_X"))
                skill.Variable.Remove("Save_X");
            skill.Variable.Add("Save_X", actor.X);

            //Y
            if (skill.Variable.ContainsKey("Save_Y"))
                skill.Variable.Remove("Save_Y");
            skill.Variable.Add("Save_Y", actor.Y);
            actor.Buff.MainSkillPowerUp3RD = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.MainSkillPowerUp3RD = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void UpdateEventHandler(Actor actor, DefaultBuff skill)
        {
            if (actor.X != (short)skill.Variable["Save_X"] || actor.Y != (short)skill.Variable["Save_Y"])
            {
                Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
                actor.Status.Additions["RoyalDealer"].AdditionEnd();
                actor.Status.Additions.Remove("RoyalDealer");
                actor.Buff.MainSkillPowerUp3RD = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, false);
            }
        }
        #endregion
    }
}