
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace SagaScript.M30210000
{
    public partial class MOBTEST : Event
    {
        ActorMob.MobInfo 外塔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 9240010;//血量
            info.name = "活动·北方领主";
            info.range = 9;
            info.speed = 650;//移動速度
            info.atk_min = 1300;//最低物理攻擊
            info.atk_max = 1800;//最高物理攻擊
            info.matk_min = 600;//最低魔法攻擊
            info.matk_max = 950;//最高物理攻擊
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
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 20;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 20;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            /*---------娱乐马戏---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31001, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31001, skillinfo);//將這個技能加進進程技能表

            /*---------我的守护骑士们---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31002, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31002, skillinfo);//將這個技能加進進程技能表

            /*---------陷阱戏法---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31003, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31003, skillinfo);//將這個技能加進進程技能表

            /*---------天击地裂---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 40;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31004, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31004, skillinfo);//將這個技能加進進程技能表


            /*---------地裂术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16401, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16401, skillinfo);//將這個技能加進進程技能表

            /*---------冰河术---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(16201, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(16201, skillinfo);//將這個技能加進進程技能表

            /*---------寒冰斩-攻击---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 5;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(18021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(18021, skillinfo);//將這個技能加進進程技能表

            /*---------炎龙斩-暴击---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 5;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(18020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(18020, skillinfo);//將這個技能加進進程技能表

            return ai;
        }

    }
}

