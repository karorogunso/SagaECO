using System;
using System.Collections.Generic;
using System.Text;

using SagaLib;
using SagaDB.Skill;

namespace SagaMap.Packets.Server
{
    public class SSMG_SKILL_LIST : Packet
    {
        public SSMG_SKILL_LIST()
        {
            this.data = new byte[8];
            this.offset = 2;
            this.ID = 0x0226;   
        }

        /// <summary>
        /// Set skills
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="job">0 for basic, 1 for expert, 2 for technical</param>
        public void Skills(List<SagaDB.Skill.Skill> list, byte job, SagaDB.Actor.PC_JOB job2, bool ifDominion,SagaDB.Actor.ActorPC pc)
        {
            if (Configuration.Instance.Version >= SagaLib.Version.Saga11)
                this.data = new byte[8 + list.Count * 2 + list.Count * 3];
            else
                this.data = new byte[7 + list.Count * 2 + list.Count * 3];
            this.ID = 0x0226;
            for (int i = 0; i < list.Count; i++)
            {
                SagaDB.Skill.Skill skill = list[i];
                byte jobLv = 1;
                if (SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.none).ContainsKey(skill.ID))
                {
                    jobLv = SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.none)[skill.ID];
                }
                this.PutByte((byte)list.Count, 2);
                this.PutUShort((ushort)skill.ID, (ushort)(3 + i * 2));
                this.PutByte((byte)list.Count, (ushort)(3 + list.Count * 2));
                if (pc.DominionJobLevel < jobLv && ifDominion)
                    this.PutByte(0, (ushort)(4 + list.Count * 2 + i));
                else
                    this.PutByte(skill.Level, (ushort)(4 + list.Count * 2 + i));
                this.PutByte((byte)list.Count, (ushort)(4 + list.Count * 3));
                this.PutByte((byte)list.Count, (ushort)(5 + list.Count * 4));
                if (SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.none).ContainsKey(skill.ID))
                {
                    jobLv = SkillFactory.Instance.CheckSkillList(pc, SkillFactory.SkillPaga.none)[skill.ID];
                    SagaDB.Skill.Skill skill2 = SkillFactory.Instance.GetSkill(skill.ID, skill.Level);
                    if (skill2.JobLv == 0)
                        this.PutByte(jobLv, (ushort)(6 + list.Count * 4 + i));
                    else
                        this.PutByte(skill2.JobLv, (ushort)(6 + list.Count * 4 + i));
                }
                else
                    this.PutByte(1, (ushort)(6 + list.Count * 4 + i));
            }
            this.PutByte(job, (ushort)(6 + list.Count * 5));
            /*
            this.data = new byte[43];
            byte[] s = { 0x02, 0x26, 0x07, 0x03, 0xD6, 0x04, 0x4C, 0x09, 0xB4, 0x09, 0xC2, 0x09, 0xC7, 0x0D, 0x16, 0x0D, 0x22, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x03, 0x0D, 0x14, 0x17, 0x19, 0x06, 0x0A, 0x03, 0x00 };
            this.data = s;//*/
        }
    }
}

