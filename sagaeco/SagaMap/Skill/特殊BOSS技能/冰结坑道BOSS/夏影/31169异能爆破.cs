using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31169 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short[] pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 2500);
            int range = SagaLib.Global.Random.Next(1, 2);
            ActorSkill actor = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(range > 1 ? 31112u : 31138u,1), sActor)
            {
                Name = "湮灭之暗",
                MapID = sActor.MapID,
                X = pos[0],
                Y = pos[1],
                e = new ActorEventHandlers.NullEventHandler(),
            };
            map.RegisterActor(actor);
            actor.invisble = false;
            actor.Stackable = true;
            

            /*-------------------魔法阵的技能体-----------------*/
            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(range > 1 ? 31112u : 31138u, 1), sActor)
            {
                Name = "暗魔法阵",
                MapID = sActor.MapID,
                X = pos[0],
                Y = pos[1],
                e = new ActorEventHandlers.NullEventHandler()
            };
            map.RegisterActor(actor2);
            actor2.invisble = false;
            actor2.Stackable = true;
            
            map.OnActorVisibilityChange(actor2);
            /*-------------------魔法阵的技能体-----------------*/

            旋涡 skill = new 旋涡(sActor, actor, actor2,range);
            skill.Activate();


        }
        class 旋涡 : MultiRunTask
        {
            ActorSkill actor;//技能体
            ActorSkill actor2;//技能体
            Actor caster;//释放者
            Map map;//地图
            int range; //魔法阵大小
            int countMax = 1000, count = 0;//最大循环次数
            int acountMax = 1, acount = 0;//最大攻击次数
            public 旋涡(Actor caster, ActorSkill actor,ActorSkill actor2,int range)//构造函数，传入释放者\技能体\范围大小
            {
                this.actor = actor;
                this.actor2 = actor2;
                this.caster = caster;
                this.range = range;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 500;//每次循环间隔设置为0.5秒
                this.dueTime = 3000;
            }
            public override void CallBack()
            {
                //ClientManager.EnterCriticalArea();
                try
                {
                    if (actor.e == null)
                    {
                        Deactivate();
                        //map.DeleteActor(actor);
                        return;
                    };
                    if (count == 0 && actor2 != null)
                    {
                        map.DeleteActor(actor2);//先删除魔法阵效果
                        map.OnActorVisibilityChange(actor);//再广播旋涡特效
                    }
                    if (count < countMax && acount < acountMax)//如果循环次数未到达临界，或者攻击次数未到达临界
                    {
                        count++;
                        List<Actor> actors = map.GetActorsArea(actor, (short)(range * 100), false);//获取技能周围的目标
                        bool target = false;
                        foreach (Actor i in actors)//遍历目标，不需要检查合法性，mob、玩家一起干掉。
                        {
                            if ((i.type == ActorType.PC || i.type == ActorType.MOB )&& i.HP > 0)//检查是否可攻击
                            {
                                //target = true;
                                SkillHandler.Instance.CauseDamage(i, i, 666666);
                                SkillHandler.Instance.ShowVessel(i, 666666);
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
                //ClientManager.LeaveCriticalArea();
            }
        }
    }
}
