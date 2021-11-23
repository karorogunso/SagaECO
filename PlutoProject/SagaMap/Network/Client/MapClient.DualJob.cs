using SagaDB.Actor;
using SagaDB.DualJob;
using SagaLib;
using SagaMap.Packets.Server;
using SagaMap.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public bool changeDualJob = false;

        public void OnDualChangeRequest(Packets.Client.CSMG_DUALJOB_CHANGE_CONFIRM p)
        {
            this.Character.DualJobID = p.DualJobID;

            if (!this.Character.PlayerDualJobList.ContainsKey(this.Character.DualJobID))
            {
                var dualjobinfo = new PlayerDualJobInfo();
                dualjobinfo.DualJobExp = 0;
                dualjobinfo.DualJobID = this.Character.DualJobID;
                dualjobinfo.DualJobLevel = 1;
                this.Character.PlayerDualJobList.Add(this.Character.DualJobID, dualjobinfo);
            }

            if (Character.PlayerDualJobList[Character.DualJobID].DualJobLevel <= 0)
            {
                Character.PlayerDualJobList[Character.DualJobID].DualJobLevel = 1;
                Character.PlayerDualJobList[Character.DualJobID].DualJobExp = 0;
            }

            List<SagaDB.Skill.Skill> skills = new List<SagaDB.Skill.Skill>();
            var ids = p.DualJobSkillList;
            foreach (var item in ids)
            {
                if (item != 0)
                {
                    var sks = DualJobSkillFactory.Instance.items[this.Character.DualJobID].Where(x => x.DualJobID == this.Character.DualJobID && x.SkillID == item && x.LearnSkillLevel.Where(y => y <= Character.DualJobLevel).Count() > 0).FirstOrDefault();
                    if (sks != null)
                    {
                        var sk = DualJobSkillFactory.Instance.items[this.Character.DualJobID].FirstOrDefault(x => x.SkillID == item);
                        var lv = sk.LearnSkillLevel.Count(x => x > 0 && x <= this.Character.DualJobLevel);
                        skills.Add(SagaDB.Skill.SkillFactory.Instance.GetSkill(item, (byte)lv));
                    }
                }
            }
            this.Character.DualJobSkill = skills;
            this.Character.DualJobLevel = this.Character.PlayerDualJobList[this.Character.DualJobID].DualJobLevel;

            MapServer.charDB.SaveDualJobInfo(this.Character, true);

            SendPlayerInfo();
            SendPlayerDualJobSkillList();

            SkillHandler.Instance.CastPassiveSkills(this.Character, true);

            this.changeDualJob = false;

            SSMG_DUALJOB_SET_DUALJOB_INFO pi = new SSMG_DUALJOB_SET_DUALJOB_INFO();
            pi.Result = true;
            pi.RetType = 0x00;
            this.netIO.SendPacket(pi);
        }


        public void SendPlayerDualJobInfo()
        {
            SSMG_DUALJOB_INFO_SEND p2 = new SSMG_DUALJOB_INFO_SEND();
            p2.JobList = new byte[25] { 0xC, 0x0, 0x1, 0x0, 0x2, 0x0, 0x3, 0x0, 0x4, 0x0, 0x5, 0x0, 0x6, 0x0, 0x7, 0x0, 0x8, 0x0, 0x9, 0x0, 0xa, 0x0, 0xb, 0x0, 0xc };
            var levels = new byte[13];
            levels[0] = 0x0C;
            for (byte i = 1; i <= 0x0C; i++)
            {
                if (this.Character.PlayerDualJobList.ContainsKey(i))
                    levels[i] = this.Character.PlayerDualJobList[i].DualJobLevel;
                else
                    levels[i] = 0;
            }
            p2.JobLevel = levels;
            this.netIO.SendPacket(p2);
        }

        public void SendPlayerDualJobSkillList()
        {
            SSMG_DUALJOB_SKILL_SEND p1 = new SSMG_DUALJOB_SKILL_SEND();
            p1.Skills = this.Character.DualJobSkill;
            p1.SkillLevels = this.Character.DualJobSkill;

            this.netIO.SendPacket(p1);
        }

        public void OnDualJobWindowClose()
        {
            this.changeDualJob = false;
        }

        /// <summary>
        /// 打开副职转职窗口
        /// </summary>
        /// <param name="pc">角色对象</param>
        /// <param name="ChangeDualJob">是否允许更改副职系统(是否为习得副职系统)</param>
        public void OpenDualJobChangeUI(ActorPC pc, bool ChangeDualJob)
        {
            SSMG_DUALJOB_WINDOW_OPEN p = new SSMG_DUALJOB_WINDOW_OPEN();
            if (ChangeDualJob)
                p.CanChange = 0x01;
            else
                p.CanChange = 0x00;

            p.SetDualJobList(0x0C, new byte[] { 0x0, 0x1, 0x0, 0x2, 0x0, 0x3, 0x0, 0x4, 0x0, 0x5, 0x0, 0x6, 0x0, 0x7, 0x0, 0x8, 0x0, 0x9, 0x0, 0xa, 0x0, 0xb, 0x0, 0xc });

            var dualjoblevel = new byte[12];
            for (int i = 0; i < dualjoblevel.Length; i++)
            {
                if (pc.PlayerDualJobList.ContainsKey(byte.Parse((i + 1).ToString())))
                    dualjoblevel[i] = pc.PlayerDualJobList[byte.Parse((i + 1).ToString())].DualJobLevel;
                else
                    dualjoblevel[i] = 0;
            }
            p.DualJobLevel = dualjoblevel;
            p.CurrentDualJobSerial = pc.DualJobID;
            if (ChangeDualJob)
                p.CurrentSkillList = pc.DualJobSkill;
            else
                p.CurrentSkillList = new List<SagaDB.Skill.Skill>();

            this.netIO.SendPacket(p);
        }
    }
}
