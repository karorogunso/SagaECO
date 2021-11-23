using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaMap.Skill.Additions.Global;
using SagaMap.Mob;

namespace SagaMap.Skill.SkillDefinations
{
    public class S31106 : ISkill
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
                if (SkillHandler.Instance.CheckValidAttackTarget(sActor, item) && item != dActor)//检查sActor是否可以攻击遍历的item，当前仇恨目标不会加入列表。
                    Targets.Add(item);//如果可以攻击，就加进Targets里
            }
            if (Targets.Count == 0) Targets.Add(dActor);//如果Targets里没东西，则攻击当前仇恨目标
            int ran = SagaLib.Global.Random.Next(0, Targets.Count - 1);//随机一个数字，从0到获取的目标数-1
            Target = Targets[ran];//根据随机到的数字，指定Targets里第ran个Actor，然后赋值给Target
            /*-------------------获取随机目标完成-----------------*/

            SkillHandler.Instance.ActorSpeak(sActor, "你的灵魂…不应该存在于这里！");
            SkillHandler.Instance.PhysicalAttack(sActor, Target, args, Elements.Dark, 1.7f);//对目标造成伤害

            Target.Buff.リボーン = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Target, true);
            SkillHandler.Instance.ShowEffectOnActor(Target, 5318);
            SkillHandler.Instance.ShowEffectOnActor(Target, 5298);

            祸乱之魂 skill = new 祸乱之魂(sActor, Target);//为目标实例化 祸乱之魂 
            skill.Activate();//把 祸乱之魂 激活

        }
        class 祸乱之魂 : MultiRunTask//定一个 祸乱之魂 的类
        {
            Actor sActor;//攻击者
            Actor dActor;//目标，因为涉及到队伍，目标必须为玩家
            Map map;
            public 祸乱之魂(Actor sActor, Actor dActor)//传入2个角色，一个是攻击者sActor，一个是目标dActor
            {
                this.sActor = sActor;//让外面的sActor，等于传入的sActor
                this.dActor = dActor;
                dueTime = 10000;//设置启动延迟为10秒
                map = SagaMap.Manager.MapManager.Instance.GetMap(sActor.MapID);//根据释放者的地图ID，获取地图数据，保存在map里

            }
            public override void CallBack()//计时器的回应函数
            {
                if (dActor == null)//如果dActor为空值
                {
                    this.Deactivate();//结束定时器
                    return;//并立刻返回
                }
                if (dActor.HP == 0)//如果dActor已经死了，或者没有队伍
                {
                    dActor.Buff.リボーン = false;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);
                    this.Deactivate();//结束定时器
                    return;//并立刻返回
                }
                /*if (dActor.HP == dActor.MaxHP)//如果dActor变成了满血
                {
                    this.Deactivate();//结束定时器
                    return;//并立刻返回
                }*/
                dActor.Buff.リボーン = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, dActor, true);

                List<Actor> actors = map.GetActorsArea(dActor, (short)(dActor.HP/2), false);

                int damage = (int)dActor.HP;//先定一个固定伤害，这个伤害为目标的当前血量
                foreach (var item in actors)//遍历目标队伍的所有人
                {
                    if(SkillHandler.Instance.CheckValidAttackTarget(sActor,item))
                    {
                        if (item != dActor)//如果队员和目标的地图ID相等，并且当前遍历的队员不是目标本人
                        {
                            SkillHandler.Instance.CauseDamage(sActor, item, damage);//实现这个伤害
                            SkillHandler.Instance.ShowVessel(item, damage);//显示伤害数字
                            SkillHandler.Instance.ShowEffectOnActor(item, 5319);
                            if (item.HP <= 0)//如果造成伤害后目标死亡
                            {
                                if (!sActor.Status.Additions.ContainsKey("瘴气兵装"))
                                {
                                    瘴气兵装 buff = new 瘴气兵装(null, sActor, 15000);
                                    SkillHandler.ApplyAddition(sActor, buff);
                                }
                            }
                            else
                            {
                                if (sActor.Status.Additions.ContainsKey("瘴气兵装"))//如果BOSS身上已经有 瘴气兵装 BUFF了，要降低目标6维
                                {
                                    addbuff(item);//跳转到Addbuff
                                    SkillHandler.Instance.ShowEffectOnActor(dActor, 5293);
                                }
                            }
                        }
                        //此处需要填补特效
                    }
                }
                this.Deactivate();//结束定时器
                return;//并立刻返回
            }

            void addbuff(Actor dActor)
            {
                int ran = SagaLib.Global.Random.Next(1, 6);//随机1-6，来选择降低哪个属性
                int value = 25;//定好降多少
                switch (ran)
                {
                    case 1://降低STR
                        if (!dActor.Status.Additions.ContainsKey("STRDOWN"))
                        {
                            STRDOWN sd = new STRDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 2:
                        if (!dActor.Status.Additions.ContainsKey("AGIDOWN"))
                        {
                            AGIDOWN sd = new AGIDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 3:
                        if (!dActor.Status.Additions.ContainsKey("VITDOWN"))
                        {
                            VITDOWN sd = new VITDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 4:
                        if (!dActor.Status.Additions.ContainsKey("INTDOWN"))
                        {
                            INTDOWN sd = new INTDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 5:
                        if (!dActor.Status.Additions.ContainsKey("DEXDOWN"))
                        {
                            DEXDOWN sd = new DEXDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                    case 6:
                        if (!dActor.Status.Additions.ContainsKey("MAGDOWN"))
                        {
                            MAGDOWN sd = new MAGDOWN(null, dActor, 180000, value);
                            SkillHandler.ApplyAddition(dActor, sd);
                        }
                        break;
                }
            }
        }
    }
}
