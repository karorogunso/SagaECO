using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;

namespace SagaMap.Skill.SkillDefinations.ForceMaster
{
    class ShockWave : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            ActorSkill actor = new ActorSkill(args.skill, sActor);//Register the substituted groove skill-actor.
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            actor.MapID = sActor.MapID;
            actor.X = sActor.X;
            actor.Y = sActor.Y;
            actor.e = new ActorEventHandlers.NullEventHandler();
            actor.Name = "ShockWave";//Set a flag that marking not to show the dispperance information when groove disppear. 
            map.RegisterActor(actor);
            actor.invisble = false;
            map.OnActorVisibilityChange(actor);
            ActivatorA timer = new ActivatorA(actor, dActor, sActor, args, level);
            timer.Activate();//Call ActivatorA.CallBack 500ms later.
        }

        #endregion

    }
    class ActivatorA : MultiRunTask
    {

        ActorSkill SkillBody;
        SkillArg Arg;
        Actor AimActor;
        Map map;
        Actor sActor;
        int count = 0;
        int countMax = 3;
        float factor = 1;
        SkillArg SkillFireBolt = new SkillArg();
        public ActivatorA(ActorSkill actor, Actor dActor, Actor sActor, SkillArg args, byte level)
        {
            this.dueTime = 100;
            this.period = 1000;
            this.AimActor = dActor;
            this.Arg = args;
            this.SkillBody = actor;
            this.sActor = sActor;
            map = Manager.MapManager.Instance.GetMap(AimActor.MapID);
            ActorPC Me = (ActorPC)sActor;//Get the total skill level of skill with fire element.

            switch (level)
            {
                case 1:
                    factor = 0.25f;
                    countMax = 4;
                    this.period = 1000;
                    break;
                case 2:
                    factor = 0.5f;
                    countMax = 6;
                    this.period = 800;
                    break;
                case 3:
                    factor = 0.75f;
                    countMax = 10;
                    this.period = 700;
                    break;
                case 4:
                    factor = 1.0f;
                    countMax = 12;
                    this.period = 400;
                    break;
                case 5:
                    factor = 1.25f;
                    countMax = 16;
                    this.period = 100;
                    break;
            }
        }

        public override void CallBack(object o)
        {
            //测试去除技能同步锁ClientManager.EnterCriticalArea();
            short DistanceA = Map.Distance(SkillBody, AimActor);
            if (count <= countMax)
            {
                if (DistanceA <= 600)//If mob is out the range that FireBolt can cast, skip out.
                {
                    if (count == countMax)
                        SkillFireBolt.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3124, 1);
                    else
                        SkillFireBolt.skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(3001, 1);

                    SkillFireBolt.argType = SkillArg.ArgType.Active;//Configure the skillarg of firebolt, the caster is the skillactor of subsituted groove.
                    SkillFireBolt.sActor = SkillBody.ActorID;
                    SkillFireBolt.dActor = AimActor.ActorID;
                    SkillFireBolt.x = 255;
                    SkillFireBolt.y = 255;
                    SkillHandler.Instance.MagicAttack(sActor, AimActor, SkillFireBolt, Elements.Neutral, factor);
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, SkillFireBolt, SkillBody, true);
                    if (SkillFireBolt.flag.Contains(AttackFlag.DIE | AttackFlag.HP_DAMAGE | AttackFlag.ATTACK_EFFECT))//If mob died,terminate the proccess.
                    {
                        map.DeleteActor(SkillBody);
                        this.Deactivate();
                    }

                }
                count++;
            }
            else
            {
                map.DeleteActor(SkillBody);
                this.Deactivate();
            }
            //测试去除技能同步锁ClientManager.LeaveCriticalArea();
        }
    }
}
