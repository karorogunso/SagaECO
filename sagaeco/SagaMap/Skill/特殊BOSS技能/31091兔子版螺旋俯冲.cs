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
    public class S31091 : ISkill
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
                dueTime = 4000;
                this.dactor = dActor;
                this.pos = pos;
            }
            public override void CallBack()
            {
                try
                {
                    List<Actor> actors = map.GetActorsArea(pos[0],pos[1],50,false);
                    SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5378);
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)item.MaxHP;
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                        }
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
