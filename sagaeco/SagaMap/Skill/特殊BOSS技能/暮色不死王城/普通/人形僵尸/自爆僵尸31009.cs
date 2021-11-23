using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Mob;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31009 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            SkillHandler.Instance.ActorSpeak(sActor, "出来吧，我的忠实的仆从们————");

            int count = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000).Count;
            if (count < 3) count = 3;
            if (count > 8) count = 8;

            var mobs = map.SpawnCustomMob(10000000, map.ID, 10690500, 0, 0, x, y, count, 3, 0, Info(sActor,count), AI(), null, 0);
            foreach (ActorMob itme in mobs)
            {
                sActor.Slave.Add(itme);
                SkillHandler.Instance.ShowEffectByActor(itme, 5043);
                List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(itme, 1000);
                if (actors.Count > 0)
                {
                    double len = 999999;
                    Actor da = null;
                    foreach (Actor a in actors)
                    {
                        double len1 = MobAI.GetLengthD(itme.X, itme.Y, a.X, a.Y);
                        if (len1 < len)
                        {
                            len = len1;
                            da = a;
                        }
                    }
                    if (da != null)
                    {
                        if (!((ActorEventHandlers.MobEventHandler)itme.e).AI.Hate.ContainsKey(da.ActorID))
                            ((ActorEventHandlers.MobEventHandler)itme.e).AI.Hate.Add(da.ActorID, 99999);
                    }
                }
            }
        }
        ActorMob.MobInfo Info(Actor Boss,int count)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "自爆僵尸！！";
            info.maxhp = (uint)(Boss.MaxHP * 0.05f);

            switch (count)
            {
                case 1:
                    info.maxhp = (uint)(info.maxhp * 0.6f);
                    info.atk_min = (ushort)(info.atk_min * 0.6f);
                    info.atk_max = (ushort)(info.atk_max * 0.6f);
                    info.matk_min = (ushort)(info.matk_min * 0.6f);
                    info.matk_max = (ushort)(info.matk_max * 0.6f);
                    break;
                case 2:
                    info.maxhp = (uint)(info.maxhp * 0.7f);
                    info.atk_min = (ushort)(info.atk_min * 0.8f);
                    info.atk_max = (ushort)(info.atk_max * 0.8f);
                    info.matk_min = (ushort)(info.matk_min * 0.8f);
                    info.matk_max = (ushort)(info.matk_max * 0.8f);
                    break;
                case 3:
                    info.maxhp = (uint)(info.maxhp * 0.8f);
                    info.atk_min = (ushort)(info.atk_min * 0.9f);
                    info.atk_max = (ushort)(info.atk_max * 0.9f);
                    info.matk_min = (ushort)(info.matk_min * 0.9f);
                    info.matk_max = (ushort)(info.matk_max * 0.69);
                    break;
                case 4:
                    info.maxhp = (uint)(info.maxhp * 1f);
                    info.atk_min = (ushort)(info.atk_min * 1f);
                    info.atk_max = (ushort)(info.atk_max * 1f);
                    info.matk_min = (ushort)(info.matk_min * 1f);
                    info.matk_max = (ushort)(info.matk_max * 1f);
                    break;
                case 5:
                    info.maxhp = (uint)(info.maxhp * 1.3f);
                    info.atk_min = (ushort)(info.atk_min * 1.05f);
                    info.atk_max = (ushort)(info.atk_max * 1.05f);
                    info.matk_min = (ushort)(info.matk_min * 1.05f);
                    info.matk_max = (ushort)(info.matk_max * 1.05f);
                    break;
                case 6:
                    info.maxhp = (uint)(info.maxhp * 1.65f);
                    info.atk_min = (ushort)(info.atk_min * 1.1f);
                    info.atk_max = (ushort)(info.atk_max * 1.1f);
                    info.matk_min = (ushort)(info.matk_min * 1.1f);
                    info.matk_max = (ushort)(info.matk_max * 1.1f);
                    break;
                case 7:
                    info.maxhp = (uint)(info.maxhp * 2.05f);
                    info.atk_min = (ushort)(info.atk_min * 1.15f);
                    info.atk_max = (ushort)(info.atk_max * 1.15f);
                    info.matk_min = (ushort)(info.matk_min * 1.15f);
                    info.matk_max = (ushort)(info.matk_max * 1.15f);
                    break;
                case 8:
                    info.maxhp = (uint)(info.maxhp * 2.55f);
                    info.atk_min = (ushort)(info.atk_min * 1.2f);
                    info.atk_max = (ushort)(info.atk_max * 1.2f);
                    info.matk_min = (ushort)(info.matk_min * 1.2f);
                    info.matk_max = (ushort)(info.matk_max * 1.2f);
                    break;
            }
            info.speed = 390;
            info.atk_min = (ushort)(Boss.Status.min_atk1 * 0.5f);
            info.atk_max = (ushort)(Boss.Status.max_atk1 * 0.5f); ;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 21;
            info.def_add = 121;
            info.mdef = 21;
            info.mdef_add = 118;
            info.hit_critical = 23;
            info.hit_magic = 118;
            info.hit_melee = 118;
            info.hit_ranged = 120;
            info.avoid_critical = 24;
            info.avoid_magic = 59;
            info.avoid_melee = 60;
            info.avoid_ranged = 60;
            info.Aspd = 540;
            info.Cspd = 540;
            info.elements[SagaLib.Elements.Neutral] = 0;
            info.elements[SagaLib.Elements.Fire] = 50;
            info.elements[SagaLib.Elements.Water] = 0;
            info.elements[SagaLib.Elements.Wind] = 0;
            info.elements[SagaLib.Elements.Earth] = 0;
            info.elements[SagaLib.Elements.Holy] = 40;
            info.elements[SagaLib.Elements.Dark] = 0;
            info.abnormalstatus[SagaLib.AbnormalStatus.Confused] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Frosen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Paralyse] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Poisen] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Silence] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Sleep] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stone] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.Stun] = 30;
            info.abnormalstatus[SagaLib.AbnormalStatus.鈍足] = 30;
            info.baseExp = 100;
            info.jobExp = 100;



            MobData.DropData newDrop = new MobData.DropData();
            /*---------物理掉落---------*/
            /*---------物理掉落---------*/

            return info;
        }

        AIMode AI()
        {
            AIMode ai = new AIMode(1);//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10111302;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 1;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();

            /*---------自爆---------*/
            skillinfo.CD = 3;//技能CD
            skillinfo.Rate = 100;//釋放概率
            skillinfo.MaxHP = 100;//低於100%血量的時候才會釋放
            skillinfo.MinHP = 0;//高於0%血量的時候才會釋放
            skillinfo.OverTime = 30;
            ai.SkillOfShort.Add(31005, skillinfo);//將這個技能加進進程技能表
            ai.SkillOfLong.Add(31005, skillinfo);//將這個技能加進進程技能表
            return ai;
        }
    }
}
