
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaMap.Mob;
using SagaMap.Skill;
using SagaDB.Mob;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S242425 : Event
    {
        public S242425()
        {
            this.EventID = 242425;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "刷吗？", "", "刷刷刷", "不") == 1)
            {
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                ActorMob mob;
                mob = map.SpawnCustomMob(10000000, map.ID, 16010000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 老二Info困难(pc.Name), 老二AI困难(), null, 0)[0];
                mob.TInt["playersize"] = 1200;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
            }
        }

        ActorMob.MobInfo 老二Info困难(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 1545925;//血量 
            info.name = "夺魂者";
            info.speed = 650;//移動速度
            info.atk_min = 600;//最低物理攻擊
            info.atk_max = 800;//最高物理攻擊
            info.matk_min = 400;//最低魔法攻擊
            info.matk_max = 500;//最高物理攻擊
            info.def = 40;//物理左防
            info.mdef = 40;//魔法左防
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
            info.baseExp = info.maxhp / 10;//基礎經驗值
            info.jobExp = info.maxhp / 10;//職業經驗值


            return info;
        }
        AIMode 老二AI困难()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 5;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 5;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------漆黑之刃---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 7;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(3083, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(3083, skillinfo);//將這個技能加進進程技能表

            /*---------漆黑之刃---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 7;//技能CD
            skillinfo.Rate = 150;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(13107, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(13107, skillinfo);//將這個技能加進進程技能表


            /*---------漆黑之刃---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 15;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31104, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31104, skillinfo);//將這個技能加進進程技能表


            /*---------空蝉之衣---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 55;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31105, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31105, skillinfo);//將這個技能加進進程技能表

            /*---------祸乱之镰---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 85;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31106, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31106, skillinfo);//將這個技能加進進程技能表

            /*---------泡沫之镰---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 20;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31107, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31107, skillinfo);//將這個技能加進進程技能表

            /*---------灵魂漩涡---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 77;//技能CD
            skillinfo.Rate = 200;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31108, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31108, skillinfo);//將這個技能加進進程技能表

            /*---------灵魂回响---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 78;//技能CD
            skillinfo.Rate = 250;//釋放概率
            skillinfo.MaxHP = 80;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31109, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31109, skillinfo);//將這個技能加進進程技能表



            return ai;
        }
    }
}

