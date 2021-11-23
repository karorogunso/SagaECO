using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31002
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

            foreach (var item in sActor.Slave)
            {
                //sActor.Slave.Remove(item);
                item.ClearTaskAddition();
                map.DeleteActor(item);
            }
            sActor.Slave.Clear();

            ActorMob mob = map.SpawnCustomMob(10110100, sActor.MapID, x, y, 5, 1, 0, 樱桃兔Info(), 樱桃兔AI())[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            mob = map.SpawnCustomMob(10110400, sActor.MapID, x, y, 5, 1, 0, 椰子兔Info(), 椰子兔AI())[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
            //((ActorEventHandlers.MobEventHandler)mob.e).AI.Start();
            mob.Owner = sActor;
            sActor.Slave.Add(mob);
            SkillHandler.Instance.ShowEffectByActor(mob, 4111);

            mob = map.SpawnCustomMob(10111000, sActor.MapID, x, y, 5, 1, 0, 荔枝兔Info(), 荔枝兔AI())[0];
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Master = sActor;
            ((ActorEventHandlers.MobEventHandler)mob.e).AI.Hate = ((ActorEventHandlers.MobEventHandler)sActor.e).AI.Hate;
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
                this.period = 30000;
                this.dueTime = 10000;
            }
            public override void CallBack()
            {
                if(caster.HP == 0 || caster.Slave.Count == 0)
                {
                    this.Deactivate();
                    return;
                }
                if (caster.Slave.Count > 0)
                {
                    int hpheal = (int)((0.05f * caster.Slave.Count) * caster.MaxHP);
                    if (caster.HP + hpheal > caster.MaxHP)
                    {
                        hpheal = (int)(caster.MaxHP - caster.HP);
                    }
                    SkillHandler.Instance.ActorSpeak(caster, "加油有萝卜哦");

                    SkillHandler.Instance.CauseDamage(caster, caster, -hpheal);
                    SkillHandler.Instance.ShowVessel(caster, -hpheal);
                    foreach (var item in caster.Slave)
                    {
                        hpheal = (int)(item.MaxHP - item.HP);
                        SkillHandler.Instance.CauseDamage(item, item, -hpheal);
                        SkillHandler.Instance.ShowVessel(item, -hpheal);

                        SkillHandler.Instance.ActorSpeak(item, "有萝卜!!!");
                        DefaultBuff buff = new DefaultBuff(skill.skill, item, "加油有萝卜哦", 300000);
                        buff.OnAdditionStart += this.StartEventHandler;
                        buff.OnAdditionEnd += this.EndEventHandler;
                        SkillHandler.ApplyAddition(item, buff);
                    }
                }
                //this.Deactivate();
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
        ActorMob.MobInfo 樱桃兔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "樱桃兔";
            info.maxhp = 30000;
            info.speed = 500;
            info.atk_min = 300;
            info.atk_max = 900;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 10;
            info.def_add = 10;
            info.mdef = 10;
            info.mdef_add = 10;
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

            return info;
        }

        AIMode 樱桃兔AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 1;
            ai.MobID = 10110100;//怪物ID
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;
        }
        #endregion
        #region 椰子兔属性
        ActorMob.MobInfo 椰子兔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "椰子兔";
            info.maxhp = 20000;
            info.speed = 500;
            info.atk_min = 300;
            info.atk_max = 900;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 1;
            info.def_add = 1;
            info.mdef = 1;
            info.mdef_add = 1;
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

            return info;
        }

        AIMode 椰子兔AI()
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
        ActorMob.MobInfo 荔枝兔Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "荔枝兔";
            info.maxhp = 20000;
            info.speed = 500;
            info.atk_min = 300;
            info.atk_max = 600;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 1;
            info.def_add = 1;
            info.mdef = 1;
            info.mdef_add = 1;
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

            return info;
        }

        AIMode 荔枝兔AI()
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
