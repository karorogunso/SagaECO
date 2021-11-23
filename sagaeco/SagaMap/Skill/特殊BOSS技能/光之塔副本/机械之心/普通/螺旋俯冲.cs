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
    public class S31027 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 2000);
            SkillHandler.Instance.ActorSpeak(sActor, "钻头，零件！");
            硬直 y = new 硬直(args.skill, sActor, 4000);
            SkillHandler.ApplyAddition(sActor, y);
            foreach (var item in actors)
            {
                short[] pos = new short[2];
                pos[0] = item.X;
                pos[1] = item.Y;
                Activator timer = new Activator(sActor, item, pos);
                timer.Activate();
                SkillHandler.Instance.ShowEffect(map, sActor, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5327);
            }
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Actor dactor;
            Map map;
            float rate;
            short[] pos;
            public Activator(Actor caster, Actor dActor, short[] pos)
            {
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 2500;
                this.dactor = dActor;
                this.pos = pos;
            }
            public override void CallBack()
            {
                try
                {
                    List<Actor> actors = map.GetActorsArea(pos[0],pos[1],200,false);
                    SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5345);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)(item.MaxHP * 0.75f);
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                        }
                    }

                    List<Actor> actors2 = map.GetActorsArea(pos[0], pos[1], 700, false);
                    foreach (var item in actors2)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = 300;
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5115);
                        }
                    }
                    if (caster.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)caster;
                        int count = actors.Count + actors2.Count;
                        mob.TInt["零件数"] += count;
                        SkillHandler.Instance.ShowVessel(mob, 0, -mob.TInt["零件数"], 0);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
                Deactivate();
            }
            #endregion
        }
    }
}
