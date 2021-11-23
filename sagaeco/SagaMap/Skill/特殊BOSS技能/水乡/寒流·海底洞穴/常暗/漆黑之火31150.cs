using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31150 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            for (int i = 0; i < 5; i++)
            {
                short[] pos;
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 1200);

                ActorSkill actor = new ActorSkill(args.skill, sActor);
                actor.MapID = sActor.MapID;
                actor.X = pos[0];
                actor.Y = pos[1];
                actor.Speed = 500;
                actor.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor);
                actor.invisble = false;
                map.OnActorVisibilityChange(actor);
                actor.Stackable = false;
                Activator timer = new Activator(sActor, actor);
                timer.Activate();
            }
        }
        private class Activator : MultiRunTask
        {
            ActorSkill actor;
            Actor caster;
            Map map;
            int countMax = 25, count = 0;
            public Activator(Actor caster, ActorSkill skillActor)
            {
                actor = skillActor;
                this.caster = caster;
                period = 1000;
                dueTime = 1000;
                map = Manager.MapManager.Instance.GetMap(caster.MapID);
            }
            public override void CallBack()
            {
                try
                {
                    count++;
                    if (count < countMax && caster.HP != caster.MaxHP && caster.HP > 0)
                    {
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, item))
                            {
                                if (!item.Status.Additions.ContainsKey("漆黑之火"))
                                {
                                    OtherAddition skill = new OtherAddition(null, item, "漆黑之火", 10000);
                                    skill.period = 1000;
                                    skill.OnAdditionStart += (s, e) =>
                                    {
                                        item.Buff.恶炎 = true;
                                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                                    };
                                    skill.OnAdditionEnd += (s, e) =>
                                    {
                                        item.Buff.恶炎 = false;
                                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, item, true);
                                    };
                                    skill.OnUpdate += (s, e) =>
                                    {
                                        int damage = (int)(item.MaxHP * 0.1f);
                                        SkillHandler.Instance.CauseDamage(caster, item, damage);
                                        SkillHandler.Instance.ShowVessel(item, damage);
                                    };
                                }
                                else
                                    ((OtherAddition)item.Status.Additions["漆黑之火"]).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 10);
                                if(item.Status.Additions.ContainsKey("深海毒液"))
                                {
                                    SkillHandler.RemoveAddition(item, "深海毒液");
                                    AtkUp atkup = new AtkUp(null, item, 10000, 700);
                                    SkillHandler.ApplyAddition(item, atkup);
                                    SkillHandler.SendSystemMessage(item, "你的体内深海毒液因为接触漆黑之火的关系，被转化成力量了！攻击力属性得到大幅度上升。");
                                }
                                if(item.Status.Additions.ContainsKey("Silence"))
                                {
                                    SkillHandler.RemoveAddition(item, "Silence");
                                    SkillHandler.SendSystemMessage(item, "漆黑之火的效果使你的沉默效果移除了。");
                                }
                            }
                        }
                    }
                    else
                    {
                        Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                    map.DeleteActor(actor);
                }
            }
        }
    }
}
