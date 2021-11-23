using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

using SagaDB;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaMap.Skill.Additions.Global;



namespace SagaMap.Skill
{

    public partial class SkillHandler
    {
        internal int physicalCounter = 0;
        internal int magicalCounter = 0;
        ActorPC thispc = new ActorPC();
        Elements Toelements = Elements.Neutral;
        public enum DefType
        {
            Def,
            MDef,
            IgnoreAll,
            IgnoreLeft,
            IgnoreRight,
            MDefIgnoreLeft,
            MdefIgnoreRight,
            DefIgnoreLeft,
            DefIgnoreRight
        }

        /// <summary>
        /// 对单一目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return PhysicalAttack(sActor, list, arg, element, ATKBonus);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float ATKBonus)
        {
            return PhysicalAttack(sActor, dActor, arg, element, 0, ATKBonus);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int index, float ATKBonus)
        {
            //if(index >0)
            /*if (dActor.Count > 12)
            {
                int ACount = dActor.Count;
                for (int i = 12; i < ACount; i++)
                {
                    dActor.RemoveAt(12);
                }
            }*/
            return PhysicalAttack(sActor, dActor, arg, DefType.Def, element, index, ATKBonus, false);
        }
        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        ///<param name="defType">使用的防御类型</param>
        /// <param name="ATKBonus">攻击加成</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, 0, false);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, 0, 0);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, shitbonus, scribonus, "noeffect", 0);
        }

        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, string effectname, int lifetime, float ignore = 0)
        {
            return PhysicalAttack(sActor, dActor, arg, defType, element, index, ATKBonus, setAtk, SuckBlood, doublehate, shitbonus, scribonus, 0, "noeffect", 0);
        }

        /// <summary>
        /// 对多个目标进行物理攻击
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标集合</param>
        /// <param name="arg">技能参数</param>
        /// <param name="element">元素</param>
        /// <param name="index">arg中参数偏移</param>
        ///<param name="defType">使用的防御类型</param>
        /// <param name="ATKBonus">攻击加成</param>
        ///  <param name="ignore">无视防御比</param>
        public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, int cridamagebonus, string effectname, int lifetime, float ignore = 0)
        {
            if (dActor.Count == 0) return 0;
            if (sActor.Status == null)
                return 0;
            if (sActor.Status.Additions.ContainsKey("ArmBreak") && arg.skill != null)//断腕击状态无法使用物理技能
            {
                if(sActor.type==ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("目前处于物理技能封印状态");
                }
                return 0;
            }
            if (sActor.type == ActorType.PC && arg.skill == null)//要求不是技能
            {
                ActorPC pc = (ActorPC)sActor;
                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                {
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                                {
                                    GetlongARROW(pc, arg);
                                    //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                                }

                            }
                        }
                    }
                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                        pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                    {
                        if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                        {
                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                                    GetlongARROW(pc, arg);
                                //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                            }
                        }
                    }
                }
            }
            if (dActor.Count > 10)
            {
                foreach (var item in dActor)
                    DoDamage(true, sActor, item, arg, defType, element, index, ATKBonus, scribonus, cridamagebonus);
                return 0;
            }

            int damage = 0;

            int atk;
            int mindamage = 0;
            int maxdamage = 0;
            int counter = 0;
            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);

            if (index == 0)
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }

            switch (arg.type)
            {
                case ATTACK_TYPE.BLOW:
                    mindamage = sActor.Status.min_atk1;
                    maxdamage = sActor.Status.max_atk1;
                    break;
                case ATTACK_TYPE.SLASH:
                    mindamage = sActor.Status.min_atk2;
                    maxdamage = sActor.Status.max_atk2;
                    break;
                case ATTACK_TYPE.STAB:
                    mindamage = sActor.Status.min_atk3;
                    maxdamage = sActor.Status.max_atk3;
                    break;
            }


            //if (sActor.Status.Additions.ContainsKey("破戒"))
            //{
            //    mindamage = sActor.Status.min_matk;
            //    maxdamage = sActor.Status.max_matk;
            //}
            if (mindamage > maxdamage) maxdamage = mindamage;
            foreach (Actor i in dActor)
            {
                if (i.Status == null)
                    continue;
                if (i.type == ActorType.ITEM)
                    continue;
                //NOTOUCH類MOB 跳過判定
                if (i.type == ActorType.MOB)
                {
                    ActorMob checkmob = (ActorMob)i;
                    switch (checkmob.BaseData.mobType)
                    {
                        case SagaDB.Mob.MobType.ANIMAL_NOTOUCH:
                        case SagaDB.Mob.MobType.BIRD_NOTOUCH:
                        case SagaDB.Mob.MobType.ELEMENT_BOSS_NOTOUCH:
                        case SagaDB.Mob.MobType.HUMAN_NOTOUCH:
                        case SagaDB.Mob.MobType.ELEMENT_NOTOUCH:
                        case SagaDB.Mob.MobType.PLANT_NOTOUCH:
                        case SagaDB.Mob.MobType.MACHINE_NOTOUCH:
                        case SagaDB.Mob.MobType.NONE_NOTOUCH:
                        case SagaDB.Mob.MobType.UNDEAD_NOTOUCH:
                        case SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH:
                        case SagaDB.Mob.MobType.PLANT_BOSS_NOTOUCH:
                            continue;

                    }
                }

                //投掷武器
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;

                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                    {
                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW ||
                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
                        {


                            if (pc.Skills3.ContainsKey(989) || pc.DualJobSkill.Exists(x => x.ID == 989))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD &&
                                   pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Stack > 0)
                                {
                                    var duallv = 0;
                                    if (pc.DualJobSkill.Exists(x => x.ID == 989))
                                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 989).Level;

                                    var mainlv = 0;
                                    if (pc.Skills3.ContainsKey(989))
                                        mainlv = pc.Skills3[989].Level;

                                    int maxlv = Math.Max(duallv, mainlv);
                                    if (arg.skill == null)//普通攻击
                                    {
                                        if (SagaLib.Global.Random.Next(0, 99) > maxlv * 5)
                                        {
                                            GetlongCARD(pc, arg);
                                            //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                                        }


                                    }
                                    else if (arg.skill.ID == 2517 || arg.skill.ID == 2518)//ストレートフラッシュ
                                    {
                                        if (SagaLib.Global.Random.Next(0, 99) > maxlv * 3)
                                        {
                                            GetlongCARD(pc, arg);
                                            //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Stack > 0)
                                    MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                            }

                        }
                    }
                }

                ////弓箭，枪
                //if (sActor.type == ActorType.PC)
                //{
                //    ActorPC pc = (ActorPC)sActor;
                //    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                //    {
                //        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                //        {
                //            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                //            {
                //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
                //                {
                //                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                //                    {
                //                        GetlongARROW(pc, arg);
                //                        //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                //                    }

                //                }
                //                else
                //                {
                //                    if (counter == 0)
                //                        arg.result = -1;
                //                    continue;
                //                }
                //            }
                //            else
                //            {
                //                if (counter == 0)
                //                    arg.result = -1;
                //                continue;
                //            }
                //        }
                //        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
                //            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
                //            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
                //        {
                //            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                //            {
                //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
                //                {
                //                    if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                //                        GetlongARROW(pc, arg);
                //                    //MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                //                }
                //                else
                //                {
                //                    if (counter == 0)
                //                        arg.result = -1;
                //                    continue;
                //                }
                //            }
                //            else
                //            {
                //                if (counter == 0)
                //                    arg.result = -1;
                //                continue;
                //            }
                //        }
                //    }
                //}
                //判断命中结果
                //short dis = Map.Distance(sActor, i);
                //if (arg.argType == SkillArg.ArgType.Active)
                //    shitbonus = 50;

                AttackResult res = CalcAttackResult(sActor, i, sActor.Range > 3, shitbonus, scribonus);
                if (i.Status.Additions.ContainsKey("Warn"))//警戒
                {
                    if (res == AttackResult.Critical)
                    {
                        res = AttackResult.Hit;
                    }
                }
                if (sActor.Status.Additions.ContainsKey("PerfectRiotStamp"))
                {
                    if (arg.skill != null && arg.skill.ID != 0)
                    {
                        if (arg.skill.ID == 2180)
                            res = AttackResult.Hit;
                    }

                }
                if (sActor.Status.Additions.ContainsKey("Super_A_T_PJoint"))
                {
                    if (arg.skill != null && arg.skill.ID != 0)
                    {
                        RemoveAddition(sActor, "Super_A_T_PJoint");
                        res = AttackResult.Critical;
                    }

                }
                Actor target = i;

                if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard || res == AttackResult.Parry)
                {
                    if (res == AttackResult.Miss)
                        arg.flag[index + counter] = AttackFlag.MISS;
                    else if (res == AttackResult.Avoid)
                        arg.flag[index + counter] = AttackFlag.AVOID;
                    else if (res == AttackResult.Parry)
                        arg.flag[index + counter] = AttackFlag.AVOID2;
                    else
                        arg.flag[index + counter] = AttackFlag.GUARD;

                    try
                    {
                        string y = "普通攻击";
                        if (arg != null)
                        {
                            if (arg.skill != null)
                                y = arg.skill.Name;
                        }
                        //string s = "物理伤害";
                        SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "被你 " + res.ToString());
                        SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "被 " + res.ToString());
                    }
                    catch (Exception ex)
                    {
                        SagaLib.Logger.ShowError(ex);
                    }
                }
                else
                {
                    int restKryrie = 0;
                    if (i.type == ActorType.PC)
                    {
                        ActorPC me = (ActorPC)i;
                        if (me.Skills2_2.ContainsKey(956) || me.DualJobSkill.Exists(x => x.ID == 956))//二段斩击，已激活副职判定
                        {
                            var duallv = 0;
                            if (me.DualJobSkill.Exists(x => x.ID == 956))
                                duallv = me.DualJobSkill.FirstOrDefault(x => x.ID == 956).Level;

                            var mainlv = 0;
                            if (me.Skills2_2.ContainsKey(956))
                                mainlv = me.Skills2_2[956].Level;


                            int TotalLv = Math.Max(duallv, mainlv);
                            int nr = SagaLib.Global.Random.Next(0, 1000);
                            if ((TotalLv * 20) > nr)
                            {
                                if (i.Status.Additions.ContainsKey("ConSword"))
                                {
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                                    EffectArg arg2 = new EffectArg();
                                    arg2.effectID = 4173;
                                    arg2.actorID = i.ActorID;
                                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                                    restKryrie = 1;
                                }
                            }
                        }
                    }

                    if (i.Status.Additions.ContainsKey("MobKyrie"))
                    {
                        Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["MobKyrie"];
                        restKryrie = buf["MobKyrie"];
                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                        if (restKryrie > 0)
                        {
                            buf["MobKyrie"]--;
                            EffectArg arg2 = new EffectArg();
                            arg2.effectID = 4173;
                            arg2.actorID = i.ActorID;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                            if (restKryrie == 1)
                            {
                                SkillHandler.RemoveAddition(i, "MobKyrie");
                                SkillHandler.RemoveAddition(i, "KyrieEleison");
                            }

                        }
                    }
                    if (i.Status.Additions.ContainsKey("ArtFullTrap1"))
                    {
                        int healnum = i.Status.max_matk_bs + i.Status.max_atk_bs;
                        i.HP += (uint)healnum;
                        if (i.HP > i.MaxHP)
                            i.HP = i.MaxHP;
                        SkillHandler.Instance.ShowVessel(i, -healnum);
                        //SkillHandler.RemoveAddition(i, "ArtFullTrap1");
                    }
                    if (i.Status.Additions.ContainsKey("ArtFullTrap2"))
                    {
                        int healnum = (int)((i.Status.max_matk_bs + i.Status.max_atk_bs) / 5.0f);
                        i.SP += (uint)healnum;
                        if (i.SP > i.MaxSP)
                            i.SP = i.MaxSP;
                        SkillHandler.Instance.ShowVessel(i, 0, 0, -healnum);
                        //SkillHandler.RemoveAddition(i, "ArtFullTrap2");
                    }
                    if (i.Status.Additions.ContainsKey("ArtFullTrap3"))
                    {
                        int healnum = (int)((i.Status.max_matk_bs + i.Status.max_atk_bs) / 5.0f);
                        i.MP += (uint)healnum;
                        if (i.MP > i.MaxMP)
                            i.MP = i.MaxMP;
                        SkillHandler.Instance.ShowVessel(i, 0, -healnum);
                        //SkillHandler.RemoveAddition(i, "ArtFullTrap3");
                    }
                    //if (i.Status.Additions.ContainsKey("ArtFullTrap4"))
                    //{
                    //    arg.argType = SkillArg.ArgType.Attack;
                    //    PhysicalAttack(i, sActor, arg, i.WeaponElement, 1.0f);
                    //    SkillArg args = new SkillArg();
                    //    args.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2552, 4);
                    //    Poison skill = new Poison(args.skill, sActor, 7000);
                    //    //SkillHandler.RemoveAddition(i, "ArtFullTrap4");
                    //}
                    //if (i.Status.Additions.ContainsKey("ArtFullTrap5"))
                    //{
                    //    SkillArg args = new SkillArg();
                    //    args.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2478, 5);
                    //    //SagaDB.Skill.SkillFactory.Instance.GetSkill(2478, 5);
                    //    //args.type = ATTACK_TYPE.SLASH;
                    //    //AutoCastInfo info = new AutoCastInfo();
                    //    //info.skillID = 2478;
                    //    //args.autoCast.Add(info);
                    //    args.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2478, 5, 0));
                    //    //SkillHandler.RemoveAddition(i, "ArtFullTrap5");
                    //}
                    if (restKryrie == 0)
                    {
                        bool isPossession = false;
                        bool isHost = false;
                        if (i.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                            {
                                isPossession = true;
                                isHost = true;
                            }
                            if (pc.PossessionTarget != 0)
                            {
                                isPossession = true;
                                isHost = false;
                            }
                        }
                        //处理凭依伤害
                        if (isHost && isPossession && ATKBonus > 0)
                        {
                            List<Actor> possessionDamage = ProcessAttackPossession(i);
                            if (possessionDamage.Count > 0)
                            {
                                arg.Remove(i);
                                int oldcount = arg.flag.Count;
                                arg.Extend(possessionDamage.Count);
                                foreach (Actor j in possessionDamage)
                                {
                                    if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
                                        arg.affectedActors.Add(i);
                                    else
                                        arg.affectedActors.Add(j);
                                }
                                PhysicalAttack(sActor, possessionDamage, arg, element, oldcount, ATKBonus);
                                continue;
                            }
                        }
                        if (!setAtk)
                        {
                            atk = Global.Random.Next(mindamage, maxdamage);
                            //TODO: element bonus, range bonus
                            float eleBonus = CalcElementBonus(sActor, i, element, 0, false);

                            if (i.Status.Contract_Lv != 0)
                            {
                                Elements tmpele = Elements.Neutral;
                                switch (i.Status.Contract_Lv)
                                {
                                    case 1:
                                        tmpele = Elements.Fire;
                                        break;
                                    case 2:
                                        tmpele = Elements.Water;
                                        break;
                                    case 3:
                                        tmpele = Elements.Earth;
                                        break;
                                    case 4:
                                        tmpele = Elements.Wind;
                                        break;
                                }
                                if (tmpele == element)
                                    eleBonus -= 0.15f;
                                else
                                    eleBonus += 1.0f;

                            }
                            if ((sActor.Status.Additions.ContainsKey("EvilSoul") || sActor.Status.Additions.ContainsKey("SoulTaker")) && element == Elements.Dark && eleBonus > 0 &&
                                (arg.skill.ID == 2537 ||//黑暗之光
                                arg.skill.ID == 2230 ||//吸收灵魂
                                arg.skill.ID == 2229 //死神之刃
                                ))//邪恶灵魂仅对物理攻击本身为暗属性的技能有效
                            {
                                if (sActor.Status.Additions.ContainsKey("EvilSoul"))
                                {
                                    //atkValue += (sActor.Status.Additions["EvilSoul"] as DefaultBuff).Variable["EvilSoul"];
                                    eleBonus += ((float)((sActor.Status.Additions["EvilSoul"] as DefaultBuff).Variable["EvilSoul"]) / 100.0f);
                                }
                                if (sActor.Status.Additions.ContainsKey("SoulTaker") && arg.skill != null && arg.skill.ID != 0)
                                {
                                    //atkValue += (sActor.Status.Additions["SoulTaker"] as DefaultBuff).Variable["SoulTaker"];
                                    eleBonus += ((float)((sActor.Status.Additions["SoulTaker"] as DefaultBuff).Variable["SoulTaker"]) / 100.0f);
                                }
                            }
                            atk = (int)(Math.Ceiling(atk * eleBonus * ATKBonus));
                            /*
                            if (i.Buff.Frosen == true && element == Elements.Fire)
                            {
                                RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                            }
                            if (i.Buff.Stone == true && element == Elements.Water)
                            {
                                RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                            }
                            */
                            if (arg.skill != null)
                            {
                                if (sActor.Status.doubleUpList.Contains((ushort)arg.skill.ID))
                                {
                                    atk *= 2;
                                }
                            }
                        }
                        else
                            atk = (int)ATKBonus;

                        damage = CalcPhyDamage(sActor, i, defType, atk, ignore, res);

                        //if (damage > atk)
                        //    damage = atk;

                        //IStats stats = (IStats)i;
                        switch (arg.type)
                        {
                            case ATTACK_TYPE.BLOW:
                                damage = (int)(damage * (1f - i.Status.damage_atk1_discount));
                                break;
                            case ATTACK_TYPE.SLASH:
                                damage = (int)(damage * (1f - i.Status.damage_atk2_discount));
                                break;
                            case ATTACK_TYPE.STAB:
                                damage = (int)(damage * (1f - i.Status.damage_atk3_discount));
                                break;

                        }


                        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                        {
                            damage = (int)(damage * Configuration.Instance.PVPDamageRatePhysic);
                        }

                        if (damage <= 0) damage = 1;


                        if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                            if (Global.Random.Next(0, 99) < buf["Rate"])
                            {
                                Actor dst = map.GetActor((uint)buf["Target"]);
                                if (dst != null)
                                {
                                    target = dst;
                                    arg.affectedActors[index + counter] = target;
                                }
                            }
                        }



                        //计算暴击增益
                        if (res == AttackResult.Critical)
                        {
                            damage = (int)(((float)damage) * (float)(CalcCritBonus(sActor, target, scribonus) + (float)cridamagebonus));
                            if (sActor.Status.Additions.ContainsKey("CriDamUp"))
                            {
                                float rate = (float)((float)(sActor.Status.Additions["CriDamUp"] as DefaultPassiveSkill).Variable["CriDamUp"] / 100.0f + 1.0f);
                                damage = (int)((float)damage * rate);
                            }
                        }

                        //宠判定？
                        bool ride = false;
                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Pet != null)
                                ride = pc.Pet.Ride;
                        }
                        //宠物成长
                        if (res == AttackResult.Critical)
                        {
                            if (sActor.type == ActorType.PET)
                                ProcessPetGrowth(sActor, PetGrowthReason.CriticalHit);
                            if (i.type == ActorType.PET && damage > 0)
                                ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
                            if (i.type == ActorType.PC && damage > 0)
                            {
                                ActorPC pc = (ActorPC)target;

                                if (ride)
                                {
                                    ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                        }
                        else
                        {
                            if (sActor.type == ActorType.PET)
                                ProcessPetGrowth(sActor, PetGrowthReason.PhysicalHit);
                            if (i.type == ActorType.PET && damage > 0)
                            {
                                ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
                            }
                            if (i.type == ActorType.PC && damage > 0)
                            {
                                ActorPC pc = (ActorPC)target;

                                if (ride)
                                {
                                    ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
                                }
                            }
                        }

                        //技能以及状态判定
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pcsActor = (ActorPC)sActor;
                            if (sActor.Status.Additions.ContainsKey("BurnRate"))// && SkillHandler.Instance.isEquipmentRight(pcsActor, SagaDB.Item.ItemType.CARD))//皇家贸易商
                            {
                                //副职不存在3371技能,不进行副职逻辑判定
                                if (pcsActor.Skills3.ContainsKey(3371))
                                {
                                    if (pcsActor.Skills3[3371].Level > 1)
                                    {
                                        int[] gold = { 0, 0, 100, 250, 500, 1000 };
                                        if (pcsActor.Gold > gold[pcsActor.Skills3[3371].Level])
                                        {
                                            pcsActor.Gold -= gold[pcsActor.Skills3[3371].Level];
                                            damage += gold[pcsActor.Skills3[3371].Level];
                                        }
                                    }
                                }
                            }
                        }
                        //if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                        //{
                        //    ActorMob mob = (ActorMob)target;
                        //    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                        //        damage = damage / 10;
                        //}

                        //if (sActor.type == ActorType.PC)
                        //{
                        //    int score = damage / 100;
                        //    if (score == 0)
                        //        score = 1;
                        //    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, score);
                        //}
                        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                        {
                            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                            damage = (int)(damage * DamageUpRank);
                        }

                        if (target.Status.PhysiceReduceRate > 0)//物理抗性
                        {
                            damage -= (int)((float)damage * (target.Status.PhysiceReduceRate / 100.0f));
                            //if (target.Status.PhysiceReduceRate > 1)
                            //    damage = (int)((float)damage / target.Status.PhysiceReduceRate);
                            //else
                            //    damage = (int)((float)damage / (1.0f + target.Status.PhysiceReduceRate));
                        }
                        if (target.Status.physice_rate_iris < 100)
                        {
                            damage = (int)(damage * (float)(target.Status.physice_rate_iris / 100.0f));
                        }

                        //加伤处理下
                        //if (target.Seals > 0)
                        //    damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
                        //if (sActor.Status.Additions.ContainsKey("ruthless") &&
                        //    (target.Buff.Stun || target.Buff.Stone || target.Buff.Frosen || target.Buff.Poison ||
                        //    target.Buff.Sleep || target.Buff.SpeedDown || target.Buff.Confused || target.Buff.Paralysis))
                        //{
                        //    if (sActor.type == ActorType.PC)
                        //    {
                        //        float rate = 1f + (((ActorPC)sActor).TInt["ruthless"] * 0.1f);
                        //        damage = (int)(damage * rate);//无情打击
                        //    }
                        //}
                        //加伤处理上

                        //减伤处理下(已完成副职逻辑)
                        if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                            damage = (int)(damage * (float)0f);
                        if (target.Status.Additions.ContainsKey("EnergyShield"))//能量加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["EnergyShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        if (target.Status.Additions.ContainsKey("Counter"))
                        {
                            damage /= 2;
                        }

                        if (target.Status.Additions.ContainsKey("Assumptio"))
                        {
                            damage = (int)((damage / 3.0f) * 2.0f);
                        }

                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Party != null && pc.Status.pt_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.pt_dmg_up_iris / 100.0f));
                            }
                            if (pc.Status.Element_down_iris < 100 && element != Elements.Neutral)
                            {
                                damage = (int)(damage * (float)(pc.Status.Element_down_iris / 100.0f));
                            }

                            //iris卡种族减伤部分
                            if (sActor.Race == Race.HUMAN && pc.Status.human_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.human_dmg_down_iris / 100.0f));
                            }

                            else if (sActor.Race == Race.BIRD && pc.Status.bird_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.bird_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ANIMAL && pc.Status.animal_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.animal_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.magic_c_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.PLANT && pc.Status.plant_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.plant_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.water_a_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.MACHINE && pc.Status.machine_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.machine_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ROCK && pc.Status.rock_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.rock_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ELEMENT && pc.Status.element_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.element_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.UNDEAD && pc.Status.undead_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.undead_dmg_down_iris / 100.0f));
                            }


                        }
                        if (target.Status.Additions.ContainsKey("Blocking") && target.Status.Blocking_LV != 0 && target.type == ActorType.PC)//3转骑士格挡
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
                                pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
                                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                                {
                                    int SutanOdds = target.Status.Blocking_LV * 5;
                                    int SutanTime = 1000 + i.Status.Blocking_LV * 500;
                                    int ParryOdds = new int[] { 0, 15, 25, 35, 65, 75 }[target.Status.Blocking_LV];
                                    float ParryResult = 4 + 6 * target.Status.Blocking_LV;
                                    SagaDB.Skill.Skill args = new SagaDB.Skill.Skill();
                                    //不管是主职还是副职,判定盾牌专精知识是否存在
                                    if (pc.Skills.ContainsKey(116) || pc.DualJobSkill.Exists(x => x.ID == 116))
                                    {
                                        //这里取副职的盾牌专精等级
                                        var duallv = 0;
                                        if (pc.DualJobSkill.Exists(x => x.ID == 116))
                                            duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 116).Level;

                                        //这里取主职的剑圣等级
                                        var mainlv = 0;
                                        if (pc.Skills.ContainsKey(116))
                                            mainlv = pc.Skills[116].Level;

                                        //这里取等级最高的剑圣等级用来做居合的倍率加成
                                        int level = Math.Max(duallv, mainlv);

                                        ParryResult += level * 3;
                                        //ParryResult += pc.Skills[116].Level * 3;
                                    }
                                    if (Global.Random.Next(1, 100) <= ParryOdds)
                                    {
                                        damage = damage - (int)(damage * ParryResult / 100.0f);
                                        if (SkillHandler.Instance.CanAdditionApply(target, sActor, SkillHandler.DefaultAdditions.Stun, SutanOdds))
                                        {
                                            Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args, sActor, 1000 + 500 * target.Status.Blocking_LV);
                                            SkillHandler.ApplyAddition(sActor, skill);
                                        }
                                    }
                                }
                            }
                        }
                        //减伤处理上

                        //开始处理最终伤害放大

                        if (sActor.Status.Additions.ContainsKey("HpLostDamUp"))//暂时判定血色战刃在该位置,因为wiki明确提出龙眼有效,所以推测最终伤害百分比增益都有效
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)sActor.Status.Additions["HpLostDamUp"];
                            if (sActor.HP > buf["HPLost"])
                            {
                                sActor.HP -= (uint)buf["HPLost"];
                                damage += buf["DamUp"];
                                SkillArg tmp = new SkillArg();
                                tmp.sActor = sActor.ActorID;
                                tmp.dActor = 0xffffffff;
                                tmp.x = arg.x;
                                tmp.y = arg.y;
                                tmp.argType = SkillArg.ArgType.Active;
                                tmp.autoCast = arg.autoCast;
                                tmp.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2200, 1);
                                tmp.affectedActors.Add(sActor);
                                tmp.Init();
                                tmp.hp[0] = buf["HPLost"];
                                tmp.flag[0] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, tmp, sActor, true);
                            }
                        }

                        //杀戮
                        if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                            damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

                        //血印
                        if (target.Status.Additions.ContainsKey("BradStigma") && element == Elements.Dark)
                        {
                            int rate = (target.Status.Additions["BradStigma"] as DefaultBuff).Variable["BradStigma"];
                            //MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("你的血印技能，使你的暗屬攻擊加成(" + rate + "%)。");
                            damage += (int)((double)damage * (double)((double)rate / 100.0f));
                        }

                        //火心放大
                        if (sActor.Status.Additions.ContainsKey("FrameHart"))
                        {
                            int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                            damage = (int)((double)damage * (double)((double)rate / 100.0f));
                        }

                        //友情的一击
                        if (sActor.Status.Additions.ContainsKey("BlowOfFriendship"))
                        {
                            damage = (int)(damage * 1.15f);
                        }

                        //竜眼放大
                        if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                        {
                            int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                            damage = (int)((double)damage * (double)((double)rate / 100.0f));
                        }
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)sActor;
                            if (pc.Party != null && sActor.Status.pt_dmg_up_iris > 100)
                            {
                                damage = (int)((float)damage * (float)(sActor.Status.pt_dmg_up_iris / 100.0f));
                            }
                            //iris卡种族增伤部分
                            if (target.Race == Race.HUMAN && pc.Status.human_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.human_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.BIRD && pc.Status.bird_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.bird_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.ANIMAL && pc.Status.animal_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.animal_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.magic_c_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.PLANT && pc.Status.plant_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.plant_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.water_a_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.MACHINE && pc.Status.machine_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.machine_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.ROCK && pc.Status.rock_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.rock_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.ELEMENT && pc.Status.element_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.element_dmg_up_iris / 100.0f));
                            }
                            else if (target.Race == Race.UNDEAD && pc.Status.undead_dmg_up_iris > 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.undead_dmg_up_iris / 100.0f));
                            }

                            if (pc.Skills2_1.ContainsKey(310) || pc.DualJobSkill.Exists(x => x.ID == 310))//HAW2-1追魂箭
                            {
                                var duallv = 0;
                                if (pc.DualJobSkill.Exists(x => x.ID == 310))
                                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 310).Level;

                                var mainlv = 0;
                                if (pc.Skills2_1.ContainsKey(310))
                                    mainlv = pc.Skills2_1[310].Level;

                                int level = Math.Max(duallv, mainlv);
                                if (target.Buff.Stun ||
                                   target.Buff.Stone ||
                                   target.Buff.Frosen ||
                                   target.Buff.Poison ||
                                   target.Buff.Sleep ||
                                   target.Buff.SpeedDown ||
                                   target.Buff.Confused ||
                                   target.Buff.Paralysis ||
                                   target.Buff.STRDown ||
                                   target.Buff.VITDown ||
                                   target.Buff.INTDown ||
                                   target.Buff.DEXDown ||
                                   target.Buff.AGIDown ||
                                   target.Buff.MAGDown ||
                                   target.Buff.MaxHPDown ||
                                   target.Buff.MaxMPDown ||
                                   target.Buff.MaxSPDown ||
                                   target.Buff.MinAtkDown ||
                                   target.Buff.MaxAtkDown ||
                                   target.Buff.MinMagicAtkDown ||
                                   target.Buff.MaxMagicAtkDown ||
                                   target.Buff.DefDown ||
                                   target.Buff.DefRateDown ||
                                   target.Buff.MagicDefDown ||
                                   target.Buff.MagicDefRateDown ||
                                   target.Buff.ShortHitDown ||
                                   target.Buff.LongHitDown ||
                                   target.Buff.MagicHitDown ||
                                   target.Buff.ShortDodgeDown ||
                                   target.Buff.LongDodgeDown ||
                                   target.Buff.MagicAvoidDown ||
                                   target.Buff.CriticalRateDown ||
                                   target.Buff.CriticalDodgeDown ||
                                   target.Buff.HPRegenDown ||
                                   target.Buff.MPRegenDown ||
                                   target.Buff.SPRegenDown ||
                                   target.Buff.AttackSpeedDown ||
                                   target.Buff.CastSpeedDown ||
                                   target.Buff.SpeedDown ||
                                   target.Buff.Berserker)
                                {
                                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                                    {
                                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
                                        {
                                            damage = (int)(damage * (1.1f + 0.02f * level));
                                        }
                                    }

                                }
                            }
                            if (pc.Skills2_2.ContainsKey(314) || pc.DualJobSkill.Exists(x => x.ID == 314))//GU2-1追魂刃
                            {
                                var duallv = 0;
                                if (pc.DualJobSkill.Exists(x => x.ID == 314))
                                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 314).Level;

                                var mainlv = 0;
                                if (pc.Skills2_2.ContainsKey(314))
                                    mainlv = pc.Skills2_2[314].Level;

                                int level = Math.Max(duallv, mainlv);
                                if (target.Buff.Stun ||
                                   target.Buff.Stone ||
                                   target.Buff.Frosen ||
                                   target.Buff.Poison ||
                                   target.Buff.Sleep ||
                                   target.Buff.SpeedDown ||
                                   target.Buff.Confused ||
                                   target.Buff.Paralysis ||
                                   target.Buff.STRDown ||
                                   target.Buff.VITDown ||
                                   target.Buff.INTDown ||
                                   target.Buff.DEXDown ||
                                   target.Buff.AGIDown ||
                                   target.Buff.MAGDown ||
                                   target.Buff.MaxHPDown ||
                                   target.Buff.MaxMPDown ||
                                   target.Buff.MaxSPDown ||
                                   target.Buff.MinAtkDown ||
                                   target.Buff.MaxAtkDown ||
                                   target.Buff.MinMagicAtkDown ||
                                   target.Buff.MaxMagicAtkDown ||
                                   target.Buff.DefDown ||
                                   target.Buff.DefRateDown ||
                                   target.Buff.MagicDefDown ||
                                   target.Buff.MagicDefRateDown ||
                                   target.Buff.ShortHitDown ||
                                   target.Buff.LongHitDown ||
                                   target.Buff.MagicHitDown ||
                                   target.Buff.ShortDodgeDown ||
                                   target.Buff.LongDodgeDown ||
                                   target.Buff.MagicAvoidDown ||
                                   target.Buff.CriticalRateDown ||
                                   target.Buff.CriticalDodgeDown ||
                                   target.Buff.HPRegenDown ||
                                   target.Buff.MPRegenDown ||
                                   target.Buff.SPRegenDown ||
                                   target.Buff.AttackSpeedDown ||
                                   target.Buff.CastSpeedDown ||
                                   target.Buff.SpeedDown ||
                                   target.Buff.Berserker)
                                {
                                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
                                    {
                                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHORT_SWORD ||
                                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SWORD ||
                                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RAPIER)
                                        {
                                            damage = (int)(damage * (1.1f + 0.02f * level));
                                        }
                                    }

                                }
                            }
                        }
                        if (sActor.WeaponElement == Elements.Holy)
                        {
                            if (target.Status.Additions.ContainsKey("Oratio"))
                            {
                                damage = (int)((float)damage * 1.25f);
                            }
                        }
                        //"ChgstSwoDamUp"
                        if (sActor.Status.Additions.ContainsKey("ホークアイ"))//HAW站桩
                        {
                            damage = (int)(damage * ((sActor.Status.Additions["ホークアイ"] as DefaultBuff).Variable["ホークアイ"] / 100.0f));
                        }
                        //最终伤害放大处理结束

                        if (sActor.Status.Additions.ContainsKey("RoyalDealer"))//皇家贸易商站桩追加不受任何因素影响的1000伤害
                        {
                            ActorPC pc = (ActorPC)sActor;
                            int maxlv = 0;
                            if (pc.Skills3.ContainsKey(3371))
                                maxlv = pc.Skills3[3371].Level;
                            damage += new int[] { 0, 0, 100, 250, 500, 1000 }[maxlv];
                        }

                        if (i.Status.NeutralDamegeDown_rate > 0 && element == Elements.Neutral)
                        {
                            damage = (int)(damage * (1.0f - (i.Status.NeutralDamegeDown_rate / 100.0f)));

                        }
                        if (i.Status.NeutralDamegeDown_rate > 0 && element != Elements.Neutral)
                        {
                            damage = (int)(damage * (1.0f - (i.Status.ElementDamegeDown_rate / 100.0f)));

                        }
                        //金刚不坏处理
                        if (i.Status.Additions.ContainsKey("MentalStrength"))
                        {
                            int rate = (i.Status.Additions["MentalStrength"] as DefaultBuff).Variable["MentalStrength"];
                            damage = (int)((double)damage * (double)(1.0f - (double)rate / 100.0f));
                        }

                        //处理bonus的技能伤害控制
                        uint skid = 0;
                        if (arg != null)
                        {
                            if (arg.skill != null)
                                skid = arg.skill.ID;
                        }
                        if (skid != 0)
                        {
                            if (sActor.Status.SkillRate.ContainsKey(skid))
                                damage = (int)(damage * (float)(1.0f + (float)((float)sActor.Status.SkillRate[skid] / 100.0f)));
                            if (sActor.Status.SkillDamage.ContainsKey(skid))
                                damage += (int)sActor.Status.SkillDamage[skid];
                        }


                        //处理无法恢复
                        if (target.Buff.NoRegen == true && damage < 0)
                            damage = 0;

                        //吸血效果下
                        if (SuckBlood != 0 && damage != 0)
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                int hp = (int)(damage * SuckBlood);
                                if (((ActorPC)sActor).TInt["SuckBlood"] > 0)
                                    hp = (int)(hp * (float)(1f + ((ActorPC)sActor).TInt["SuckBlood"] * 0.1f));
                                sActor.HP += (uint)hp;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                SkillHandler.Instance.ShowVessel(sActor, -hp);

                                try
                                {
                                    string y1 = "普通攻击";
                                    if (arg != null)
                                    {
                                        if (arg.skill != null)
                                            y1 = arg.skill.Name;
                                    }
                                    SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y1 + "", "受到了 " + (-damage).ToString() + " 点" + "恢复效果");
                                }
                                catch (Exception ex) { SagaLib.Logger.ShowError(ex); }

                            }
                        }
                        //吸血效果上(已完成副职逻辑)
                        if (i.type == ActorType.PC)
                        {
                            ActorPC pcs = (ActorPC)i;


                            //不管是主职还是副职,判定技能黑玫瑰的刺是否存在(前部逻辑不明暂且无视)
                            if (i.Status.Additions.ContainsKey("Bounce") && Global.Random.Next(0, 100) < 35 && (pcs.Skills3.ContainsKey(2497) || pcs.DualJobSkill.Exists(x => x.ID == 2497)))//黒薔薇の棘
                            {

                                //这里取副职的黑玫瑰的刺等级
                                var duallv = 0;
                                if (pcs.DualJobSkill.Exists(x => x.ID == 2497))
                                    duallv = pcs.DualJobSkill.FirstOrDefault(x => x.ID == 2497).Level;

                                //这里取主职的黑玫瑰的刺等级
                                var mainlv = 0;
                                if (pcs.Skills3.ContainsKey(2497))
                                    mainlv = pcs.Skills3[2497].Level;

                                //这里取等级最高的黑玫瑰的刺等级
                                int skilllv = Math.Max(duallv, mainlv);
                                //byte skilllv = pcs.Skills3[2497].Level;
                                float rank = 0;
                                int damage1 = 0;
                                if (sActor.type == ActorType.PC)
                                {
                                    rank = 0.4f + 0.2f * skilllv;
                                }
                                else if (sActor.type == ActorType.MOB)
                                {
                                    rank = 2.0f + 0.2f * skilllv;
                                }
                                damage1 = (int)(damage * rank);
                                arg.affectedActors.Add(sActor);
                                arg.hp.Add(damage1);
                                arg.sp.Add(0);
                                arg.mp.Add(0);
                                arg.flag.Add(AttackFlag.HP_DAMAGE);
                                if (sActor.HP < damage1 + 1)
                                {
                                    sActor.HP -= sActor.HP + 1;
                                }
                                else
                                    sActor.HP -= (uint)damage1;
                            }

                        }
                        try
                        {
                            string y = "普通攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            string s = "物理伤害";
                            if (res == AttackResult.Critical)
                                s = "物理暴击伤害";
                            if (damage < 0)
                                s = "治疗效果";
                            if (damage > 0)
                                SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "受到了 " + damage.ToString() + " 点" + s);
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "造成了 " + damage.ToString() + " 点" + s);
                        }
                        catch (Exception ex) { SagaLib.Logger.ShowError(ex); }

                        //伤害结算之前附加中毒效果,如果有涂毒而且目标没中毒的话
                        if (sActor.Status.Additions.ContainsKey("AppliePoison") && !i.Status.Additions.ContainsKey("Poison"))
                        {
                            if (SkillHandler.Instance.CanAdditionApply(sActor, i, DefaultAdditions.Poison, 95))
                            {
                                Poison poi = new Poison(arg.skill, i, 15000);
                                SkillHandler.ApplyAddition(i, poi);
                            }
                        }

                        if (target.HP != 0)
                        {
                            if (damage > 0)
                            {
                                if (target.Status.PlantShield > 0)
                                {
                                    var dmgleft = target.Status.PlantShield - damage;
                                    if (dmgleft <= 0)
                                    {
                                        target.Status.PlantShield = 0;
                                        target.Status.Additions["PlantShield"].AdditionEnd();

                                        if (target.HP > Math.Abs(dmgleft))
                                            target.HP = (uint)(target.HP + dmgleft);
                                        else
                                        {
                                            target.HP = 0;
                                        }
                                    }
                                    else
                                    {
                                        target.Status.PlantShield -= (uint)damage;
                                    }

                                }
                                else
                                {
                                    if (damage > target.HP)
                                        target.HP = 0;
                                    else
                                        target.HP = (uint)(target.HP - damage);
                                }
                            }
                            else
                                target.HP = (uint)(target.HP - damage);
                        }

                        if (target.HP > target.MaxHP)
                            target.HP = target.MaxHP;

                        //结算HP结果
                        if (target.HP != 0)
                        {
                            arg.hp[index + counter] = damage;
                            if (target.HP > 0)
                            {
                                //damage = (short)sActor.Status.min_atk1;
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;

                                //处理反击
                                if (target.Status.Additions.ContainsKey("Counter"))
                                {
                                    SkillArg newArg = new SkillArg();
                                    float rate = (target.Status.Additions["Counter"] as DefaultBuff).Variable["Counter"] / 100.0f;
                                    SkillHandler.Instance.Attack(target, sActor, newArg, rate);
                                    target.Status.Additions["Counter"].AdditionEnd();
                                    MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
                                }
                                if (target.Status.Additions.ContainsKey("Gladiator"))
                                {
                                    ActorPC pcs = (ActorPC)target;
                                    //if (Global.Random.Next(0, 100) >= 50 && i.HP > damage)
                                    if (Global.Random.Next(0, 100) <= 30 + (10 * pcs.Skills3[3362].Level) && i.HP > 0)//不认可需要进行HP与伤害的互动判定,修改为生命值大于0即可,目前运作正常
                                    {
                                        SkillArg newArg = new SkillArg();
                                        SkillHandler.Instance.Attack(target, sActor, newArg, 1.5f);
                                        MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
                                    }
                                }
                                if (target.Status.Additions.ContainsKey("ArtFullTrap4"))
                                {
                                    SkillArg newArg = new SkillArg();
                                    SkillHandler.Instance.Attack(target, sActor, newArg, 1.0f);
                                    Poison skill = new Poison(newArg.skill, sActor, 7000);
                                    MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
                                }
                            }
                            else
                            {
                                //damage = (int)target.HP;
                                target.HP = 0;
                                if (!ride && !target.Buff.Reborn)
                                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                else
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                if (res == AttackResult.Critical)
                                    arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            }
                            //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;

                            arg.hp[index + counter] = damage;
                        }
                        else
                        {
                            if (!ride && !target.Buff.Reborn)
                                arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            else
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            if (res == AttackResult.Critical)
                                arg.flag[index + counter] |= AttackFlag.CRITICAL;
                            arg.hp[index + counter] = damage;
                        }

                        //吸血？
                        if (sActor.Status.Additions.ContainsKey("BloodLeech") && !sActor.Buff.NoRegen)
                        {
                            Additions.Global.BloodLeech add = (Additions.Global.BloodLeech)sActor.Status.Additions["BloodLeech"];
                            if (sActor.type == ActorType.PC)
                            {
                                int b = 0;
                                b = SagaLib.Global.Random.Next(0, 10000);
                                if (b <= 10000 - (2000 + 1000 * add.rate / 0.1))//wiki没写具体,所以根据内容,推算为1级吸收概率50%,5级10%
                                {
                                    uint heal = (uint)(damage * add.rate);//吸收量
                                    uint healmax = (uint)(sActor.MaxHP * (0.08 + 0.02 * add.rate / 0.1));
                                    if (heal > healmax)
                                    {
                                        heal = healmax;
                                    }

                                    sActor.HP += heal;
                                    if (sActor.HP > sActor.MaxHP)
                                        sActor.HP = sActor.MaxHP;
                                    SkillHandler.Instance.ShowVessel(sActor, (int)-heal, 0, 0);
                                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                                }
                            }
                            else
                            {
                                int heal = (int)(damage * add.rate);
                                arg.affectedActors.Add(sActor);
                                arg.hp.Add(heal);
                                arg.sp.Add(0);
                                arg.mp.Add(0);
                                arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);

                                sActor.HP += (uint)heal;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                            }


                            Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                        }
                        //SP吸收
                        if (sActor.Status.Additions.ContainsKey("SpLeech") && !sActor.Buff.NoRegen)
                        {
                            Additions.Global.SpLeech add = (Additions.Global.SpLeech)sActor.Status.Additions["SpLeech"];
                            if (sActor.type == ActorType.PC)
                            {
                                int SpLevel = (int)(add.rate / 0.05f);
                                if (SagaLib.Global.Random.Next(0, 99) < SpLevel * 10)
                                {
                                    //uint Spheal = (uint)(damage*);//吸收量,SP吸收没有吸收概率,都是100%
                                    uint Sphealmax = (uint)(Math.Min(damage, (uint)(sActor.MaxSP * 0.05 * SpLevel)));
                                    //if (Spheal > Sphealmax)
                                    //{
                                    //    Spheal = Sphealmax;
                                    //}
                                    //arg.affectedActors.Add(sActor);
                                    //arg.hp.Add(0);
                                    //arg.sp.Add((int)Spheal);
                                    //arg.mp.Add(0);
                                    sActor.SP += Sphealmax;
                                    SkillHandler.Instance.ShowVessel(sActor, 0, 0, (int)-Sphealmax);
                                    //arg.flag.Add(AttackFlag.SP_HEAL | AttackFlag.NO_DAMAGE);

                                    //sActor.SP += (uint)Spheal;
                                    if (sActor.SP > sActor.MaxSP)
                                        sActor.SP = sActor.MaxSP;
                                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                                }

                            }
                            else
                            {
                                uint Spheal = (uint)(damage * add.rate);//吸收量,SP吸收没有吸收概率,都是100%
                                if (Spheal > (uint)sActor.MaxSP * 0.5 * 5)
                                {
                                    Spheal = (uint)(sActor.MaxSP * 0.5 * 5);
                                }
                                sActor.SP += Spheal;
                                SkillHandler.Instance.ShowVessel(sActor, 0, 0, (int)-Spheal);
                                sActor.SP += (uint)Spheal;
                                if (sActor.SP > sActor.MaxSP)
                                    sActor.SP = sActor.MaxSP;
                                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
                            }



                        }
                    }
                }

                ApplyDamage(sActor, target, damage, doublehate, arg);
                if ((res == AttackResult.Critical || res == AttackResult.Hit) && sActor.Status.Additions.ContainsKey("WithinWeeks") && sActor.type == ActorType.PC)
                {
                    ActorPC thispc = (ActorPC)sActor;
                    int level = thispc.CInt["WithinWeeksLevel"];
                    switch (thispc.CInt["WithinWeeksLevel"])
                    {
                        case 1:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Silence, 5))
                            {
                                Additions.Global.Silence skill = new SagaMap.Skill.Additions.Global.Silence(arg.skill, target, (int)(750 + 250 * level));
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 2:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.CannotMove, 5))
                            {
                                Additions.Global.CannotMove skill = new SagaMap.Skill.Additions.Global.CannotMove(arg.skill, target, 1000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 3:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Stiff, 5))
                            {
                                Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(arg.skill, target, 1000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 4:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Confuse, 5))
                            {
                                Additions.Global.Confuse skill = new SagaMap.Skill.Additions.Global.Confuse(arg.skill, target, 3000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 5:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Stun, 10 * level))
                            {
                                Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(arg.skill, target, 2000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                    }
                }

                if ((res == AttackResult.Critical || res == AttackResult.Hit) && sActor.Status.Additions.ContainsKey("EnchantWeapon") && sActor.type == ActorType.PC && dActor.Count == 1)
                {
                    ActorPC thispc = (ActorPC)sActor;
                    int level = thispc.CInt["EnchantWeaponLevel"];
                    switch (thispc.CInt["EnchantWeaponLevel"])
                    {
                        case 1:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.CannotMove, 25))
                            {
                                Additions.Global.CannotMove skill = new SagaMap.Skill.Additions.Global.CannotMove(arg.skill, target, 6000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 2:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Frosen, 20))
                            {
                                Additions.Global.Freeze skill = new SagaMap.Skill.Additions.Global.Freeze(arg.skill, target, 4000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                        case 3:
                            if (SkillHandler.Instance.CanAdditionApply(sActor, target, SkillHandler.DefaultAdditions.Stiff, 15))
                            {
                                Additions.Global.Stiff skill = new SagaMap.Skill.Additions.Global.Stiff(arg.skill, target, 2000);
                                SkillHandler.ApplyAddition(target, skill);
                            }
                            break;
                    }
                }


                if ((res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard) && dActor.Count == 1)//弓3转23级技能
                {
                    if (sActor.Status.MissRevenge_rate > 0 && Global.Random.Next(0, 100) < sActor.Status.MissRevenge_rate)
                    {
                        sActor.Status.MissRevenge_hit = true;
                        arg.sActor = sActor.ActorID;
                        arg.dActor = i.ActorID;
                        arg.type = sActor.Status.attackType;
                        arg.delayRate = 1f;
                        PhysicalAttack(sActor, target, arg, sActor.WeaponElement, 1f);
                    }
                }

                if (i.Status.Additions.ContainsKey("TranceBody") && element != Elements.Neutral && element != Elements.Holy && element != Elements.Dark)//ASJOB13技能
                {
                    thispc = (ActorPC)i;
                    if (i.Status.Additions.ContainsKey("HolyShield"))
                        SkillHandler.RemoveAddition(i, "HolyShield");
                    if (i.Status.Additions.ContainsKey("DarkShield"))
                        SkillHandler.RemoveAddition(i, "DarkShield");
                    if (i.Status.Additions.ContainsKey("FireShield"))
                        SkillHandler.RemoveAddition(i, "FireShield");
                    if (i.Status.Additions.ContainsKey("WaterShield"))
                        SkillHandler.RemoveAddition(i, "WaterShield");
                    if (i.Status.Additions.ContainsKey("WindShield"))
                        SkillHandler.RemoveAddition(i, "WindShield");
                    if (i.Status.Additions.ContainsKey("EarthShield"))
                        SkillHandler.RemoveAddition(i, "EarthShield");
                    SkillHandler.RemoveAddition(i, "TranceBody");
                    i.Buff.BodyDarkElementUp = false;
                    i.Buff.BodyEarthElementUp = false;
                    i.Buff.BodyFireElementUp = false;
                    i.Buff.BodyWaterElementUp = false;
                    i.Buff.BodyWindElementUp = false;
                    i.Buff.BodyHolyElementUp = false;
                    int life = 150000 + 30000 * arg.skill.Level;
                    if (element == Elements.Earth)//魔法属性
                    {
                        Toelements = Elements.Earth;
                    }
                    else if (element == Elements.Wind)
                    {
                        Toelements = Elements.Wind;
                    }
                    else if (element == Elements.Fire)
                    {
                        Toelements = Elements.Fire;
                    }
                    else if (element == Elements.Water)
                    {
                        Toelements = Elements.Water;
                    }
                    DefaultBuff skill = new DefaultBuff(arg.skill, i, Toelements.ToString() + "Shield", life);
                    skill.OnAdditionStart += this.StartEventHandlerMele;
                    skill.OnAdditionEnd += this.EndEventHandlerMele;
                    SkillHandler.ApplyAddition(i, skill);

                }

                counter++;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
            }

            short aspd = (short)(sActor.Status.aspd + sActor.Status.aspd_skill);
            if (aspd > 800)
                aspd = 800;
            //可能的攻速位置?备注
            arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);

            arg.delay = (uint)(arg.delay * arg.delayRate);

            if (sActor.Status.aspd_skill_perc >= 1f)
                arg.delay = (uint)(arg.delay / sActor.Status.aspd_skill_perc);

            return damage;
        }


        ///// <summary>
        ///// 对多个目标进行物理攻击
        ///// </summary>
        ///// <param name="sActor">原目标</param>
        ///// <param name="dActor">对象目标集合</param>
        ///// <param name="arg">技能参数</param>
        ///// <param name="element">元素</param>
        ///// <param name="index">arg中参数偏移</param>
        /////<param name="defType">使用的防御类型</param>
        ///// <param name="ATKBonus">攻击加成</param>
        /////  <param name="ignore">无视防御比</param>
        //public int PhysicalAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int index, float ATKBonus, bool setAtk, float SuckBlood, bool doublehate, int shitbonus, int scribonus, int cridamagebonus, string effectname, int lifetime, float ignore = 0, int igoreAddDef = 0)
        //{
        //    if (dActor.Count == 0) return 0;
        //    if (sActor.Status == null) return 0;

        //    if (!CheckStatusCanBeAttact(sActor, 1))
        //        return 0;

        //    if (dActor.Count > 10)
        //    {
        //        foreach (var item in dActor)
        //            DoDamage(true, sActor, item, arg, defType, element, index, ATKBonus);
        //        return 0;
        //    }

        //    if (index == 0)
        //    {
        //        arg.affectedActors = new List<Actor>();
        //        foreach (Actor i in dActor)
        //            arg.affectedActors.Add(i);
        //        arg.Init();
        //    }

        //    # region 基础攻击力计算

        //    int damage = 0;
        //    int atk;
        //    int mindamage = 0;
        //    int maxdamage = 0;
        //    int mindamageM = 0;
        //    int maxdamageM = 0;
        //    int counter = 0;
        //    Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);

        //    switch (arg.type)
        //    {
        //        case ATTACK_TYPE.BLOW:
        //            mindamage = sActor.Status.min_atk1;
        //            maxdamage = sActor.Status.max_atk1;
        //            break;
        //        case ATTACK_TYPE.SLASH:
        //            mindamage = sActor.Status.min_atk2;
        //            maxdamage = sActor.Status.max_atk2;
        //            break;
        //        case ATTACK_TYPE.STAB:
        //            mindamage = sActor.Status.min_atk3;
        //            maxdamage = sActor.Status.max_atk3;
        //            break;
        //    }
        //    //check
        //    if (mindamage > maxdamage) maxdamage = mindamage;

        //    mindamageM = sActor.Status.min_matk;
        //    maxdamageM = sActor.Status.max_matk;
        //    if (mindamageM > maxdamageM) maxdamageM = mindamageM;

        //    # endregion

        //    foreach (Actor i in dActor)
        //    {
        //        if (i.Status == null)
        //            continue;

        //        //NOTOUCH類MOB 跳過判定
        //        if (i.type == ActorType.MOB)
        //        {
        //            ActorMob checkmob = (ActorMob)i;
        //            switch (checkmob.BaseData.mobType)
        //            {
        //                case SagaDB.Mob.MobType.ANIMAL_NOTOUCH:
        //                case SagaDB.Mob.MobType.BIRD_NOTOUCH:
        //                case SagaDB.Mob.MobType.ELEMENT_BOSS_NOTOUCH:
        //                case SagaDB.Mob.MobType.HUMAN_NOTOUCH:
        //                case SagaDB.Mob.MobType.ELEMENT_NOTOUCH:
        //                case SagaDB.Mob.MobType.PLANT_NOTOUCH:
        //                case SagaDB.Mob.MobType.MACHINE_NOTOUCH:
        //                case SagaDB.Mob.MobType.NONE_NOTOUCH:
        //                case SagaDB.Mob.MobType.UNDEAD_NOTOUCH:
        //                case SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH:
        //                case SagaDB.Mob.MobType.PLANT_BOSS_NOTOUCH:
        //                    continue;

        //            }

        //        }

        //        #region 注释内容
        //        //投掷武器
        //        /*if (sActor.type == ActorType.PC)
        //        {
        //            ActorPC pc = (ActorPC)sActor;
        //            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
        //            {
        //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW)
        //                {
        //                    MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
        //                }

        //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD)
        //                {
        //                    MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
        //                }
        //            }
        //        }

        //        //弓箭，枪
        //        if (sActor.type == ActorType.PC)
        //        {
        //            ActorPC pc = (ActorPC)sActor;
        //            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND))
        //            {
        //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BOW)
        //                {
        //                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
        //                    {
        //                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW)
        //                        {
        //                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
        //                                MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
        //                        }
        //                        else
        //                        {
        //                            if (counter == 0)
        //                                arg.result = -1;
        //                            continue;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (counter == 0)
        //                            arg.result = -1;
        //                        continue;
        //                    }
        //                }
        //                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.GUN ||
        //                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.DUALGUN ||
        //                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.RIFLE)
        //                {
        //                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
        //                    {
        //                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET)
        //                        {
        //                            if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
        //                                MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
        //                        }
        //                        else
        //                        {
        //                            if (counter == 0)
        //                                arg.result = -1;
        //                            continue;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (counter == 0)
        //                            arg.result = -1;
        //                        continue;
        //                    }
        //                }
        //            }
        //        }*/
        //        //判断命中结果
        //        //short dis = Map.Distance(sActor, i);
        //        //这个补全技能补正后去掉
        //        #endregion
        //        if (arg.argType == SkillArg.ArgType.Active)
        //            shitbonus = 50;
        //        AttackResult res = CalcAttackResult(sActor, i, sActor.Range > 3);
        //        bool ismiss = false;
        //        if (res == AttackResult.Miss)
        //        {
        //            res = AttackResult.Hit;
        //            ismiss = true;
        //        }
        //        #region 注释卡片判定
        //        /*
        //    if (sActor.type == ActorType.MOB && dActor.type == ActorType.PC)
        //    {
        //        ActorMob mob = (ActorMob)sActor;
        //        Addition[] list = dActor.Status.Additions.Values.ToArray();
        //        foreach (Addition i in list)
        //        {
        //            if (i.GetType() == typeof(Skill.Additions.Global.SomeTypeAvoUp))
        //            {
        //                Additions.Global.SomeTypeAvoUp up = (Additions.Global.SomeTypeAvoUp)i;
        //                if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
        //                {
        //                    dAvoid += (int)(dAvoid * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
        //                }
        //            }
        //        }
        //    }
        //    if (dActor.type == ActorType.MOB && sActor.type == ActorType.PC)
        //    {
        //        ActorMob mob = (ActorMob)dActor;
        //        Addition[] list = sActor.Status.Additions.Values.ToArray();
        //        foreach (Addition i in list)
        //        {
        //            if (i.GetType() == typeof(Skill.Additions.Global.SomeTypeHitUp))
        //            {
        //                Additions.Global.SomeTypeHitUp up = (Additions.Global.SomeTypeHitUp)i;
        //                if (up.MobTypes.ContainsKey(mob.BaseData.mobType))
        //                {
        //                    sHit += (int)(sHit * ((float)up.MobTypes[mob.BaseData.mobType] / 100));
        //                }
        //            }
        //        }
        //    }*/
        //        #endregion
        //        Actor target = i;
        //        //if (i.type == ActorType.PC)
        //        //{
        //        //    ActorPC me = (ActorPC)i;
        //        //    if (me.Status.Additions.ContainsKey("圣骑士的牺牲") && me.Party != null)
        //        //    {
        //        //        ActorPC victim = (ActorPC)map.Actors[(uint)me.TInt["牺牲者ActorID"]];
        //        //        if (victim == null) break;
        //        //        if (victim.Party != me.Party && (5 > Math.Max(Math.Abs(me.X - victim.X) / 100, Math.Abs(me.Y - victim.Y) / 100))) break;
        //        //        target = victim;
        //        //        ShowEffectByActor(i, 4345);
        //        //    }
        //        //}

        //        if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard)
        //        {
        //            if (res == AttackResult.Miss)
        //            {
        //                arg.flag[index + counter] = AttackFlag.MISS;
        //            }
        //            else if (res == AttackResult.Avoid)
        //                arg.flag[index + counter] = AttackFlag.AVOID;
        //            else
        //                arg.flag[index + counter] = AttackFlag.GUARD;
        //            if (i.Status.Additions.ContainsKey("Parry"))//格挡
        //            {
        //                if (sActor == null)
        //                    return 0;
        //                ActorPC pc = (ActorPC)i;
        //                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) || pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
        //                {
        //                    Network.Client.MapClient.FromActorPC(pc).SendSkillDummy(100, 1);
        //                    if (i.Status.Additions.ContainsKey("Parry"))
        //                        i.Status.Additions["Parry"].AdditionEnd();
        //                    ShowEffect(pc, pc, 4135);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            int restKryrie = 0;
        //            if (i.Status.Additions.ContainsKey("MobKyrie"))//救援邀请，留着有参考价值
        //            {
        //                Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["MobKyrie"];
        //                restKryrie = buf["MobKyrie"];
        //                arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
        //                if (restKryrie > 0)
        //                {
        //                    buf["MobKyrie"]--;
        //                    EffectArg arg2 = new EffectArg();
        //                    arg2.effectID = 4173;
        //                    arg2.actorID = i.ActorID;
        //                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
        //                    if (restKryrie == 1)
        //                        SkillHandler.RemoveAddition(i, "MobKyrie");
        //                }
        //            }
        //            if (restKryrie == 0)
        //            {
        //                bool isPossession = false;
        //                bool isHost = false;
        //                if (i.type == ActorType.PC)
        //                {
        //                    ActorPC pc = (ActorPC)i;
        //                    if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
        //                    {
        //                        isPossession = true;
        //                        isHost = true;
        //                    }
        //                    if (pc.PossessionTarget != 0)
        //                    {
        //                        isPossession = true;
        //                        isHost = false;
        //                    }
        //                }
        //                //处理凭依伤害
        //                if (isHost && isPossession && ATKBonus > 0)
        //                {
        //                    List<Actor> possessionDamage = ProcessAttackPossession(i);
        //                    if (possessionDamage.Count > 0)
        //                    {
        //                        arg.Remove(i);
        //                        int oldcount = arg.flag.Count;
        //                        arg.Extend(possessionDamage.Count);
        //                        foreach (Actor j in possessionDamage)
        //                        {
        //                            if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
        //                                arg.affectedActors.Add(i);
        //                            else
        //                                arg.affectedActors.Add(j);
        //                        }
        //                        PhysicalAttack(sActor, possessionDamage, arg, element, oldcount, ATKBonus);
        //                        continue;
        //                    }
        //                }

        //                if (!setAtk)
        //                {
        //                    atk = (int)Global.Random.Next(mindamage, maxdamage);
        //                    //TODO: element bonus, range bonus
        //                    atk = (int)(atk * CalcElementBonus(sActor, i, element, 0, false) * ATKBonus);
        //                }
        //                else
        //                {
        //                    atk = (int)Global.Random.Next(mindamage, maxdamage);
        //                    atk += (int)Global.Random.Next(mindamageM, maxdamageM);

        //                    atk = (int)(atk * CalcElementBonus(sActor, i, element, 0, false) * ATKBonus);
        //                    //atk = (int)ATKBonus;
        //                }
        //                //int igoreAddDef = 0;


        //                damage = CalcPhyDamage(sActor, i, defType, atk, ignore);

        //                if (damage > atk)
        //                    damage = atk;

        //                if (i.type == ActorType.PARTNER) return 20;
        //                IStats stats = (IStats)i;
        //                switch (arg.type)
        //                {
        //                    case ATTACK_TYPE.BLOW:
        //                        damage = (int)(damage * (1f - i.Status.damage_atk1_discount));
        //                        break;
        //                    case ATTACK_TYPE.SLASH:
        //                        damage = (int)(damage * (1f - i.Status.damage_atk2_discount));
        //                        break;
        //                    case ATTACK_TYPE.STAB:
        //                        damage = (int)(damage * (1f - i.Status.damage_atk3_discount));
        //                        break;
        //                }

        //                if (sActor.type == ActorType.PC && target.type == ActorType.PC)
        //                {
        //                    damage = (int)(damage * Configuration.Instance.PVPDamageRatePhysic);
        //                }
        //                damage = checkbuff(sActor, target, arg, 0, damage);
        //                damage = checkirisbuff(sActor, target, arg, 0, damage);
        //                if (damage <= 0) damage = 1;

        //                if (res == AttackResult.Critical)
        //                {
        //                    damage = (int)(((float)damage) * (float)(CalcCritBonus(sActor, target, scribonus) + (float)cridamagebonus));
        //                    if (sActor.Status.Additions.ContainsKey("CriDamUp"))
        //                    {
        //                        float rate = (float)((float)(sActor.Status.Additions["CriDamUp"] as DefaultPassiveSkill).Variable["CriDamUp"] / 100.0f + 1.0f);
        //                        damage = (int)((float)damage * rate);
        //                    }
        //                }

        //                checkdebuff(sActor, target, arg, 0);
        //                //宠判定？
        //                bool ride = false;
        //                if (target.type == ActorType.PC)
        //                {
        //                    ActorPC pc = (ActorPC)target;
        //                    if (pc.Pet != null)
        //                        ride = pc.Pet.Ride;
        //                }
        //                //宠物成长
        //                if (res == AttackResult.Critical)
        //                {
        //                    if (sActor.type == ActorType.PET)
        //                        ProcessPetGrowth(sActor, PetGrowthReason.CriticalHit);
        //                    if (i.type == ActorType.PET && damage > 0)
        //                        ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
        //                    if (i.type == ActorType.PC && damage > 0)
        //                    {
        //                        ActorPC pc = (ActorPC)target;

        //                        if (ride)
        //                        {
        //                            ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (sActor.type == ActorType.PET)
        //                        ProcessPetGrowth(sActor, PetGrowthReason.PhysicalHit);
        //                    if (i.type == ActorType.PET && damage > 0)
        //                    {
        //                        ProcessPetGrowth(i, PetGrowthReason.PhysicalBeenHit);
        //                    }
        //                    if (i.type == ActorType.PC && damage > 0)
        //                    {
        //                        ActorPC pc = (ActorPC)target;

        //                        if (ride)
        //                        {
        //                            ProcessPetGrowth(pc.Pet, PetGrowthReason.PhysicalBeenHit);
        //                        }
        //                    }
        //                }

        //                //技能以及状态判定
        //                if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
        //                {
        //                    ActorMob mob = (ActorMob)target;
        //                    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
        //                        damage = damage / 10;
        //                }

        //                if (sActor.type == ActorType.PC)
        //                {
        //                    int score = damage / 100;
        //                    if (score == 0)
        //                        score = 1;
        //                    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, score);
        //                }
        //                //加伤处理下
        //                if (i.Status.Additions.ContainsKey("Invincible"))//绝对壁垒
        //                    damage = 0;
        //                //技能以及状态判定
        //                if (sActor.type == ActorType.PC)
        //                {
        //                    ActorPC pcsActor = (ActorPC)sActor;
        //                    if (sActor.Status.Additions.ContainsKey("BurnRate"))// && SkillHandler.Instance.isEquipmentRight(pcsActor, SagaDB.Item.ItemType.CARD))//皇家贸易商
        //                    {
        //                        if (pcsActor.Skills3.ContainsKey(3371))
        //                        {
        //                            if (pcsActor.Skills3[3371].Level > 1)
        //                            {
        //                                int[] gold = { 0, 0, 100, 250, 500, 1000 };
        //                                if (pcsActor.Gold > gold[pcsActor.Skills3[3371].Level])
        //                                {
        //                                    pcsActor.Gold -= gold[pcsActor.Skills3[3371].Level];
        //                                    damage += gold[pcsActor.Skills3[3371].Level];
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                if (sActor.type == ActorType.PC && i.type == ActorType.MOB)
        //                {
        //                    ActorMob mob = (ActorMob)i;
        //                    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
        //                        damage = damage / 10;
        //                }

        //                //if (sActor.type == ActorType.PC)
        //                //{
        //                //    int score = damage / 100;
        //                //    if (score == 0)
        //                //        score = 1;
        //                //    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, score);
        //                //}
        //                if (i.Status.Additions.ContainsKey("DamageUp"))//伤害标记
        //                {
        //                    float DamageUpRank = i.Status.Damage_Up_Lv * 0.1f + 1.1f;
        //                    damage = (int)(damage * DamageUpRank);
        //                }

        //                if (i.Status.PhysiceReduceRate > 0)//物理抗性
        //                {
        //                    if (i.Status.PhysiceReduceRate > 1)
        //                        damage = (int)((float)damage / i.Status.PhysiceReduceRate);
        //                    else
        //                        damage = (int)((float)damage / (1.0f + i.Status.PhysiceReduceRate));
        //                }

        //                //加伤处理下
        //                if (i.Seals > 0)
        //                    damage = (int)(damage * (float)(1f + 0.05f * i.Seals));//圣印
        //                if (sActor.Status.Additions.ContainsKey("ruthless") &&
        //                    (i.Buff.Stun || i.Buff.Stone || i.Buff.Frosen || i.Buff.Poison ||
        //                    i.Buff.Sleep || i.Buff.SpeedDown || i.Buff.Confused || i.Buff.Paralysis))
        //                {
        //                    if (sActor.type == ActorType.PC)
        //                    {
        //                        float rate = 1f + (((ActorPC)sActor).TInt["ruthless"] * 0.1f);
        //                        damage = (int)(damage * rate);//无情打击
        //                    }
        //                }
        //                //加伤处理上

        //                //减伤处理下
        //                if (i.Status.Additions.ContainsKey("DamageNullify"))//boss状态
        //                    damage = (int)(damage * (float)0f);
        //                if (i.Status.Additions.ContainsKey("EnergyShield"))//能量加护
        //                {
        //                    if (i.type == ActorType.PC)
        //                        damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)i).TInt["EnergyShieldlv"]));
        //                    else
        //                        damage = (int)(damage * (float)0.9f);
        //                }
        //                if (i.Status.Additions.ContainsKey("Counter"))
        //                {
        //                    damage /= 2;
        //                }

        //                if (i.Status.Additions.ContainsKey("Blocking") && i.Status.Blocking_LV != 0 && i.type == ActorType.PC)//3转骑士格挡
        //                {
        //                    ActorPC pc = (ActorPC)i;
        //                    if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND) &&
        //                        pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))
        //                    {
        //                        if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD ||
        //                            pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
        //                        {
        //                            int SutanOdds = i.Status.Blocking_LV * 5;
        //                            int SutanTime = 1000 + i.Status.Blocking_LV * 500;
        //                            int ParryOdds = new int[] { 0, 15, 25, 35, 65, 75 }[i.Status.Blocking_LV];
        //                            float ParryResult = 4 + 6 * i.Status.Blocking_LV;
        //                            SagaDB.Skill.Skill args = new SagaDB.Skill.Skill();
        //                            if (pc.Skills.ContainsKey(116))
        //                            {
        //                                ParryResult += pc.Skills[116].Level * 3;
        //                            }
        //                            if (Global.Random.Next(1, 100) <= ParryOdds)
        //                            {
        //                                damage = damage - (int)(damage * ParryResult / 100.0f);
        //                                if (SkillHandler.Instance.CanAdditionApply(i, sActor, SkillHandler.DefaultAdditions.Stun, SutanOdds))
        //                                {
        //                                    Additions.Global.Stun skill = new SagaMap.Skill.Additions.Global.Stun(args, sActor, 1000 + 500 * i.Status.Blocking_LV);
        //                                    SkillHandler.ApplyAddition(sActor, skill);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                //减伤处理上

        //                //开始处理最终伤害放大

        //                //杀戮放大
        //                if (sActor.Status.Additions.ContainsKey("Efuikasu"))
        //                    damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

        //                //火心放大
        //                if (sActor.Status.Additions.ContainsKey("FrameHart"))
        //                {
        //                    int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
        //                    damage = (int)((double)damage * (double)((double)rate / 100));
        //                }

        //                //竜眼放大
        //                if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
        //                {
        //                    int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
        //                    damage = (int)((double)damage * (double)((double)rate / 100));
        //                }
        //                //吸血效果下
        //                if (SuckBlood != 0)
        //                {
        //                    if (sActor.type == ActorType.PC)
        //                    {
        //                        int hp = (int)(damage * SuckBlood);
        //                        if (((ActorPC)sActor).TInt["SuckBlood"] > 0)
        //                            hp = (int)(hp * (float)(1f + ((ActorPC)sActor).TInt["SuckBlood"] * 0.1f));
        //                        sActor.HP += (uint)hp;
        //                        if (sActor.HP > sActor.MaxHP)
        //                            sActor.HP = sActor.MaxHP;
        //                        Instance.ShowVessel(sActor, -hp);
        //                    }
        //                }
        //                //吸血效果上


        //                if (i.type == ActorType.PC)
        //                {
        //                    ActorPC pcs = (ActorPC)i;

        //                    if (i.Status.Additions.ContainsKey("剑斗士"))
        //                    {
        //                        if (Global.Random.Next(0, 100) >= 50 && i.HP > damage)
        //                            PhysicalAttack(i, sActor, arg, Elements.Neutral, 1.5f);
        //                    }

        //                    if (i.Status.Additions.ContainsKey("Bounce") && Global.Random.Next(0, 100) < 35 && pcs.Skills3.ContainsKey(2497))//黒薔薇の棘
        //                    {
        //                        byte skilllv = pcs.Skills3[2497].Level;
        //                        float rank = 0;
        //                        int damage1 = 0;
        //                        if (sActor.type == ActorType.PC)
        //                        {
        //                            rank = 0.4f + 0.2f * skilllv;
        //                        }
        //                        else if (sActor.type == ActorType.MOB)
        //                        {
        //                            rank = 2.0f + 0.2f * skilllv;
        //                        }
        //                        damage1 = (int)(damage * rank);
        //                        arg.affectedActors.Add(sActor);
        //                        arg.hp.Add(damage1);
        //                        arg.sp.Add(0);
        //                        arg.mp.Add(0);
        //                        arg.flag.Add(AttackFlag.HP_DAMAGE);
        //                        if (sActor.HP < damage1 + 1)
        //                        {
        //                            sActor.HP -= sActor.HP + 1;
        //                        }
        //                        else
        //                            sActor.HP -= (uint)damage1;
        //                    }

        //                }

        //                //结算HP结果
        //                arg.hp[index + counter] = damage;
        //                if (damage >= 0)
        //                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                else
        //                    arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
        //                if (res == AttackResult.Critical)
        //                    arg.flag[index + counter] |= AttackFlag.CRITICAL;

        //                //伤害结算之前附加中毒效果,如果有涂毒而且目标没中毒的话
        //                if (sActor.Status.Additions.ContainsKey("AppliePoison") && !i.Status.Additions.ContainsKey("Poison"))
        //                {
        //                    if (SkillHandler.Instance.CanAdditionApply(sActor, i, DefaultAdditions.Poison, 95))
        //                    {
        //                        Poison poi = new Poison(arg.skill, i, 15000);
        //                        SkillHandler.ApplyAddition(i, poi);
        //                    }
        //                }

        //                //结算HP结果
        //                if (target.HP != 0)
        //                {
        //                    arg.hp[index + counter] = damage;
        //                    if (target.HP > damage)
        //                    {
        //                        //damage = (short)sActor.Status.min_atk1;
        //                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                        if (res == AttackResult.Critical)
        //                            arg.flag[index + counter] |= AttackFlag.CRITICAL;

        //                        //处理反击
        //                        if (target.Status.Additions.ContainsKey("Counter"))
        //                        {
        //                            SkillArg newArg = new SkillArg();
        //                            float rate = (target.Status.Additions["Counter"] as DefaultBuff).Variable["Counter"] / 100.0f;
        //                            SkillHandler.Instance.Attack(target, sActor, newArg, rate);
        //                            target.Status.Additions["Counter"].AdditionEnd();
        //                            MapManager.Instance.GetMap(target.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, newArg, target, true);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        damage = (int)target.HP;
        //                        if (!ride && !target.Buff.Reborn)
        //                            arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                        else
        //                            arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                        if (res == AttackResult.Critical)
        //                            arg.flag[index + counter] |= AttackFlag.CRITICAL;
        //                    }
        //                    //arg.flag[i] |=  AttackFlag.ATTACK_EFFECT;
        //                    if (target.HP != 0)
        //                        target.HP = (uint)(target.HP - damage);
        //                }
        //                else
        //                {
        //                    if (!ride && !target.Buff.Reborn)
        //                        arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                    else
        //                        arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                    if (res == AttackResult.Critical)
        //                        arg.flag[index + counter] |= AttackFlag.CRITICAL;
        //                    arg.hp[index + counter] = damage;
        //                }

        //                //吸血？
        //                if (sActor.Status.Additions.ContainsKey("BloodLeech") && !sActor.Buff.NoRegen)
        //                {
        //                    Additions.Global.BloodLeech add = (Additions.Global.BloodLeech)sActor.Status.Additions["BloodLeech"];
        //                    int heal = (int)(damage * add.rate);
        //                    arg.affectedActors.Add(sActor);
        //                    arg.hp.Add(heal);
        //                    arg.sp.Add(0);
        //                    arg.mp.Add(0);
        //                    arg.flag.Add(AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE);

        //                    sActor.HP += (uint)heal;
        //                    if (sActor.HP > sActor.MaxHP)
        //                        sActor.HP = sActor.MaxHP;
        //                    Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        //                }
        //            }
        //        }
        //        ApplyDamage(sActor, target, damage, doublehate, arg);
        //        counter++;
        //        Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
        //        //MapClient.FromActorPC((ActorPC)sActor).SendPartyMemberHPMPSP((ActorPC)sActor);
        //    }


        //    short aspd = (short)(sActor.Status.aspd + sActor.Status.aspd_skill);
        //    if (aspd > 800)
        //        aspd = 800;

        //    arg.delay = 2000 - (uint)(2000 * aspd * 0.001f);

        //    arg.delay = (uint)(arg.delay * arg.delayRate);

        //    if (sActor.Status.aspd_skill_perc >= 1f)
        //        arg.delay = (uint)(arg.delay / sActor.Status.aspd_skill_perc);

        //    return damage;
        //}

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }
        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, float MATKBonus, float ignore)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, DefType.MDef, element, 50, MATKBonus, 0, false, false, 0, false, ignore);
        }
        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, element, elementValue, MATKBonus);
        }

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, Actor dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            List<Actor> list = new List<Actor>();
            list.Add(dActor);
            return MagicAttack(sActor, list, arg, defType, element, elementValue, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, element, elementValue, MATKBonus, 0);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0);//Use element holy to represent not using this param.
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, Elements element, int elementValue, float MATKBonus, int index)
        {
            return MagicAttack(sActor, dActor, arg, DefType.MDef, element, elementValue, MATKBonus, index);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, false);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, float MATKBonus, int index, bool setAtk)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, 50, MATKBonus, index, setAtk);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, setAtk, false);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, index, setAtk, false, 0);
        }
        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        {
            return MagicAttack(sActor, dActor, arg, defType, element, elementValue, MATKBonus, 0, index, setAtk, false, SuckBlood, WeaponAttack, ignore);
        }

        public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int mcridamagebonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        {

            if (dActor.Count == 0)
                return 0;
            if (sActor.Status == null)
                return 0;

            //wiz3转元素增伤
            //if (sActor.Status.PlusElement_rate > 0)
            //   MATKBonus += sActor.Status.PlusElement_rate;

            if (dActor.Count > 10)
            {
                foreach (var item in dActor)
                    DoDamage(false, sActor, item, arg, defType, element, index, MATKBonus);
                return 0;
            }

            int damage = 0;

            //calculate the MATK
            int matk;
            int mindamage = 0;
            int maxdamage = 0;
            int counter = 0;
            Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);
            if (index == 0)
            {
                arg.affectedActors = new List<Actor>();
                foreach (Actor i in dActor)
                    arg.affectedActors.Add(i);
                arg.Init();
            }
            if (WeaponAttack)
            {
                mindamage = ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].MAtk + ((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.matk;

                maxdamage = mindamage;
            }
            else
            {
                mindamage = sActor.Status.min_matk;
                maxdamage = sActor.Status.max_matk;
            }

            if (mindamage > maxdamage) maxdamage = mindamage;

            foreach (Actor i in dActor)
            {
                damage = 0;
                Actor target = i;
                if (i.Status == null)
                    continue;
                if (i.type == ActorType.ITEM)
                    continue;
                //NOTOUCH類MOB 跳過判定
                if (i.type == ActorType.MOB)
                {
                    ActorMob checkmob = (ActorMob)i;
                    switch (checkmob.BaseData.mobType)
                    {
                        case SagaDB.Mob.MobType.ANIMAL_NOTOUCH:
                        case SagaDB.Mob.MobType.BIRD_NOTOUCH:
                        case SagaDB.Mob.MobType.ELEMENT_BOSS_NOTOUCH:
                        case SagaDB.Mob.MobType.HUMAN_NOTOUCH:
                        case SagaDB.Mob.MobType.ELEMENT_NOTOUCH:
                        case SagaDB.Mob.MobType.PLANT_NOTOUCH:
                        case SagaDB.Mob.MobType.MACHINE_NOTOUCH:
                        case SagaDB.Mob.MobType.NONE_NOTOUCH:
                        case SagaDB.Mob.MobType.UNDEAD_NOTOUCH:
                        case SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH:
                        case SagaDB.Mob.MobType.PLANT_BOSS_NOTOUCH:
                            continue;

                    }
                }
                if (i.type == ActorType.PC && i.Status.Additions.ContainsKey("GoodLucky"))
                {
                    ActorPC pc = (ActorPC)i;
                    if (pc.Skills2_2.ContainsKey(960) || pc.DualJobSkill.Exists(x => x.ID == 960))
                    {
                        var duallv = 0;
                        if (pc.DualJobSkill.Exists(x => x.ID == 960))
                            duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 960).Level;

                        //这里取主职的剑圣等级
                        var mainlv = 0;
                        if (pc.Skills2_2.ContainsKey(960))
                            mainlv = pc.Skills2_2[960].Level;

                        int maxlv = Math.Max(duallv, mainlv) * 4;
                        if (SagaLib.Global.Random.Next(0, 99) < maxlv)
                        {
                            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("魔法被回避了");
                            continue;
                        }
                    }
                }
                int restKryrie = 0;
                if (i.Status.Additions.ContainsKey("DispelField"))
                {
                    Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)i.Status.Additions["DispelField"];
                    restKryrie = buf["DispelField"];
                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.NO_DAMAGE;
                    if (restKryrie > 0)
                    {
                        buf["DispelField"]--;
                        EffectArg arg2 = new EffectArg();
                        arg2.effectID = 4173;
                        arg2.actorID = i.ActorID;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg2, i, true);
                        if (restKryrie == 1)
                            SkillHandler.RemoveAddition(i, "DispelField");
                    }
                }
                if (restKryrie == 0)
                {
                    //判断命中结果
                    //short dis = Map.Distance(sActor, i);
                    //if (arg.argType == SkillArg.ArgType.Active)
                    //    shitbonus = 50;
                    AttackResult res = CalcMagicAttackResult(sActor, i);
                    //res = AttackResult.Miss;

                    if (res == AttackResult.Miss || res == AttackResult.Avoid || res == AttackResult.Guard || res == AttackResult.Parry)
                    {
                        if (res == AttackResult.Miss)
                            arg.flag[index + counter] = AttackFlag.MISS;
                        else if (res == AttackResult.Avoid)
                            arg.flag[index + counter] = AttackFlag.AVOID;
                        else if (res == AttackResult.Parry)
                            arg.flag[index + counter] = AttackFlag.AVOID2;
                        else
                            arg.flag[index + counter] = AttackFlag.GUARD;

                        try
                        {
                            string y = "普通攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            //string s = "物理伤害";
                            SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "被你 " + res.ToString());
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "被 " + res.ToString());
                        }
                        catch (Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                        }
                    }
                    else
                    {

                        if (i.Status.Additions.ContainsKey("MagicReflect") && i != sActor && !noReflect)
                        {
                            arg.Remove(i);
                            int oldcount = arg.flag.Count;
                            arg.Extend(1);
                            arg.affectedActors.Add(sActor);
                            List<Actor> dst = new List<Actor>();
                            dst.Add(sActor);
                            RemoveAddition(i, "MagicReflect");
                            MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, setAtk, true);
                            continue;
                        }
                        if (i.Status.reflex_odds > 0 && i.type == ActorType.PC)//3转骑士反射盾
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.LEFT_HAND))//右手不可能持盾
                            {
                                if (pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.SHIELD)
                                {
                                    if (Global.Random.Next(0, 100) < 100 * i.Status.reflex_odds)
                                    {
                                        arg.Remove(i);
                                        int oldcount = arg.flag.Count;
                                        arg.Extend(1);
                                        arg.affectedActors.Add(sActor);
                                        List<Actor> dst = new List<Actor>();
                                        dst.Add(sActor);
                                        MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, false, true);
                                        continue;
                                    }
                                }
                            }
                        }

                        bool isPossession = false;
                        bool isHost = false;

                        if (i.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)i;
                            if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
                            {
                                isPossession = true;
                                isHost = true;
                            }
                            if (pc.PossessionTarget != 0)
                            {
                                isPossession = true;
                                isHost = false;
                            }
                        }
                        //处理凭依伤害
                        if (isHost && isPossession && MATKBonus > 0)
                        {
                            List<Actor> possessionDamage = ProcessAttackPossession(i);
                            if (possessionDamage.Count > 0)
                            {
                                arg.Remove(i);
                                int oldcount = arg.flag.Count;
                                arg.Extend(possessionDamage.Count);
                                foreach (Actor j in possessionDamage)
                                {
                                    if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
                                        arg.affectedActors.Add(i);
                                    else
                                        arg.affectedActors.Add(j);
                                }
                                MagicAttack(sActor, possessionDamage, arg, element, elementValue, MATKBonus, oldcount);
                                continue;
                            }
                        }
                        //先校验施法者是不是玩家
                        if (sActor.type == ActorType.PC)
                        {
                            //wiz3转JOB3,属性对无属性魔法增伤
                            ActorPC pci = sActor as ActorPC;
                            float rates = 0;
                            //不管是主职还是副职, 只要习得技能,则进入增伤判定
                            if ((pci.Skills3.ContainsKey(986) || pci.DualJobSkill.Exists(x => x.ID == 986)) && element == Elements.Neutral)
                            {
                                //这里取副职的等级
                                var duallv = 0;
                                if (pci.DualJobSkill.Exists(x => x.ID == 986))
                                    duallv = pci.DualJobSkill.FirstOrDefault(x => x.ID == 986).Level;

                                //这里取主职的等级
                                var mainlv = 0;
                                if (pci.Skills3.ContainsKey(986))
                                    mainlv = pci.Skills3[986].Level;
                                rates = 0.02f + 0.002f * mainlv;
                                //int elements = (int)pci.WeaponElement[pci.WeaponElement];
                                int elements = pci.Status.attackElements_item[pci.WeaponElement]
                                    + pci.Status.attackElements_skill[pci.WeaponElement]
                                    + pci.Status.attackelements_iris[pci.WeaponElement];

                                //int elements = pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Dark] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Earth] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Fire] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Holy] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Neutral] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Water] +
                                //pci.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.element[SagaLib.Elements.Wind];

                                if (elements > 0)
                                {
                                    MATKBonus += rates * elements;
                                }

                            }

                        }
                        if (!setAtk)
                        {
                            matk = Global.Random.Next(mindamage, maxdamage);
                            if (element != Elements.Neutral)
                            {
                                float eleBonus = CalcElementBonus(sActor, i, element, 1, ((MATKBonus < 0) && !((i.Status.undead == true) && (element == Elements.Holy))));
                                if ((sActor.Status.Additions.ContainsKey("EvilSoul") || sActor.Status.Additions.ContainsKey("SoulTaker")) && element == Elements.Dark && eleBonus > 0)
                                {
                                    if (sActor.Status.Additions.ContainsKey("EvilSoul"))
                                    {
                                        //atkValue += (sActor.Status.Additions["EvilSoul"] as DefaultBuff).Variable["EvilSoul"];
                                        eleBonus += ((float)((sActor.Status.Additions["EvilSoul"] as DefaultBuff).Variable["EvilSoul"]) / 100.0f);
                                    }
                                    if (sActor.Status.Additions.ContainsKey("SoulTaker") && arg.skill != null && arg.skill.ID != 0)
                                    {
                                        //atkValue += (sActor.Status.Additions["SoulTaker"] as DefaultBuff).Variable["SoulTaker"];
                                        eleBonus += ((float)((sActor.Status.Additions["SoulTaker"] as DefaultBuff).Variable["SoulTaker"]) / 100.0f);
                                    }
                                }
                                if (sActor.Status.Contract_Lv != 0)//CAJOB40
                                {
                                    Elements tmpele = Elements.Neutral;
                                    switch (sActor.Status.Contract_Lv)
                                    {
                                        case 1:
                                            tmpele = Elements.Fire;
                                            break;
                                        case 2:
                                            tmpele = Elements.Water;
                                            break;
                                        case 3:
                                            tmpele = Elements.Earth;
                                            break;
                                        case 4:
                                            tmpele = Elements.Wind;
                                            break;
                                    }
                                    if (tmpele == element)
                                        eleBonus += 0.5f;
                                    else
                                        eleBonus -= 0.65f;

                                }
                                if (i.Status.Contract_Lv != 0)
                                {
                                    Elements tmpele = Elements.Neutral;
                                    switch (i.Status.Contract_Lv)
                                    {
                                        case 1:
                                            tmpele = Elements.Fire;
                                            break;
                                        case 2:
                                            tmpele = Elements.Water;
                                            break;
                                        case 3:
                                            tmpele = Elements.Earth;
                                            break;
                                        case 4:
                                            tmpele = Elements.Wind;
                                            break;
                                    }
                                    if (tmpele == element)
                                        eleBonus -= 0.15f;
                                    else
                                        eleBonus += 1.0f;

                                }

                                matk = (int)(matk * eleBonus * MATKBonus);

                            }
                            else
                                matk = (int)(matk * 1f * MATKBonus);
                        }
                        else
                            matk = (int)MATKBonus;

                        if (MATKBonus > 0)
                        {
                            damage = CalcMagDamage(sActor, i, defType, matk, ignore);
                        }
                        else
                        {
                            damage = matk;
                        }

                        //AttackResult res = AttackResult.Hit;
                        //AttackResult res = CalcAttackResult(sActor, target, true);
                        //if (res == AttackResult.Critical)
                        //    res = AttackResult.Hit;
                        /*
                        if (i.Buff.Frosen == true && element == Elements.Fire)
                        {
                            RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
                        }
                        if (i.Buff.Stone == true && element == Elements.Water)
                        {
                            RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
                        }
                        */

                        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
                        {
                            if (damage > 0)
                                damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
                        }

                        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
                        {
                            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
                            damage = (int)(damage * DamageUpRank);
                        }
                        if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
                            damage = (int)(damage * (float)0f);

                        if (target.Status.MagicRuduceRate > 0)//魔法抵抗力
                        {
                            if (target.Status.MagicRuduceRate > 1)
                                damage = (int)((float)damage / target.Status.MagicRuduceRate);
                            else
                                damage = (int)((float)damage / (1.0f + target.Status.MagicRuduceRate));
                        }

                        if (target.Status.magic_rate_iris < 100 && MATKBonus >= 0)//iris卡片提供的魔法伤害减少
                        {
                            damage = (int)(damage * (float)(target.Status.magic_rate_iris / 100.0f));
                        }


                        if (damage <= 0 && MATKBonus >= 0)
                            damage = 1;

                        if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
                        {
                            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
                            if (Global.Random.Next(0, 99) < buf["Rate"])
                            {
                                Actor dst = map.GetActor((uint)buf["Target"]);
                                if (dst != null)
                                {
                                    target = dst;
                                    arg.affectedActors[index + counter] = target;
                                }
                            }
                        }
                        if (sActor.type == ActorType.PET)
                            ProcessPetGrowth(sActor, PetGrowthReason.SkillHit);
                        if (i.type == ActorType.PET && damage > 0)
                            ProcessPetGrowth(i, PetGrowthReason.MagicalBeenHit);

                        bool ride = false;
                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Pet != null)
                                ride = pc.Pet.Ride;
                        }

                        //if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
                        //{
                        //    ActorMob mob = (ActorMob)target;
                        //    if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
                        //        damage = damage / 10;
                        //}

                        //if (sActor.type == ActorType.PC)
                        //{
                        //    int score = damage / 100;
                        //    if (score == 0 && damage != 0)
                        //        score = 1;
                        //    ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(score));
                        //}

                        //减伤处理下
                        if (target.Status.Additions.ContainsKey("无敌") && MATKBonus > 0)
                            damage = 0;


                        if (target.Status.Additions.ContainsKey("MagicShield"))//魔力加护
                        {
                            if (target.type == ActorType.PC)
                                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["MagicShieldlv"]));
                            else
                                damage = (int)(damage * (float)0.9f);
                        }
                        if (target.Status.MagicRuduceRate != 0)
                        {
                            damage = (int)(damage * (float)1f - target.Status.MagicRuduceRate);
                        }
                        if (target.Status.Additions.ContainsKey("Assumptio"))
                        {
                            damage = (int)((damage / 3.0f) * 2.0f);
                        }

                        if (target.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)target;
                            if (pc.Party != null && pc.Status.pt_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.pt_dmg_up_iris / 100.0f));
                            }
                            if (pc.Status.Element_down_iris < 100 && element != Elements.Neutral)
                            {
                                damage = (int)(damage * (float)(pc.Status.Element_down_iris / 100.0f));
                            }

                            //iris卡种族减伤部分
                            if (sActor.Race == Race.HUMAN && pc.Status.human_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.human_dmg_down_iris / 100.0f));
                            }

                            else if (sActor.Race == Race.BIRD && pc.Status.bird_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.bird_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ANIMAL && pc.Status.animal_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.animal_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.magic_c_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.PLANT && pc.Status.plant_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.plant_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.water_a_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.MACHINE && pc.Status.machine_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.machine_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ROCK && pc.Status.rock_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.rock_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.ELEMENT && pc.Status.element_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.element_dmg_down_iris / 100.0f));
                            }
                            else if (sActor.Race == Race.UNDEAD && pc.Status.undead_dmg_down_iris < 100)
                            {
                                damage = (int)(damage * (float)(pc.Status.undead_dmg_down_iris / 100.0f));
                            }
                        }
                        //减伤处理上
                        //开始处理最终伤害放大

                        if (!setAtk)
                        {
                            //杀戮放大
                            if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                                damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

                            //魔法攻擊不再有火心加成
                            //By KK 2018-04-09
                            //火心放大
                            /*
                            if (sActor.Status.Additions.ContainsKey("FrameHart"))
                            {
                                int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
                                damage = (int)((double)damage * (double)((double)rate / 100));
                            }
                            */
                            //血印
                            if (target.Status.Additions.ContainsKey("BradStigma") && element == Elements.Dark)
                            {
                                int rate = (target.Status.Additions["BradStigma"] as DefaultBuff).Variable["BradStigma"];
                                //MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("你的血印技能，使你的暗屬攻擊加成(" + rate + "%)。");
                                damage += (int)((double)damage * (double)((double)rate / 100.0f));
                            }
                            //友情的一击
                            if (sActor.Status.Additions.ContainsKey("BlowOfFriendship"))
                            {
                                damage = (int)(damage * 1.15f);
                            }

                            //竜眼放大
                            if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
                            {
                                int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
                                damage = (int)((double)damage * (double)((double)rate / 100));
                            }
                            //极大放大
                            if (sActor.Status.Additions.ContainsKey("Zensss") && !sActor.ZenOutLst.Contains(arg.skill.ID))
                            {
                                float zenbonus = (float)((float)(sActor.Status.Additions["Zensss"] as DefaultBuff).Variable["Zensss"] / 10.0f);
                                //MATKBonus *= zenbonus;
                                damage = (int)((float)damage * zenbonus);
                            }
                            if (sActor.type == ActorType.PC)
                            {
                                ActorPC pc = (ActorPC)sActor;
                                if (pc.Party != null && pc.Status.pt_dmg_up_iris > 100)
                                {
                                    damage = (int)((float)damage * (float)(pc.Status.pt_dmg_up_iris / 100.0f));
                                }

                                //iris卡种族增伤部分
                                if (target.Race == Race.BIRD && pc.Status.human_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.human_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.HUMAN && pc.Status.bird_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.bird_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.ANIMAL && pc.Status.animal_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.animal_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.MAGIC_CREATURE && pc.Status.magic_c_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.magic_c_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.PLANT && pc.Status.plant_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.plant_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.WATER_ANIMAL && pc.Status.water_a_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.water_a_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.MACHINE && pc.Status.machine_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.machine_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.ROCK && pc.Status.rock_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.rock_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.ELEMENT && pc.Status.element_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.element_dmg_up_iris / 100.0f));
                                }
                                else if (target.Race == Race.UNDEAD && pc.Status.undead_dmg_up_iris > 100)
                                {
                                    damage = (int)(damage * (float)(pc.Status.undead_dmg_up_iris / 100.0f));
                                }
                            }
                            if (sActor.WeaponElement == Elements.Holy)
                            {
                                if (target.Status.Additions.ContainsKey("Oratio"))
                                {
                                    damage = (int)(damage / 0.8f);
                                }
                            }
                            //if (sActor.Status.Additions.ContainsKey("ホークアイ"))//HAW站桩
                            //{
                            //    damage *= (int)((sActor.Status.Additions["ホークアイ"] as DefaultBuff).Variable["ホークアイ"] / 100.0f);
                            //}
                        }

                        //最终伤害放大处理结束
                        //金刚不坏处理
                        if (i.Status.Additions.ContainsKey("MentalStrength") && MATKBonus > 0)
                        {
                            int rate = (i.Status.Additions["MentalStrength"] as DefaultBuff).Variable["MentalStrength"];
                            damage = (int)((double)damage * (double)(1.0f - (double)rate / 100.0f));
                        }

                        if (i.Status.NeutralDamegeDown_rate > 0 && element == Elements.Neutral)
                        {
                            damage = (int)(damage * (1.0f - (i.Status.NeutralDamegeDown_rate / 100.0f)));

                        }
                        if (i.Status.NeutralDamegeDown_rate > 0 && element != Elements.Neutral)
                        {
                            damage = (int)(damage * (1.0f - (i.Status.ElementDamegeDown_rate / 100.0f)));

                        }

                        if (i.Status.Additions.ContainsKey("BarrierShield") && MATKBonus > 0)
                        {
                            damage = 0;
                        }
                        //处理bonus的技能伤害控制
                        uint skid = 0;
                        if (arg != null)
                        {
                            if (arg.skill != null)
                                skid = arg.skill.ID;
                        }
                        if (skid != 0)
                        {
                            if (sActor.Status.SkillRate.ContainsKey(skid))
                                damage = (int)(damage * (float)(1.0f + (float)((float)sActor.Status.SkillRate[skid] / 100.0f)));
                            if (sActor.Status.SkillDamage.ContainsKey(skid))
                                damage += (int)sActor.Status.SkillDamage[skid];
                        }

                        //吸血效果下
                        if (SuckBlood != 0 && damage != 0 && !sActor.Buff.NoRegen)//吸血效果
                        {
                            if (sActor.type == ActorType.PC)
                            {
                                int hp = (int)(damage * SuckBlood);
                                sActor.HP += (uint)hp;
                                if (sActor.HP > sActor.MaxHP)
                                    sActor.HP = sActor.MaxHP;
                                SkillHandler.Instance.ShowVessel(sActor, -hp);

                                try
                                {
                                    string y1 = "攻击";
                                    if (arg != null)
                                    {
                                        if (arg.skill != null)
                                            y1 = arg.skill.Name;
                                    }
                                    SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y1 + "", "受到了 " + (-damage).ToString() + " 点" + "治疗效果");
                                }
                                catch (Exception ex) { SagaLib.Logger.ShowError(ex); }
                            }
                        }
                        //吸血效果上

                        try
                        {
                            string s = "魔法伤害";
                            string y = "攻击";
                            if (arg != null)
                            {
                                if (arg.skill != null)
                                    y = arg.skill.Name;
                            }
                            if (damage < 0)
                            {
                                if (target.Buff.NoRegen)
                                    damage = 0;
                                s = "治疗效果";
                                SendAttackMessage(1, target, "从 " + sActor.Name + " 处的 " + y + "", "接受了 " + (-damage).ToString() + " 点" + s);
                            }
                            else
                                SendAttackMessage(2, target, "从 " + sActor.Name + " 处的 " + y + "", "受到了 " + damage.ToString() + " 点" + s);
                            SendAttackMessage(3, sActor, "你的 " + y + " 对 " + target.Name + "", "造成了 " + (damage >= 0 ? damage.ToString() : (-damage).ToString()) + " 点" + s);
                        }
                        catch (Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                        }
                        arg.hp[index + counter] = damage;

                        if (target.HP != 0)
                        {
                            if (damage > 0)
                            {
                                if (target.Status.PlantShield > 0)
                                {
                                    var dmgleft = target.Status.PlantShield - damage;
                                    if (dmgleft <= 0)
                                    {
                                        target.Status.PlantShield = 0;
                                        target.Status.Additions["PlantShield"].AdditionEnd();

                                        if (target.HP > Math.Abs(dmgleft))
                                            target.HP = (uint)(target.HP + dmgleft);
                                        else
                                        {
                                            target.HP = 0;
                                        }
                                    }
                                    else
                                    {
                                        target.Status.PlantShield -= (uint)damage;
                                    }

                                }
                                else
                                {
                                    if (damage > target.HP)
                                        target.HP = 0;
                                    else
                                        target.HP = (uint)(target.HP - damage);
                                }
                            }
                            else
                            {
                                target.HP = (uint)(target.HP - damage);
                            }
                        }

                        if (damage > 0)
                        {
                            if (target.HP > 0)
                            {
                                arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
                            }
                            else
                            {

                                if (!ride && !target.Buff.Reborn)
                                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                                else
                                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
                            }
                        }
                        else
                        {
                            arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                        }
                        if (target.HP > target.MaxHP)
                            target.HP = target.MaxHP;

                    }
                }
                ApplyDamage(sActor, target, damage, arg);
                if (sActor.Status.Additions.ContainsKey("Relement") && element != Elements.Neutral && element != Elements.Holy && element != Elements.Dark)//ASJOB20技能
                {
                    int Skilllevel = (sActor.Status.Additions["Relement"] as DefaultBuff).skill.Level;
                    thispc = (ActorPC)sActor;
                    SkillHandler.RemoveAddition(target, "HolyShield");
                    SkillHandler.RemoveAddition(target, "DarkShield");
                    SkillHandler.RemoveAddition(target, "FireShield");
                    SkillHandler.RemoveAddition(target, "WaterShield");
                    SkillHandler.RemoveAddition(target, "WindShield");
                    SkillHandler.RemoveAddition(target, "EarthShield");
                    target.Buff.BodyDarkElementUp = false;
                    target.Buff.BodyEarthElementUp = false;
                    target.Buff.BodyFireElementUp = false;
                    target.Buff.BodyWaterElementUp = false;
                    target.Buff.BodyWindElementUp = false;
                    target.Buff.BodyHolyElementUp = false;
                    int life = 150000 + 30000 * Skilllevel;
                    if (element == Elements.Earth)//魔法属性
                    {
                        Toelements = Elements.Wind;
                    }
                    else if (element == Elements.Wind)
                    {
                        Toelements = Elements.Fire;
                    }
                    else if (element == Elements.Fire)
                    {
                        Toelements = Elements.Water;
                    }
                    else if (element == Elements.Water)
                    {
                        Toelements = Elements.Earth;
                    }
                    DefaultBuff skill = new DefaultBuff(arg.skill, target, Toelements.ToString() + "Shield", life);
                    skill.OnAdditionStart += this.StartEventHandlerSele;
                    skill.OnAdditionEnd += this.EndEventHandlerSele;
                    SkillHandler.ApplyAddition(target, skill);

                }

                if (i.Status.Additions.ContainsKey("TranceBody") && element != Elements.Neutral && element != Elements.Holy && element != Elements.Dark)//ASJOB13技能
                {
                    int Skilllevel = (i.Status.Additions["TranceBody"] as DefaultBuff).skill.Level;
                    //(sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff)
                    thispc = (ActorPC)i;
                    SkillHandler.RemoveAddition(i, "HolyShield");
                    SkillHandler.RemoveAddition(i, "DarkShield");
                    SkillHandler.RemoveAddition(i, "FireShield");
                    SkillHandler.RemoveAddition(i, "WaterShield");
                    SkillHandler.RemoveAddition(i, "WindShield");
                    SkillHandler.RemoveAddition(i, "EarthShield");
                    i.Buff.BodyDarkElementUp = false;
                    i.Buff.BodyEarthElementUp = false;
                    i.Buff.BodyFireElementUp = false;
                    i.Buff.BodyWaterElementUp = false;
                    i.Buff.BodyWindElementUp = false;
                    i.Buff.BodyHolyElementUp = false;
                    int life = 150000 + 30000 * Skilllevel;
                    if (element == Elements.Earth)//魔法属性
                    {
                        Toelements = Elements.Earth;
                    }
                    else if (element == Elements.Wind)
                    {
                        Toelements = Elements.Wind;
                    }
                    else if (element == Elements.Fire)
                    {
                        Toelements = Elements.Fire;
                    }
                    else if (element == Elements.Water)
                    {
                        Toelements = Elements.Water;
                    }
                    DefaultBuff skill = new DefaultBuff(arg.skill, i, Toelements.ToString() + "Shield", life);
                    skill.OnAdditionStart += this.StartEventHandlerMele;
                    skill.OnAdditionEnd += this.EndEventHandlerMele;
                    SkillHandler.ApplyAddition(i, skill);

                }
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
                counter++;
                //return ;
            }
            SkillHandler.RemoveAddition(sActor, "Relement");
            return damage;
            //magicalCounter--;
        }

        void StartEventHandlerSele(Actor actor, DefaultBuff skill)
        {
            int atk1 = (thispc.Status.Additions["Relement"] as DefaultBuff).skill.Level * 5;
            uint SkillID = 0;
            if (Toelements == Elements.Earth)
            {
                SkillID = 3110;
            }
            else if (Toelements == Elements.Wind)
            {
                SkillID = 3108;
            }
            else if (Toelements == Elements.Fire)
            {
                SkillID = 3107;
            }
            else if (Toelements == Elements.Water)
            {
                SkillID = 3109;
            }

            if (thispc.Skills2_2.ContainsKey(SkillID) || thispc.DualJobSkill.Exists(x => x.ID == SkillID))
            {


                //这里取副职等级
                var duallv = 0;
                if (thispc.DualJobSkill.Exists(x => x.ID == SkillID))
                    duallv = thispc.DualJobSkill.FirstOrDefault(x => x.ID == SkillID).Level;

                //这里取主职等级
                var mainlv = 0;
                if (thispc.Skills2_2.ContainsKey(SkillID))
                    mainlv = thispc.Skills2_2[SkillID].Level;

                //这里取等级最高等级用来做倍率加成
                atk1 += 10 * Math.Max(duallv, mainlv);
            }
            if (skill.Variable.ContainsKey("ElementShield"))
                skill.Variable.Remove("ElementShield");
            skill.Variable.Add("ElementShield", atk1);
            actor.Status.elements_skill[Toelements] += atk1;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + Toelements.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, true, null);

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandlerSele(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["ElementShield"];
            actor.Status.elements_skill[Toelements] -= (short)value;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + Toelements.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }


        void StartEventHandlerMele(Actor actor, DefaultBuff skill)
        {
            int atk1 = 15 + (thispc.Status.Additions["TranceBody"] as DefaultBuff).skill.Level * 5;
            uint SkillID = 0;
            if (Toelements == Elements.Earth)
            {
                SkillID = 3042;
            }
            else if (Toelements == Elements.Wind)
            {
                SkillID = 3018;
            }
            else if (Toelements == Elements.Fire)
            {
                SkillID = 3007;
            }
            else if (Toelements == Elements.Water)
            {
                SkillID = 3030;
            }

            if (thispc.Skills.ContainsKey(SkillID) || thispc.DualJobSkill.Exists(x => x.ID == SkillID))
            {


                //这里取副职等级
                var duallv = 0;
                if (thispc.DualJobSkill.Exists(x => x.ID == SkillID))
                    duallv = thispc.DualJobSkill.FirstOrDefault(x => x.ID == SkillID).Level;

                //这里取主职等级
                var mainlv = 0;
                if (thispc.Skills.ContainsKey(SkillID))
                    mainlv = thispc.Skills[SkillID].Level;

                //这里取等级最高等级用来做倍率加成
                atk1 += 5 * Math.Max(duallv, mainlv);
            }
            if (skill.Variable.ContainsKey("ElementShield"))
                skill.Variable.Remove("ElementShield");
            skill.Variable.Add("ElementShield", atk1);
            actor.Status.elements_skill[Toelements] += atk1;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + Toelements.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, true, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }

        void EndEventHandlerMele(Actor actor, DefaultBuff skill)
        {
            int value = skill.Variable["ElementShield"];
            actor.Status.elements_skill[Toelements] -= (short)value;

            Type type = actor.Buff.GetType();
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty("Body" + Toelements.ToString() + "ElementUp");
            propertyInfo.SetValue(actor.Buff, false, null);
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        //public int MagicAttack(Actor sActor, List<Actor> dActor, SkillArg arg, DefType defType, Elements element, int elementValue, float MATKBonus, int mcridamagebonus, int index, bool setAtk, bool noReflect, float SuckBlood, bool WeaponAttack = false, float ignore = 0)
        //{
        //    if (dActor.Count == 0) return 0;
        //    if (sActor.Status == null)
        //        return 0;
        //    if (dActor.Count > 10)
        //    {
        //        foreach (var item in dActor)
        //            DoDamage(false, sActor, item, arg, defType, element, index, MATKBonus);
        //        return 0;
        //    }

        //    if (!CheckStatusCanBeAttact(sActor, 0))
        //        return 0;

        //    int damage = 0;

        //    int matk;
        //    int mindamage = 0;
        //    int maxdamage = 0;

        //    int counter = 0;
        //    Map map = Manager.MapManager.Instance.GetMap(dActor[0].MapID);
        //    if (index == 0)
        //    {
        //        arg.affectedActors = new List<Actor>();
        //        foreach (Actor i in dActor)
        //            arg.affectedActors.Add(i);
        //        arg.Init();
        //    }
        //    mindamage = sActor.Status.min_matk;
        //    maxdamage = sActor.Status.max_matk;

        //    if (mindamage > maxdamage) maxdamage = mindamage;

        //    foreach (Actor i in dActor)
        //    {
        //        bool isPossession = false;
        //        bool isHost = false;
        //        Actor target = i;
        //        if (i.Status == null)
        //            continue;

        //        //NOTOUCH類MOB 跳過判定
        //        if (i.type == ActorType.MOB)
        //        {
        //            ActorMob checkmob = (ActorMob)i;
        //            switch (checkmob.BaseData.mobType)
        //            {
        //                case SagaDB.Mob.MobType.ANIMAL_NOTOUCH:
        //                case SagaDB.Mob.MobType.BIRD_NOTOUCH:
        //                case SagaDB.Mob.MobType.ELEMENT_BOSS_NOTOUCH:
        //                case SagaDB.Mob.MobType.HUMAN_NOTOUCH:
        //                case SagaDB.Mob.MobType.ELEMENT_NOTOUCH:
        //                case SagaDB.Mob.MobType.PLANT_NOTOUCH:
        //                case SagaDB.Mob.MobType.MACHINE_NOTOUCH:
        //                case SagaDB.Mob.MobType.NONE_NOTOUCH:
        //                case SagaDB.Mob.MobType.UNDEAD_NOTOUCH:
        //                case SagaDB.Mob.MobType.WATER_ANIMAL_NOTOUCH:
        //                case SagaDB.Mob.MobType.PLANT_BOSS_NOTOUCH:
        //                    continue;

        //            }

        //        }

        //        if (i.type == ActorType.PC)
        //        {
        //            ActorPC pc = (ActorPC)i;
        //            if (pc.PossesionedActors.Count > 0 && pc.PossessionTarget == 0)
        //            {
        //                isPossession = true;
        //                isHost = true;
        //            }
        //            if (pc.PossessionTarget != 0)
        //            {
        //                isPossession = true;
        //                isHost = false;
        //            }
        //        }

        //        //处理凭依伤害
        //        if (isHost && isPossession && MATKBonus > 0)
        //        {
        //            List<Actor> possessionDamage = ProcessAttackPossession(i);
        //            if (possessionDamage.Count > 0)
        //            {
        //                arg.Remove(i);
        //                int oldcount = arg.flag.Count;
        //                arg.Extend(possessionDamage.Count);
        //                foreach (Actor j in possessionDamage)
        //                {
        //                    if (Global.Random.Next(0, 99) < i.Status.possessionTakeOver)
        //                        arg.affectedActors.Add(i);
        //                    else
        //                        arg.affectedActors.Add(j);
        //                }
        //                MagicAttack(sActor, possessionDamage, arg, element, elementValue, MATKBonus, oldcount);
        //                continue;
        //            }
        //        }
        //        if (i.Status.Additions.ContainsKey("MagicReflect") && i != sActor && !noReflect)
        //        {
        //            arg.Remove(i);
        //            int oldcount = arg.flag.Count;
        //            arg.Extend(1);
        //            arg.affectedActors.Add(sActor);
        //            List<Actor> dst = new List<Actor>();
        //            dst.Add(sActor);
        //            RemoveAddition(i, "MagicReflect");
        //            MagicAttack(sActor, dst, arg, DefType.MDef, element, elementValue, MATKBonus, oldcount, false, true);
        //            continue;
        //        }

        //        if (!setAtk)
        //        {
        //            matk = Global.Random.Next(mindamage, maxdamage);
        //            if (element != Elements.Neutral)
        //            {
        //                float eleBonus = CalcElementBonus(sActor, i, element, 1, ((MATKBonus < 0) && !((i.Status.undead == true) && (element == Elements.Holy))));
        //                matk = (int)(matk * eleBonus * MATKBonus);
        //            }
        //            else
        //                matk = (int)(matk * 1f * MATKBonus);
        //            if (sActor.Status.zenList.Contains((ushort)arg.skill.ID))
        //            {
        //                matk *= 2;
        //            }
        //            if (sActor.Status.darkZenList.Contains((ushort)arg.skill.ID))
        //            {
        //                matk *= 2;
        //            }
        //        }
        //        else
        //            matk = (int)MATKBonus;
        //        if (MATKBonus > 0)
        //        {
        //            damage = CalcPhyDamage(sActor, i, defType, matk, ignore);
        //        }
        //        else
        //        {
        //            damage = matk;
        //        }


        //        //魔法会心判定
        //        AttackResult res = AttackResult.Hit;

        //        damage = checkbuff(sActor, target, arg, 1, damage);
        //        damage = checkirisbuff(sActor, target, arg, 1, damage);
        //        if (i.Buff.Frosen == true && element == Elements.Fire)
        //        {
        //            RemoveAddition(i, i.Status.Additions["Frosen"]);
        //        }
        //        if (i.Buff.Stone == true && element == Elements.Water)
        //        {
        //            RemoveAddition(i, i.Status.Additions["Stone"]);
        //        }

        //        if (i.Buff.Frosen == true && element == Elements.Fire)
        //        {
        //            RemoveAddition(i, i.Status.Additions["WaterFrosenElement"]);
        //        }
        //        if (i.Buff.Stone == true && element == Elements.Water)
        //        {
        //            RemoveAddition(i, i.Status.Additions["StoneFrosenElement"]);
        //        }

        //        if (sActor.type == ActorType.PC && target.type == ActorType.PC)
        //        {
        //            if (damage > 0)
        //                damage = (int)(damage * Configuration.Instance.PVPDamageRateMagic);
        //        }

        //        if (target.Status.Additions.ContainsKey("DamageUp"))//伤害标记
        //        {
        //            float DamageUpRank = target.Status.Damage_Up_Lv * 0.1f + 1.1f;
        //            damage = (int)(damage * DamageUpRank);
        //        }
        //        if (target.Status.Additions.ContainsKey("DamageNullify"))//boss状态
        //            damage = (int)(damage * (float)0f);

        //        if (target.Status.MagicRuduceRate > 0)//魔法抵抗力
        //        {
        //            if (target.Status.MagicRuduceRate > 1)
        //                damage = (int)((float)damage / target.Status.MagicRuduceRate);
        //            else
        //                damage = (int)((float)damage / (1.0f + target.Status.MagicRuduceRate));
        //        }

        //        if (damage <= 0 && MATKBonus >= 0)
        //            damage = 1;

        //        if (isPossession && isHost && target.Status.Additions.ContainsKey("DJoint"))
        //        {
        //            Additions.Global.DefaultBuff buf = (Additions.Global.DefaultBuff)target.Status.Additions["DJoint"];
        //            if (Global.Random.Next(0, 99) < buf["Rate"])
        //            {
        //                Actor dst = map.GetActor((uint)buf["Target"]);
        //                if (dst != null)
        //                {
        //                    target = dst;
        //                    arg.affectedActors[index + counter] = target;
        //                }
        //            }
        //        }
        //        if (sActor.type == ActorType.PET)
        //            ProcessPetGrowth(sActor, PetGrowthReason.SkillHit);
        //        if (i.type == ActorType.PET && damage > 0)
        //            ProcessPetGrowth(i, PetGrowthReason.MagicalBeenHit);

        //        bool ride = false;
        //        if (target.type == ActorType.PC)
        //        {
        //            ActorPC pc = (ActorPC)target;
        //            if (pc.Pet != null)
        //                ride = pc.Pet.Ride;
        //        }

        //        if (sActor.type == ActorType.PC && target.type == ActorType.MOB)
        //        {
        //            ActorMob mob = (ActorMob)target;
        //            if (mob.BaseData.mobType.ToString().Contains("CHAMP") && !sActor.Buff.StateOfMonsterKillerChamp)
        //                damage = damage / 10;
        //        }


        //        if (sActor.type == ActorType.PC)
        //        {
        //            int score = damage / 100;
        //            if (score == 0 && damage != 0)
        //                score = 1;
        //            ODWarManager.Instance.UpdateScore(sActor.MapID, sActor.ActorID, Math.Abs(score));
        //        }

        //        //加伤处理下
        //        if (target.Seals > 0)
        //            damage = (int)(damage * (float)(1f + 0.05f * target.Seals));//圣印
        //                                                                        //加伤处理上
        //                                                                        //减伤处理下
        //        if (target.Status.Additions.ContainsKey("无敌"))
        //            damage = 0;
        //        if (target.Status.Additions.ContainsKey("MagicShield"))//魔力加护
        //        {
        //            if (target.type == ActorType.PC)
        //                damage = (int)(damage * (float)(1f - 0.02f * ((ActorPC)target).TInt["MagicShieldlv"]));
        //            else
        //                damage = (int)(damage * (float)0.9f);
        //        }
        //        if (target.Status.MagicRuduceRate != 0)
        //        {
        //            damage = (int)(damage * (float)1f - target.Status.MagicRuduceRate);
        //        }
        //        //减伤处理上
        //        //开始处理最终伤害放大

        //        if (!setAtk)
        //        {
        //            //杀戮放大
        //            if (sActor.Status.Additions.ContainsKey("Efuikasu"))
        //                damage = (int)((float)damage * (1.0f + (float)sActor.KillingMarkCounter * 0.05f));

        //            //火心放大
        //            if (sActor.Status.Additions.ContainsKey("FrameHart"))
        //            {
        //                int rate = (sActor.Status.Additions["FrameHart"] as DefaultBuff).Variable["FrameHart"];
        //                damage = (int)((double)damage * (double)((double)rate / 100));
        //            }
        //            //竜眼放大
        //            if (sActor.Status.Additions.ContainsKey("DragonEyeOpen"))
        //            {
        //                int rate = (sActor.Status.Additions["DragonEyeOpen"] as DefaultBuff).Variable["DragonEyeOpen"];
        //                damage = (int)((double)damage * (double)((double)rate / 100));
        //            }
        //            //极大放大
        //            if (sActor.Status.Additions.ContainsKey("Zensss") && !sActor.ZenOutLst.Contains(arg.skill.ID))
        //            {
        //                float zenbonus = (float)((sActor.Status.Additions["Zensss"] as DefaultBuff).Variable["Zensss"] / 10);
        //                //MATKBonus *= zenbonus;
        //                damage = (int)((float)damage * zenbonus);
        //            }
        //        }

        //        //最终伤害放大处理结束

        //        //吸血效果下
        //        if (SuckBlood != 0 && damage != 0)//吸血效果
        //        {
        //            if (sActor.type == ActorType.PC)
        //            {
        //                int hp = (int)(damage * SuckBlood);
        //                sActor.HP += (uint)hp;
        //                if (sActor.HP > sActor.MaxHP)
        //                    sActor.HP = sActor.MaxHP;
        //                Instance.ShowVessel(sActor, -hp);
        //            }
        //        }
        //        //吸血效果上
        //        if (target.Status.Additions.ContainsKey("Sacrifice") && damage < 0)
        //            damage = 0;

        //        arg.hp[index + counter] += damage;
        //        if (damage > 0)
        //        {
        //            if (target.HP > damage)
        //            {
        //                arg.flag[index + counter] = AttackFlag.HP_DAMAGE;
        //            }
        //            else
        //            {
        //                damage = (int)target.HP;
        //                if (!ride && !target.Buff.Reborn)
        //                    arg.flag[index + counter] = AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //                else
        //                    arg.flag[index + counter] = AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT;
        //            }
        //        }
        //        else
        //        {
        //            arg.flag[index + counter] = AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
        //        }

        //        if (target.HP != 0)
        //            target.HP = (uint)(target.HP - damage);
        //        if (target.HP > target.MaxHP)
        //            target.HP = target.MaxHP;

        //        counter++;
        //        ApplyDamage(sActor, target, damage, arg);
        //        MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, target, true);
        //    }
        //    return damage;
        //    //magicalCounter--;
        //}

        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        public void ApplyDamage(Actor sActor, Actor dActor, int damage, SkillArg arg2 = null)
        {
            ApplyDamage(sActor, dActor, damage, false, arg2);
        }


        //public List<int> range = new List<int>();
        //public void GravityGL()
        //{
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(1, 0, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(2, 0, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(-1, 0, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(-2, 0, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(1, 1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(0, 1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(-1, 1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(0, 2, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(1, -1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(0, -1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(-1, -1, 2));
        //    range.Add(SkillHandler.Instance.CalcPosHashCode(0, -2, 2));
        //}

        /// <summary>
        /// 对指定目标附加伤害
        /// </summary>
        /// <param name="sActor">原目标</param>
        /// <param name="dActor">对象目标</param>
        /// <param name="damage">伤害值</param>
        public void ApplyDamage(Actor sActor, Actor dActor, int damage, bool doublehate, SkillArg arg2 = null)
        {
            if ((DateTime.Now - dActor.Status.attackStamp).TotalSeconds > 5)
            {
                dActor.Status.attackStamp = DateTime.Now;
                dActor.Status.attackingActors.Clear();
                if (!dActor.Status.attackingActors.Contains(sActor))
                    dActor.Status.attackingActors.Add(sActor);
            }
            else
            {
                if (!dActor.Status.attackingActors.Contains(sActor))
                    dActor.Status.attackingActors.Add(sActor);
            }

            if (sActor.type == ActorType.PC)
            {
                WeaponWorn((ActorPC)sActor);
            }
            if (dActor.type == ActorType.PC && damage > (dActor.MaxHP / 100))
            {
                ArmorWorn((ActorPC)dActor);
            }

            if (arg2 != null && arg2.skill != null && arg2.skill.ID == 2541 && !arg2.flag.Contains(AttackFlag.MISS) && !arg2.flag.Contains(AttackFlag.AVOID) && !arg2.flag.Contains(AttackFlag.GUARD) && !arg2.flag.Contains(AttackFlag.AVOID2))
            {
                //this.CreateAutoCastInfo()
                arg2.autoCast.Add(SkillHandler.Instance.CreateAutoCastInfo(2542, arg2.skill.Level, 1000));
            }

            //3转剑35级技能(已完成副职逻辑)
            if (dActor.Status.Pressure_lv > 0)
            {
                int level = dActor.Status.Pressure_lv;
                float[] hprank = { 0.2f, 0.2f, 0.25f, 0.25f, 0.3f };
                float[] rank = { 0, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
                float[] rank2 = { 0, 0.1f, 0.1f, 0.15f, 0.15f, 0.2f };
                float factor = 3f + 0.3f * level;
                ActorPC pc = (ActorPC)dActor;
                //不管是主职还是副职,确定技能存在
                if (pc.Skills3.ContainsKey(1113) || pc.DualJobSkill.Exists(x => x.ID == 1113))
                {
                    for (int i = 1; i < level; i++)
                    {
                        if (pc.HP < (uint)((float)pc.MaxHP * hprank[i - 1]) && pc.HP > damage)
                        {
                            if (SagaLib.Global.Random.Next(0, 100) <= (level * 10))
                            {
                                Map map = Manager.MapManager.Instance.GetMap(dActor.MapID);
                                List<Actor> affected = map.GetActorsArea(dActor, 400, false);
                                List<Actor> realAffected = new List<Actor>();
                                arg2 = new SkillArg();
                                arg2.Init();
                                //获取副职AutoHeal的等级
                                var duallv = 0;
                                if (pc.DualJobSkill.Exists(x => x.ID == 1113))
                                    duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1113).Level;

                                //这里取主职的AutoHeal等级
                                var mainlv = 0;
                                if (pc.Skills3.ContainsKey(1113))
                                    mainlv = pc.Skills3[1113].Level;

                                //这里取等级最高的AutoHeal等级
                                int level2 = Math.Max(duallv, mainlv);

                                arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(1113, (byte)level2);
                                var duallv2 = 0;
                                //不屈意志触发模块
                                if (pc.DualJobSkill.Exists(x => x.ID == 1100))
                                    duallv2 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1100).Level;

                                //这里取主职的不屈斗志等级
                                var mainlv2 = 0;
                                if (pc.Skills3.ContainsKey(1100))
                                    mainlv2 = pc.Skills3[1100].Level;

                                //这里取等级最高的不屈斗志等级
                                int level1100 = Math.Max(duallv2, mainlv2);
                                if (level1100 != 0 && pc.Buff.NoRegen == false)
                                {
                                    float[] recoveryrate = new float[] { 0.24f, 0.22f, 0.20f, 0.18f, 0.16f };
                                    arg2 = new SkillArg();
                                    arg2.Init();
                                    arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(2066, 5);
                                    int hpheal = (int)(pc.MaxHP * recoveryrate[level2 - 1]);
                                    int mpheal = (int)(pc.MaxMP * recoveryrate[level2 - 1]);
                                    int spheal = (int)(pc.MaxSP * recoveryrate[level2 - 1]);
                                    arg2.hp.Add(hpheal);
                                    arg2.mp.Add(mpheal);
                                    arg2.sp.Add(spheal);
                                    arg2.flag.Add(AttackFlag.HP_HEAL | AttackFlag.SP_HEAL | AttackFlag.MP_HEAL | AttackFlag.NO_DAMAGE);
                                    pc.HP += (uint)hpheal;
                                    pc.MP += (uint)mpheal;
                                    pc.SP += (uint)spheal;
                                    if (pc.HP > pc.MaxHP)
                                        pc.HP = pc.MaxHP;
                                    if (pc.MP > pc.MaxMP)
                                        pc.MP = pc.MaxMP;
                                    if (pc.SP > pc.MaxSP)
                                        pc.SP = pc.MaxSP;
                                    ShowEffect((ActorPC)pc, pc, 4321);
                                    SkillHandler.Instance.ShowVessel(pc, -hpheal, -mpheal, -spheal);
                                    Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg2, pc, true);
                                }
                                foreach (Actor act in affected)
                                {
                                    arg2.argType = SkillArg.ArgType.Attack;
                                    arg2.flag.Add(AttackFlag.HP_DAMAGE);
                                    arg2.type = ATTACK_TYPE.BLOW;
                                    arg2.hp.Add(damage);
                                    arg2.sp.Add(0);
                                    arg2.mp.Add(0);
                                    arg2.affectedActors.Add(act);
                                    SkillHandler.Instance.PhysicalAttack(dActor, realAffected, arg2, SagaLib.Elements.Neutral, factor);
                                    ShowEffect((ActorPC)dActor, act, 4002);
                                    ShowEffect((ActorPC)dActor, dActor, 4321);
                                    SkillHandler.Instance.PushBack(dActor, act, 4);
                                    if (SkillHandler.Instance.CanAdditionApply(sActor, act, DefaultAdditions.Stun, level * 10))
                                    {
                                        Stun stun = new Stun(arg2.skill, act, 4000);
                                        SkillHandler.ApplyAddition(act, stun);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //如果玩家有被动buff AutoHeal
            //自动治疗(已完成副职逻辑)
            if (dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("AutoHeal") && !dActor.Buff.NoRegen)
            {
                ActorPC pc = (ActorPC)dActor;
                //不管是主职还是副职
                //如果玩家加了被动技能AutoHeal,并且玩家有AutoHeal这个技能
                if (pc.Skills3.ContainsKey(1109) || pc.DualJobSkill.Exists(x => x.ID == 1109))
                {
                    //获取AutoHeal的等级
                    //int level = pc.Skills3[1109].Level;
                    //获取副职AutoHeal的等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 1109))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1109).Level;

                    //这里取主职的AutoHeal等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(1109))
                        mainlv = pc.Skills3[1109].Level;

                    //这里取等级最高的AutoHeal等级
                    int level = Math.Max(duallv, mainlv);
                    //声明触发治疗标记
                    bool active = false;
                    //触发治疗的血线
                    float[] activerate = new float[] { 0.2f, 0.4f, 0.6f, 0.7f, 0.8f };

                    //遍历触发血线数组
                    for (int i = 1; i <= level; i++)
                    {
                        //如果玩家当前的血量 小于触发血线
                        if (pc.HP < (uint)((float)dActor.MaxHP * activerate[i - 1]) && pc.HP > damage)
                        {
                            // 40%机率触发治疗
                            if (SagaLib.Global.Random.Next(1, 100) <= 40)
                            {
                                active = true;
                                break;
                            }
                        }
                    }
                    if (active)
                    {
                        //自动咏唱Healing
                        SkillArg autoheal = new SkillArg();
                        //获取副职Healing的等级
                        var duallv2 = 0;
                        if (pc.DualJobSkill.Exists(x => x.ID == 3054))
                            duallv2 = pc.DualJobSkill.FirstOrDefault(x => x.ID == 3054).Level;

                        //这里取主职的Healing等级
                        var mainlv2 = 0;
                        if (pc.Skills.ContainsKey(3054))
                            mainlv2 = pc.Skills[3054].Level;

                        //这里取等级最高的Healing等级
                        int level2 = Math.Max(duallv2, mainlv2);
                        if (level2 == 0)
                        {
                            level2 = 1;//習得していない場合はヒーリングLv1×0.8倍の回復量になる-wiki
                        }
                        SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3054, (byte)level2);
                        //SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3054, pc.Skills[3054].Level);
                        autoheal.sActor = pc.ActorID;
                        autoheal.dActor = pc.ActorID;
                        autoheal.skill = skill;
                        autoheal.argType = SkillArg.ArgType.Cast;
                        autoheal.useMPSP = false;
                        SagaMap.Network.Client.MapClient.FromActorPC(pc).OnSkillCastComplete(autoheal);
                    }
                }
            }



            //不屈の闘志(已完成副职逻辑)
            if (dActor.type == ActorType.PC && dActor.Status.Additions.ContainsKey("不屈の闘志"))
            {
                ActorPC pc = (ActorPC)dActor;
                //不管是主职还是副职
                //如果玩家加了被动技能 不屈的斗志
                if (pc.Skills3.ContainsKey(1100) || pc.DualJobSkill.Exists(x => x.ID == 1100))
                {
                    //获取不屈的斗志的等级
                    //int level = pc.Skills3[1100].Level;
                    //这里取副职的不屈的斗志等级
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 1100))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 1100).Level;

                    //这里取主职的不屈的斗志等级
                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(1100))
                        mainlv = pc.Skills3[1100].Level;

                    //这里取等级最高的不屈的斗志进行判定
                    int level = Math.Max(duallv, mainlv);

                    //触发治疗的血线
                    float[] activerate = new float[] { 0.08f, 0.16f, 0.24f, 0.32f, 0.4f };
                    //治疗量
                    float[] recoveryrate = new float[] { 0.24f, 0.22f, 0.20f, 0.18f, 0.16f };
                    //遍历触发血线数组
                    for (int i = 1; i < level; i++)
                    {
                        //如果玩家当前的血量 小于触发血线
                        if (pc.HP < (uint)((float)dActor.MaxHP * activerate[i - 1]) && pc.HP > damage)
                        {
                            if (dActor.Buff.NoRegen)
                                break;
                            arg2 = new SkillArg();
                            arg2.Init();
                            ShowEffect((ActorPC)pc, pc, 4321);
                            int hpheal = (int)(dActor.MaxHP * recoveryrate[i - 1]);
                            int mpheal = (int)(dActor.MaxMP * recoveryrate[i - 1]);
                            int spheal = (int)(dActor.MaxSP * recoveryrate[i - 1]);
                            arg2.hp.Add(hpheal);
                            arg2.mp.Add(mpheal);
                            arg2.sp.Add(spheal);
                            arg2.flag.Add(AttackFlag.HP_HEAL | AttackFlag.SP_HEAL | AttackFlag.MP_HEAL | AttackFlag.NO_DAMAGE);
                            dActor.HP += (uint)hpheal;
                            dActor.MP += (uint)mpheal;
                            dActor.SP += (uint)spheal;
                            if (dActor.HP > dActor.MaxHP)
                                dActor.HP = dActor.MaxHP;
                            if (dActor.MP > dActor.MaxMP)
                                dActor.MP = dActor.MaxMP;
                            if (dActor.SP > dActor.MaxSP)
                                dActor.SP = dActor.MaxSP;
                            SkillHandler.Instance.ShowVessel(pc, -hpheal, -mpheal, -spheal);
                            Manager.MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, arg2, dActor, true);
                        }
                    }
                }
            }

            if ((dActor.type == ActorType.MOB || dActor.type == ActorType.PET) && damage >= 0)
            {
                Actor attacker;

                //凭依中仇恨转移到寄主
                if (sActor.type == ActorType.PC)
                {
                    ActorPC pc = (ActorPC)sActor;
                    if (pc.PossessionTarget != 0)
                    {
                        Actor possession = Manager.MapManager.Instance.GetMap(pc.MapID).GetActor(pc.PossessionTarget);
                        if (possession != null)
                        {
                            if (possession.type == ActorType.PC)
                                attacker = possession;
                            else
                                attacker = sActor;
                        }
                        else
                            attacker = sActor;
                    }
                    else
                        attacker = sActor;
                }
                else
                    attacker = sActor;
                if (dActor.type == ActorType.MOB)
                {
                    ActorEventHandlers.MobEventHandler mob = (ActorEventHandlers.MobEventHandler)dActor.e;
                    if (sActor.Status.Additions.ContainsKey("柔和魔法"))
                        mob.AI.OnAttacked(attacker, damage / 2);
                    else
                        mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 2);
                }
                else
                {
                    ActorEventHandlers.PetEventHandler mob = (ActorEventHandlers.PetEventHandler)dActor.e;
                    if (sActor.Status.Additions.ContainsKey("柔和魔法"))
                        mob.AI.OnAttacked(attacker, damage / 2);
                    else
                        mob.AI.OnAttacked(attacker, damage);
                    if (doublehate)
                        mob.AI.OnAttacked(attacker, damage * 2);
                }
            }

            if (dActor.type == ActorType.PC)
            {
                //如果凭依中受攻击解除凭依
                //TODO: 支援スキル使用時の憑依解除設定
                ActorPC pc = (ActorPC)dActor;
                if (pc.Online)
                {
                    MapClient client = MapClient.FromActorPC(pc);
                    if (client.Character.Buff.GetReadyPossession)
                    {
                        client.Character.Buff.GetReadyPossession = false;
                        client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, client.Character, true);
                        if (client.Character.Tasks.ContainsKey("Possession"))
                        {
                            client.Character.Tasks["Possession"].Deactivate();
                            client.Character.Tasks.Remove("Possession");
                        }
                    }
                    if (dActor.Status.Additions.ContainsKey("Hiding"))
                    {
                        dActor.Status.Additions["Hiding"].AdditionEnd();
                        dActor.Status.Additions.Remove("Hiding");
                    }
                    if (dActor.Status.Additions.ContainsKey("fish"))
                    {
                        dActor.Status.Additions["fish"].AdditionEnd();
                        dActor.Status.Additions.Remove("fish");
                    }
                    if (dActor.Status.Additions.ContainsKey("Cloaking"))
                    {
                        dActor.Status.Additions["Cloaking"].AdditionEnd();
                        dActor.Status.Additions.Remove("Cloaking");
                    }
                    if (dActor.Status.Additions.ContainsKey("IAmTree"))
                    {
                        dActor.Status.Additions["IAmTree"].AdditionEnd();
                        dActor.Status.Additions.Remove("IAmTree");
                    }
                    if (dActor.Status.Additions.ContainsKey("Invisible"))
                    {
                        dActor.Status.Additions["Invisible"].AdditionEnd();
                        dActor.Status.Additions.Remove("Invisible");
                    }
                }
            }

            //魔力吸收
            if (dActor.Status.Additions.ContainsKey("Desist") && damage >= 0)
            {
                float desistfactor = ((float)(dActor.Status.Additions["Desist"] as DefaultBuff).Variable["Desist"] / 100.0f);
                int mpdesist = (int)Math.Floor((float)damage * desistfactor);
                if (dActor.MaxMP < (dActor.MP + mpdesist))
                    dActor.MP = dActor.MaxMP;
                else
                    dActor.MP += (uint)mpdesist;
                MapManager.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, dActor, true);
            }

            //如果对象目标死亡
            if (dActor.HP == 0)
            {
                if (sActor.Status.Additions.ContainsKey("Efuikasu"))
                {
                    if (sActor.KillingMarkSoulUse)
                        sActor.KillingMarkCounter = Math.Min(++sActor.KillingMarkCounter, 10);
                    else
                        sActor.KillingMarkCounter = Math.Min(++sActor.KillingMarkCounter, 20);
                }

                if (!dActor.Buff.Dead)
                {
                    if (dActor.type == ActorType.PC)
                    {
                        ActorPC pc = (ActorPC)dActor;
                        if (pc.Pet != null)
                        {
                            if (pc.Pet.Ride)
                            {
                                ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;
                                Packets.Client.CSMG_ITEM_MOVE p = new SagaMap.Packets.Client.CSMG_ITEM_MOVE();
                                p.data = new byte[11];
                                if (pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.PET))
                                {
                                    SagaDB.Item.Item item = pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.PET];
                                    if (item.Durability != 0) item.Durability--;
                                    eh.Client.SendItemInfo(item);
                                    eh.Client.SendSystemMessage(string.Format(Manager.LocalManager.Instance.Strings.PET_FRIENDLY_DOWN, item.BaseData.name));
                                    EffectArg arg = new EffectArg();
                                    arg.actorID = eh.Client.Character.ActorID;
                                    arg.effectID = 8044;
                                    eh.OnShowEffect(eh.Client.Character, arg);
                                    p.InventoryID = item.Slot;
                                    p.Target = SagaDB.Item.ContainerType.BODY;
                                    p.Count = 1;
                                    eh.Client.OnItemMove(p);
                                }
                                //return;
                            }
                        }

                        //凭依者死亡自动解除凭依
                        if (pc.PossessionTarget != 0)
                        {
                            Packets.Client.CSMG_POSSESSION_CANCEL p = new SagaMap.Packets.Client.CSMG_POSSESSION_CANCEL();
                            p.PossessionPosition = PossessionPosition.NONE;
                            MapClient.FromActorPC(pc).OnPossessionCancel(p);
                        }
                        if (((ActorPC)dActor).Mode != PlayerMode.COLISEUM_MODE && ((ActorPC)dActor).Mode != PlayerMode.KNIGHT_WAR)
                            ExperienceManager.Instance.DeathPenalty((ActorPC)dActor);
                        //处理WRP
                        if (sActor.type == ActorType.PC)
                        {
                            Map map = MapManager.Instance.GetMap(pc.MapID);
                            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Wrp))
                                ExperienceManager.Instance.ProcessWrp((ActorPC)sActor, pc);
                        }
                    }

                    if (dActor.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)dActor;
                        if (sActor.type == ActorType.PC)
                        {
                            ActorPC pc = (ActorPC)sActor;
                            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)pc.e;
                            Map map;// = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                            List<Actor> eventactors;// = map.GetActorsArea(dActor, 3000, false, true);
                            List<ActorPC> owners;// = new List<ActorPC>();
                            if (SkillHandler.Instance.isBossMob(mob) && ((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.SpawnDelay >= 1800000)
                            {
                                map = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                                eventactors = map.GetActorsArea(dActor, 12700, false, true);
                                eventactors = eventactors.Where(x => x.type == ActorType.PC && (x as ActorPC).Online).ToList();
                                foreach (var item in eventactors)
                                {
                                    MapClient.FromActorPC((ActorPC)item).SendAnnounce("本次: [" + mob.Name + "]已经完成击杀,设置系召唤系技能造成的伤害不被统计为你的个人伤害.");
                                    MapClient.FromActorPC((ActorPC)item).SendAnnounce("正在分配击杀......");
                                }
                                owners = new List<ActorPC>();
                                foreach (var item in ((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable)
                                {
                                    Actor act = eventactors.FirstOrDefault(x => x.ActorID == item.Key);
                                    if (act != null && act.type == ActorType.PC && (act as ActorPC).Online)
                                    {
                                        MapClient.FromActorPC((ActorPC)act).SendAnnounce(string.Format("本次击杀: [{0}],你贡献了: {1}点伤害", mob.Name, item.Value));
                                        owners.Add((ActorPC)act);
                                        if ((act as ActorPC).PossesionedActors.Count > 0)
                                        {
                                            foreach (var pitem in (act as ActorPC).PossesionedActors)
                                            {
                                                if (!pitem.Online)
                                                    continue;
                                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == pitem.ActorID).ToList().Count == 0 && pitem.MapID == mob.MapID)
                                                {
                                                    MapClient.FromActorPC(pitem).SendAnnounce(string.Format("本次击杀: [{0}],你未提供伤害贡献,但是你混到了凭依击杀", mob.Name));
                                                    owners.Add((ActorPC)pitem);
                                                }
                                            }
                                        }
                                        if ((act as ActorPC).PossessionTarget != 0)
                                        {
                                            if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == (act as ActorPC).PossessionTarget).ToList().Count == 0 && act.MapID == mob.MapID)
                                            {
                                                MapClient cli = MapClient.FromActorPC((ActorPC)eventactors.FirstOrDefault(x => x.ActorID == (act as ActorPC).PossessionTarget));
                                                if (cli != null)
                                                {
                                                    cli.SendAnnounce(string.Format("本次击杀: [{0}],你没有提供伤害贡献,但是你混到了凭依跑者击杀", mob.Name));
                                                    owners.Add((ActorPC)eventactors.First(x => x.ActorID == (act as ActorPC).PossessionTarget));
                                                }
                                            }
                                        }
                                        if ((act as ActorPC).Party != null)
                                        {
                                            foreach (var ptitem in (act as ActorPC).Party.Members)
                                            {
                                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.Where(x => x.Key == ptitem.Value.ActorID).ToList().Count == 0 && ptitem.Value.Online && ptitem.Value.MapID == mob.MapID)
                                                {
                                                    MapClient.FromActorPC((ActorPC)ptitem.Value).SendAnnounce(string.Format("本次击杀: [{0}],你没有提供伤害贡献,但是你混到了组队击杀", mob.Name));
                                                    owners.Add(ptitem.Value);
                                                }
                                            }
                                        }
                                    }
                                }
                                foreach (var ac in owners)
                                {

                                    if ((ac as ActorPC) == null)
                                        continue;
                                    if (!(ac as ActorPC).Online)
                                        continue;
                                    ActorEventHandlers.PCEventHandler ieh = (ActorEventHandlers.PCEventHandler)(ac as ActorPC).e;
                                    ieh.Client.EventMobKilled(mob);
                                    ieh.Client.QuestMobKilled(mob, false);
                                }
                            }
                            else
                            {
                                //处理任务信息
                                if (pc.Party != null)
                                {
                                    foreach (ActorPC tmp in pc.Party.Members.Values)
                                    {
                                        if (!tmp.Online) continue;
                                        if (tmp == pc) continue;
                                        ((ActorEventHandlers.PCEventHandler)tmp.e).Client.QuestMobKilled(mob, true);
                                    }
                                    eh.Client.QuestMobKilled(mob, false);
                                }
                                else
                                {
                                    eh.Client.QuestMobKilled(mob, false);
                                }
                            }
                            map = SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID);
                            eventactors = map.GetActorsArea(dActor, 3000, false, true);
                            owners = new List<ActorPC>();
                            foreach (Actor ac in eventactors)
                            {
                                if (((SagaMap.ActorEventHandlers.MobEventHandler)mob.e).AI.DamageTable.ContainsKey(ac.ActorID) && ac.type == ActorType.PC)
                                    owners.Add((ActorPC)ac);
                            }
                            foreach (ActorPC i in owners)
                            {
                                if (!i.Online) continue;
                                if (i == null) continue;
                                ActorEventHandlers.PCEventHandler ieh = (ActorEventHandlers.PCEventHandler)i.e;
                                ieh.Client.EventMobKilled(mob);
                            }
                        }
                        //ExperienceManager.Instance.ProcessMobExp(mob);
                    }
                    //if (sActor.type == ActorType.PC && dActor.type == ActorType.PC && sActor != dActor)
                    //{
                    //    ActorPC pc = (ActorPC)sActor;
                    //    ActorPC dpc = (ActorPC)dActor;
                    //    Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                    //    if (pc.Mode == PlayerMode.KNIGHT_EAST || pc.Mode == PlayerMode.KNIGHT_WEST)
                    //        pc.Mode = PlayerMode.NORMAL;
                    //    if (dpc.Mode == PlayerMode.KNIGHT_EAST || dpc.Mode == PlayerMode.KNIGHT_WEST)
                    //        dpc.Mode = PlayerMode.NORMAL;
                    //    map.Announce("玩家 " + dActor.Name + " 被 " + sActor.Name + " 擊殺了。");
                    //    pc.TInt["PVP连杀"]++;
                    //    if (pc.TInt["PVP连杀"] > pc.TInt["PVP最大连杀"]) pc.TInt["PVP最大连杀"] = pc.TInt["PVP连杀"];
                    //    if (pc.TInt["PVP连杀"] > 2)
                    //        map.Announce(sActor.Name + "連續擊殺了 " + pc.TInt["PVP连杀"].ToString() + " 人！");
                    //    if (dpc.TInt["PVP连杀"] > 2)
                    //        map.Announce(sActor.Name + "結束了 " + dpc.Name + " 的" + dpc.TInt["PVP连杀"].ToString() + " 連續擊殺。");

                    //    if (sActor.type == ActorType.PC)
                    //    {
                    //        ActorPC spc = (ActorPC)sActor;
                    //        if (spc.Mode == PlayerMode.NORMAL)
                    //        {
                    //            MapClient.FromActorPC(spc).TitleProccess(spc, 10, 1);
                    //        }
                    //    }

                    //}
                    //if (dActor.type == ActorType.PC && sActor != dActor)
                    //{
                    //    ActorPC pc = (ActorPC)dActor;
                    //    if (pc.Online)
                    //        pc.TInt["死亡统计"]++;
                    //    if (sActor.type == ActorType.PC)
                    //    {
                    //        ActorPC pc2 = (ActorPC)sActor;
                    //        if (pc2.Online)
                    //        {
                    //            pc2.TInt["击杀统计"]++;
                    //            MapClient.FromActorPC(pc2).SendSystemMessage("击杀数：" + pc2.TInt["击杀统计"].ToString() +
                    //                " 最高连杀数：" + pc2.TInt["PVP最大连杀"].ToString() + " 死亡数：" + pc2.TInt["死亡统计"].ToString() +
                    //                " 已造成伤害：" + pc2.TInt["伤害统计"].ToString() + " 已治疗：" + pc2.TInt["治疗统计"].ToString());
                    //        }
                    //    }
                    //}
                    dActor.e.OnDie();
                }
            }
        }

        void GetlongARROW(ActorPC pc, SkillArg arg)
        {
            if (pc.Skills.ContainsKey(2035) || pc.Skills2_2.ContainsKey(2035) || pc.DualJobSkill.Exists(x => x.ID == 2035))
            {
                if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.ARROW ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].BaseData.itemType == SagaDB.Item.ItemType.BULLET) &&
                   pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Stack > 0)
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2035))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2035).Level;

                    var mainlv = 0;
                    if (pc.Skills.ContainsKey(2035))
                        mainlv = pc.Skills[2035].Level;

                    var mainlv2 = 0;
                    if (pc.Skills2_2.ContainsKey(2035))
                        mainlv2 = pc.Skills2_2[2035].Level;

                    int maxlv = Math.Max(duallv, mainlv);
                    maxlv = Math.Max(maxlv, mainlv2);
                    if (arg.skill == null)//普通攻击
                    {
                        if (SagaLib.Global.Random.Next(0, 99) >= maxlv * 10)
                        {
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                        }


                    }
                    else
                    {
                        if (SagaLib.Global.Random.Next(0, 99) >= maxlv * 10)
                        {
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
                        }
                    }
                }
            }
            else
            {
                MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.LEFT_HAND].Slot, 1, false);
            }
        }

        void GetlongCARD(ActorPC pc, SkillArg arg)
        {
            if (pc.Skills.ContainsKey(2035) || pc.Skills2_2.ContainsKey(2035) || pc.DualJobSkill.Exists(x => x.ID == 2035))
            {
                if ((pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.THROW ||
                    pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.itemType == SagaDB.Item.ItemType.CARD) &&
                   pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Stack > 0)
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2035))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2035).Level;

                    var mainlv = 0;
                    if (pc.Skills.ContainsKey(2035))
                        mainlv = pc.Skills[2035].Level;

                    var mainlv2 = 0;
                    if (pc.Skills2_2.ContainsKey(2035))
                        mainlv2 = pc.Skills2_2[2035].Level;

                    int maxlv = Math.Max(duallv, mainlv);
                    maxlv = Math.Max(maxlv, mainlv2);
                    if (arg.skill == null)//普通攻击
                    {
                        if (SagaLib.Global.Random.Next(0, 99) >= maxlv * 10)
                        {
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                        }


                    }
                    else
                    {
                        if (SagaLib.Global.Random.Next(0, 99) >= maxlv * 10)
                        {
                            MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
                        }
                    }
                }
            }
            else
            {
                MapClient.FromActorPC(pc).DeleteItem(pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].Slot, 1, false);
            }
        }
    }
}
