using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S30016 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            List<Actor> actors = map.GetActorsArea(sActor, 2000, false);
            List<Actor> targets = new List<Actor>();
            foreach (var item in actors)
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))
                {
                    if (sActor.type == ActorType.MOB)
                        SkillHandler.Instance.ActorSpeak(sActor, "来吧，我将指引你们步入黄泉。");
                    SkillHandler.Instance.ShowEffectOnActor(item, 5442);
                    SkillHandler.Instance.ShowEffectOnActor(item, 5236);
                    if (item.type == ActorType.PC)
                        Network.Client.MapClient.FromActorPC((ActorPC)item).SendSystemMessage("你将在5秒内死亡。");
                    ActivatorA a = new ActivatorA(sActor, item);
                    a.Activate();
                }
            }

        }
        class ActivatorA : MultiRunTask
        {
            Actor sActor;
            Actor dActor;
            Map map;

            public ActivatorA(Actor sActor, Actor dActor)
            {
                this.sActor = sActor;
                this.dActor = dActor;
                map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                this.dueTime = 5000;
            }
            public override void CallBack()
            {
                try
                {
                    if(dActor != null)
                    {
                        SkillHandler.Instance.ShowEffectOnActor(dActor, 5396);
                        SkillHandler.Instance.CauseDamage(sActor, dActor, 99999);
                        SkillHandler.Instance.ShowVessel(dActor, 99999);
                    }
                    Deactivate();
                    return;
                }
                catch (Exception ex)
                {
                    Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
