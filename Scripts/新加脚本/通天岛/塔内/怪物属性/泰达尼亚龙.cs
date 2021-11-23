
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
namespace Exploration
{
    public partial class 塔内
    {
        public static ActorMob.MobInfo 泰达尼亚龙Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "泰达尼亚龙";
            info.maxhp = 3000000;
            info.speed = 500;
            info.atk_min = 400;
            info.atk_max = 600;
            info.matk_min = 400;
            info.matk_max = 600;
            info.def = 20;
            info.def_add = 200;
            info.mdef = 20;
            info.mdef_add = 200;
            info.hit_critical = 14;
            info.hit_magic = 50;
            info.hit_melee = 78;
            info.hit_ranged = 79;
            info.avoid_critical = 0;
            info.avoid_magic = 0;
            info.avoid_melee = 0;
            info.avoid_ranged = 0;
            info.Aspd = 550;
            info.Cspd = 440;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 70;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 0;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 0;
            info.baseExp = info.maxhp;
            info.jobExp = info.maxhp;


            MobData.DropData newDrop = new MobData.DropData();
            newDrop = new MobData.DropData();
            newDrop.ItemID = 110131612;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;
            info.dropItems.Add(newDrop);

            newDrop = new MobData.DropData();
            newDrop.ItemID = 10081300;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;
            info.dropItems.Add(newDrop);

            return info;
        }
       public static AIMode 泰达尼亚龙AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10960002;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 3;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 3;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*--------招架---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(32002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(32002, skillinfo);

            /*---------星原之握---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31140, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31140, skillinfo);

            /*---------星河急涌---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 70;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31141, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31141, skillinfo);

            /*---------星龙雷电吐息---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 65;//技能CD
            skillinfo.Rate = 50;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31142, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31142, skillinfo);

            /*---------狂暴技·黑暗物质---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.OverTime = 300;
            skillinfo.CD = 1;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31143, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31143, skillinfo);


            return ai;
        }
    }
}

