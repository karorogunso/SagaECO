using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Vates
{
    public class Healing:ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            //if (pc.Status.Additions.ContainsKey("HealingCD"))
            //{
                //Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(string.Format("该技能正在单独冷却中，剩余时间：{0}毫秒", pc.Status.Additions["HealingCD"].RestLifeTime));
                //return -99;
            //}
            if (pc.Status.Additions.ContainsKey("Spell") || pc.Status.Additions.ContainsKey("EvilSoul"))
            {
                return -7;
            }
            if (dActor.type == ActorType.MOB)
            {
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)dActor.e;
                if (eh.AI.Mode.Symbol)
                    return -14;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //SkillHandler.Instance.ActorSpeak(sActor, "啊哈哈哈哈哈啊");
            //DefaultBuff skill = new DefaultBuff(args.skill, sActor, "HealingCD", 3000);
            //SkillHandler.ApplyAddition(sActor, skill);
            float factor = 0.5f + 0.5f * level;
            uint hpadd = (uint)(dActor.MaxHP * (float)(0.02f * level));
            SkillArg arg2 = new SkillArg();
            arg2 = args.Clone();
            SkillHandler.Instance.FixAttack(sActor, dActor, arg2, SagaLib.Elements.Holy, -hpadd);
            arg2.skill.BaseData.id = 100;
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
            arg2.skill.BaseData.id = 3054;
            SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, -factor);

        }
        #endregion
    }
}
