using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S25002 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            
            short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2500);
            ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31133,1), sActor)
            {
                Name = "焚烬之火",
                MapID = sActor.MapID,
                X = pos[0],
                Y = pos[1],
                e = new ActorEventHandlers.NullEventHandler()
            };
            map.RegisterActor(actor);
            actor.invisble = false;
            actor.Stackable = true;

            map.OnActorVisibilityChange(actor);

            旋涡 skill = new 旋涡(sActor, actor);
            skill.Activate();


        }
        class 旋涡 : MultiRunTask
        {
            ActorSkill actor;//技能体
            Actor caster;//释放者
            Map map;//地图
            int countMax = 600, count = 0;//最大循环次数
            int acountMax = 1, acount = 0;//最大攻击次数
            public 旋涡(Actor caster, ActorSkill actor)//构造函数，传入释放者\技能体
            {
                this.actor = actor;
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 100;//每次循环间隔设置为0.1秒
                this.dueTime = 0;
            }
            public override void CallBack()
            {
                try
                {
                    if (actor.e == null)
                    {
                        Deactivate();
                        //map.DeleteActor(actor);
                        return;
                    };
                    if (count < countMax && acount < acountMax)//如果循环次数未到达临界，或者攻击次数未到达临界
                    {
                        count++;
                        List<Actor> actors = map.GetActorsArea(actor, 100, false);//获取技能周围的目标
                        bool target = false;
                        foreach (Actor i in actors)//遍历目标，不需要检查合法性，mob、玩家一起处理。
                        {
                            if (i.type == ActorType.PC || i.type == ActorType.MOB)//检查目标
                            {
                                target = true;
                                if (!i.Status.Additions.ContainsKey("焚烬之火"))
                                {
                                    OtherAddition skill = new OtherAddition(null, i, "焚烬之火", 30000);
                                    skill.OnAdditionStart += (x, e) =>
                                    {
                                        SkillHandler.Instance.ShowEffectOnActor(i, 4114);
                                        if (i.type == ActorType.PC)
                                            Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("获得『焚烬之火』，所有技能不需要吟唱了！");
                                    };
                                    skill.OnAdditionEnd += (x, e) =>
                                    {
                                        if (i.type == ActorType.PC)
                                            Network.Client.MapClient.FromActorPC((ActorPC)i).SendSystemMessage("『焚烬之火』效果结束了！");
                                    };
                                    SkillHandler.ApplyAddition(i, skill);
                                }
                                actor.invisble = true;
                                map.OnActorVisibilityChange(actor);
                            }
                        }
                        if (target)
                            acount++;//如果可攻击目标，则视为攻击过一次
                    }
                    else
                    {
                        Deactivate();
                        map.DeleteActor(actor);
                    }
                }
                catch (Exception ex)
                {
                    Deactivate();
                    map.DeleteActor(actor);
                    Logger.ShowError(ex);
                }
            }
        }
    }
}
