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
    class S14011: ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.TInt["燃烬"] < 1) return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            float factor = level * 5f;
            byte ranjin = 1;
            if (sActor.type == ActorType.PC)
            {
                if (sActor.TInt["燃烬"] < 1)
                    return;
                sActor.TInt["燃烬"] -= 1;
                sActor.EP += 500;
                ActorPC pc = (ActorPC)sActor;
                pc.TInt["火属性魔法释放"] = 1;
                if (pc.TInt["水属性魔法释放"] == 1)
                    pc.TInt["火属性魔法释放"] = 2;
                if (pc.Skills.ContainsKey(14051)) //精通：燃烧地狱
                {
                    if (pc.Status.Additions.ContainsKey("彼岸焚烧")) //彼岸火湖效果中
                    {
                        pc.MP += (uint)(ranjin * pc.MaxMP * 0.2f);
                        if (pc.MP > pc.MaxMP) pc.MP = pc.MaxMP;
                    }
                    if (pc.Status.Additions.ContainsKey("魔力引导CD"))
                    {
                        ((OtherAddition)pc.Status.Additions["魔力引导CD"]).endTime -= new TimeSpan(0, 0, 0, 5);
                        if (((OtherAddition)pc.Status.Additions["魔力引导CD"]).RestLifeTime <= 0)
                        {
                            pc.Status.Additions.Remove("魔力引导CD");
                            Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『魔力引导』冷却完毕。");
                        }
                            
                    }
                        
                }

                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
            }
            int criup = 0;
            List<Actor> actors = map.GetActorsArea(sActor, 500, false);
            List<Actor> targets = new List<Actor>();
            List<Actor> otherpc = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item) && item.Status.Additions.ContainsKey("Burning"))
                {
                    if (!targets.Contains(item))
                        targets.Add(item);
                }
            }
            foreach (var item in targets)
            {
                List<Actor> targetaround = map.GetActorsArea(item, 200, true);
                List<Actor> targetaround2 = new List<Actor>();
                bool Fortified = false;
                foreach (var item2 in targetaround)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item2))
                    {
                        targetaround2.Add(item2);
                        criup += 10;
                        //SkillHandler.Instance.ShowEffectOnActor(item, 7917);
                        if (item2.Status.Additions.ContainsKey("空间震") && sActor.type == ActorType.PC)
                        {
                            ActorPC Me = (ActorPC)sActor;
                            if (Me.Skills.ContainsKey(14007))
                            {
                                byte lv = Me.Skills[14007].Level;
                                float fup = 1.25f + lv * 0.05f;
                                if (!Fortified)
                                {
                                    factor *= fup;
                                    Fortified = true;
                                }
                                SkillHandler.RemoveAddition(item, "空间震");
                                SkillHandler.Instance.ShowEffectOnActor(item, 5266, sActor);
                            }
                        }
                        SkillHandler.Instance.ShowEffectOnActor(item, 7917, sActor);
                    }
                    else
                    {
                        if (item2.Status.Additions.ContainsKey("Frosen"))
                            SkillHandler.RemoveAddition(item, "Frosen");
                        if (item2.type == ActorType.PC && !otherpc.Contains(item2))
                            otherpc.Add(item2);
                    }
                }
                SkillHandler.Instance.MagicAttack(sActor, targetaround2, args, Elements.Fire, factor);
                SkillHandler.Instance.ShowEffectOnActor(item, 7917, sActor);
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                        int phoenixrate = 10;
                    if (pc.Status.Additions.ContainsKey("魔法少女") && pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) && pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SPEAR)
                        phoenixrate = 15;
                    if (SagaLib.Global.Random.Next(0, 100) <= phoenixrate && pc.TInt["燃烬"] < 3)
                    {
                        pc.TInt["燃烬"]++;
                        SkillHandler.Instance.ShowEffectOnActor(sActor, 4264);
                        Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("获得了一枚[燃烬]" + pc.TInt["燃烬"].ToString() + "/3");
                    }
                }


            }
            if (criup > 50) criup = 50;
            foreach (var item in otherpc)
            {
                if (item.type == ActorType.PC)
                {
                    HitCriUp hcu = new HitCriUp(args.skill, item, 8000, criup);
                    SkillHandler.ApplyAddition(item, hcu);
                }

            }

            //if (targets.Count <= 1)
            //{
            //    float factor = factor1 + factor2;
            //    SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Fire, factor);
            //}
            //else
            //{
            //    SkillHandler.Instance.MagicAttack(sActor, dActor, args, Elements.Fire, factor1);
            //    factor2 = factor2 / targets.Count;
            //    foreach (var item in targets)
            //        SkillHandler.Instance.DoDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Fire, 50, factor2);
            //}
        }
        #endregion
    }
}
