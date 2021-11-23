using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S42001 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("属性契约") && !pc.Status.Additions.ContainsKey("元素解放"))
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition EB = new OtherAddition(args.skill, sActor, "元素解放", 35000);
            EB.OnAdditionStart += this.StartEvent;
            EB.OnAdditionEnd += this.EndEvent;
            SkillHandler.ApplyAddition(sActor, EB);
            uint mpheal = sActor.EP / 2;
            sActor.MP += mpheal;
            if (sActor.MP > sActor.MaxMP)
                sActor.MP = sActor.MaxMP;
            SkillHandler.Instance.ShowVessel(sActor, 0, (int)-mpheal);
        }
        void StartEvent(Actor actor, OtherAddition skill)
        {
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("进入元素解放状态！");
        }
        void EndEvent(Actor actor, OtherAddition skill)
        {
            SkillHandler.RemoveAddition(actor, "元素解放");
            SkillHandler.RemoveAddition(actor, "属性契约");
            SkillHandler.RemoveAddition(actor, "WeaponFire");
            SkillHandler.RemoveAddition(actor, "WeaponWater");
            SkillHandler.RemoveAddition(actor, "WeaponWind");
            SkillHandler.RemoveAddition(actor, "WeaponEarth");
            Network.Client.MapClient.FromActorPC((ActorPC)actor).SendSystemMessage("元素解放状态解除！");
        }
        #endregion
    }
}
