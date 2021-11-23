using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31124 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);

            for (int i = 0; i < 6; i++)
            {
                short[] pos;
                pos = map.GetRandomPosAroundPos(sActor.X, sActor.Y, 700);

                if (i == 2)
                {
                    pos[0] = sActor.X;
                    pos[1] = sActor.Y;
                }

                /*-------------------魔法阵的技能体-----------------*/
                ActorSkill actor2 = new ActorSkill(SagaDB.Skill.SkillFactory.Instance.GetSkill(31112, 1), sActor);
                actor2.Name = "水AOE小魔法阵";
                actor2.MapID = sActor.MapID;
                actor2.X = pos[0];
                actor2.Y = pos[1];
                actor2.e = new ActorEventHandlers.NullEventHandler();
                map.RegisterActor(actor2);
                actor2.invisble = false;
                map.OnActorVisibilityChange(actor2);
                actor2.Stackable = false;
                /*-------------------魔法阵的技能体-----------------*/

                暗黑坠击 skill = new 暗黑坠击(sActor,actor2);
                skill.Activate();
            }

        }
        class 暗黑坠击 : MultiRunTask
        {
            Actor sActor;
            Map map;
            ActorSkill ActorSkill;
            short posX;//定一个short参数，用来记3D坐标的X
            short posY;//定一个short参数，用来记3D坐标的Y
            byte x, y;
            byte count = 0, maxCount = 11;
            public 暗黑坠击(Actor sActor, ActorSkill actorSkill)
            {
                ActorSkill = actorSkill;
                period = 80;
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
                    if(count == 1)
                    {
                        map.DeleteActor(ActorSkill);
                        SkillHandler.Instance.ShowEffect(map, sActor, x, y, 4385);
                    }
                    if (count < maxCount && sActor.HP > 0)
                    {
                        List<Actor> actors = map.GetActorsArea(posX, posY, 300, false);//获取坐标X,Y周围1.5格内的所有Actor，并装在actors里
                        foreach (var item in actors)
                        {
                            if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item))//检查目标是否可攻击
                            {
                                SkillHandler.Instance.DoDamage(true, sActor, item, null, SkillHandler.DefType.Def, Elements.Dark, 50, 1.5f);
                                SkillHandler.Instance.ShowEffectOnActor(item, 8067);
                            }
                        }
                        count++;
                    }
                    else
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
