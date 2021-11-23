using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
namespace SagaMap.Skill.SkillDefinations
{
    /// <summary>
    /// 治愈术：单体大量治疗，并随机消除一个控制状态，如果消除了控制状态则再额外恢复少量hp
    /// </summary>
    public class S140022 : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            if (dActor.type == ActorType.ANOTHERMOB && sActor.type == ActorType.PC)
            {
                if(dActor.Buff.Stone == true)
                {
                    ActorPC pc = (ActorPC)sActor;
                    pc.TInt["新手教程3"]++;
                    if(pc.TInt["新手教程3"] >= 3)
                    {
                        Network.Client.MapClient.FromActorPC(pc).EventActivate(50200001);
                        pc.TInt["新手教程3"] = 0;
                    }
                }
            }
            float factors = 3.5f;
            //cure a random control addition
            SkillHandler.Instance.ShowEffect(Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 5076);
            if (sActor.type == ActorType.PC)
            {
                List<string> ss = new List<string>();
                if (dActor.Status.Additions.ContainsKey("Confuse")) ss.Add("Confuse");
                if (dActor.Status.Additions.ContainsKey("Frosen")) ss.Add("Frosen");
                if (dActor.Status.Additions.ContainsKey("Paralyse")) ss.Add("Paralyse");
                if (dActor.Status.Additions.ContainsKey("Silence")) ss.Add("Silence");
                if (dActor.Status.Additions.ContainsKey("Sleep")) ss.Add("Sleep");
                if (dActor.Status.Additions.ContainsKey("Stone")) ss.Add("Stone");
                if (dActor.Status.Additions.ContainsKey("Stun")) ss.Add("Stun");
                if (dActor.Status.Additions.ContainsKey("鈍足")) ss.Add("鈍足");
                if (dActor.Status.Additions.ContainsKey("冰棍的冻结")) ss.Add("冰棍的冻结");
                if (ss.Count > 1)
                {
                    RemoveAddition(dActor, ss[SagaLib.Global.Random.Next(0, ss.Count - 1)]);
                    factors = factors + 1.5f;
                }
                else if (ss.Count == 1)
                {
                    RemoveAddition(dActor, ss[0]);
                    factors = factors + 1.5f;
                }
            }
            int damage = SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, -factors);

        }
        public void RemoveAddition(Actor actor, String additionName)
        {
            if (actor.Status.Additions.ContainsKey(additionName))
            {
                Addition addition = actor.Status.Additions[additionName];
                actor.Status.Additions.Remove(additionName);
                if (addition.Activated)
                {
                    addition.AdditionEnd();
                }
                addition.Activated = false;
            }
        }
        #endregion
    }
}

