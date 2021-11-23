using SagaDB.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaMap.Skill.SkillDefinations.Cardinal
{
    /// <summary>
    /// 3436 救赎 (サルベイション)
    /// </summary>
    public class Salvation : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
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
            float[] skillbasefactor = new float[] { -0f, -1.8f, -1.5f, -2.8f, -2.5f, -4.1f, -50f };
            float[] flasfactor = new float[] { -0f, -0.4f, -0.5f, -0.6f, -0.7f, -0.8f, -0.9f, -1.0f, -1.1f, -1.3f, -1.5f, -50f };

            float factors = skillbasefactor[level];

            int[] basecurerate = { 0, 20, 25, 30, 35, 40, 100 };
            int[] flascurerate = new int[] { 0, 10, 15, 20, 25, 30, 35, 40, 45, 50, 100, 100 };
            int rate = basecurerate[level];
            if (sActor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Skills2_1.ContainsKey(3146))
                {
                    factors += flasfactor[pc.Skills2_1[3146].Level];
                    rate += flascurerate[pc.Skills2_1[3146].Level];
                }
            }
            factors += sActor.Status.Cardinal_Rank;

            if (level % 2 == 0)
            {
                Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                List<Actor> actors;
                if (level / 2 == 1)
                    actors = map.GetActorsArea(dActor, 100, true);
                else
                    actors = map.GetActorsArea(dActor, 200, true);

                List<Actor> affected = new List<Actor>();
                foreach (Actor i in actors)
                    if (i is ActorPC)
                        if (!SkillHandler.Instance.CheckValidAttackTarget(sActor, i))
                            affected.Add(i);

                SkillHandler.Instance.MagicAttack(sActor, affected, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factors);
                foreach (var item in affected)
                {
                    RemoveAddition(item, "Poison");
                    RemoveAddition(item, "MoveSpeedDown");
                    RemoveAddition(item, "Stone");
                    RemoveAddition(item, "Silence");
                    RemoveAddition(item, "Stun");
                    RemoveAddition(item, "Sleep");
                    RemoveAddition(item, "Frosen");
                    RemoveAddition(item, "Confuse");
                }
            }
            else
            {
                SkillHandler.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, SagaLib.Elements.Holy, factors);

                if (SagaLib.Global.Random.Next(0, 99) < rate)
                {
                    RemoveAddition(dActor, "Poison");
                    RemoveAddition(dActor, "MoveSpeedDown");
                    RemoveAddition(dActor, "Stone");
                    RemoveAddition(dActor, "Silence");
                    RemoveAddition(dActor, "Stun");
                    RemoveAddition(dActor, "Sleep");
                    RemoveAddition(dActor, "Frosen");
                    RemoveAddition(dActor, "Confuse");
                }
            }
        }

        #endregion
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
    }
}