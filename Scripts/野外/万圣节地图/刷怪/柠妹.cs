
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaDB.Actor;
using SagaMap.Mob;
using SagaDB.Mob;
namespace WeeklyExploration
{
    public partial class 万圣节
    {
        ActorMob.MobInfo 柠妹Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "柠妹！！！";
            info.maxhp = 13000000;
            info.speed = 550;
            info.range = 4;
            info.atk_min = 400;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 500;//最低魔法攻擊
            info.matk_max = 950;//最高物理攻擊
            info.def = 30;
            info.def_add = 225;
            info.mdef = 34;
            info.mdef_add = 270;
            info.hit_critical = 22;
            info.hit_magic = 264;
            info.hit_melee = 181;
            info.hit_ranged = 181;
            info.avoid_critical = 22;
            info.avoid_magic = 133;
            info.avoid_melee = 36;
            info.avoid_ranged = 37;
            info.Aspd = 432;
            info.Cspd = 649;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 40;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 50;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 50;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000060;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.count = 1;
            newDrop.Public = true;
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/

            return info;
        }
        AIMode 柠妹AI()
        {
            AIMode ai = new AIMode(32); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10060600;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 90;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31070, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31070, skillinfo);//將這個技能加進進程技能表

            /*---------炎爆术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 70;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16103, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16103, skillinfo);//將這個技能加進進程技能表

            /*---------白想剑---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 50;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16507, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16507, skillinfo);//將這個技能加進進程技能表

            /*---------献血之路---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 300;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30023, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30023, skillinfo);//將這個技能加進進程技能表

            /*---------地狱之火---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 200;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31071, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31071, skillinfo);//將這個技能加進進程技能表

            /*---------冰棍的制作方式---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 200;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 98;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30028, skillinfo);//將這個技能加進進程技能表

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

            /*---------死神召唤---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31011, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31011, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}