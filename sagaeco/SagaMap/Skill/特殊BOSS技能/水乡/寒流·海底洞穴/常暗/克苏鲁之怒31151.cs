using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31151 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            //复活死掉的随从
            if (sActor.SettledSlave.Count > 0)
            {
                foreach (var item in sActor.SettledSlave)
                {
                    if (item.type == ActorType.MOB)
                    {
                        ActorMob mob = (ActorMob)item;
                        ActorEventHandlers.MobEventHandler eh = (ActorEventHandlers.MobEventHandler)mob.e;
                        short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 200);
                        if (mob.Buff.Dead)
                        {
                            mob.Buff.Clear();
                            mob.Dir = (ushort)SagaLib.Global.Random.Next(0, 7);
                            mob.X = pos[0];
                            mob.Y = pos[1];
                            eh.AI.X_Spawn = mob.X;
                            eh.AI.Y_Spawn = mob.Y;
                            map.RegisterActor(mob);
                            mob.HP = mob.MaxHP;
                            mob.MP = mob.MaxMP;
                            mob.SP = mob.MaxSP;

                            mob.invisble = false;
                            map.OnActorVisibilityChange(mob);
                            map.SendVisibleActorsToActor(mob);

                            //((ActorEventHandlers.MobEventHandler)(mob.e)).AI.Master = sActor;
                            ((ActorEventHandlers.MobEventHandler)(mob.e)).AI.Start();

                            SkillHandler.Instance.ShowEffectByActor(mob, 5212);
                        }
                    }
                }
            }

            //伤害+沉默
            List<Actor> actors = map.GetActorsArea(sActor, 1000, false);
            foreach (var item in actors)
            {
                if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                {
                    SkillHandler.Instance.DoDamage(false, sActor, item, null, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, 2f);
                    Silence skill = new Silence(null, item, 10000);
                    SkillHandler.ApplyAddition(item, skill);
                }
            }

            actors = map.GetActorsArea(sActor, 3000, false);
            int count = 0;
            foreach (var item in actors)
                if (item.type == ActorType.SKILL && item.Name == "召唤寒冰球")
                {
                    item.TInt["克苏鲁之怒"] = 1;
                    count++;
                }
            if(count > 0)
            {
                uint heal = (uint)(sActor.MaxHP * 0.1f * count);
                sActor.HP += heal;
                if (sActor.HP > sActor.MaxHP)
                    sActor.HP = sActor.MaxHP;
                SkillHandler.Instance.ShowVessel(sActor, (int)-heal);
            }
        }
        #endregion
    }
}
