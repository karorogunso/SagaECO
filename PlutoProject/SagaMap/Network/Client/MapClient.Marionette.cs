using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Actor;
using SagaDB.Marionette;
using SagaDB.Skill;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Skill;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public void MarionetteActivate(uint marionetteID)
        {
            MarionetteActivate(marionetteID, true, true);
        }

        public void MarionetteActivate(uint marionetteID, bool delay,bool duration)
        {
            Marionette marionette = MarionetteFactory.Instance[marionetteID];
            if (marionette != null)
            {
                Tasks.PC.Marionette task = new SagaMap.Tasks.PC.Marionette(this, marionette.Duration);
                if (this.Character.Tasks.ContainsKey("Marionette") && duration)
                {
                    MarionetteDeactivate();
                    this.Character.Tasks["Marionette"].Deactivate();
                    this.Character.Tasks.Remove("Marionette");
                }
                if (!duration && this.Character.Marionette != null)
                {
                    foreach (uint i in this.Character.Marionette.skills)
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(i, 1);
                        if (skill != null)
                        {
                            if (this.Character.Skills.ContainsKey(i))
                            {
                                this.Character.Skills.Remove(i);
                            }
                        }
                    }
                    SkillHandler.Instance.CastPassiveSkills(this.Character);            
                }
                if (!this.Character.Tasks.ContainsKey("Marionette"))
                {
                    this.Character.Tasks.Add("Marionette", task);
                    task.Activate();
                }
                if (delay)
                {
                    if (!this.Character.Status.Additions.ContainsKey("MarioTimeUp"))
                        this.Character.NextMarionetteTime = DateTime.Now + new TimeSpan(0, 0, marionette.Delay);
                    else
                        this.Character.NextMarionetteTime = DateTime.Now + new TimeSpan(0, 0, (int)(marionette.Delay * 0.6f));
                }
                this.Character.Marionette = marionette;
                SendCharInfoUpdate();
                foreach (uint i in marionette.skills)
                {
                    SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(i, 1);
                    if (skill != null)
                    {
                        if (!this.Character.Skills.ContainsKey(i))
                        {
                            skill.NoSave = true;
                            this.Character.Skills.Add(i, skill);
                            if (!skill.BaseData.active)
                            {
                                SkillArg arg = new SkillArg();
                                arg.skill = skill;
                                SkillHandler.Instance.SkillCast(this.Character, this.Character, arg);
                            }
                        }
                    }
                }
                PC.StatusFactory.Instance.CalcStatus(this.Character);
                SendPlayerInfo();
            }
        }

        public void MarionetteDeactivate()
        {
            MarionetteDeactivate(false);
        }

        public void MarionetteDeactivate(bool disconnecting)
        {
            if (this.Character.Marionette == null)
                return;
            Marionette marionette = this.Character.Marionette;
            this.Character.Marionette = null;
            if (!disconnecting) SendCharInfoUpdate();
            foreach (uint i in marionette.skills)
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(i, 1);
                if (skill != null)
                {
                    if (this.Character.Skills.ContainsKey(i))
                    {
                        this.Character.Skills.Remove(i);
                    }
                }
            }
            SkillHandler.Instance.CastPassiveSkills(this.Character);
            PC.StatusFactory.Instance.CalcStatus(this.Character);
            if (!disconnecting)
            {
                SendPlayerInfo();
                SendMotion(MotionType.JOY, 0);
            }
        }
    }
}
