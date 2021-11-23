using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S12108 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("审判CD") && !pc.Status.Additions.ContainsKey("火力全开"))
            {
                return -30;
            }
            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
            {
                SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                if ((item.BaseData.itemType == SagaDB.Item.ItemType.BOW || item.BaseData.itemType == SagaDB.Item.ItemType.RIFLE
                    || item.BaseData.itemType == SagaDB.Item.ItemType.GUN || item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN) && pc.TInt["斥候远程模式"] == 1
                    && pc.Status.Additions.ContainsKey("自由射击"))
                    return 0;
                else return -2;
            }
            return -2;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (!sActor.Status.Additions.ContainsKey("火力全开"))
            {
                OtherAddition skill = new OtherAddition(null, sActor, "审判", 20000);
                skill.OnAdditionStart += (s, e) =>
                {
                    sActor.Buff.审判 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                skill.OnAdditionEnd += (s, e) =>
                {
                    sActor.Buff.审判 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                SkillHandler.ApplyAddition(sActor, skill);

                OtherAddition fire = new OtherAddition(null, sActor, "火力全开", 20000);
                SkillHandler.ApplyAddition(sActor, fire);

                StableAddition cd = new StableAddition(null, sActor, "审判CD", 120000);
                cd.OnAdditionEnd += (s, e) =>
                {
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 5459);
                    SkillHandler.SendSystemMessage(sActor, "『审判』可以再次使用了。");
                };
                SkillHandler.ApplyAddition(sActor, cd);
                SkillHandler.Instance.ShowEffectByActor(sActor, 7065);
                SkillHandler.Instance.ShowEffectByActor(sActor, 7063);
                SkillHandler.Instance.ShowEffectByActor(sActor, 7128);
                SkillHandler.Instance.ShowEffectByActor(sActor, 5061);
            }
            else
            {
                SkillHandler.RemoveAddition(sActor, "火力全开");
                float factor = 2f + 1f * level;

                List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 1000);
                List<Actor> dest = new List<Actor>();
                List<Actor> K = new List<Actor>();
                if (targets.Count > 0)
                {

                    SkillHandler.Instance.ShowEffectByActor(sActor, 8017);
                    SkillHandler.Instance.ShowEffectByActor(sActor, 8008);
                    for (int i = 0; i < 10; i++)
                    {
                        Actor a = targets[SagaLib.Global.Random.Next(0, targets.Count - 1)];
                        dest.Add(a);
                        if (!K.Contains(a))
                        {
                            K.Add(a);
                            SkillHandler.Instance.ShowEffectByActor(a, 5460);
                        }
                    }

                    args.argType = SkillArg.ArgType.Attack;
                    args.type = ATTACK_TYPE.STAB;
                    args.delayRate = 30f;
                    ActorPC pc = (ActorPC)sActor;
                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND];
                    if (item.BaseData.itemType == SagaDB.Item.ItemType.DUALGUN)
                        args.delayRate = 5f;

                    SkillHandler.Instance.PhysicalAttack(sActor, dest, args, SkillHandler.DefType.Def, Elements.Neutral, 0, factor, false, 0, false, 0, 100);
                }
            }

        }
    }
}
