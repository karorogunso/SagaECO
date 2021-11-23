    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations
{
    class S14010: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("烈焰焚烧CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            OtherAddition cd = new OtherAddition(null, sActor, "烈焰焚烧CD", 1000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);
            sActor.TInt["火属性魔法释放"] = 1;
            if (sActor.TInt["水属性魔法释放"] == 1)
                sActor.TInt["火属性魔法释放"] = 2;

            float factor = level * 6f;

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x,map.Width), SagaLib.Global.PosY8to16(args.y,map.Height), 200, true);
            List<Actor> targets = new List<Actor>();
            bool Fortified = false;
            foreach (var item in actors)
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    targets.Add(item);
                    //SkillHandler.Instance.ShowEffectOnActor(item, 5206);
                    if (item.Status.Additions.ContainsKey("空间震") &&sActor.type == ActorType.PC)
                    {
                        ActorPC Me = (ActorPC)sActor;
                        if (Me.Skills.ContainsKey(14007))
                        {
                            byte lv = Me.Skills[14007].Level;
                            float fup =1.25f + lv * 0.05f;
                            if (!Fortified)
                            {
                                factor *= fup;
                                Fortified = true;
                            }
                            SkillHandler.RemoveAddition(item,"空间震");
                            SkillHandler.Instance.ShowEffectOnActor(item, 5266);
                        }
                    }
                }
                else if ((item.type==ActorType.PC|| item.type == ActorType.PARTNER) && item.Status.Additions.ContainsKey("Frosen"))
                    SkillHandler.RemoveAddition(item, "Frosen");
            foreach (var item in targets)
            {
                int damage = SkillHandler.Instance.MagicAttack(sActor, item, args, Elements.Fire, factor);
                if (sActor.type == ActorType.PC)
                {
                    ActorPC Me = (ActorPC)sActor;
                    if (Me.Skills2.ContainsKey(14011))
                    {
                        int phoenixrate = 10;
                        if (Me.Status.Additions.ContainsKey("魔法少女") && Me.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) && Me.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SPEAR)
                            phoenixrate = 15;
                        if (SagaLib.Global.Random.Next(0, 100) <= phoenixrate && Me.TInt["燃烬"] < 3)
                        {
                            Me.TInt["燃烬"]++;
                            SkillHandler.Instance.ShowEffectOnActor(sActor, 4264);
                            Network.Client.MapClient.FromActorPC(Me).SendSystemMessage("获得了一枚[燃烬]" + Me.TInt["燃烬"].ToString() + "/3");
                        }
                    }
                    if (damage > 0)
                    {
                        damage = (int)(damage * (Me.TInt["升温"] / 100f)) / 5;
                        if (item.Status.Additions.ContainsKey("Burning"))
                            SkillHandler.RemoveAddition(item, "Burning");
                        Burning b = new Burning(null, item, 4000, damage);
                        if (sActor.type == ActorType.PC)
                            ((ActorPC)sActor).TInt["伤害统计"] += damage * 5;
                        if (item.type == ActorType.MOB)
                        {
                            ActorMob mob = (ActorMob)item;
                            if (((ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(sActor.ActorID))
                                ((ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable[sActor.ActorID] += (damage * 5);
                        }
                        SkillHandler.ApplyAddition(item, b);
                    }
                }
            }
        }
        #endregion
    }
}
