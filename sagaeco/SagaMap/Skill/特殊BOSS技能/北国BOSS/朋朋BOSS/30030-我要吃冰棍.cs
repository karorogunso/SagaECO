using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;
using SagaMap.Scripting;
using SagaMap.ActorEventHandlers;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30030 : ISkill
    {
        List<ActorMob> actorMobs = new List<ActorMob>();
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            byte x = SagaLib.Global.PosX16to8(sActor.X, map.Width);
            byte y = SagaLib.Global.PosY16to8(sActor.Y, map.Height);
            for (int i = 0; i < 3; i++)
            {
                ActorMob mob = map.SpawnCustomMob(30030001, sActor.MapID, x, y, 8, 1, 0, 冰棍Info(), 冰棍AI(), (MobCallback)Ondie, 1)[0];
                actorMobs.Add(mob);
                SkillHandler.Instance.ShowEffectByActor(mob, 4023);
            }
            Activator timer = new Activator(sActor, args, actorMobs);
            timer.Activate();
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;

            List<ActorMob> actorMobs;
            public Activator(Actor caster, SkillArg args, List<ActorMob> actorMobs)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 0;
                this.dueTime = 20000;
                this.actorMobs = actorMobs;
                SkillHandler.Instance.ActorSpeak(caster, "这是朋朋珍藏的冰棍，谁也不给吃。");
            }
            public override void CallBack()
            {
                byte count = 0;
                foreach (var i in actorMobs)
                {
                    SkillHandler.Instance.ShowEffect(map, i, SagaLib.Global.PosX16to8(i.X, map.Width), SagaLib.Global.PosY16to8(i.Y, map.Height), 4086);
                    map.DeleteActor(i);
                    count++;
                }
                if(count == 0)
                    SkillHandler.Instance.ActorSpeak(caster, "啊啊，冰棍都被糟蹋了，朋朋好气呀！");
                else
                    SkillHandler.Instance.ActorSpeak(caster, "冰棍好好吃，朋朋最喜欢吃冰棍了~");
                if (caster.HP > 0)
                {
                    int heal = (int)(caster.MaxHP * 0.05f * count);
                    SkillHandler.Instance.CauseDamage(caster, caster, -heal);
                    SkillHandler.Instance.ShowVessel(caster, -heal);
                    if (caster.HP > caster.MaxHP)
                        caster.HP = caster.MaxHP;
                    if (caster.Status.Additions.ContainsKey("朋朋BUFF冰棍提升"))
                    {
                        DefaultBuff ibuff = (DefaultBuff)caster.Status.Additions["朋朋BUFF冰棍提升"];
                        ibuff.AdditionEnd();
                    }
                    DefaultBuff buff = new DefaultBuff(this.skill.skill, caster, "朋朋BUFF冰棍提升", 30000, 0);
                    buff.OnAdditionStart += this.StartEventHandler;
                    buff.OnAdditionEnd += this.EndEventHandler;
                    SkillHandler.ApplyAddition(caster, buff);
                }
                actorMobs.Clear();
                this.Deactivate();
            }
            void StartEventHandler(Actor actor, DefaultBuff skill)
            {
                short atk1 = (short)(actor.Status.max_matk * 1f);
                if (skill.Variable.ContainsKey("朋朋BUFFATKUp"))
                    skill.Variable.Remove("朋朋BUFFATKUp");
                skill.Variable.Add("朋朋BUFFATKUp", atk1);

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
                if (skill.Variable.ContainsKey("朋朋BUFFATKUp"))
                {
                    short atk1 = (short)skill.Variable["朋朋BUFFATKUp"];
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
        #region 冰棍死亡事件，会给玩家加debuff
        void Ondie(MobEventHandler e, ActorPC pc)
        {
            actorMobs.Remove(e.mob);
            Actor sActor = e.mob;
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 300);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4353);
            foreach (var i in actors)
            {
                if (i.type == ActorType.PC)
                    Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("受到了冰棍的寒气..");
                if (i.Status.Additions.ContainsKey("冰棍的冻结"))
                    continue;
                if (i.Status.Additions.ContainsKey("冰棍"))
                {
                    冰棍 ibuff = (冰棍)i.Status.Additions["冰棍"];
                    if (i.Plies == 1)
                    {
                        冰棍 buff = new 冰棍(null, i, 30000, 2);
                        SkillHandler.ApplyAddition(i, buff);
                    }
                    else
                    {
                        ibuff.AdditionEnd();
                        冰棍的冻结 buff = new 冰棍的冻结(null, i, 30000);
                        SkillHandler.ApplyAddition(i, buff);
                    }
                }
                else
                {
                    冰棍 buff = new 冰棍(null, i, 30000, 1);
                    SkillHandler.ApplyAddition(i, buff);
                }
            }
        }
        #endregion
        #region 冰棍属性
        ActorMob.MobInfo 冰棍Info()
        {
            ActorMob.MobInfo info = new ActorMob.MobInfo();
            info.name = "冰棍";
            info.maxhp = 5000;
            info.speed = 500;
            info.atk_min = 1;
            info.atk_max = 1;
            info.matk_min = 1;
            info.matk_max = 1;
            info.def = 20;
            info.def_add = 20;
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
            info.elements[SagaLib.Elements.Fire] = 0;
            info.elements[SagaLib.Elements.Water] = 50;
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


            /*---------物理掉落---------*/
            /*newDrop.ItemID = 10009000;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率,10000是100%，5000是50%
            info.dropItems.Add(newDrop);

            newDrop.ItemID = 10000104;//掉落物品ID
            newDrop.Rate = 5000;//掉落幾率
            info.dropItems.Add(newDrop);*/
            /*---------物理掉落---------*/

            return info;
        }

        AIMode 冰棍AI()
        {
            AIMode ai = new AIMode();//1為主動，0為被動
            ai.AI = 6;
            ai.MobID = 30030001;//怪物ID
            ai.EventAttackingSkillRate = 0;
            ai.isNewAI = true;
            ai.Distance = 3;
            ai.ShortCD = 3;
            ai.LongCD = 3;
            AIMode.SkilInfo skillinfo = new AIMode.SkilInfo();
            return ai;//*/
        }
        #endregion
    }
}
