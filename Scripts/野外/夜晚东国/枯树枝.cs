
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
    public class S910000114 : Event
    {
        public S910000114()
        {
            this.EventID = 910000114;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID == 10057002)
            {
                if(SStr["东国枯树枝"] == DateTime.Now.Hour.ToString() && pc.Account.GMLevel < 50)
                {
                    Say(pc, 0, "目前小时的妖气似乎不足以使用枯树枝。$R$R※每个小时只能召唤一次。");
                    return;
                }
                if (CountItem(pc, 910000114) > 0)
                {
SStr["东国枯树枝"] =  DateTime.Now.Hour.ToString();
TakeItem(pc,910000114,1);
                    int r = Global.Random.Next(1, 1);
                    Map map = SagaMap.Manager.MapManager.Instance.GetMap(10057002);
                    ActorMob mob;
                    switch (r)
                    {
                        case 1:
                            SInt["东国死神出现次数"]++;
                            Announce(pc.Name + "在 漆黑法伊斯特 使用了枯树枝！召唤出了死神！！！");
                            mob = map.SpawnCustomMob(10000000, map.ID, 16370000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 死神Info(pc.Name), 死神AI(), null, 0)[0];
                            SkillHandler.Instance.ShowEffect(pc, Global.PosX16to8(mob.X, map.Width), Global.PosY16to8(mob.Y, map.Height), 5180);
                            mob.TInt["playersize"] = 1500;
                            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                            break;
                    }
                }
            }
            else
            {
                Say(pc, 0, "在这里使用$R似乎太危险了哦。");
                return;
            }
        }
        ActorMob.MobInfo 死神Info(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 1545925;//血量
            info.name = name + "召唤的死神";
            info.speed = 420;//移動速度
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 850;//最高物理攻擊
            info.matk_min = 650;//最低魔法攻擊
            info.matk_max = 850;//最高物理攻擊
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
            newDrop.ItemID = 10066800;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;//1%-20%掉落
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 10066800;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;//20%以上掉落
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 死神AI()
        {
            AIMode ai = new AIMode(33); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 5;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD =5;//遠程技能表最短釋放間隔，3秒一次
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

            /*--------救援邀请---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 44;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(7753, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(7753, skillinfo);//將這個技能加進進程技能表

            /*---------奈何桥之路---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 65;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30015, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30015, skillinfo);//將這個技能加進進程技能表

            /*---------黄泉---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 135;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30016, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30016, skillinfo);//將這個技能加進進程技能表

            /*---------灵压---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 35;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30017, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30017, skillinfo);//將這個技能加進進程技能表

            /*---------收割---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 25;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 75;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30018, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30018, skillinfo);//將這個技能加進進程技能表

            /*---------吸魂---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 12;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30019, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30019, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

