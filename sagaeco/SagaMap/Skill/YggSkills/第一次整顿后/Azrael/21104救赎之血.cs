using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
using SagaLib;

namespace SagaMap.Skill.SkillDefinations
{
    public partial class S21104 : ISkill
    {
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            if (!pc.Status.Additions.ContainsKey("P_瘴气兵装"))
                return -2;
            if (pc.Status.Additions.ContainsKey("瘴气兵装CD"))
                return -30;
            return 0;
        }

        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            //BUFF所需基础设置
            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            short 影响范围 = 500;
            float 治疗效果 = 3f + level * 1f;

            //显示特效、附加CD及移除瘴气兵装 
            //TODO:增加释放特效
            SkillHandler.ApplyAddition(sActor, new OtherAddition(null, sActor, "瘴气兵装CD", 10000));
            SkillHandler.RemoveAddition(sActor, "P_瘴气兵装");

            //附加效果
            List<Actor> targets = map.GetPlayersArea(sActor, 影响范围, true);
            targets.ForEach(a =>
            {
                int damage = SkillHandler.Instance.CalcDamage(false, sActor, a, null, SkillHandler.DefType.IgnoreAll, Elements.Holy, 50, -治疗效果);
                SkillHandler.Instance.ShowEffectOnActor(a, 5235);
                a.HP = (uint)(a.HP - damage);
                if (a.HP > a.MaxHP)
                    a.HP = a.MaxHP;
                SkillHandler.Instance.ShowVessel(a, damage);
                List<string> ss = new List<string>();
                if (level >= 2)//解除弱化
                {
                    if (a.Status.Additions.ContainsKey("STRDOWN")) ss.Add("STRDOWN");
                    if (a.Status.Additions.ContainsKey("MAGDOWN")) ss.Add("MAGDOWN");
                    if (a.Status.Additions.ContainsKey("INTDOWN")) ss.Add("INTDOWN");
                    if (a.Status.Additions.ContainsKey("AGIDOWN")) ss.Add("AGIDOWN");
                    if (a.Status.Additions.ContainsKey("DEXDOWN")) ss.Add("DEXDOWN");
                    if (a.Status.Additions.ContainsKey("VITDOWN")) ss.Add("VITDOWN");
                }
                if (level >= 3)//解除异常
                {
                    if (a.Status.Additions.ContainsKey("Confuse")) ss.Add("Confuse");
                    if (a.Status.Additions.ContainsKey("Frosen")) ss.Add("Frosen");
                    if (a.Status.Additions.ContainsKey("Paralyse")) ss.Add("Paralyse");
                    if (a.Status.Additions.ContainsKey("Silence")) ss.Add("Silence");
                    if (a.Status.Additions.ContainsKey("Sleep")) ss.Add("Sleep");
                    if (a.Status.Additions.ContainsKey("Stone")) ss.Add("Stone");
                    if (a.Status.Additions.ContainsKey("Stun")) ss.Add("Stun");
                    if (a.Status.Additions.ContainsKey("鈍足")) ss.Add("鈍足");
                    if (a.Status.Additions.ContainsKey("冰棍的冻结")) ss.Add("冰棍的冻结");
                }
                if (ss.Count > 0)
                    ss.ForEach(buff => { SkillHandler.RemoveAddition(a, buff); });
            });
        }
    }
}
