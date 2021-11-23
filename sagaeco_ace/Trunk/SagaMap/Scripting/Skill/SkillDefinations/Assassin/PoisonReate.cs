using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;

namespace SagaMap.Skill.SkillDefinations.Assassin
{
    /// <summary>
    /// 狂毒氣（狂気毒）
    /// </summary>
    public class PoisonReate :  ISkill 
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            uint itemID = 10000353;//刺客的內服藥
            if (SkillHandler.Instance.CountItem(pc, itemID) > 0)
            {
                if (CheckPossible(pc))
                {
                    SkillHandler.Instance.TakeItem(pc, itemID, 1);
                    return 0;
                }
                else
                {
                    return -5;
                }
            }
            return -57;
        }
        bool CheckPossible(Actor sActor)
        {
            if (sActor.type == ActorType.PC)
            {
                return SkillHandler.Instance.isEquipmentRight(sActor, ItemType.CLAW) || SkillHandler.Instance.CheckDEMRightEquip(sActor, SagaDB.Item.ItemType.PARTS_SLASH );
            }
            else
            {
                return true;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            if (CheckPossible(realdActor))
            {
                int life = 20000;
                DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "PoisonReate", life);
                skill.OnAdditionStart += this.StartEventHandler;
                skill.OnAdditionEnd += this.EndEventHandler;
                skill.OnCheckValid += this.ValidCheck;
                SkillHandler.ApplyAddition(realdActor, skill);
            }
        }
        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            float spd = 0;
            int level = skill.skill.Level, rate=0;
            switch (level)
            {
                case 1:
                    spd = 1.33f;
                    rate = 1;
                    break;
                case 2:
                    rate = 25;
                    spd = 1.5f;
                    break;
                case 3:
                    rate = 50;
                    spd = 1.67f;
                    break;
            }
            actor.Status.aspd_skill_perc += spd;
            //中毒?
            if (SkillHandler.Instance.CanAdditionApply(actor,actor, SkillHandler.DefaultAdditions.Poison , rate))
            {
                Additions.Global.Poison nskill = new SagaMap.Skill.Additions.Global.Poison(skill.skill  , actor, 7000);
                SkillHandler.ApplyAddition(actor, nskill);
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            float spd = 0;
            int level = skill.skill.Level;
            switch (level)
            {
                case 1:
                    spd = 1.33f;
                    break;
                case 2:
                    spd = 1.5f;
                    break;
                case 3:
                    spd = 1.67f;
                    break;
            }
            //攻擊速度
            if (actor.Status.aspd_skill_perc > spd)
            {
                actor.Status.aspd_skill_perc -= spd;
            }
            else
            {
                actor.Status.aspd_skill_perc = 0;
            }
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        #endregion
    }
}
