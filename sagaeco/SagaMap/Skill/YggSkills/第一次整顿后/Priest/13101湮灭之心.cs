using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S13101 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("ContractCD")) return -30;
            if (pc.BeliefLight > 0)
                return -12;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime1 = 20000;
            int lifetime2 = 20000;
            int cd = 170000 - 20000 * level;

            sActor.MP = sActor.MaxMP;

            bool special = false;

            sActor.EP = 0;
            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);

            OtherAddition Dark = new OtherAddition(args.skill, sActor, "湮灭之心", lifetime1);
            Dark.OnAdditionEnd += Dark_OnAdditionEnd;
            SkillHandler.ApplyAddition(sActor, Dark);

            OtherAddition 意志坚定 = new OtherAddition(args.skill, sActor, "意志坚定", lifetime2);
            意志坚定.OnAdditionEnd += 意志坚定_OnAdditionEnd1;
            SkillHandler.ApplyAddition(sActor, 意志坚定);

            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "ContractCD", cd);
            skill.OnAdditionStart += StartEvent;
            skill.OnAdditionEnd += EndEvent;
            SkillHandler.ApplyAddition(dActor, skill);
        }

        private void 意志坚定_OnAdditionEnd1(Actor actor, OtherAddition skill)
        {
            if (actor.type == ActorType.PC)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『坚定意志』效果消失了。");
        }

        private void Dark_OnAdditionEnd(Actor actor, OtherAddition skill)
        {
            actor.BeliefDark = 0;
            if (actor.type == ActorType.PC)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『湮灭之心』效果消失了。");
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, actor, true);
        }


        void StartEvent(Actor actor, DefaultBuff skill)
        {
        }
        void EndEvent(Actor actor, DefaultBuff skill)
        {
            if (actor.type == ActorType.PC)
                Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("『神圣祷告&湮灭之心』可以再次使用了。");
        }
        #endregion
    }
}
