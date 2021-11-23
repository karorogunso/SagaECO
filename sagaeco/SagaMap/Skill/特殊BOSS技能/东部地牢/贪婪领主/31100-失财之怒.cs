using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31100 : ISkill
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

            /*-------------------魔法阵的技能体-----------------*/
            ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31136, 1), sActor);
            actor2.Name = "火AOE小魔法阵";
            actor2.MapID = sActor.MapID;
            actor2.X = Target.X;
            actor2.Y = Target.Y;
            actor2.e = new ActorEventHandlers.NullEventHandler();
            map.RegisterActor(actor2);
            actor2.invisble = false;
            map.OnActorVisibilityChange(actor2);
            actor2.Stackable = false;
            /*-------------------魔法阵的技能体-----------------*/

            失财之怒 skill = new 失财之怒(sActor, actor2);
            skill.Activate();
        }

        class 失财之怒 : MultiRunTask
        {
            Actor sActor;
            Map map;
            ActorSkill ActorSkill;
            short posX;//定一个short参数，用来记3D坐标的X
            short posY;//定一个short参数，用来记3D坐标的Y
            byte x, y;
            public 失财之怒(Actor sActor, ActorSkill actorSkill)
            {
                ActorSkill = actorSkill;
                dueTime = 2000;
                this.sActor = sActor;
                map = Manager.MapManager.Instance.GetMap(sActor.MapID);
                posX = actorSkill.X;//让3D坐标X等于目标的3D坐标X
                posY = actorSkill.Y;//让3D坐标Y等于目标的3D坐标Y
                x = SagaLib.Global.PosX16to8(posX, map.Width);//将3D坐标转成2D坐标
                y = SagaLib.Global.PosY16to8(posY, map.Height);//将3D坐标转成2D坐标
            }
            public override void CallBack()
            {
                try
                {
                    map.DeleteActor(ActorSkill);
                    SkillHandler.Instance.ShowEffect(map, sActor, x, y, 4172);
                    List<Actor> actors = map.GetActorsArea(posX, posY, 100, false);//获取坐标X,Y周围1.5格内的所有Actor，并装在actors里
                    foreach (var item in actors)
                    {
                        if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查目标是否可攻击
                        {
                            if (!item.Status.Additions.ContainsKey("Stun"))//检查目标身上有没有晕眩，没有才执行
                            {
                                Stun stun = new Stun(null, item, 5000);//实例化一个晕眩效果的计时器类。它的构造函数里有3个参数，分别是技能(设null)，目标和时间(毫秒)
                                SkillHandler.ApplyAddition(item, stun);//把实例好的晕眩效果，加到目标上去
                            }
                            SkillHandler.Instance.DoDamage(true, sActor, item, null, SkillHandler.DefType.Def, Elements.Fire, 50, 4f);
                        }
                    }
                    Deactivate();//结束定时器
                }
                catch (Exception ex)//一旦上文发生任何错误，则
                {
                    Logger.ShowError(ex);//让模拟器显示错误
                    Deactivate();//结束定时器
                    return;//不继续往下走了
                }
            }
        }
    }
}
 