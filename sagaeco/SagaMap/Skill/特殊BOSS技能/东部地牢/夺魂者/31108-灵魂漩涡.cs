using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31108 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            /*-------------------获取随机目标-----------------*/
            Actor Target;//先定一个随机后的目标，备用！
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
            List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
            actors = map.GetActorsArea(sActor, 1000, false);//获取sActor周围10格内的所有Actor，并装在actors里
            List<Actor> Targets = new List<Actor>();//再定一个Actor的列表，用来装可以供释放者攻击的所有Actor
            foreach (var item in actors)//遍历刚刚获得的actors
            {
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查sActor是否可以攻击遍历的item
                    Targets.Add(item);//如果可以攻击，就加进Targets里
            }
            if (Targets.Count == 0) return;//如果Targets里没东西，则直接返回
            int ran = SagaLib.Global.Random.Next(0, Targets.Count - 1);//随机一个数字，从0到获取的目标数-1
            Target = Targets[ran];//根据随机到的数字，指定Targets里第ran个Actor，然后赋值给Target
            /*-------------------获取随机目标完成-----------------*/

            byte x = SagaLib.Global.PosX16to8(Target.X, map.Width);//将Target的3D坐标转成2D坐标
            byte y = SagaLib.Global.PosY16to8(Target.Y, map.Height);//将Target的3D坐标转成2D坐标
            args.x = x;//技能参数的坐标
            args.y = y;//技能参数的坐标

            int count = 1;
            if (sActor.TInt["难度"] > 1)//困难难度下，生成3个灵魂漩涡
                count = 3;

            /*-------------------以下内容是设置系技能固定搭配-----------------*/

            for(int i = 0; i < count; i++)
            {
                //创建设置型技能技能体（一个黑洞）
                ActorSkill actor = new ActorSkill(args.skill, sActor);

                actor.Name = "灵魂漩涡";
                //设定技能体位置
                actor.MapID = sActor.MapID;

                short[] pos=new short[2];
                if (i == 0)//第一个目标选定一名玩家
                {
                    pos[0] = SagaLib.Global.PosX8to16(args.x, map.Width);
                    pos[1] = SagaLib.Global.PosY8to16(args.y, map.Height);
                }
                else//其余的灵魂漩涡会出现在随机位置
                {
                    pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 800);
                }
                actor.X = pos[0];
                actor.Y = pos[1];
                //设定技能体的事件处理器，由于技能体不需要得到消息广播，因此创建个空处理器
                actor.e = new ActorEventHandlers.NullEventHandler();
                //在指定地图注册技能体Actor
                map.RegisterActor(actor);
                //设置Actor隐身属性为非
                actor.invisble = false;
                //广播隐身属性改变事件，以便让玩家看到技能体

                //設置系
                actor.Stackable = false;
                /*-------------------设置系为地图的一个actor-----------------*/

                /*-------------------魔法阵的技能体-----------------*/
                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31112, 1), sActor);
                actor2.Name = "按魔法阵";
                actor2.MapID = sActor.MapID;
                actor2.X = pos[0];
                actor2.Y = pos[1];
                actor2.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor2);
                actor2.invisble = false;
                map.OnActorVisibilityChange(actor2);
                actor2.Stackable = true;
                /*-------------------魔法阵的技能体-----------------*/

                旋涡 skill = new 旋涡(sActor, actor, actor2);
                skill.Activate();
            }


        }
        class 旋涡 : MultiRunTask
        {
            ActorSkill actor;//技能体
            ActorSkill actor2;//技能体
            Actor caster;//释放者
            Map map;//地图
            int countMax = 100, count = 0;//最大循环次数
            int acountMax = 4, acount = 0;//最大攻击次数
            public 旋涡(Actor caster, ActorSkill actor,ActorSkill actor2)//构造函数，传入释放者和技能体2个参数
            {
                this.actor = actor;
                this.actor2 = actor2;
                this.caster = caster;
                map = Manager.MapManager.Instance.GetMap(actor.MapID);
                this.period = 1000;//每次循环间隔设置为1秒
                this.dueTime = 4000;
            }
            public override void CallBack()
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (count == 0 && actor2 != null)
                    {
                        map.DeleteActor(actor2);//先删除魔法阵效果
                        map.OnActorVisibilityChange(actor);//再广播旋涡特效
                    }
                    if (count < countMax && acount < acountMax && caster.HP > 0)//如果循环次数未到达临界，或者攻击次数未到达临界
                    {
                        count++;
                        List<Actor> actors = map.GetActorsArea(actor, 300, false);//获取技能周围的目标
                        List<Actor> affected = new List<Actor>();
                        foreach (Actor i in actors)//遍历目标
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(caster, i))//检查是否可攻击
                            {
                                affected.Add(i);
                                int damage = (int)(i.MaxHP * 0.2f);
                                SkillHandler.Instance.CauseDamage(caster, i, damage);
                                SkillHandler.Instance.ShowVessel(i, damage);
                                if (i.HP == 0)//如果造成伤害令目标死亡，释放者需要进入瘴气兵装状态
                                {
                                    if (!caster.Status.Additions.ContainsKey("瘴气兵装"))//进入瘴气兵装状态
                                    {
                                        瘴气兵装 buff = new 瘴气兵装(null, caster, 15000);
                                        SkillHandler.ApplyAddition(caster, buff);
                                    }
                                }
                            }
                            if(i.Name == "夺魂者" && i.type == ActorType.MOB)//如果目标是BOSS
                            {
                                Deactivate();//停止线程
                                map.DeleteActor(actor);//删除技能体
                                /*-----------对全体玩家造成大量伤害-----------*/
                                List<Actor> actorsB = map.GetActorsArea(actor, 5000, false);//获取技能周围的目标
                                foreach (Actor y in actorsB)//遍历目标
                                {
                                    if (SkillHandler.Instance.CheckValidAttackTarget(caster, y))//检查是否可攻击
                                    {
                                        int damage = (int)(y.MaxHP * 0.7f);
                                        SkillHandler.Instance.CauseDamage(caster, y, damage);
                                        SkillHandler.Instance.ShowVessel(y, damage);
                                        SkillHandler.Instance.ShowEffectOnActor(y, 5003);
                                    }
                                }
                                if (!caster.Status.Additions.ContainsKey("瘴气兵装"))//进入瘴气兵装状态
                                {
                                    瘴气兵装 buff = new 瘴气兵装(null, caster, 15000);
                                    SkillHandler.ApplyAddition(caster, buff);
                                }
                                /*-----------对全体玩家造成大量伤害-----------*/
                            }
                        }
                        if (affected.Count > 0) acount++;//如果可攻击目标，则视为攻击过一次
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
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
