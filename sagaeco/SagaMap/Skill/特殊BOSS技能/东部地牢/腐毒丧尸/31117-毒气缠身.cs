using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31117 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            毒气 skill = new 毒气(sActor);
            skill.Activate();
        }

        class 毒气 :MultiRunTask
        {
            Actor sActor;
            byte MaxCount = 60, Count = 0;
            Map map;
            public 毒气(Actor sActor)
            {
                period = 1500;
                this.sActor = sActor;
                map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
                sActor.Buff.Poison = true;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, false);
            }
            public override void CallBack()
            {
                try
                {
                    if (Count < MaxCount && sActor.HP > 0 && sActor.HP != sActor.MaxHP)
                    {
                        float factor = 1f;
                        int hps = (int)(sActor.MaxHP - sActor.HP);
                        float cha = (float)hps / (float)sActor.MaxHP;
                        int rate = (int)(cha / 0.05f);
                        factor = 1f + rate * 0.3f;
                        SkillHandler.Instance.ShowEffectOnActor(sActor, 4321);
                        Count++;
                        List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
                        actors = map.GetActorsArea(sActor, 1000, false);//获取sActor周围10格内的所有Actor，并装在actors里
                        foreach (var item in actors)//遍历刚刚获得的actors
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查sActor是否可以攻击遍历的item
                            {
                                if (item.Status.Additions.ContainsKey("腐毒丧尸感染"))
                                {
                                    factor *= 2;
                                    int damage = (int)(item.MaxHP / (100f / factor));
                                    SkillHandler.Instance.CauseDamage(sActor, item, damage);
                                    SkillHandler.Instance.ShowVessel(item, damage);
                                    SkillHandler.Instance.ShowEffectOnActor(item, 5072);

                                    if(item.HP == 0)
                                    {
                                        List<Actor> actors2;//定一个actor的列表，用来装释放者周围的所有Actor的
                                        actors2 = map.GetActorsArea(item, 1000, false);//获取sActor周围10格内的所有Actor，并装在actors里
                                        foreach (var item2 in actors2)
                                        {
                                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item2) && item2.Status.Additions.ContainsKey("腐毒丧尸感染"))//检查sActor是否可以攻击遍历的item
                                            {
                                                SkillHandler.Instance.CauseDamage(sActor, item, 66666);
                                                SkillHandler.Instance.ShowVessel(item, 66666);
                                                SkillHandler.Instance.ShowEffectOnActor(item, 5044);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    factor /= 2;
                                    int damage = (int)(item.MaxHP / (100f/ factor));
                                    SkillHandler.Instance.CauseDamage(sActor, item, damage);
                                    SkillHandler.Instance.ShowVessel(item, damage);
                                    SkillHandler.Instance.ShowEffectOnActor(item, 8036);
                                }
                            }
                        }
                    }
                    else
                    {
                        sActor.Buff.Poison = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, false);
                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    sActor.Buff.Poison = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, false);
                    this.Deactivate();
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
