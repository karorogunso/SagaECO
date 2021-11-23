
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
using SagaMap.Skill;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public class S323512515 : Event
    {
        public S323512515()
        {
            this.EventID = 323512515;
        }
        public override void OnEvent(ActorPC pc)
        {
            string input = "49510055";
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob =WeeklySpawn.Instance.SpawnMob(uint.Parse(input), pc.MapID, SagaLib.Global.PosX16to8(pc.X, map.Width), SagaLib.Global.PosY16to8(pc.Y, map.Height), 1, 1, 0, 低级步行龙Info(), 低级步行龙AI(), (MobCallback)BOSS1死亡Ondie,1)[0];
            ((MobEventHandler)mob.e).Dying += BOSS1死亡Ondie;
           ((MobEventHandler)mob.e).Defending += SQuest_Defending;
            ((MobEventHandler)mob.e).Returning += SQuest_Returning;
        }
        void BOSS1死亡Ondie(MobEventHandler e, ActorPC pc)
        {
            ActorMob mob = e.mob;
            SkillHandler.Instance.ActorSpeak(mob, "姐...姐姐大人....");
            SagaMap.Map map; map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            if (pc.Party != null)
            {
                foreach (var item in pc.Party.Members)
                {
                    if (item.Value.Online)
                    {
                        if (item.Value.Buff.Dead)
                        {
                            SagaMap.Network.Client.MapClient.FromActorPC(item.Value).RevivePC(item.Value);
                        }
                        string s = "队友" + item.Value.Name + " 在本次副本总共造成伤害：" + item.Value.TInt["伤害统计"].ToString() + " 受到伤害：" + item.Value.TInt["受伤害统计"].ToString() +
                            " 共治疗：" + item.Value.TInt["治疗统计"].ToString() + " 共受到治疗：" + item.Value.TInt["受治疗统计"].ToString() + " 总死亡次数：" + item.Value.TInt["死亡统计"];
                        foreach (var item2 in pc.Party.Members)
                        {
                            if (item2.Value.Online)
                                SagaMap.Network.Client.MapClient.FromActorPC(item2.Value).SendSystemMessage(s);
                        }
                    }
                }
            }
        }
        private void SQuest_Returning(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent != 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "请慢走~！");
            }
        }

        private void SQuest_Defending(MobEventHandler eh, ActorPC pc)
        {
            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "原来是你们这群家伙！");
                eh.mob.AttackedForEvent = 1;
            }
            else if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.5f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "呃...好痛啊，你们这群家伙，本小姐可饶不了你们！");
                    eh.mob.AttackedForEvent = 2;
                }
            }
            else if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.25f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "什么！？你、你们这群杂碎认错人了！本小姐才不是什么番茄！或是什么羽川柠！...");
                    eh.mob.AttackedForEvent = 3;
                }
            }
            else if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.1f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "不、不行..我不能倒下..沙月...");
                    eh.mob.AttackedForEvent = 4;
                }
            }
        }
        ActorMob.MobInfo 低级步行龙Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 300000;//血量
            info.name = "低级步行龙";
            info.speed = 900;//移動速度
            info.atk_min = 10;//最低物理攻擊
            info.atk_max = 10;//最高物理攻擊
            info.matk_min = 1000;//最低魔法攻擊
            info.matk_max = 1000;//最高物理攻擊
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
        AIMode 低级步行龙AI()
        {
            AIMode ai = new AIMode(8);//1為主動，0為被動
            ai.MobID = 49510055;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 20;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 20;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------居合斬---------*/
            skillinfo.CD = 15;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30022, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30023, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30024, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30025, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfShort.Add(30026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30020, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30021, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30022, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30023, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30024, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30026, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

