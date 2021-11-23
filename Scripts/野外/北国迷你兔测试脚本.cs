
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.ActorEventHandlers;
namespace WeeklyExploration
{
    public class S3235125153 : Event
    {
        public S3235125153()
        {
            this.EventID = 3235125153;
        }
        public override void OnEvent(ActorPC pc)
        {
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, pc.MapID, 16200000, 0, 0, 0, SagaLib.Global.PosX16to8(pc.X, map.Width), SagaLib.Global.PosY16to8(pc.Y, map.Height), 1, 1, 0, 迷你兔Info(), 迷你兔AI(), null, 0)[0];
            //格式: 怪物ID 地圖ID X Y 範圍 數量 復活時間(秒) Info AI
            ((MobEventHandler)mob.e).Defending += S3235125153_Defending;
            ((MobEventHandler)mob.e).Dying += (s, e) =>
            {
                if (mob.HP < 50 && mob.AttackedForEvent != 7)
                {
                    List<Actor> actors = map.GetActorsArea(mob, 5000, false);
                    mob.AttackedForEvent = 7;
                    foreach (var item in actors)
                    {
                        if (item.type == ActorType.PC)
                        {
                            ActorPC m = (ActorPC)item;
                            if (m.Online && m != null)
                            {
                                if (m.Buff.Dead)
                                    SagaMap.Network.Client.MapClient.FromActorPC(m).RevivePC(m);
                                string s2 = "玩家 " + m.Name + " 在本次战斗总共造成伤害：" + m.TInt["伤害统计"].ToString() + " 受到伤害：" + m.TInt["受伤害统计"].ToString() +
                                       " 共治疗：" + m.TInt["治疗统计"].ToString() + " 共受到治疗：" + m.TInt["受治疗统计"].ToString();
                                foreach (var item2 in actors)
                                {
                                    if (item2.type == ActorType.PC)
                                    {
                                        ActorPC m2 = (ActorPC)item2;
                                        if (m2.Online && m2 != null)
                                            SagaMap.Network.Client.MapClient.FromActorPC(m2).SendSystemMessage(s2);
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private void S3235125153_Defending(MobEventHandler eh, ActorPC pc)
        {

            if (eh.mob.AttackedForEvent == 0)
            {
                ActorMob mob = eh.mob;
                SkillHandler.Instance.ActorSpeak(mob, "大家好，在下乃北国大侦探，朋朋是也，大家可不能受某个红发的奇怪家伙蛊惑而开始互相残杀哟！");
                eh.mob.AttackedForEvent = 1;
            }
            else if (eh.mob.AttackedForEvent == 1)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.95f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "不是说了要友好相处嘛！((o(>口<)o)) !!");
                    eh.mob.AttackedForEvent = 2;
                }
            }
            else if (eh.mob.AttackedForEvent == 2)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.8f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "嘶！这是什么..？刀？血迹？莎..莎仁辣！！");
                    eh.mob.AttackedForEvent = 3;
                }
            }
            else if (eh.mob.AttackedForEvent == 3)
            {
                ActorMob mob = eh.mob;
                if (mob.HP < mob.MaxHP * 0.8f)
                {
                    SkillHandler.Instance.ActorSpeak(mob, "呜..可恶，朋朋要认真了！朋朋是不会轻易倒下的。");
                    eh.mob.AttackedForEvent = 4;
                }
            }
        }

        ActorMob.MobInfo 迷你兔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.maxhp = 25240010;//血量
            info.name = "北国领主·朋朋";
            info.range = 3;
            info.speed = 650;//移動速度
            info.atk_min = 800;//最低物理攻擊
            info.atk_max = 1400;//最高物理攻擊
            info.matk_min = 400;//最低魔法攻擊
            info.matk_max = 500;//最高物理攻擊
            info.def = 35;//物理左防
            info.mdef = 35;//魔法左防
            info.def_add = 250;//物理右防
            info.mdef_add = 250;//魔法右防
            info.hit_critical = 80;//暴擊值
            info.hit_magic = 500;//魔法命中值（目前沒用
            info.hit_melee = 500;//近戰命中值
            info.hit_ranged = 500;//遠程命中值
            info.avoid_critical = 30;//暴擊閃避值
            info.avoid_magic = 30;//魔法閃避值
            info.avoid_melee = 30;//近戰閃避值
            info.avoid_ranged = 30;//遠程閃避值
            info.Aspd = 400;//攻速
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
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 50;//冰抗性
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
            newDrop.ItemID = 910000041;//掉落物品ID
            newDrop.Rate = 10000;//掉落幾率,10000是100%，5000是50%
            newDrop.Public = true;
            info.dropItems.Add(newDrop);

            /*---------物理掉落---------*/

            return info;
        }
        AIMode 迷你兔AI()
        {
            AIMode ai = new AIMode(33); ai.MobID = 10000000; ai.isNewAI = true;//1為主動，0為被動
            ai.MobID = 10000000;//怪物ID
            ai.isNewAI = true;//使用的是TT AI
            ai.Distance = 3;//遠程進程切換距離，與敵人3格距離切換
            ai.ShortCD = 7;//進程技能表最短釋放間隔，3秒一次
            ai.LongCD = 7;//遠程技能表最短釋放間隔，3秒一次
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();


            /*---------寒冰之夜--------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 110;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 90;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30026, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30026, skillinfo);//將這個技能加進進程技能表


            /*---------冰棍的制作方式---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 30;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30028, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30028, skillinfo);//將這個技能加進進程技能表


            /*---------来打雪仗吧---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 12;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30029, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30029, skillinfo);//將這個技能加進進程技能表

            /*---------我要吃冰棍---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 60;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 70;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30030, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30030, skillinfo);//將這個技能加進進程技能表

            /*---------快点下雪吧---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 65;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 99;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30031, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30031, skillinfo);//將這個技能加進進程技能表


            /*---------在雪山怎么能少得了雪崩---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 88;//技能CD
            skillinfo.Rate = 20;//釋放概率
            skillinfo.MaxHP = 95;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30032, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30032, skillinfo);//將這個技能加進進程技能表

            /*---------终结冻结---------*/
            skillinfo = new AIMode.SkilInfo();
            skillinfo.CD = 130;//技能CD
            skillinfo.Rate = 90;//釋放概率
            skillinfo.MaxHP = 97;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            ai.SkillOfShort.Add(30033, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(30033, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}

