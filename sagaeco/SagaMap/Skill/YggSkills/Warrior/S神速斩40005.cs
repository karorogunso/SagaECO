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
    public class S40005 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            Activator timer = new Activator(sActor, args, dActor);
            timer.Activate();
            sActor.EP += 1000;
            if (sActor.EP >= sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
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
                this.period = 90;
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
            int maxcount = 10;
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
                        float factor = 3.1f;

                        if (dActor.HP > 0 && !dActor.Buff.Dead)
                        {
                            SkillHandler.Instance.DoDamage(true, caster, dActor, arg, SkillHandler.DefType.Def, Elements.Neutral, 0, factor);
                            SkillHandler.Instance.ShowEffect(SagaMap.Manager.MapManager.Instance.GetMap(dActor.MapID), dActor, 8041);
                        }
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
