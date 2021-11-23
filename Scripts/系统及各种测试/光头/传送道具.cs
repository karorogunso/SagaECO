
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
    public class S910000031 : Event
    {
        public S910000031()
        {
            this.EventID = 910000031;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (Select(pc, "刷吗？", "", "刷刷刷", "不") == 1)
            {
                Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
                ActorMob mob;
                mob = map.SpawnCustomMob(10000000, map.ID, 18630000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 3, 1, 0, 老二Info困难(pc.Name), 老二AI困难(), null, 0)[0];
                mob.TInt["playersize"] = 1200;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mob, false);
                //Announce("虽然强化失败后不能降级了，但敲碎你们的强化石，是我的爱好——！我依然还是——万碎爷！！！" + pc.Name + "在 牛牛草原 放出了 万碎爷");
                //虽然强化失败后不能降级了，但敲碎你们的强化石，是我的爱好——！我依然还是——万碎爷！！！羽川柠 在 牛牛草原 放出了 万碎爷
            }
        }

        ActorMob.MobInfo 老二Info困难(string name)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 5545925;//血量 
            info.name = name+"放出的万碎爷";
            info.speed = 650;//移動速度
            info.atk_min = 600;//最低物理攻擊
            info.atk_max = 800;//最高物理攻擊
            info.matk_min = 500;//最低魔法攻擊
            info.matk_max = 800;//最高物理攻擊
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
            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            newDrop.ItemID = 910000026;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;//1%-20%掉落
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/
            newDrop = new MobData.DropData();
            newDrop.ItemID = 910000026;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public20 = true;//20%以上掉落
            info.dropItems.Add(newDrop);
            /*---------物理掉落---------*/

            return info;
        }
        AIMode 老二AI困难()
        {
            AIMode ai = new AIMode(1);
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 7;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 7;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------螺旋俯冲---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 85;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31027, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31027, skillinfo);//將這個技能加進進程技能表


            /*---------百万坠击---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 42;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 88;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(31028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31028, skillinfo);//將這個技能加進進程技能表

            /*---------扭曲射线---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 52;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 77;//低於100%血量的時候才會釋放
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

