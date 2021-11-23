using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31142 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            for (int i = 0; i <8; i++)
            {
                Activator2 skill = new Activator2(sActor, map, i * 45);
                skill.Activate();
            }
        }
        private class Activator2 : MultiRunTask
        {
            Actor caster;
            Map map;
            double Dir;
            byte count = 0, Maxcount = 5;
            public Activator2(Actor sActor, Map map, double Dir)
            {
                caster = sActor;
                DueTime = SagaLib.Global.Random.Next(100,500);
                period = SagaLib.Global.Random.Next(400, 500);
                this.map = map;
                this.Dir = Dir;
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count < Maxcount)
                    {
                        short[] pos = SkillHandler.Instance.GetNewPoint(Dir, caster.X, caster.Y, count * 3);
                        short X = pos[0];
                        short Y = pos[1];
                        List<Actor> actors = map.GetActorsArea(X, Y, 100);
                        List<Actor> targets = new List<Actor>();
                        SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(X, map.Width), SagaLib.Global.PosY16to8(Y, map.Height), 5003);
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                if (!item.Status.Additions.ContainsKey("Stun"))
                                {
                                    SkillHandler.Instance.DoDamage(false, caster, item, null, SkillHandler.DefType.MDef, Elements.Wind, 50, 3f);
                                    Stun stun = new Stun(null, item, 5000);
                                    SkillHandler.ApplyAddition(item, stun);
                                }
                            }
                        }
                    }
                    else
                    {
                        Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
    }
}
