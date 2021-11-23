using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31160
        : ISkill
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
            Actor[] s = new Actor[sActor.Slave.Count];
            sActor.Slave.CopyTo(s);

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].HP > 0)
                    SkillHandler.Instance.ShowEffectByActor(s[i], 4310);
                ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)s[i].e;
                s[i].Buff.死んだふり = true;
                eh.OnDie(false);
                map.DeleteActor(s[i]);
            }

            ActorMob mob = map.SpawnCustomMob(10000000, sActor.MapID, 20390051, 0, 0, x, y, 5, 1, 0, 鱼人妹妹Info(sActor), 鱼人妹妹AI(), null, 0)[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            mob = map.SpawnCustomMob(10000000, sActor.MapID, 18520000,0,0, x, y, 5, 1, 0, 傘狸Info(sActor), 傘狸AI(), null, 0)[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Start();
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            mob = map.SpawnCustomMob(10000000, sActor.MapID, 18450200, 0, 0, x, y, 5, 1, 0, 可怕的鲨鱼Info(sActor), 可怕的鲨鱼AI(), null, 0)[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Start();
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            Activator timer = new Activator(sActor, args);
            timer.Activate();

            MultiRunTask mrt = null;
            if (sActor.Tasks.TryGetValue("我的守护骑士们", out mrt))
            {
                mrt.Deactivate();
            }
            sActor.Tasks["我的守护骑士们"] = timer;
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 20000;
                this.dueTime = 20000;
            }
            public override void CallBack()
            {
                this.Deactivate();
                SkillHandler.Instance.ActorSpeak(caster, "玩得开心吗？！！");
                if (caster.Slave.Count > 0)
                {
                    int hpheal = (int)((0.05f * caster.Slave.Count) * caster.MaxHP);
                    if (caster.HP + hpheal > caster.MaxHP)
                    {
                        hpheal = (int)(caster.MaxHP - caster.HP);
                    }
                  

                    SkillHandler.Instance.CauseDamage(caster, caster, -hpheal);
                    SkillHandler.Instance.ShowVessel(caster, -hpheal);
                    foreach (var item in caster.Slave)
                    {
                        hpheal = (int)(item.MaxHP - item.HP);
                        SkillHandler.Instance.CauseDamage(item, item, -hpheal);
                        SkillHandler.Instance.ShowVessel(item, -hpheal);

                        SkillHandler.Instance.ActorSpeak(item, "好开心呀！！");

                        DefaultBuff buff = new DefaultBuff(skill.skill, item, "加油有萝卜哦", 300000);
                        buff.OnAdditionStart += this.StartEventHandler;
                        buff.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(item, buff);
                    }
                }
                //
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                short atk1 = (short)(actor.Status.max_atk1 * 10);
                if (skill.Variable.ContainsKey("加油有萝卜哦ATKUp"))
                    skill.Variable.Remove("加油有萝卜哦ATKUp");
                skill.Variable.Add("加油有萝卜哦ATKUp", atk1);


                actor.Status.max_atk1_skill += atk1;
                actor.Status.max_atk2_skill += atk1;
                actor.Status.max_atk3_skill += atk1;
                actor.Status.max_matk_skill += atk1;
                actor.Status.min_atk1_skill += atk1;
                actor.Status.min_atk2_skill += atk1;
                actor.Status.min_atk3_skill += atk1;
                actor.Status.min_matk_skill += atk1;

                actor.Buff.AtkMaxUp = true;
                actor.Buff.MAtkMaxUp = true;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            }
            void EndEventHandler(Actor actor, DefaultBuff skill)
            {
                if (skill.Variable.ContainsKey("加油有萝卜哦ATKUp"))
                {
                    short atk1 = (short)skill.Variable["加油有萝卜哦ATKUp"];
                    actor.Status.max_atk1_skill -= atk1;
                    actor.Status.max_atk2_skill -= atk1;
                    actor.Status.max_atk3_skill -= atk1;
                    actor.Status.max_matk_skill -= atk1;
                    actor.Status.min_atk1_skill -= atk1;
                    actor.Status.min_atk2_skill -= atk1;
                    actor.Status.min_atk3_skill -= atk1;
                    actor.Status.min_matk_skill -= atk1;
                }
                actor.Buff.AtkMaxUp = false;
                actor.Buff.MAtkMaxUp = false;
                Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);

            }
        }


        #region 樱桃兔属性
        ActorMob.MobInfo 鱼人妹妹Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo()
            {
                name = "鱼人妹妹",
                maxhp = (uint)(boss.MaxHP * 0.025f),
                speed = 500,
                atk_min = 300,
                atk_max = 900,
                matk_min = 300,
                range = 2,
                matk_max = 500,
                def = 10,
                def_add = 10,
                mdef = 10,
                mdef_add = 10,
                hit_critical = 23,
                hit_magic = 118,
                hit_melee = 118,
                hit_ranged = 120,
                avoid_critical = 24,
                avoid_magic = 59,
                avoid_melee = 60,
                avoid_ranged = 60,
                Aspd = 540,
                Cspd = 540
            };
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

            return info;
        }

        AIMode 鱼人妹妹AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10110100;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            return ai;
        }
        #endregion
        #region 椰子兔属性
        ActorMob.MobInfo 傘狸Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo()
            {
                name = "傘狸",
                maxhp = (uint)(boss.MaxHP * 0.02f),
                speed = 500,
                atk_min = 1000,
                atk_max = 1500,
                matk_min = 1,
                matk_max = 1,
                range = 2,
                def = 1,
                def_add = 1,
                mdef = 1,
                mdef_add = 1,
                hit_critical = 23,
                hit_magic = 118,
                hit_melee = 118,
                hit_ranged = 120,
                avoid_critical = 24,
                avoid_magic = 59,
                avoid_melee = 60,
                avoid_ranged = 60,
                Aspd = 540,
                Cspd = 540
            };
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

            return info;
        }

        AIMode 傘狸AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10110400;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;
        }
        #endregion
        #region 荔枝兔属性
        ActorMob.MobInfo 可怕的鲨鱼Info(Actor boss)
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo()
            {
                name = "可怕的鲨鱼",
                maxhp = (uint)(boss.MaxHP * 0.01f),
                speed = 500,
                atk_min = 1000,
                atk_max = 2000,
                range = 2,
                matk_min = 1,
                matk_max = 1,
                def = 1,
                def_add = 1,
                mdef = 1,
                mdef_add = 1,
                hit_critical = 23,
                hit_magic = 118,
                hit_melee = 118,
                hit_ranged = 120,
                avoid_critical = 24,
                avoid_magic = 59,
                avoid_melee = 60,
                avoid_ranged = 60,
                Aspd = 540,
                Cspd = 540
            };
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

            return info;
        }

        AIMode 可怕的鲨鱼AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10111000;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;
        }
        #endregion
    }
}
