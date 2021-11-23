
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaDB.Mob;
namespace WeeklyExploration
{
    public class S3235125152 : Event
    {
        public S3235125152()
        {
            this.EventID = 3235125152;
        }
        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            map.SpawnCustomMob(10000000, pc.MapID,16200000,0,0,SagaLib.Global.PosX16to8(pc.X, map.Width), SagaLib.Global.PosY16to8(pc.Y, map.Height), 1, 1, 0, 朋朋Info(), 朋朋AI(),null,0);
            //格式: 怪物ID 地圖ID X Y 範圍 數量 復活時間(秒) Info AI
        }
        ActorMob.MobInfo 朋朋Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 10000000;//血量
            info.name = "接受测试的朋朋";
            info.speed = 500;//移動速度
            info.atk_min = 500;//最低物理攻擊
            info.atk_max = 500;//最高物理攻擊
            info.matk_min = 500;//最低魔法攻擊
            info.matk_max = 500;//最高物理攻擊
            info.def = 50;//物理左防
            info.mdef = 50;//魔法左防
            info.def_add = 50;//物理右防
            info.mdef_add = 50;//魔法右防
            info.hit_critical = 80;//暴擊值
            info.hit_magic = 80;//魔法命中值（目前沒用
            info.hit_melee = 80;//近戰命中值
            info.hit_ranged = 80;//遠程命中值
            info.avoid_critical = 0;//暴擊閃避值
            info.avoid_magic = 0;//魔法閃避值
            info.avoid_melee = 0;//近戰閃避值
            info.avoid_ranged = 0;//遠程閃避值
            info.Aspd = 400;//攻速
            info.Cspd = 100;//唱速
            info.AttackType = SagaDB.Actor.ATTACK_TYPE.BLOW;//攻擊類型，打 刺 斬，一般可以不管
            info.elements[SagaLib.Elements.Fire] = 50;//火屬性
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
            newDrop.ItemID = 910000000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 朋朋AI()
        {
            AIMode ai = new AIMode(0); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 16200000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 10;
            ai.ShortCD = 5;
            ai.LongCD = 5;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------技能---------*/
            skillinfo.CD = 5;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30029, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30030, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30031, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30032, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

