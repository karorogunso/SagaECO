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
    public class S40004 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            SkillHandler.Instance.ShowEffect(map, sActor, 5131);
            Activator timer = new Activator(sActor, args);
            timer.Activate();
            硬直 y = new 硬直(args.skill, sActor, 2000);
            SkillHandler.ApplyAddition(sActor, y);
            sActor.EP += 1000;
            if (sActor.EP >= sActor.MaxEP)
                sActor.EP = sActor.MaxEP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, sActor, true);
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            SkillArg skill;
            Map map;
            List<Actor> dactors = new List<Actor>();
            int countMax = 20, count = 0;
            public Activator(Actor caster, SkillArg args)
            {
                this.caster = caster;
                this.skill = args.Clone();
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                this.period = 100;
                this.dueTime = 100;

                List<Actor> targets = map.GetActorsArea(caster, 600, true);
                foreach (var item in targets)
                {
                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                    {
                        dactors.Add(item);
                    }
                }
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (count < countMax)
                    {
                        Actor i = dactors[SagaLib.Global.Random.Next(0, dactors.Count - 1)];
                        if (i == null) return;
                        //map.TeleportActor(caster,i.X, i.Y);
                        SkillHandler.Instance.ShowEffect(map, i, 5194);
                        SkillHandler.AttackResult res = SkillHandler.AttackResult.Hit;
                        int damaga = SkillHandler.Instance.CalcDamage(true, caster, i, skill, SkillHandler.DefType.Def, Elements.Neutral, 50, 3.0f, out res);
                        SkillHandler.Instance.CauseDamage(caster, i, damaga);
                        SkillHandler.Instance.ShowVessel(i, damaga, 0, 0, res);
                        count++;
                    }
                    else
                    {
                        this.Deactivate();
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
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }

}
