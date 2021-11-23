
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.Manager;
using SagaMap.Network.Client;
using SagaMap.ActorEventHandlers;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S59999999 : Event
    {
        public S59999999()
        {
            this.EventID = 59999999;
        }

        public override void OnEvent(ActorPC pc)
        {

            if ((pc.Account.AccountID <= 247 || pc.Name == "吉田由美") && pc.CInt["数据转移"] != 1)
            {
                Say(pc, 0, "欢迎回归！$R$R由于新版本数据大量不同，$R所有的老账号都需要进行数据转移。");
                Say(pc, 0, "数据转移内容包括：$R$R清除道具栏及仓库、$R冻结所有当前金钱、$R洗掉身上所有强化");
                Say(pc, 0, "数据转移将保留：$R当前身上装备的所有物品$R（不包括宠物和特效装备）、$R等级直升新版本的60/25、$R获得3个25强化的机会。$R$R※状态异常的物品将无法被转移！（比如图标变成了问号的）");
                Say(pc, 0, "当一个角色进行转移后，$R账号的其他角色将无法再使用仓库。$R因此请在转移前，$R将其他角色需要转移的装备从仓库中整理好。");
                Say(pc, 0, "转移后，可能会产生部分BUG。$R如有遇到，请报给番茄，番茄会尽快修理。");
                switch (Select(pc, "数据转移选项", "", "打开仓库", "我身上卡装备了！", "离开"))
                {
                    case 1:
                        if (pc.AInt["第一次转"] != 1)
                        {
                            OpenWareHouse(pc, WarehousePlace.Acropolis);
                        }
                        else
                        {
                            Say(pc, 0, "你已经转移过其他角色，仓库不能再使用了。");
                            return;
                        }
                        break;
                    case 2:
                        Say(pc, 0, "由于卡装备是由于旧数据作废，$R修复后，卡中的装备将被删除$R$R修复时，你将会被强制下线。$R再次上线即可。");
                        Say(pc, 0, "修复卡装备需要清除身上的所有物品，$R请将所有物品先存入仓库，再确认修复。");
                        switch (Select(pc, "是否要修复身上卡物品？", "", "是的【会清除身上所有物品】", "我还没把东西放好在仓库"))
                        {
                            case 1:
                                Say(pc, 0, "再次提醒：修复卡装备需要清除身上的所有物品，$R请将所有物品先存入仓库，再确认修复。");
                                if (Select(pc, "是否要修复身上卡物品？", "", "是的【会清除身上所有物品】", "我还没把东西放好在仓库") == 1)
                                {
                                    if (pc.Inventory.Items.ContainsKey(ContainerType.RIGHT_HAND))
                                        pc.Inventory.Items[ContainerType.RIGHT_HAND].Clear();
                                    if (pc.Inventory.Items.ContainsKey(ContainerType.UPPER_BODY))
                                        pc.Inventory.Items[ContainerType.UPPER_BODY].Clear();
                                    //pc.Inventory = new Inventory(pc);
                                    GiveItem(pc, 100000001, 1);
                                    SagaMap.MapServer.charDB.SaveChar(pc, true);
                                    SagaMap.Network.Client.MapClient.FromActorPC(pc).netIO.Disconnect();
                                }
                                break;
                        }
                        break;
                    case 3:
                        break;
                }
                return;
            }
            if (pc.CharID < 633 && pc.CInt["数据转移"] != 1)
            {
                Say(pc, 0, "对不起，您所在的位置不在服务区", "圆滚滚联通");
                return;
            }
            if (pc.Level > 30)
                每日奖励(pc);
            if (pc.Account.GMLevel > 200)
            {
                int set = Select(pc, "测试！是否给宠物改名？", "", "改！", "提升1级RANK", "no");
                if (set == 1)
                {
                    string name = InputBox(pc, "取个COOOOOOL的名字！", InputType.PetRename);
                    if (pc.Partner != null)
                    {
                        pc.Partner.Name = name;
						pc.Inventory.Equipments[EnumEquipSlot.PET].Name = name;
                        MapClient.FromActorPC(pc).SendPetBasicInfo();
                        MapClient.FromActorPC(pc).SendPetDetailInfo();
                        SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc.Partner, true);
						SagaMap.Network.Client.MapClient.FromActorPC(pc).SendItemInfo(pc.Inventory.Equipments[EnumEquipSlot.PET]);
                    }
                    //Say(pc, 0, "重新召唤生效 哦！");
                }
                else if (set == 2)
                {
                    if (pc.Partner != null)
                    {
                        if (pc.Partner.rank < 9)
                            pc.Partner.rank++;
                        MapClient.FromActorPC(pc).SendPetBasicInfo();
                        MapClient.FromActorPC(pc).SendPetDetailInfo();
                    }
                }
            }
            switch (Select(pc, "是我！明明是我先的！和" + pc.Name + "成为朋友也好还是成为恋人也好！", "", "传送回据点", "打开仓库(2000G)", "进入飞空艇", "进入除夕活动地图！！", "修理装备【收费服务】", "进入活动地图", "关闭手机"))
            {
                case 1:
                    pc.HP = pc.MaxHP;
                    pc.MP = pc.MaxMP;
                    pc.SP = pc.MaxSP;
                    Warp(pc, 10054000, 153, 148);
                    pc.TInt["副本复活标记"] = 0;
                    break;
                case 2:
                    if (pc.Gold < 2000)
                    {
                        Say(pc, 0, "对不起，您的手机已欠费", "圆滚滚联通");
                        return;
                    }
                    pc.Gold -= 2000;
                    OpenWareHouse(pc, WarehousePlace.Acropolis);
                    break;
                case 3:
                    if (pc.Party != null)
                    {
                        if (Select(pc, "请选择", "", "进入队长的飞空艇", "进入自己的飞空艇", "离开") == 1)
                        {
                            if (pc.Party.Leader.FGarden != null)
                            {
                                Packet p = new Packet(10);//unknown packet
                                p.ID = 0x18E3;
                                p.PutUInt(pc.ActorID, 2);
                                p.PutUInt(pc.MapID, 6);
                                MapClient.FromActorPC(pc).netIO.SendPacket(p);
                                Warp(pc, pc.Party.Leader.FGarden.MapID, 6, 11);
                                return;
                            }
                            else
                            {
                                Say(pc, 0, "队长尚未开放飞空艇");
                                return;
                            }
                        }
                    }
                    if (pc.FGarden == null)
                        pc.FGarden = new SagaDB.FGarden.FGarden(pc);

                    Packet p2 = new Packet(10);//unknown packet
                    p2.ID = 0x18E3;
                    p2.PutUInt(pc.ActorID, 2);
                    p2.PutUInt(pc.MapID, 6);
                    MapClient.FromActorPC(pc).netIO.SendPacket(p2);



                    Map map = MapManager.Instance.GetMap(pc.MapID);
                    pc.FGarden.MapID = MapManager.Instance.CreateMapInstance(pc, 70000000, pc.MapID, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height));
                    //spawn furnitures
                    map = MapManager.Instance.GetMap(pc.FGarden.MapID);
                    foreach (ActorFurniture i in pc.FGarden.Furnitures[SagaDB.FGarden.FurniturePlace.GARDEN])
                    {
                        i.e = new SagaMap.ActorEventHandlers.NullEventHandler();
                        map.RegisterActor(i);
                        i.invisble = false;
                    }

                    pc.BattleStatus = 0;
                    pc.Speed = 200;
                    MapClient.FromActorPC(pc).SendChangeStatus();
                    Map newMap = MapManager.Instance.GetMap(pc.FGarden.MapID);
                    MapClient.FromActorPC(pc).Map.SendActorToMap(pc, newMap, Global.PosX8to16(6, newMap.Width), Global.PosY8to16(11, newMap.Height));
                    break;
                case 4:
                    Warp(pc, 10071004, 254, 92);
                    break;
                    Map map2 = MapManager.Instance.GetMap(pc.MapID);
                    pc.Mode = PlayerMode.KNIGHT_SOUTH;
                    if (pc.Name == "羽川柠")
                        pc.Mode = PlayerMode.KNIGHT_NORTH;
                    map2.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, pc, true);
                    map2.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);

                    break;
                    /*
                    Say(pc, 131, "嗯哼，$R小姑娘,$R你的衣服破洞洞了呢$R$R想不想让我来帮你修补修补呢？", "修理师");
                    switch (Select(pc, "你找这个变态要干什么", "", "我要修理我的装备", "拒绝"))
                    {
                        case 1:
                            SagaDB.Item.Item Equip = new SagaDB.Item.Item();
                            List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
                            List<string> names = new List<string>();
                            string name = "";
                            int basecost = 0;//价格基数，损失3以下 10000一点，3以上 20000一点。
                            foreach (var item in pc.Inventory.Equipments)
                            {
                                if (!Equips.Contains(item.Value))
                                {
                                    if (item.Value.Durability < item.Value.maxDurability)
                                    {
                                        name = "【" + item.Value.BaseData.name + "】持久度：" + item.Value.Durability.ToString() + "/" + item.Value.maxDurability.ToString();
                                        Equips.Add(item.Value);
                                        names.Add(item.Value.BaseData.name);
                                    }
                                }
                            }
                            foreach (var item in pc.Inventory.Items[ContainerType.BODY])
                            {
                                if (!Equips.Contains(item))
                                {
                                    if (item.Durability < item.maxDurability)
                                    {
                                        name = "【" + item.BaseData.name + "】持久度：" + item.Durability.ToString() + "/" + item.maxDurability.ToString();
                                        Equips.Add(item);
                                        names.Add(item.BaseData.name);
                                    }
                                }
                            }
                            if (names.Count < 1)
                            {
                                Say(pc, 0, "不好意思...$R$R您似乎身上没有坏的装备呢。", "修理师");
                                return;
                            }
                            int set = Select(pc, "怎么办呢", "", "我要单独修理", "修理身上全部已损坏的物品", "离开");
                            if (set == 1)
                            {
                                Equip = Equips[Select(pc, "请选择要修理的部位", "", names.ToArray()) - 1];
                                int dif = Equip.maxDurability - Equip.Durability;
                                int cost = basecost;//价格计算
                                if (dif < 3)
                                    cost = (Equip.maxDurability - Equip.Durability) * basecost;
                                else
                                    cost = (Equip.maxDurability - Equip.Durability) * basecost * 2;
                                switch (Select(pc, "是否修理【" + name + "】?", "", "进行修理 (当前费用:" + cost.ToString() + ")", "离开"))
                                {
                                    case 1:
                                        if (pc.Gold >= cost)
                                        {
                                            pc.Gold -= cost;
                                        }
                                        else
                                        {
                                            Say(pc, 131, "唔。。你似乎没带够钱呢", "强化店");
                                            return;
                                        }
                                        PlaySound(pc, 2863, false, 100, 50);
                                        Equip.Durability = Equip.maxDurability;
                                        Say(pc, 131, "【" + Equip.BaseData.name + "】$R$R" + "修理完成了！");
                                        return;
                                }
                            }
                            else
                            {
                                foreach (var item in Equips)
                                {
                                    PlaySound(pc, 2863, false, 100, 50);
                                    item.Durability = item.maxDurability;
                                    Say(pc, 131, "【" + Equip.BaseData.name + "】$R$R" + "修理完成了！");
                                }
                            }
                            break;
                    }*/
                    break;
                case 5:
                    SagaDB.Item.Item Equip = new SagaDB.Item.Item();
                    List<SagaDB.Item.Item> Equips = new List<SagaDB.Item.Item>();
                    List<string> names = new List<string>();
                    int totalgold = 0;
                    string name = "";
                    foreach (var item in pc.Inventory.Equipments)
                    {
                        if (!Equips.Contains(item.Value))
                        {
                            if (item.Value.Durability < item.Value.maxDurability)
                            {
                                int singlegold = 0;
                                if (item.Value.Refine > 0)
                                    singlegold = 300 * item.Value.Refine;
                                if (item.Value.Refine > 50)
                                    singlegold *= 2;
                                totalgold += singlegold;
                                Equips.Add(item.Value);
                                names.Add(item.Value.BaseData.name);
                            }
                        }
                    }
                    foreach (var item in pc.Inventory.Items[ContainerType.BODY])
                    {
                        if (!Equips.Contains(item))
                        {
                            if (item.Durability < item.maxDurability)
                            {
                                int singlegold = 0;
                                if (item.Refine > 0)
                                    singlegold = 300 * item.Refine;
                                if (item.Refine > 50)
                                    singlegold *= 2;
                                totalgold += singlegold;
                                Equips.Add(item);
                                names.Add(item.BaseData.name);
                            }
                        }
                    }
                    if (names.Count < 1)
                    {
                        Say(pc, 0, "不好意思...$R$R您似乎身上没有坏的装备呢。", "修理师");
                        return;
                    }
                    Say(pc, 0, "喂喂喂，这里是修理熊！$R哦，你要修理装备啊，$R远程服务是需要收费的！$R$R费用如下：$R0强装备修理免费$R强化装备每强化1次费用多300G$R超过50强费用翻倍！", "修理熊");
                    if (Select(pc, "是否修理身上的全部物品？", "", "修理全部！费用：" + totalgold, "还是算了") == 1)
                    {
                        if (pc.Gold < totalgold)
                        {
                            Say(pc, 131, "咳，你钱不够了！$R快点回来我给你免费检查~$R$R嘿嘿嘿!", "修理师");
                            return;
                        }
                        pc.Gold -= totalgold;
                        foreach (var item in Equips)
                        {
                            PlaySound(pc, 2863, false, 100, 50);
                            item.Durability = item.maxDurability;
                            Say(pc, 131, "【" + item.BaseData.name + "】$R$R" + "修理完成了！");
                        }
                    }
                    break;
                case 6:
                        Say(pc, 0, "GM尚未开启活动");
                        return;
                    if (SInt["活动地图MAPID"] == 0)
                    {

                    }
                    byte count = 0;
                    foreach (MapClient i in MapClientManager.Instance.OnlinePlayer)
                        if (i.Character.Account.LastIP == pc.Account.LastIP && i.Character.Account.GMLevel < 20)
                            count++;
                    if (count > 1)
                    {
                        Say(pc, 131, "请不要多开。");
                        return;
                    }
                    pc.TInt["复活次数"] = 15;
                    pc.TInt["设定复活次数"] = 15;
                    pc.TInt["副本复活标记"] = 3;

                    pc.TInt["伤害统计"] = 0;
                    pc.TInt["受伤害统计"] = 0;
                    pc.TInt["受治疗统计"] = 0;
                    pc.TInt["治疗溢出统计"] = 0;
                    pc.TInt["死亡统计"] = 0;
                    Warp(pc, 20080018, 20, 20);
                    break;
            }
        }

        ActorMob mob;

        void 刷怪()
        {
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(20212000);
            mob = map.SpawnCustomMob(10000000, 20212000, 16290000, 0, 0, 60, 24, 0, 1, 0, 外塔Info(), 外塔AI(), null, 0)[0];
            ((MobEventHandler)mob.e).Defending += KitaAreaBOSS_Defending;
        }

        private void KitaAreaBOSS_Defending(MobEventHandler eh, ActorPC pc)
        {
            ActorMob mob = eh.mob;
            if (mob.HP < 50 && eh.mob.AttackedForEvent != 1)
            {
                SkillHandler.Instance.ActorSpeak(mob, "T_T");
                SagaMap.Map map;
                map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                List<Actor> actors = map.GetActorsArea(mob, 5000, false);
                eh.mob.AttackedForEvent = 1;
                foreach (var item in actors)
                {
                    if (item.type == ActorType.PC)
                    {
                        ActorPC m = (ActorPC)item;
                        if (m.Online && m != null)
                        {
                            if (m.Buff.Dead)
                                SagaMap.Network.Client.MapClient.FromActorPC(m).RevivePC(m);
                            string s = "玩家 " + m.Name + " 在本次战斗总共造成伤害：" + m.TInt["伤害统计"].ToString() + " 受到伤害：" + m.TInt["受伤害统计"].ToString() +
                                   " 共治疗：" + m.TInt["治疗统计"].ToString() + " 共受到治疗：" + m.TInt["受治疗统计"].ToString();
                            foreach (var item2 in actors)
                            {
                                if (item2.type == ActorType.PC)
                                {
                                    ActorPC m2 = (ActorPC)item2;
                                    if (m2.Online && m2 != null)
                                        SagaMap.Network.Client.MapClient.FromActorPC(m2).SendSystemMessage(s);
                                }
                            }
                        }
                    }
                }
            }
        }

        ActorMob.MobInfo 外塔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 23240010;//血量
            info.name = "北方领主·迷你兔";
            info.range = 3;
            info.speed = 650;//移動速度
            info.atk_min = 1200;//最低物理攻擊
            info.atk_max = 1900;//最高物理攻擊
            info.matk_min = 700;//最低魔法攻擊
            info.matk_max = 1050;//最高物理攻擊
            info.def = 35;//物理左防
            info.mdef = 35;//魔法左防
            info.def_add = 90;//物理右防
            info.mdef_add = 90;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 40;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 500;//攻速 
            info.Cspd = 100;//唱速
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 0;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 0;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 50;//水屬性
            info.elements[SagaLib.Elements.Wind] = 0;//風屬性
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;//混亂抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;//冰抗性
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;//麻痺
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;//毒抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;//沉默抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;//睡抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;//石抗
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;//暈抗
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;//頓足抗
            info.baseExp = info.maxhp;//基礎經驗值
            info.jobExp = info.maxhp;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000010;
            newDrop.Rate = 10000;
            newDrop.Public = true;
            info.dropItems.Add(newDrop);


            /*---------物理掉落---------*/

            return info;
        }
        AIMode 外塔AI()
        {
            AIMode ai = new AIMode(33);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 5;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 5;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            /*---------娱乐马戏---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 50;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31001, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31001, skillinfo);//將這個技能加進進程技能表

            /*---------我的守护骑士们---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 70;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31002, skillinfo);//將這個技能加進進程技能表

            /*---------陷阱戏法---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31003, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31003, skillinfo);//將這個技能加進進程技能表

            /*---------天击地裂---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 40;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31004, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31004, skillinfo);//將這個技能加進進程技能表


            /*---------地裂术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16401, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16401, skillinfo);//將這個技能加進進程技能表

            /*---------冰河术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16201, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16201, skillinfo);//將這個技能加進進程技能表

            /*---------闪电弧---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16302, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16302, skillinfo);//將這個技能加進進程技能表


            return ai;
        }
        private void Time_OnTimerCall(Timer timer, ActorPC pc)
        {
            try
            {
                timer.Deactivate();
                MapClient.FromActorPC(pc).EventActivate(51000001);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        void 每日奖励(ActorPC pc)
        {
            if (pc.AStr["314活动每日奖励记录"] != DateTime.Now.ToString("yyyy-MM-dd") && pc.Level >= 31)
            {
                if (DateTime.Now.Month == 3 && DateTime.Now.Day >= 9)
                {
                    pc.AStr["314活动每日奖励记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                    GiveItem(pc, 950000034, 200);
                    Say(pc, 0, "获得了$CR『200个巧克力』$CD！$R$R※314活动每日送！$R要求玩家达到31级。$R$R活动时间：3月8日-4月8日");
                    MapClient.FromActorPC(pc).SendSystemMessage("领到了『200个巧克力』！");
                }
                if (DateTime.Now.Month == 4 && DateTime.Now.Day <= 9)
                {
                    pc.AStr["314活动每日奖励记录"] = DateTime.Now.ToString("yyyy-MM-dd");
                    GiveItem(pc, 950000034, 200);
                    Say(pc, 0, "获得了$CR『200个巧克力』$CD！$R$R※314活动每日送！$R要求玩家达到31级。$R$R活动时间：3月8日-4月8日");
                    MapClient.FromActorPC(pc).SendSystemMessage("领到了『200个巧克力』！");
                }
            }
            if (pc.AStr["每日奖励记录3"] != DateTime.Now.ToString("yyyy-MM-dd"))
            {
                /*int luc = Global.Random.Next(1, 11);
                string lucname = "吉";
                if (luc == 1) lucname = "大吉";
                if (luc == 2) lucname = "中吉";
                if (luc == 3) lucname = "吉";
                if (luc == 4) lucname = "小吉";
                if (luc == 5) lucname = "半吉";
                if (luc == 6) lucname = "末吉";
                if (luc == 7) lucname = "末凶";
                if (luc == 8) lucname = "小凶";
                if (luc == 9) lucname = "凶";
                if (luc == 10) lucname = "大凶";
                if (luc == 11) lucname = "超凶";

                Say(pc, 0, "你今天的运势是『" + lucname + "』。", "每日运势");
                if (luc >= 7)
                {
                    Select(pc, "你今天的运势是『" + lucname + "』。", "", "凶个屁那！");
                    luc = Global.Random.Next(1, 6);
                    if (luc == 1) lucname = "大吉";
                    if (luc == 2) lucname = "中吉";
                    if (luc == 3) lucname = "吉";
                    if (luc == 4) lucname = "小吉";
                    if (luc == 5) lucname = "半吉";
                    if (luc == 6) lucname = "末吉";
                    Say(pc, 0, "！！！$R$R你今天的运势是『" + lucname + "』。", "怕怕");
                }*/


                Say(pc, 0, "获得了每日奖励！$R");
                pc.AInt["每日盖章"]++;
                DailyStamp(pc, (uint)pc.AInt["每日盖章"], 2);
                GiveItem(pc, 910000002, 1);
                pc.AStr["每日奖励记录3"] = DateTime.Now.ToString("yyyy-MM-dd");
                if (pc.AInt["每日盖章"] >= 10)
                {
                    pc.AInt["每日盖章"] = 0;
                    GiveItem(pc, 910000015, 1);
                    PlaySound(pc, 4015, false, 100, 50);
                }

                MapClient.FromActorPC(pc).SendSystemMessage("领到了每日登陆奖励！");
            }
        }
    }
}

