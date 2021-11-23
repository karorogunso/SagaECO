
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap.Skill;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000105 : Event
    {
        public S910000105()
        {
            this.EventID = 910000105;
        }
        class NewYear
        {
            public uint itemID;
            public uint rate;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID == 10056000 || pc.MapID == 10071004)
            {
                if (CountItem(pc, 910000105) > 0)
                {
                    int mran = SagaLib.Global.Random.Next(0, 100);
                    if (mran < 100)
                    {
                        uint itemid = GetBoundId();
                        if (itemid == 0) return;
                        TakeItem(pc, 910000105, 1);
                        GiveItem(pc, itemid, 1);
                        SkillHandler.Instance.ShowEffectOnActor(pc, 8056);
                        List<uint> ann = new List<uint>() { 110174500, 910000104, 960000012, 950000028, 950000027, 950000031, 950000032 };
                        if (ann.Contains(itemid) && pc.Account.GMLevel < 100)
                        {
                            SagaDB.Item.Item i = ItemFactory.Instance.GetItem(itemid);
                            Announce(pc.Name + " 从 迎春炸裂盒子里 获得了 " + i.BaseData.name + " ！");
                        }
                    }
                    else
                    {
                        int r = Global.Random.Next(1, 2);
                        SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(10056000);
                        ActorMob mob;
                        switch (r)
                        {
                            case 1:
                                SInt["新年僵尸出现次数"]++;
                                Announce(pc.Name + "从 迎春炸裂盒子 里释放了 新年僵尸！！大家快去推僵尸妹妹呀！！");
                                mob = map.SpawnCustomMob(10000000, map.ID, 18000000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 新年僵尸Info(pc.Name), 新年僵尸AI(), null, 0)[0];
                                mob.TInt["playersize"] = 1200;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                                break;
                            case 2:
                                SInt["新年阿鲁卡多出现次数"]++;
                                Announce("是谁把本小姐放出来的？ " + pc.Name + "是你吗！？阿鲁卡多被从 迎春炸裂盒子 里召唤出来了。");
                                mob = map.SpawnCustomMob(10000000, map.ID, 16420000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 新年柠妹Info(pc.Name), 新年柠妹AI(), null, 0)[0];
                                mob.TInt["playersize"] = 1200;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                                break;
                            case 3:
                                SInt["新年麒麟出现次数"]++;
                                Announce(pc.Name + "从 迎春炸裂盒子里 里释放了 麒麟！！！");
                                mob = map.SpawnCustomMob(10000000, map.ID, 18940000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 新年麒麟Info(pc.Name), 新年麒麟AI(), null, 0)[0];
                                mob.TInt["playersize"] = 1500;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                                break;
                        }
                    }
                }
            }
            else
            {
                Say(pc, 0, "在这里打开$R似乎太危险了哦。$R$R去牛牛草原打开把！");
                return;
            }
        }
        uint GetBoundId()
        {
            uint id = 10000000;
            List<NewYear> list = GetList();
            int totalrate = GetTotalRate(list);
            int ran = Global.Random.Next(0, totalrate);
            int rate = 0;
            foreach (var item in list)
            {
                rate += (int)item.rate;
                if (ran <= rate)
                {
                    id = item.itemID;
                    break;
                }
            }
            return id;
        }
        NewYear New(uint id, uint rate)
        {
            NewYear s = new NewYear();
            s.itemID = id;
            s.rate = rate;
            return s;
        }
        int GetTotalRate(List<NewYear> list)
        {
            int t = 0;
            foreach (var item in list)
            {
                t += (int)item.rate;
            }
            return t;
        }
        List<NewYear> GetList()
        {
            List<NewYear> items = new List<NewYear>();
            items.Add(New(110174500, 5));//咕咕·阿鲁玛
            items.Add(New(10074901, 10));//咕咕发型介绍信
            items.Add(New(61024300, 10));//純鶏の羽（ピンク）
            items.Add(New(60169900, 20));//純鶏のアウターウェア（赤）
            items.Add(New(50255200, 25));//純鶏のスカート（赤）
            items.Add(New(50255300, 25));//純鶏のブーツ（茶）
            items.Add(New(31084200, 20));//華麗なる食卓（赤）
            items.Add(New(60144702, 20));//聖歌隊のローブ（白）
            items.Add(New(50241703, 20));//聖歌隊のハーフブーツ（白）
            items.Add(New(31144400, 15));//クリスマス?ターキー（丸焼き）
            items.Add(New(910000101, 50));//任务点增加勋章[5点]
            items.Add(New(910000102, 10));//任务点增加勋章[10点]
            items.Add(New(910000103, 10));//任务点增加勋章[50点]
            items.Add(New(910000104, 5));//任务点增加勋章[300点]
            /*items.Add(New(910000075, 10));//经验值增加50%
            items.Add(New(910000076, 10));//经验值增加100%
            items.Add(New(910000077, 5));//经验值增加150%
            items.Add(New(910000078, 3));//经验值增加200%
            items.Add(New(910000079, 2));//经验值增加250%
            items.Add(New(910000080, 1));//经验值增加300%
            items.Add(New(910000086, 10));//搭档经验值增加50%
            items.Add(New(910000087, 10));//搭档经验值增加100%
            items.Add(New(910000088, 5));//搭档经验值增加150%
            items.Add(New(910000089, 3));//搭档经验值增加200%
            items.Add(New(910000090, 2));//搭档经验值增加250%
            items.Add(New(910000091, 1));//搭档经验值增加300%*/
            items.Add(New(910000092, 50));//搭档经验礼盒（2000）
            items.Add(New(910000093, 20));//搭档经验礼盒（4000）
            items.Add(New(910000094, 20));//搭档经验礼盒（6000）
            items.Add(New(910000095, 15));//搭档经验礼盒（8000）
            items.Add(New(910000096, 10));//搭档经验礼盒（10000）
            items.Add(New(910000097, 10));//搭档盒子
            //items.Add(New(910000106, 10));//搭档变身卡
            items.Add(New(950000027, 5));//S级搭档突破石
            items.Add(New(950000028, 1));//SS级搭档突破石
            items.Add(New(950000000, 5));//发型代币
            items.Add(New(950000001, 5));//抽脸币
            items.Add(New(951000000, 50));//KUJI扭蛋机代币
            items.Add(New(950000025, 15));//限定KUJI代币
            items.Add(New(960000000, 10));//项链强化石
            items.Add(New(960000001, 10));//武器强化石
            items.Add(New(960000002, 10));//衣服强化石
            items.Add(New(960000010, 10));//强化11-20祝福水
            items.Add(New(960000011, 5));//强化21-30祝福水
            items.Add(New(960000012, 1));//强化31-40祝福水
            //items.Add(New(950000031, 5));//新增一个打开限定kuji盒子的钥匙(rank0-3等奖品机率x10)
            //items.Add(New(950000032, 5));//新增一个打开限定kuji盒子的钥匙(必开出盒内rank5以上的物品)
            return items;
        }
        ActorMob.MobInfo 新年麒麟Info(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 845925;//血量 
            info.name = name + "召唤的麒麟";
            info.speed = 650;//移動速度
            info.atk_min = 200;//最低物理攻擊
            info.atk_max = 300;//最高物理攻擊
            info.matk_min = 300;//最低魔法攻擊
            info.matk_max = 550;//最高物理攻擊
            info.def = 20;//物理左防
            info.mdef = 10;//魔法左防
            info.def_add = 50;//物理右防
            info.mdef_add = 50;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 500;//攻速
            info.Cspd = 100;//唱速
            info.range = 2f;
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 30;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 50;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 0;//水屬性
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
            info.baseExp = info.maxhp / 10;//基礎經驗值
            info.jobExp = info.maxhp / 10;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000111;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;//1%-20%掉落
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000111;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;//20%以上掉落
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 新年麒麟AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 6;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 6;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------火球术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 75;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31028, skillinfo);//將這個技能加進進程技能表

            /*---------暗靈術---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31030, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31030, skillinfo);//將這個技能加進進程技能表

            /*---------黑暗毒血---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 180;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31023, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31023, skillinfo);//將這個技能加進進程技能表

            /*---------雷冰球---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 160;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 85;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31021, skillinfo);//將這個技能加進進程技能表

            /*---------黑暗天幕---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 90;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30032, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30032, skillinfo);//將這個技能加進進程技能表


            /*---------血魔枪---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 60;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30020, skillinfo);//將這個技能加進進程技能表

            return ai;
        }

        ActorMob.MobInfo 新年柠妹Info(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 945925;//血量 
            info.name = name + "召唤的柠妹";
            info.speed = 650;//移動速度
            info.atk_min = 200;//最低物理攻擊
            info.atk_max = 300;//最高物理攻擊
            info.matk_min = 250;//最低魔法攻擊
            info.matk_max = 450;//最高物理攻擊
            info.def = 20;//物理左防
            info.mdef = 10;//魔法左防
            info.def_add = 50;//物理右防
            info.mdef_add = 50;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 500;//攻速
            info.Cspd = 100;//唱速
            info.range = 2f;
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 30;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 50;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 0;//水屬性
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
            info.baseExp = info.maxhp / 10;//基礎經驗值
            info.jobExp = info.maxhp / 10;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000109;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;//1%-20%掉落
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000109;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;//20%以上掉落
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 新年柠妹AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 6;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 6;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------地裂术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16401, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16401, skillinfo);//將這個技能加進進程技能表

            /*---------火球术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 10;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16101, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16101, skillinfo);//將這個技能加進進程技能表

            /*---------暗靈術---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 5;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3083, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3083, skillinfo);//將這個技能加進進程技能表

            /*---------黑暗毒血---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 6;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3134, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3134, skillinfo);//將這個技能加進進程技能表

            /*---------雷冰球---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 85;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30022, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30022, skillinfo);//將這個技能加進進程技能表

            /*---------黑暗天幕---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 12;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3085, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3085, skillinfo);//將這個技能加進進程技能表

            /*---------寒冰之夜---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 35;//技能CD
            skillinfo.Rate = 40;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30026, skillinfo);//將這個技能加進進程技能表

            /*---------黑暗雷暴---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30021, skillinfo);//將這個技能加進進程技能表

            /*---------血魔枪---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 60;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30020, skillinfo);//將這個技能加進進程技能表

            return ai;
        }
        ActorMob.MobInfo 新年僵尸Info(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 1045925;//血量
            info.name = name + "召唤的僵尸妹妹";
            info.speed = 420;//移動速度
            info.atk_min = 200;//最低物理攻擊
            info.atk_max = 550;//最高物理攻擊
            info.matk_min = 250;//最低魔法攻擊
            info.matk_max = 450;//最高物理攻擊
            info.def = 20;//物理左防
            info.mdef = 10;//魔法左防
            info.def_add = 50;//物理右防
            info.mdef_add = 50;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 700;//攻速
            info.Cspd = 100;//唱速
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 20;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 30;//暗屬性
            info.elements[SagaLib.Elements.Holy] = 0;//光屬性
            info.elements[SagaLib.Elements.Neutral] = 0;//無屬性
            info.elements[SagaLib.Elements.Water] = 0;//水屬性
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
            info.baseExp = info.maxhp / 10;//基礎經驗值
            info.jobExp = info.maxhp / 10;//職業經驗值

            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000110;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;//1%-20%掉落
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000110;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;//20%以上掉落
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 新年僵尸AI()
        {
            AIMode ai = new AIMode(1); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 20;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 20;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------暗靈術---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3083, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3083, skillinfo);//將這個技能加進進程技能表

            /*---------魔動劍---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 9;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3126, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3126, skillinfo);//將這個技能加進進程技能表

            /*---------地獄之冥火---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3310, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3310, skillinfo);//將這個技能加進進程技能表

            /*---------寒冰之夜---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30026, skillinfo);//將這個技能加進進程技能表

            /*---------火球术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16101, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16101, skillinfo);//將這個技能加進進程技能表

            /*---------地狱之火---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 50;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31010, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31010, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

