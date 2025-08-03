
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
using SagaMap.Skill.Additions.Global;
namespace SagaMap.Skill.SkillDefinations.Maestro
{
    /// <summary>
    /// レールガン
    /// </summary>
    public class LimitExceed : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {

            return -54;//已经写好了逻辑,但问题太多,放弃治疗,先封印
            //ActorPet pet = SkillHandler.Instance.GetPet(sActor);
            //if (pet == null)
            //{
            //    return -54;//需回傳"需裝備寵物"
            //}
            //if (SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            //{
            //    return 0;
            //}
            //return -54;//需回傳"需裝備寵物"
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 180000;
            DefaultBuff skill = new DefaultBuff(args.skill, sActor, "LimitExceed", lifetime);
            skill.OnAdditionStart += this.StartEventHandler;
            skill.OnAdditionEnd += this.EndEventHandler;
            skill.OnCheckValid += this.ValidCheck;
            SkillHandler.ApplyAddition(sActor, skill);
        }
        #endregion

        void ValidCheck(ActorPC pc, Actor dActor, out int result)
        {
            result = TryCast(pc, dActor, null);
        }

        void StartEventHandler(Actor actor, DefaultBuff skill)
        {

            float trans_am_atk_up = 0.3f + 0.1f * skill.skill.Level;
            float trans_am_defadd_up = 0.3f + 0.1f * skill.skill.Level;
            float trans_am_def_up = 0.3f + 0.1f * skill.skill.Level;
            if (actor is ActorPC)
            {
                ActorPC pc = actor as ActorPC;

                if (pc.Skills3.ContainsKey(2489) || pc.DualJobSkill.Exists(x => x.ID == 2489))
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2489))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2489).Level;

                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(2489))
                        mainlv = pc.Skills3[2489].Level;

                    int maxlv = Math.Max(duallv, mainlv);
                    float atkendup = 0;
                    switch (skill.skill.Level)
                    {
                        case 1:
                            atkendup = 0.8f + 0.1f * maxlv;
                            trans_am_atk_up += atkendup;
                            break;
                        case 2:
                            atkendup = 1.05f + 0.1f * maxlv;
                            trans_am_atk_up += atkendup;
                            break;
                        case 3:
                            atkendup = 1.3f + 0.1f * maxlv;
                            trans_am_atk_up += atkendup;
                            break;
                        case 4:
                            atkendup = 1.8f + 0.1f * maxlv;
                            trans_am_atk_up += atkendup;
                            break;
                        case 5:
                            atkendup = 2.3f + 0.1f * maxlv;
                            trans_am_atk_up += atkendup;
                            break;
                    }
                }

                if (pc.Skills3.ContainsKey(2500) || pc.DualJobSkill.Exists(x => x.ID == 2500))
                {
                    var duallv = 0;
                    if (pc.DualJobSkill.Exists(x => x.ID == 2500))
                        duallv = pc.DualJobSkill.FirstOrDefault(x => x.ID == 2500).Level;

                    var mainlv = 0;
                    if (pc.Skills3.ContainsKey(2500))
                        mainlv = pc.Skills3[2500].Level;

                    int maxlv = Math.Max(duallv, mainlv);
                    float defaddendup = 0;
                    float defendup = 0;
                    switch (skill.skill.Level)
                    {
                        case 1:
                            defaddendup = 0.8f + 0.1f * maxlv;
                            defendup = 0.32f + 0.1f * maxlv;
                            trans_am_defadd_up += defaddendup;
                            break;
                        case 2:
                            defaddendup = 1.05f + 0.1f * maxlv;
                            defendup = 0.34f + 0.1f * maxlv;
                            trans_am_defadd_up += defaddendup;
                            break;
                        case 3:
                            defaddendup = 1.3f + 0.1f * maxlv;
                            defendup = 0.36f + 0.1f * maxlv;
                            trans_am_defadd_up += defaddendup;
                            break;
                        case 4:
                            defaddendup = 1.8f + 0.1f * maxlv;
                            defendup = 0.38f + 0.1f * maxlv;
                            trans_am_defadd_up += defaddendup;
                            break;
                        case 5:
                            defaddendup = 2.3f + 0.1f * maxlv;
                            defendup = 0.4f + 0.1f * maxlv;
                            trans_am_defadd_up += defaddendup;
                            break;
                    }
                }


            }
            int minatk1up = (int)(actor.Status.min_atk1 * trans_am_atk_up);
            int minatk2up = (int)(actor.Status.min_atk2 * trans_am_atk_up);
            int minatk3up = (int)(actor.Status.min_atk3 * trans_am_atk_up);
            int maxatk1up = (int)(actor.Status.max_atk1 * trans_am_atk_up);
            int maxatk2up = (int)(actor.Status.max_atk2 * trans_am_atk_up);
            int maxatk3up = (int)(actor.Status.max_atk3 * trans_am_atk_up);
            int defaddup = (int)(actor.Status.def_add * trans_am_defadd_up);
            int defup = (int)(actor.Status.def * trans_am_def_up);
            if (skill.Variable.ContainsKey("Robotminatk1Up"))
                skill.Variable.Remove("Robotminatk1Up");
            skill.Variable.Add("Robotminatk1Up", minatk1up);
            actor.Status.min_atk1_skill += (short)minatk1up;
            if (skill.Variable.ContainsKey("Robotminatk2Up"))
                skill.Variable.Remove("Robotminatk2Up");
            skill.Variable.Add("Robotminatk2Up", minatk2up);
            actor.Status.min_atk2_skill += (short)minatk2up;
            if (skill.Variable.ContainsKey("Robotminatk3Up"))
                skill.Variable.Remove("Robotminatk3Up");
            skill.Variable.Add("Robotminatk3Up", minatk3up);
            actor.Status.min_atk3_skill += (short)minatk3up;

            if (skill.Variable.ContainsKey("Robotmaxatk1Up"))
                skill.Variable.Remove("Robotmaxatk1Up");
            skill.Variable.Add("Robotmaxatk1Up", maxatk1up);
            actor.Status.max_atk1_skill += (short)maxatk1up;
            if (skill.Variable.ContainsKey("Robotmaxatk2Up"))
                skill.Variable.Remove("Robotmaxatk2Up");
            skill.Variable.Add("Robotmaxatk2Up", maxatk2up);
            actor.Status.max_atk2_skill += (short)maxatk2up;
            if (skill.Variable.ContainsKey("Robotmaxatk3Up"))
                skill.Variable.Remove("Robotmaxatk3Up");
            skill.Variable.Add("Robotmaxatk3Up", maxatk3up);
            actor.Status.max_atk3_skill += (short)maxatk3up;

            if (skill.Variable.ContainsKey("RobotdefaddUp"))
                skill.Variable.Remove("RobotdefaddUp");
            skill.Variable.Add("RobotdefaddUp", defaddup);
            actor.Status.def_add_skill += (short)defaddup;

            if (skill.Variable.ContainsKey("RobotdefUp"))
                skill.Variable.Remove("RobotdefUp");
            skill.Variable.Add("RobotdefUp", defup);
            actor.Status.def_skill += (short)defup;
            actor.Buff.三转2足ATKUP = true;
            actor.Buff.三转铁匠2足DEFUP = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler(Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill -= (short)(skill.Variable["Robotminatk1Up"]);
            actor.Status.min_atk2_skill -= (short)(skill.Variable["Robotminatk2Up"]);
            actor.Status.min_atk3_skill -= (short)(skill.Variable["Robotminatk3Up"]);
            actor.Status.max_atk1_skill -= (short)(skill.Variable["Robotmaxatk1Up"]);
            actor.Status.max_atk2_skill -= (short)(skill.Variable["Robotmaxatk2Up"]);
            actor.Status.max_atk3_skill -= (short)(skill.Variable["Robotmaxatk3Up"]);

            actor.Status.def_add_skill -= (short)(skill.Variable["RobotdefaddUp"]);
            actor.Status.def_skill -= (short)(skill.Variable["RobotdefUp"]);
            actor.Buff.三转2足ATKUP = false;
            actor.Buff.三转铁匠2足DEFUP = false;

            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
            ActorPet pet = SkillHandler.Instance.GetPet(actor);
            if (pet != null && SkillHandler.Instance.CheckMobType(pet, "MACHINE_RIDE_ROBOT"))
            {
                int[] lifetime = { 0, 50000, 70000, 80000, 100000, 100000 };
                DefaultBuff skill2 = new DefaultBuff(skill.skill, actor, "LimitExceedDown", lifetime[skill.skill.Level]);
                skill.OnAdditionStart += this.StartEventHandler2;
                skill.OnAdditionEnd += this.EndEventHandler2;
                skill.OnCheckValid += this.ValidCheck;
                SkillHandler.ApplyAddition(actor, skill);
            }

        }

        void StartEventHandler2(Actor actor, DefaultBuff skill)
        {
            float trans_am_atk_down = new float[] { 0, 0.2f, 0.35f, 0.45f, 0.65f, 0.75f }[skill.skill.Level];
            float trans_am_defadd_down = 0.1f * skill.skill.Level;//没有wiki资料，暂定为下降10%每等级
            float trans_am_def_down = 0.1f * skill.skill.Level;

            int minatk1down = (int)(actor.Status.min_atk1 * trans_am_atk_down);
            int minatk2down = (int)(actor.Status.min_atk2 * trans_am_atk_down);
            int minatk3down = (int)(actor.Status.min_atk3 * trans_am_atk_down);
            int maxatk1down = (int)(actor.Status.max_atk1 * trans_am_atk_down);
            int maxatk2down = (int)(actor.Status.max_atk2 * trans_am_atk_down);
            int maxatk3down = (int)(actor.Status.max_atk3 * trans_am_atk_down);
            int defadddown = (int)(actor.Status.def_add * trans_am_defadd_down);
            int defdown = (int)(actor.Status.def * trans_am_def_down);
            if (skill.Variable.ContainsKey("Robotminatk1down"))
                skill.Variable.Remove("Robotminatk1down");
            skill.Variable.Add("Robotminatk1down", minatk1down);
            actor.Status.min_atk1_skill -= (short)minatk1down;
            if (skill.Variable.ContainsKey("Robotminatk2down"))
                skill.Variable.Remove("Robotminatk2down");
            skill.Variable.Add("Robotminatk2down", minatk2down);
            actor.Status.min_atk2_skill -= (short)minatk2down;
            if (skill.Variable.ContainsKey("Robotminatk3down"))
                skill.Variable.Remove("Robotminatk3down");
            skill.Variable.Add("Robotminatk3down", minatk3down);
            actor.Status.min_atk3_skill -= (short)minatk3down;

            if (skill.Variable.ContainsKey("Robotmaxatk1down"))
                skill.Variable.Remove("Robotmaxatk1down");
            skill.Variable.Add("Robotmaxatk1down", maxatk1down);
            actor.Status.max_atk1_skill += (short)maxatk1down;
            if (skill.Variable.ContainsKey("Robotmaxatk2down"))
                skill.Variable.Remove("Robotmaxatk2down");
            skill.Variable.Add("Robotmaxatk2down", maxatk2down);
            actor.Status.max_atk2_skill -= (short)maxatk2down;
            if (skill.Variable.ContainsKey("Robotmaxatk3down"))
                skill.Variable.Remove("Robotmaxatk3down");
            skill.Variable.Add("Robotmaxatk3down", maxatk3down);
            actor.Status.max_atk3_skill -= (short)maxatk3down;

            if (skill.Variable.ContainsKey("Robotdefadddown"))
                skill.Variable.Remove("Robotdefadddown");
            skill.Variable.Add("Robotdefadddown", defadddown);
            actor.Status.def_add_skill -= (short)defadddown;

            if (skill.Variable.ContainsKey("Robotdefdown"))
                skill.Variable.Remove("Robotdefdown");
            skill.Variable.Add("Robotdefdown", defdown);
            actor.Status.def_skill -= (short)defdown;

            actor.Buff.RobotUnknowStateDown3RD = true;
            actor.Buff.三转机器人UNKNOWS = true;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
        void EndEventHandler2(Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill += (short)(skill.Variable["Robotminatk1down"]);
            actor.Status.min_atk2_skill += (short)(skill.Variable["Robotminatk2down"]);
            actor.Status.min_atk3_skill += (short)(skill.Variable["Robotminatk3down"]);
            actor.Status.max_atk1_skill += (short)(skill.Variable["Robotmaxatk1down"]);
            actor.Status.max_atk2_skill += (short)(skill.Variable["Robotmaxatk2down"]);
            actor.Status.max_atk3_skill += (short)(skill.Variable["Robotmaxatk3down"]);

            actor.Status.def_add_skill += (short)(skill.Variable["Robotdefadddown"]);
            actor.Status.def_skill += (short)(skill.Variable["Robotdefdown"]);
            actor.Buff.RobotUnknowStateDown3RD = false;
            actor.Buff.三转机器人UNKNOWS = false;
            Manager.MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, actor, true);
        }
    }
}