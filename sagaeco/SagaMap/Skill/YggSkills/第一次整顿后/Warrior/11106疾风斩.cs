using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaMap.Skill.Additions.Global;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S11106 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Inventory.Equipments.ContainsKey(SagaDB.Item.EnumEquipSlot.RIGHT_HAND)) return -5;
            //if(!pc.Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand) return -5;
            if (pc.Status.Additions.ContainsKey("疾风斩CD")) return -30;
            if (pc.MapID == 20020000 || pc.MapID == 20021000 || pc.MapID == 20022000)
            {
                SkillHandler.SendSystemMessage(pc, "受到神秘的力量，无法在该地区使用这个技能。");
                return -30;
            }
            if (pc.AInt["接受了搬运任务"] == 1)
            {
                SkillHandler.SendSystemMessage(pc, "由于接受了搬运任务，你无法使用该技能。");
                return -30;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args, dActor);
            timer.Activate();


            int lefttime = 7000 + 3000 * level;

            /*if (sActor.type == ActorType.PC)
            {
                if (((ActorPC)sActor).Inventory.Equipments[SagaDB.Item.EnumEquipSlot.RIGHT_HAND].BaseData.doubleHand)
                {
                    lefttime *= 2;
                }
            }*/

            byte rate = 1;
            List<Actor> actors = map.GetActorsArea(sActor, 200, false);
            foreach (var item in actors)
            {
                if (item.type == ActorType.SKILL && item.Name == "心眼技能体" )
                {
                    //Addition 心眼技能 = sActor.Status.Additions["心眼持续时间"];
                    //SkillHandler.RemoveAddition(sActor, "心眼持续时间");
                    if (map.GetActor(item.ActorID) != null)
                        map.DeleteActor(item);
                    rate = 10;
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 7949);
                    break;
                }
            }

            OtherAddition 疾风斩伤害上升 = new OtherAddition(null, sActor, "疾风斩伤害上升", lefttime);
            疾风斩伤害上升.OnAdditionStart += (s, e) => {
                s.Buff.AtkMaxUp = true;
                if(rate == 10)
                s.Buff.三转2足ATKUP = true;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            疾风斩伤害上升.OnAdditionEnd += (s, e) => {
                s.Buff.AtkMaxUp = false;
                s.Buff.三转2足ATKUP = false;
                Manager.MapManager.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
            };
            SkillHandler.ApplyAddition(sActor, 疾风斩伤害上升);
            SkillHandler.Instance.ShowEffectOnActor(sActor, 4417);
            OtherAddition 疾风斩CD = new OtherAddition(null, sActor, "疾风斩CD", 20000);
            疾风斩CD.OnAdditionEnd += (s, e) => {
                if (sActor.type == ActorType.PC)
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4395);
                Network.Client.MapClient.FromActorPC((ActorPC)sActor).SendSystemMessage("『疾风斩』已准备就绪！");
            };
            SkillHandler.ApplyAddition(sActor, 疾风斩CD);

            SkillHandler.Instance.ShowEffectOnActor(sActor, 4417);
            if (!sActor.Status.Additions.ContainsKey("疾风斩移动速度UP"))
            {
                OtherAddition 疾风斩移动速度UP = new OtherAddition(null, sActor, "疾风斩移动速度UP", 5000);
                疾风斩移动速度UP.OnAdditionStart += (s, e) =>
                {
                    sActor.Speed = 1100;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SPEED_UPDATE, null, sActor, true);
                    sActor.Buff.移動力上昇 = true;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                疾风斩移动速度UP.OnAdditionEnd += (s, e) =>
                {
                    sActor.Buff.移動力上昇 = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                };
                SkillHandler.ApplyAddition(sActor, 疾风斩移动速度UP);
            }
            else
            {
                Addition 疾风斩移动速度UP = sActor.Status.Additions["疾风斩移动速度UP"];
                TimeSpan time = new TimeSpan(0, 0, 0, 5);
                ((OtherAddition)疾风斩移动速度UP).endTime =  DateTime.Now + time;
            }
            SkillHandler.Instance.SetNextComboSkill(sActor, 11100);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11101);
            SkillHandler.Instance.SetNextComboSkill(sActor, 11102);
        }

        #endregion

        #region Timer

        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            Map map;
            public Activator(Actor caster, SkillArg args, Actor dactor)
            {
                this.caster = caster;
                this.skill = args;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 1;
                this.dueTime = 0;
                this.dActor = dactor;
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    Mob.MobAI ai = new MobAI(caster, true);
                    List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(caster.X, map.Width), SagaLib.Global.PosY16to8(caster.Y, map.Height),
                    SagaLib.Global.PosX16to8(dActor.X, map.Width), SagaLib.Global.PosY16to8(dActor.Y, map.Height));
                    if (path.Count <= 1)
                    {
                        Activator2 sc = new Activator2(caster, dActor, skill);
                        sc.Activate();
                        this.Deactivate();
                        return;

                    }
                    short[] pos = new short[2];
                    pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                    map.MoveActor(Map.MOVE_TYPE.START, caster, pos, caster.Dir, 1000, true, MoveType.BATTLE_MOTION);

                    EffectArg arg = new EffectArg();
                    arg.effectID = 5323;
                    arg.actorID = 0xFFFFFFFF;
                    arg.x = SagaLib.Global.PosX16to8(pos[0], map.Width);
                    arg.y = SagaLib.Global.PosY16to8(pos[1], map.Height);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, caster, true);


                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
                //解开同步锁
                //ClientManager.LeaveCriticalArea();
            }
        }

        //以下内容在上文被注释
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            Map map;
            SkillArg arg;
            int maxcount = 1;
            int count = 0;
            public Activator2(Actor caster, Actor dactor,SkillArg args)
            {
                this.caster = caster;
                this.dActor = dactor;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 0;
                this.arg = args;
            }
            public override void CallBack()
            {
                
                //测试去除技能同步锁ClientManager.EnterCriticalArea();
                try
                {
                    if (count < maxcount)
                    {
                        float factor = 2.6f + arg.skill.Level * 0.4f;

                        if (caster.Status.Additions.ContainsKey("刀剑宗师"))
                            factor += factor * caster.TInt["刀剑宗师提升%"] / 100f;


                            SkillHandler.Instance.DoDamage(true, caster, dActor, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
                        count++;
                    }
                    else
                        this.Deactivate();
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                    this.Deactivate();
                }
                //测试去除技能同步锁ClientManager.LeaveCriticalArea();
            }
        }
        #endregion
    }
}
