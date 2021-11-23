
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
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {

        ActorMob.MobInfo 老三Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 3145925;//血量 
            info.name = "？？？";
            info.speed = 650;//移動速度
            info.atk_min = 1800;//最低物理攻擊
            info.atk_max = 3200;//最高物理攻擊
            info.matk_min = 700;//最低魔法攻擊
            info.matk_max = 1000;//最高物理攻擊
            info.def = 40;//物理左防
            info.mdef = 40;//魔法左防
            info.def_add = 230;//物理右防
            info.mdef_add = 230;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 600;//攻速
            info.Cspd = 700;//唱速
            info.range = 2f;
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 30;//火屬性
            info.elements[SagaLib.Elements.Earth] = 0;//地屬性
            info.elements[SagaLib.Elements.Dark] = 0;//暗屬性
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
            info.baseExp = info.maxhp;//基礎經驗值
            info.jobExp = info.maxhp;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 老三AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 7;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 7;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------大恶鬼之修罗狱---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfLong.Add(31020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(31020, skillinfo);//將這個技能加進進程技能表

            /*---------恶鬼之道---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 65;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31021, skillinfo);//將這個技能加進進程技能表

            /*---------鬼魂统御---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 100;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31022, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31022, skillinfo);//將這個技能加進進程技能表

            /*---------阿修罗刀狱---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 120;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 50;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31023, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31023, skillinfo);//將這個技能加進進程技能表

            /*---------虚幻斩---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 12;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 30;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31024, skillinfo);//將這個技能加進進程技能表

            /*---------鬼之影---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 80;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 30;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31025, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31025, skillinfo);//將這個技能加進進程技能表

            /*---------修罗-寒冰斩---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 10;//釋放概率
            skillinfo.MaxHP = 96;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31026, skillinfo);//將這個技能加進進程技能表


            return ai;
        }
    }
}

