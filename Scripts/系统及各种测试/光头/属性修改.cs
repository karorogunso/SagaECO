
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap;
using SagaMap.Skill;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000030 : Event
    {
        public S910000030()
        {
            this.EventID = 910000030;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            if (Select(pc, "选择！！", "", "切换阵营", "变成BOSS属性") == 1)
            {
                pc.Mode = PlayerMode.KNIGHT_SOUTH;
                map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, pc, true);
            }
            else
            {
                TakeItem(pc, 910000030, 1);
                pc.TInt["临时HP"] = 500000;
                pc.TInt["临时MP"] = 5000;
                pc.TInt["临时SP"] = 3000;
                pc.TInt["临时ATK"] = 1500;
                pc.TInt["临时MATK"] = 1500;
                SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                pc.HP = pc.MaxHP;
                pc.MP = pc.MaxMP;
                pc.SP = pc.MaxSP;
                

                SagaDB.Skill.Skill skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(31040, 1);
                pc.Skills.Add(31040, skill);
                skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(31041, 1);
                pc.Skills.Add(31041, skill);
                skill = SagaDB.Skill.SkillFactory.Instance.GetSkill(31043, 1);
                pc.Skills.Add(31043, skill);
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();

                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);
                pc.Size = 1500;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_SIZE_UPDATE, null, pc, true);

            }
        }
    }
}

