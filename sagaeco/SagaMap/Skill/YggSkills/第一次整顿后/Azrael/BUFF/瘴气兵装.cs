using SagaLib;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using System;
namespace SagaMap.Skill.SkillDefinations
{
    public class P_瘴气兵装 : MultiRunTask
    {
        Actor caster;
        uint Eid;
        bool ActivateBuff;
        int lifetime = 60000;
        public P_瘴气兵装(Actor sActor, uint EffectID, int duetime, bool activate)
        {
            caster = sActor;
            Eid = EffectID;
            DueTime = duetime;
            ActivateBuff = activate;
        }
        public override void CallBack()
        {
            Deactivate();
            if (caster.Status.Additions.ContainsKey("P_瘴气兵装") && ActivateBuff)
            {
                (caster.Status.Additions["P_瘴气兵装"] as OtherAddition).endTime = DateTime.Now + new TimeSpan(0, 0, 0, 0, lifetime);
                SkillHandler.Instance.ShowEffectByActor(caster, 4276);
            }
            if (!caster.Status.Additions.ContainsKey("P_瘴气兵装"))
            {
                if (Eid != 0)
                    SkillHandler.Instance.ShowEffectOnActor(caster, Eid);
                if (ActivateBuff)
                {
                    Map map = Manager.MapManager.Instance.GetMap(caster.MapID);
                    OtherAddition skill = new OtherAddition(null, caster, "P_瘴气兵装", lifetime, 1000);
                    skill.OnAdditionStart += (s, e) =>
                    {
                        SkillHandler.Instance.ShowEffectByActor(caster, 5440);
                        SkillHandler.Instance.ShowEffectByActor(caster, 4276);
                        s.Buff.魂之手 = true;
                        s.Buff.Undead = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                    };
                    skill.OnAdditionEnd += (s, e) =>
                    {
                        s.Buff.魂之手 = false;
                        s.Buff.Undead = false;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, s, true);
                    };
                    skill.OnUpdate += (s, e) =>
                    {
                        if (s.EP >= 1)
                            s.EP -= 1;
                        else
                            SkillHandler.RemoveAddition(s, "P_瘴气兵装");
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, s, true);
                    };
                    SkillHandler.ApplyAddition(caster, skill);
                }
            }
        }
    }
}
