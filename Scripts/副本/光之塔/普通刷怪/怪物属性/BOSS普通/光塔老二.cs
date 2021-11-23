
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

        ActorMob.MobInfo 老二Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 3545925;//血量 
            info.name = "？？？";
            info.speed = 650;//移動速度
            info.atk_min = 1500;//最低物理攻擊
            info.atk_max = 2700;//最高物理攻擊
            info.matk_min = 700;//最低魔法攻擊
            info.matk_max = 1000;//最高物理攻擊
            info.def = 40;//物理左防
            info.mdef = 40;//魔法左防
            info.def_add = 150;//物理右防
            info.mdef_add = 200;//魔法右防
            info.hit_critical = 50;//暴擊值
            info.hit_magic = 100;//魔法命中值（目前沒用
            info.hit_melee = 300;//近戰命中值
            info.hit_ranged = 300;//遠程命中值
            info.avoid_critical = 20;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 20;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 600;//攻速
            info.Cspd = 100;//唱速
            info.range = 2f;
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 0;//火屬性
            info.elements[SagaLib.Elements.Earth] = 30;//地屬性
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
            info.baseExp = info.maxhp/2;//基礎經驗值
            info.jobExp = info.maxhp/2;//職業經驗值
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 老二AI()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 10;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 7;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------螺旋俯冲---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 97;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31027, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31027, skillinfo);//將這個技能加進進程技能表


            /*---------百万坠击---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 42;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31028, skillinfo);//將這個技能加進進程技能表

            /*---------扭曲射线---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 52;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31029, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31029, skillinfo);//將這個技能加進進程技能表

            /*---------零件快速加固---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31030, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31030, skillinfo);//將這個技能加進進程技能表

            /*---------紧急维修---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 80;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 70;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31031, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31031, skillinfo);//將這個技能加進進程技能表

            /*---------零件溢出清理作业---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 70;//技能CD
            skillinfo.Rate = 70;//釋放概率
            skillinfo.MaxHP = 65;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31032, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31032, skillinfo);//將這個技能加進進程技能表


            return ai;
        }
    }
}

