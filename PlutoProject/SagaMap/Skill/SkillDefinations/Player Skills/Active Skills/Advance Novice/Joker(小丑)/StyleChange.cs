using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// ホーリーフェザー
    /// </summary>
    public class StyleChange : ISkill
    {
        #region ISkill Members
        Actor me;
        public int TryCast(ActorPC pc, Actor dActor, SkillArg args)
        {
            return 0;
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {

            me = sActor;
            Actor realdActor = SkillHandler.Instance.GetPossesionedActor((ActorPC)sActor);
            SkillHandler.RemoveAddition(realdActor, "DivineProtection");
            SkillHandler.RemoveAddition(realdActor, "StyleChange");
            DefaultBuff skill = new DefaultBuff(args.skill, realdActor, "StyleChange", 420000);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            SkillHandler.ApplyAddition(realdActor, skill);

        }
        void StartEventHandler(Actor actor, DefaultBuff skill)
        {
            short status_add = 0;
            if (me.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                if (pc.Skills3.ContainsKey(3410))
                {
                    status_add += new short[] { 0, 1, 2, 3, 5, 8 }[pc.Skills3[3410].Level];
                }
            }
            switch (skill.skill.Level)
            {
                case 1:
                    int mp_add = (int)(actor.MaxMP * 0.3f);
                    if (skill.Variable.ContainsKey("StyleChange_mp"))
                        skill.Variable.Remove("StyleChange_mp");
                    skill.Variable.Add("StyleChange_mp", (short)mp_add);
                    actor.Status.mp_skill += (short)mp_add;

                    if (skill.Variable.ContainsKey("StyleChange_dex"))
                        skill.Variable.Remove("StyleChange_dex");
                    skill.Variable.Add("StyleChange_dex", (short)(5 + status_add));
                    actor.Status.dex_skill += (short)(5 + status_add);

                    if (skill.Variable.ContainsKey("StyleChange_int"))
                        skill.Variable.Remove("StyleChange_int");
                    skill.Variable.Add("StyleChange_int", (short)(5 + status_add));
                    actor.Status.int_skill += (short)(5 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_vit"))
                        skill.Variable.Remove("StyleChange_vit");
                    skill.Variable.Add("StyleChange_vit", (short)(2 + status_add));
                    actor.Status.vit_skill += (short)(2 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_mag"))
                        skill.Variable.Remove("StyleChange_mag");
                    skill.Variable.Add("StyleChange_mag", (short)(6 + status_add));
                    actor.Status.mag_skill += (short)(6 + status_add);
                    break;
                case 2:
                    int sp_add = (int)(actor.MaxMP * 0.3f);
                    if (skill.Variable.ContainsKey("StyleChange_sp"))
                        skill.Variable.Remove("StyleChange_sp");
                    skill.Variable.Add("StyleChange_sp", (short)sp_add);
                    actor.Status.sp_skill += (short)sp_add;

                    if (skill.Variable.ContainsKey("StyleChange_str"))
                        skill.Variable.Remove("StyleChange_str");
                    skill.Variable.Add("StyleChange_str", (short)(5 + status_add));
                    actor.Status.str_skill += (short)(5 + status_add);

                    if (skill.Variable.ContainsKey("StyleChange_int"))
                        skill.Variable.Remove("StyleChange_int");
                    skill.Variable.Add("StyleChange_int", (short)(5 + status_add));
                    actor.Status.int_skill += (short)(5 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_vit"))
                        skill.Variable.Remove("StyleChange_vit");
                    skill.Variable.Add("StyleChange_vit", (short)(3 + status_add));
                    actor.Status.vit_skill += (short)(3 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_agi"))
                        skill.Variable.Remove("StyleChange_agi");
                    skill.Variable.Add("StyleChange_agi", (short)(5 + status_add));
                    actor.Status.agi_skill += (short)(5 + status_add);
                    break;
                case 3:
                    int hp_add = (int)(actor.MaxHP * 0.3f);
                    if (skill.Variable.ContainsKey("StyleChange_hp"))
                        skill.Variable.Remove("StyleChange_hp");
                    skill.Variable.Add("StyleChange_hp", (short)hp_add);
                    actor.Status.hp_skill += (short)hp_add;

                    if (skill.Variable.ContainsKey("StyleChange_str"))
                        skill.Variable.Remove("StyleChange_str");
                    skill.Variable.Add("StyleChange_str", (short)(5 + status_add));
                    actor.Status.str_skill += (short)(5 + status_add);

                    if (skill.Variable.ContainsKey("StyleChange_dex"))
                        skill.Variable.Remove("StyleChange_dex");
                    skill.Variable.Add("StyleChange_dex", (short)(5 + status_add));
                    actor.Status.dex_skill += (short)(5 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_vit"))
                        skill.Variable.Remove("StyleChange_vit");
                    skill.Variable.Add("StyleChange_vit", (short)(5 + status_add));
                    actor.Status.vit_skill += (short)(5 + status_add);
                    if (skill.Variable.ContainsKey("StyleChange_agi"))
                        skill.Variable.Remove("StyleChange_agi");
                    skill.Variable.Add("StyleChange_agi", (short)(3 + status_add));
                    actor.Status.agi_skill += (short)(3 + status_add);
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            switch (skill.skill.Level)
            {
                case 1:
                    actor.Status.mp_skill -= (short)skill.Variable["StyleChange_mp"];
                    actor.Status.dex_skill -= (short)skill.Variable["StyleChange_dex"];
                    actor.Status.int_skill -= (short)skill.Variable["StyleChange_int"];
                    actor.Status.vit_skill -= (short)skill.Variable["StyleChange_vit"];
                    actor.Status.mag_skill -= (short)skill.Variable["StyleChange_mag"];
                    break;
                case 2:
                    actor.Status.sp_skill -= (short)skill.Variable["StyleChange_sp"];
                    actor.Status.str_skill -= (short)skill.Variable["StyleChange_str"];
                    actor.Status.int_skill -= (short)skill.Variable["StyleChange_int"];
                    actor.Status.vit_skill -= (short)skill.Variable["StyleChange_vit"];
                    actor.Status.agi_skill -= (short)skill.Variable["StyleChange_agi"];
                    break;
                case 3:
                    actor.Status.hp_skill -= (short)skill.Variable["StyleChange_hp"];
                    actor.Status.str_skill -= (short)skill.Variable["StyleChange_str"];
                    actor.Status.dex_skill -= (short)skill.Variable["StyleChange_dex"];
                    actor.Status.vit_skill -= (short)skill.Variable["StyleChange_vit"];
                    actor.Status.agi_skill -= (short)skill.Variable["StyleChange_agi"];
                    break;
            }
            Map map = Manager.MapManager.Instance.GetMap(actor.MapID);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, actor, true);
        }
        #endregion
    }
}
