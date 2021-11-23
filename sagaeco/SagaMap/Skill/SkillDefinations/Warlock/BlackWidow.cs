using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Warlock
{
    public class BlackWidow : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (SkillHandler.Instance.CheckValidAttackTarget(pc, dActor))
            {
                return 0;
            }
            else
            {
                return -14;
            }
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            float factor = 0;
            factor = 1f;
            if (level == 2) factor = 1.5f;
            if (!dActor.Status.Additions.ContainsKey("暗刻"))
            {
                OtherAddition 暗刻 = new OtherAddition(null, dActor, "暗刻", 5000);
                暗刻.OnAdditionEnd += (s, e) => {
                    dActor.Buff.呪縛 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                };
                SkillHandler.ApplyAddition(dActor, 暗刻);
                SkillHandler.Instance.ShowEffectOnActor(dActor, 5081, sActor);
                dActor.Buff.呪縛 = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
            }
            List<Actor> da = new List<Actor>();
            da.Add(dActor);
            SkillHandler.Instance.MagicAttack(sActor, da, args, SkillHandler.DefType.MDef, SagaLib.Elements.Dark, 50, factor, 0, false, false);
            if(sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                Network.Client.MapClient.FromActorPC(pc).TitleProccess(pc, 71, 1);
            }
        }
        #endregion
    }
}
