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
    public class S12101 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("影袭CD"))
                return -30;
            if(pc.MapID == 20020000 || pc.MapID == 20021000 || pc.MapID == 20022000)
            {
                SkillHandler.SendSystemMessage(pc, "受到神秘的力量，无法在该地区使用这个技能。");
                return -30;
            }
            if(pc.AInt["接受了搬运任务"] == 1)
            {
                SkillHandler.SendSystemMessage(pc, "由于接受了搬运任务，你无法使用该技能。");
                return -30;
            }
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            SkillHandler.Instance.ChangdeWeapons(sActor, 0);
            OtherAddition cd = new OtherAddition(null, sActor, "影袭CD", 15000);
            cd.OnAdditionEnd += (s, e) =>
            {
                if (sActor.type == ActorType.PC)
                {
                    SkillHandler.Instance.ShowEffectOnActor(sActor, 4006);
                    SkillHandler.SendSystemMessage(sActor, "『影袭』已准备就绪。");
                }
            };
            SkillHandler.ApplyAddition(sActor, cd);

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args, dActor);
            timer.Activate();

            Invisible inv = new Invisible(args.skill, sActor, 10000);
            SkillHandler.ApplyAddition(sActor, inv);

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
                ((OtherAddition)疾风斩移动速度UP).endTime = DateTime.Now + time;
            }
        }

        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dActor;
            SkillArg skill;
            Map map;
            public Activator(Actor caster, SkillArg args, Actor dactor)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 20;
                this.dueTime = 100;
                this.dActor = dactor;
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    Mob.MobAI ai = new MobAI(caster, true);
                    List<MapNode> path = ai.FindPath(SagaLib.Global.PosX16to8(caster.X, map.Width), SagaLib.Global.PosY16to8(caster.Y, map.Height),
                    skill.x, skill.y);

                    short[] pos = new short[2];
                    pos[0] = SagaLib.Global.PosX8to16(path[0].x, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(path[0].y, map.Height);
                    map.MoveActor(Map.MOVE_TYPE.START, caster, pos, caster.Dir, 1000, true, MoveType.BATTLE_MOTION);

                    if (path.Count <= 1)
                    {
                        List<Actor> targets = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(caster, 300);
                        foreach (var item in targets)
                        {
                            SkillHandler.Instance.DoDamage(true, caster, item, null, SkillHandler.DefType.Def, Elements.Dark, 0, 3f);
                            SkillHandler.Instance.ShowEffectOnActor(item, 8051);
                        }
                        this.Deactivate();
                        return;
                    }

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
    }
}
