using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;
using SagaMap.Manager;
using SagaMap.Network.Client;

namespace SagaMap.Skill.SkillDefinations
{
    class S32100 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            dActor = SkillHandler.Instance.GetdActor(sActor, args);
            if (dActor == null) return;

            if (dActor.type != ActorType.MOB)
                return;

            if (sActor.type != ActorType.PC)
                return;
            ActorPC pc = (ActorPC)sActor;
            MapClient client = MapClient.FromActorPC(pc);
            Map map = MapManager.Instance.GetMap(sActor.MapID);
            int damage = 1000;
            SkillHandler.Instance.CauseDamage(sActor, dActor, damage);
            SkillHandler.Instance.ShowVessel(dActor, damage);
            if (dActor.Name == "红豆树")//红豆活动
            {
                uint itemid = 111120011;

                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                ushort count = (ushort)SagaLib.Global.Random.Next(1, 3);
                if(SagaLib.Global.Random.Next(1,100) < 15)
                    count = (ushort)SagaLib.Global.Random.Next(1, 5);
                if (pc.AStr["当日红豆清零"] != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    pc.AInt["当日采红豆次数"] = 0;
                    pc.AStr["当日红豆清零"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (pc.AInt["当日采红豆次数"] < 50)
                {
                    client.SendSystemMessage("当日前50次采摘『红豆』，获取数量提升！");
                    count = 5;
                }
                if (pc.AInt["当日采红豆次数"] >= 50 && pc.AInt["当日采红豆次数"] < 100)
                {
                    client.SendSystemMessage("当日前50~100次采摘『红豆』，获取数量提升！");
                    count = (ushort)SagaLib.Global.Random.Next(3, 5);
                }
                item.Stack = count;
                pc.AInt["当日采红豆次数"]++;
                pc.CInt["采摘exp"]++;
                client.SendNPCPlaySound(2602, 0, 100, 50);
                client.AddItem(item, true);
                client.SendSystemMessage("获得了 1 点采摘经验值。已累积：" + pc.CInt["采摘exp"] + " 点经验");

                if(SagaLib.Global.Random.Next(0,40) < 1)
                {
                    SagaDB.Item.Item item2 = SagaDB.Item.ItemFactory.Instance.GetItem(910000134);
                    client.SendNPCPlaySound(2814, 0, 100, 50);
                    item2.Stack = 1;
                    client.AddItem(item2, true);
                }
            }


            if (dActor.Name == "野果")//组队草莓
            {
                uint itemid = 210099800;
                

                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                item.Stack = 1;

                if (SagaLib.Global.Random.Next(0, 100) < 30)
                    client.AddItem(item, true);
                client.SendNPCPlaySound(2602, 0, 100, 50);
                pc.CInt["采摘exp"]++;
                client.SendSystemMessage("获得了 1 点采摘经验值。已累积：" + pc.CInt["采摘exp"] + " 点经验");

                /*if(pc.Party != null)
                {
                    List<Actor> acts = map.GetActorsArea(pc, 700, false);
                    SkillArg arg2 = new SkillArg();
                    foreach (var i in acts)
                    {
                        if(i.type == ActorType.PC)
                        {
                            ActorPC ipc = (ActorPC)i;
                            if(ipc.Online && ipc.MapID == pc.MapID && ipc.Party == pc.Party)
                            {
                                if(SagaLib.Global.Random.Next(0,100) < 30)
                                {
                                    arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(13199, 1);
                                    arg2.sActor = sActor.ActorID;
                                    arg2.argType = SkillArg.ArgType.Active;
                                    arg2.affectedActors.Add(i);
                                    arg2.flag.Add(AttackFlag.UNKNOWN1);
                                    arg2.hp.Add(0);
                                    arg2.mp.Add(0);
                                    arg2.sp.Add(0);

                                    itemid = 110096400;
                                    if (SagaLib.Global.Random.Next(0, 100) < 70)
                                        itemid = 110096401;
                                    if (SagaLib.Global.Random.Next(0, 100) < 10)
                                        itemid = 110096402;
                                    item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                                    item.Stack = 1;

                                    MapClient iclient = MapClient.FromActorPC(ipc);
                                    iclient.AddItem(item, true);
                                    iclient.SendNPCPlaySound(2559, 0, 100, 50);
                                    iclient.SendSystemMessage("从" + sActor.Name + "处 获得了额外的 " + item.BaseData.name + " ！");
                                }
                            }
                        }
                    }
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
                }*/
            }

            if (dActor.Name == "草莓B")//个人草莓
            {
                uint itemid = 110096400;
                if (SagaLib.Global.Random.Next(0, 100) < 70)
                    itemid = 110096401;
                if (SagaLib.Global.Random.Next(0, 100) < 10)
                    itemid = 110096402;

                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                item.Stack = 1;


                client.AddItem(item, true);
                client.SendNPCPlaySound(2621, 0, 100, 50);
                pc.CInt["采摘exp"]++;
                client.SendSystemMessage("获得了 1 点采摘经验值。已累积：" + pc.CInt["采摘exp"] + " 点经验");

                if (SagaLib.Global.Random.Next(0, 100) < 30)
                {

                    itemid = 110096400;
                    if (SagaLib.Global.Random.Next(0, 100) < 70)
                        itemid = 110096401;
                    if (SagaLib.Global.Random.Next(0, 100) < 10)
                        itemid = 110096402;
                    item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                    item.Stack = 1;

                    client.AddItem(item, true);
                    client.SendNPCPlaySound(2559, 0, 100, 50);
                    client.SendSystemMessage("获得了额外的 " + item.BaseData.name + " ！");
                }

            }

            if (dActor.Name == "草莓C")//公共草莓
            {
                uint itemid = 110096400;
                if (SagaLib.Global.Random.Next(0, 100) < 70)
                    itemid = 110096401;
                if (SagaLib.Global.Random.Next(0, 100) < 10)
                    itemid = 110096402;

                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                item.Stack = 1;


                client.AddItem(item, true);
                client.SendNPCPlaySound(2621, 0, 100, 50);
                pc.CInt["采摘exp"]++;
                client.SendSystemMessage("获得了 1 点采摘经验值。已累积：" + pc.CInt["采摘exp"] + " 点经验");

                List<Actor> acts = map.GetActorsArea(pc, 700, false);
                SkillArg arg2 = new SkillArg();
                foreach (var i in acts)
                {
                    if (i.type == ActorType.PC)
                    {
                        ActorPC ipc = (ActorPC)i;
                        if (ipc.Online && ipc.MapID == pc.MapID)
                        {
                            if (SagaLib.Global.Random.Next(0, 100) < 30)
                            {
                                arg2.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(13199, 1);
                                arg2.sActor = sActor.ActorID;
                                arg2.argType = SkillArg.ArgType.Active;
                                arg2.affectedActors.Add(i);
                                arg2.flag.Add(AttackFlag.UNKNOWN1);
                                arg2.hp.Add(0);
                                arg2.mp.Add(0);
                                arg2.sp.Add(0);

                                itemid = 110096400;
                                if (SagaLib.Global.Random.Next(0, 100) < 70)
                                    itemid = 110096401;
                                if (SagaLib.Global.Random.Next(0, 100) < 10)
                                    itemid = 110096402;
                                item = SagaDB.Item.ItemFactory.Instance.GetItem(itemid);
                                item.Stack = 1;

                                MapClient iclient = MapClient.FromActorPC(ipc);
                                iclient.AddItem(item, true);
                                iclient.SendNPCPlaySound(2559, 0, 100, 50);
                                iclient.SendSystemMessage("从" + sActor.Name + "处 获得了额外的 " + item.BaseData.name + " ！");
                            }
                        }
                    }
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg2, sActor, true);
            }


            if (dActor.Name == "苏婆蓝莓")
            {
                SagaDB.Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem(110097300);
                item.Stack = (ushort)SagaLib.Global.Random.Next(1, 2);
                if (SagaLib.Global.Random.Next(0, 100) < 50)
                    item.Stack = (ushort)SagaLib.Global.Random.Next(2, 3);
                if (SagaLib.Global.Random.Next(0, 100) < 10)
                    item.Stack = (ushort)SagaLib.Global.Random.Next(2, 5);
                client.AddItem(item, true);
                client.SendNPCPlaySound(2621, 0, 100, 50);
                pc.CInt["采摘exp"]++;
                client.SendSystemMessage("获得了 1 点采摘经验值。已累积：" + pc.CInt["采摘exp"] + " 点经验");
            }
        }
        #endregion
    }
}
