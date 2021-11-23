using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31057 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
             return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 3000);
            if (actors.Count > 0)
                dActor = actors[SagaLib.Global.Random.Next(0, actors.Count - 1)];
            /*-------------------魔法阵的技能体-----------------*/
            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31112, 1), sActor);
            actor2.Name = "暗AOE大魔法阵";
            actor2.MapID = sActor.MapID;
            actor2.X = dActor.X;
            actor2.Y = dActor.Y;
            actor2.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor2);
            actor2.invisble = false;
            map.OnActorVisibilityChange(actor2);
            actor2.Stackable = false;
            /*-------------------魔法阵的技能体-----------------*/

            Activator timer = new Activator(sActor,actor2);
            timer.Activate();
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            //Actor dActor;
            Map map;
            ActorSkill ActorSkill;
            float rate;
            public Activator(Actor caster,ActorSkill actorSkill)
            {
                this.caster = caster;
                ActorSkill = actorSkill;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 2500;
                period = 2500;
                //dActor = dactor;
            }
            public override void CallBack()
            {
                try
                {
                    short x = ActorSkill.X;
                    short y = ActorSkill.Y;

                    //SkillHandler.Instance.ShowEffectOnActor(ActorSkill, 5299);

                    map.DeleteActor(ActorSkill);
                    int damage = 6666666;
                    SkillHandler.Instance.ActorSpeak(caster, "姐姐大人，你看你看。");

                    
                    List<Actor> targets = map.GetActorsArea(x,y, 300);
                    
                    foreach (var item in targets)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5396);
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
        }
    }
}
