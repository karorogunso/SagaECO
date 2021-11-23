using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Hawkeye
{
    public class SmokeBall : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.isEquipmentRight(pc, ItemType.BOW, ItemType.GUN,ItemType.RIFLE, ItemType.EXGUN, ItemType.DUALGUN))
            {
                return 0;
            }
            return -14;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            if (!dActor.Status.Additions.ContainsKey("SmokeBall"))
            {
                int[] lifetime = { 0, 45000, 60000, 75000, 90000, 120000 };
                DefaultBuff skill = new DefaultBuff(args.skill, sActor, "SmokeBall", lifetime[level]);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                skill.OnCheckValid += this.ValidCheck;
                SkillHandler.ApplyAddition(sActor, skill);
            }
            else
            {
                sActor.Status.Additions["SmokeBall"].OnTimerEnd();
            }
            
        }

        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill += 50;
            actor.Buff.三转枪连弹 = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            int level = skill.skill.Level;
            actor.Status.combo_rate_skill -= 50;
            actor.Buff.三转枪连弹 = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}
