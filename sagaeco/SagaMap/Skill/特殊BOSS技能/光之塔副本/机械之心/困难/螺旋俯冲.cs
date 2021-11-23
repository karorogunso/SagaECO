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
    public class S31058 : ISkill
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
            硬直 y = new 硬直(args.skill, sActor, 3000);
            SkillHandler.ApplyAddition(sActor, y);
            foreach (var item in actors)
            {
                short[] pos = new short[2];
                pos[0] = item.X;
                pos[1] = item.Y;

                /*-------------------魔法阵的技能体-----------------*/
                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31136, 1), sActor);
                actor2.Name = "地AOE小魔法阵";
                actor2.MapID = sActor.MapID;
                actor2.X = item.X;
                actor2.Y = item.Y;
                actor2.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor2);
                actor2.invisble = false;
                map.OnActorVisibilityChange(actor2);
                actor2.Stackable = false;
                /*-------------------魔法阵的技能体-----------------*/


                Activator timer = new Activator(sActor, item, pos,actor2);
                timer.Activate();
                SkillHandler.Instance.ShowEffect(map, sActor, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5327);
            }
            for (int i = 0; i < 12; i++)
            {
                short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2000);
                /*-------------------魔法阵的技能体-----------------*/
                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31136, 1), sActor);
                actor2.Name = "地AOE小魔法阵";
                actor2.MapID = sActor.MapID;
                actor2.X = pos[0];
                actor2.Y = pos[1];
                actor2.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor2);
                actor2.invisble = false;
                map.OnActorVisibilityChange(actor2);
                actor2.Stackable = false;
                /*-------------------魔法阵的技能体-----------------*/

                Activator timer = new Activator(sActor, null, pos, actor2);
                timer.Activate();
                SkillHandler.Instance.ShowEffect(map, sActor, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5327);
            }
            
        }
        private class Activator : MultiRunTask
        {
            Actor caster;
            Map map;
            float rate;
            short[] pos;
            ActorSkill ActorSkill;
            public Activator(Actor caster, Actor dActor, short[] pos, ActorSkill actorSkill)
            {
                ActorSkill = actorSkill;
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
                dueTime = 3000;
                this.pos = pos;
            }
            public override void CallBack()
            {
                try
                {
                    map.DeleteActor(ActorSkill);
                    List<Actor> actors = map.GetActorsArea(pos[0],pos[1],150,false);
                    SkillHandler.Instance.ShowEffect(map, caster, SagaLib.Global.PosX16to8(pos[0], map.Width), SagaLib.Global.PosY16to8(pos[1], map.Height), 5345);
                    bool cankill = false;
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = (int)(item.MaxHP * 1.5f);
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            cankill = true;
                        }
                    }
                    if (cankill)
                    {
                        List<Actor> actors3 = map.GetActorsArea(caster, 5000, false);
                        foreach (var item in actors3)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                int damage = (int)(item.MaxHP * 0.2f);
                                SkillHandler.Instance.CauseDamage(caster, item, damage);
                                SkillHandler.Instance.ShowVessel(item, damage);
                                SkillHandler.Instance.ShowEffectOnActor(item, 5396);
                                零件回收 skill = new 零件回收(null, item, 8000, 500);
                                SkillHandler.ApplyAddition(item, skill);
                                SkillHandler.SendSystemMessage(item, "由于有人被【螺旋俯冲】命中，你受到了溅射伤害，并将受到持续的伤害效果。");
                            }
                        }
                    }


                    /*
                    List<Actor> actors2 = map.GetActorsArea(pos[0], pos[1], 700, false);
                    foreach (var item in actors2)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                        {
                            int damage = 600;
                            SkillHandler.Instance.CauseDamage(caster, item, damage);
                            SkillHandler.Instance.ShowVessel(item, damage);
                            SkillHandler.Instance.ShowEffectOnActor(item, 5115);
                        }
                    }*/
                    if (caster.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)caster;
                        int count = actors.Count;
                        mob.TInt["零件数"] += count * 2;
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
