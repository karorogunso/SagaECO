using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.SkillDefinations.Global;
using SagaLib;
using SagaMap;
using SagaMap.Skill.Additions.Global;


namespace SagaMap.Skill.SkillDefinations
{
    class S20006 : ISkill
    {
        #region ISkill Members

        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("炽烈阳炎CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //技能CD
            OtherAddition cd = new OtherAddition(null, sActor, "炽烈阳炎CD", 15000);
            SkillHandler.ApplyBuffAutoRenew(sActor, cd);

            //技能效果
            炽烈阳炎 curse = new 炽烈阳炎(sActor, dActor, level, args);
            curse.Activate();
        }
        class 炽烈阳炎 : MultiRunTask
        {
            Actor sActor;//攻击者  
            Actor dActor;//目标
            byte count = 0,maxcount = 8;
            float lastfactor;
            float dotfactor;
            byte x, y;
            Map map;
            public 炽烈阳炎(Actor sActor, Actor dActor, byte level, SkillArg args)
            {
                this.sActor = sActor;
                this.dActor = dActor;
                dueTime = 0;
                period = 1000;
                lastfactor = 4f + 0.8f * level;
                dotfactor = 1f + 0.5f * level;
                map = SkillHandler.GetActorMap(sActor);
                x = args.x;
                y = args.y;
            }
            public override void CallBack()
            {
                try
                {
                    if (count > maxcount)
                        Deactivate();
                    List<Actor> targets = SkillHandler.Instance.GetAreaActorByPosWhoCanBeAttackedTargets(sActor, x, y, 200);
                    //效果结束时造成伤害
                    if(count == maxcount)
                    {
                        foreach (var item in targets)
                        {
                            SkillHandler.Instance.DoDamage(false, sActor, item, null, SkillHandler.DefType.MDef, Elements.Neutral, 50, lastfactor);
                            //TODO：特效
                        }
                    }
                    //dot伤害
                    else
                    {
                        foreach (var i in targets)
                        {
                            SkillHandler.Instance.DoDamage(false, sActor, i, null, SkillHandler.DefType.MDef, Elements.Neutral, 50, dotfactor);
                            //TODO：特效
                            OtherAddition sd = new OtherAddition(null, i, "炽烈阳炎减速", 900);
                            sd.OnAdditionStart += (s, e) =>
                            {
                                i.TInt["炽烈阳炎减速点"] = (int)(i.Speed * 0.5f);
                                i.Speed -= (ushort)i.TInt["炽烈阳炎减速点"];
                                i.Buff.SpeedDown = true;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                            };
                            sd.OnAdditionEnd += (s, e) =>
                            {
                                i.Speed += (ushort)i.TInt["炽烈阳炎减速点"];
                                i.Buff.SpeedDown = false;
                                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, i, true);
                            };
                            SkillHandler.ApplyBuffAutoRenew(i, sd);
                        }
                    }
                    count++;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Deactivate();
                }
            }
        }
        #endregion
    }
}
