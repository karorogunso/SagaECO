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
    public class S31065 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);

            SkillHandler.Instance.ShowEffectOnActor(sActor, 5082);

            SkillHandler.Instance.ActorSpeak(sActor, "怯惧畏妄，吾唤恶鬼缠汝身！");


            List<Actor> actors = SkillHandler.Instance.GetActorsAreaWhoCanBeAttackedTargets(sActor, 1500);

            foreach (var item in actors)
            {
                /*-------------------魔法阵的技能体-----------------*/
                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31112, 1), sActor);
                actor2.Name = "暗AOE大魔法阵";
                actor2.MapID = sActor.MapID;
                actor2.X = item.X;
                actor2.Y = item.Y;
                actor2.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor2);
                actor2.invisble = false;
                map.OnActorVisibilityChange(actor2);
                actor2.Stackable = false;
                /*-------------------魔法阵的技能体-----------------*/

                魔法阵 magic = new 魔法阵(sActor, actor2);//实例化一个魔法阵，根据构造函数传入参数
                magic.Activate();//激活这个计时器
            }
           
        }

        class 魔法阵 : MultiRunTask //定一个法阵类，用来给每个玩家实例这个法阵。继承MultiRunTask
        {
            Actor sActor;//在外面定一个攻击者sActor
            ActorSkill ActorSkill;
            Map map;//在外面先定好一个map参数，方便整个class里面都能用到
            short posX;//定一个short参数，用来记3D坐标的X
            short posY;//定一个short参数，用来记3D坐标的Y
            byte count = 0;//定一个callback的次数
            byte x, y;
            public 魔法阵(Actor sActor, ActorSkill actorSkill)//传入2个角色，一个是攻击者sActor，一个是目标dActor
            {
                this.sActor = sActor;//让外面的sActor，等于传入的sActor
                this.ActorSkill = actorSkill;
                dueTime = 1800;//设置启动延迟为2秒
                map = Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里
                posX = actorSkill.X;//让3D坐标X等于目标的3D坐标X
                posY = actorSkill.Y;//让3D坐标Y等于目标的3D坐标Y
                x = SagaLib.Global.PosX16to8(posX, map.Width);//将3D坐标转成2D坐标
                y = SagaLib.Global.PosY16to8(posY, map.Height);//将3D坐标转成2D坐标
                //SkillHandler.Instance.ShowEffect(map, dActor, x, y, 5327);
            }
            public override void CallBack()//启动延迟2秒后，执行这个函数
            {
                try//如果执行try里面的内容发生了错误，则立刻跳到下面的catch{ }中
                {
                    map.DeleteActor(ActorSkill);
                    SkillHandler.Instance.ShowEffect(map, sActor, x, y, 4172);

                    OtherAddition skill = new OtherAddition(null, sActor, "恶鬼之道", 4000);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        sActor.Buff.九尾狐魅惑 = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        sActor.Buff.九尾狐魅惑 = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, sActor, true);
                    };
                    SkillHandler.ApplyAddition(sActor, skill);

                    if (count >= 1)//如果执行过1次，则
                    {
                        Deactivate();//结束定时器
                        return;//不继续往下走了
                    }
                    count++;//让次数+1
                    List<Actor> actors;//定一个actor的列表，用来装释放者周围的所有Actor的
                    actors = map.GetActorsArea(posX, posY, 300, false);//获取坐标X,Y周围1.5格内的所有Actor，并装在actors里
                    List<Actor> Targets = new List<Actor>();//再定一个Actor的列表，用来装可以供释放者攻击的所有Actor
                    foreach (var item in actors)//遍历刚刚获得的actors
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查sActor是否可以攻击遍历的item
                            Targets.Add(item);//如果可以攻击，就加进Targets里
                    }
                    if (Targets.Count == 0)
                    {
                        Deactivate();
                        return;//如果Targets里没东西，则直接返回
                    }

                    foreach (var item in Targets)//遍历所获得的可攻击目标
                    {
                        SkillHandler.Instance.CauseDamage(sActor, item, 1);//对当前目标造成1点固定伤害
                        SkillHandler.Instance.ShowVessel(item, 1);//在当前目标身上跳一个掉血1
                        SkillHandler.Instance.ShowEffectOnActor(item, 5022);
                        SkillHandler.Instance.ShowEffectOnActor(item, 5020);
                        恶鬼缠身活动 skill2 = new 恶鬼缠身活动(sActor, item, 555, 700);
                        skill2.Activate();
                    }
                    Deactivate();//结束定时器
                }
                catch (Exception ex)//一旦上文发生任何错误，则
                {
                    ClientManager.LeaveCriticalArea();//解锁
                    SagaLib.Logger.ShowError(ex);//让模拟器显示错误
                    this.Deactivate();//结束定时器
                    return;//不继续往下走了
                }
            }
        }
    }
}
