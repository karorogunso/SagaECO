
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaMap.Skill;
using SagaMap.Network.Client;
namespace SagaScript.M30210000
{
    public class S80000012 : Event
    {
        public S80000012()
        {
            this.EventID = 80000012;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要做什么呢？", "", "转职（第一次免费，附带洗素质点）", "洗素质点(第一次免费，7,000G)", "洗技能点(第一次免费，7,000G)", "离开"))
            {
                case 1:
                    转职服務(pc);

                    break;
                case 2:
                    if (pc.CInt["洗素质点第一次免费2"] != 1)
                    {
                        Say(pc, 131, "那么，第一次免费哦", "水琴");
                        pc.CInt["洗素质点第一次免费2"] = 1;
                        洗素质点(pc);
                        return;
                    }
                    if (pc.Gold >= 7000)
                    {
                        pc.Gold -= 7000;
                        洗素质点(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够哦？", "水琴");
                        return;
                    }
                    break;
                case 3:
                    if (pc.CInt["洗技能点第一次免费5"] != 1)
                    {
                        Say(pc, 131, "那么，第一次免费哦", "水琴");
                        pc.CInt["洗技能点第一次免费5"] = 1;
                        洗技能点(pc);
                        return;
                    }
                    if (pc.JobLevel3 < 10)
                    {
                        Say(pc, 131, "等级会不会有点低呢？$R$R要求10级", "水琴");
                        return;
                    }
                    if (pc.Gold >= 7000)
                    {
                        pc.Gold -= 7000;
                        洗技能点(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够哦？", "水琴");
                        return;
                    }
                    break;
            }

        }
        void 洗技能点(ActorPC pc)
        {
            int questsbouns = 0;
            if (pc.CInt["天天1任务技能点获得"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级雲切技能任务》获取了1点");
            }
            if (pc.CInt["黑暗料理1任务技能点获得"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级圣疗术，黑暗料理》获取了1点");
            }
            if (pc.CInt["沙月技能点任务标记"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《沙月》获取了1点");
            }
            if (pc.CInt["羽川柠技能点任务标记"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《羽川柠》获取了1点");
            }
            if (MapClient.FromActorPC(pc).CheckTitle(30))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级袈裟斩学习》获取了1点");
                if (pc.CInt["进阶技能解锁11003"] != 1)
                {
                    pc.CInt["进阶技能解锁11003"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级袈裟斩】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(32))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级堕落者的救赎学习》获取了1点");
                if (pc.CInt["进阶技能解锁13103"] != 1)
                {
                    pc.CInt["进阶技能解锁13103"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级堕落者的救赎】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(33))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级刺心学习》获取了1点");
                if (pc.CInt["进阶技能解锁12109"] != 1)
                {
                    pc.CInt["进阶技能解锁12109"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级刺心】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(31))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级暴风雪学习》获取了1点");
                if (pc.CInt["进阶技能解锁14020"] != 1)
                {
                    pc.CInt["进阶技能解锁14020"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级暴风雪】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(70))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级纯白信仰学习》获取了1点");
                if (pc.CInt["进阶技能解锁13000"] != 1)
                {
                    pc.CInt["进阶技能解锁13000"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级纯白信仰】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(71))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级漆黑之魂学习》获取了1点");
                if (pc.CInt["进阶技能解锁13001"] != 1)
                {
                    pc.CInt["进阶技能解锁13001"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级漆黑之魂】了。");
                }
            }
            if (MapClient.FromActorPC(pc).CheckTitle(72))
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《4级变形术！学习》获取了1点");
                if (pc.CInt["进阶技能解锁14041"] != 1)
                {
                    pc.CInt["进阶技能解锁14041"] = 1;
                    SkillHandler.SendSystemMessage(pc, "因账号其他角色完成该称号，你可以领悟【4级变形术！】了。");
                }
            }

            if (pc.CInt["通天塔考古学家技能点获得"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《通天塔的考古学家》获取了1点");
            }
            if (pc.CInt["通天塔艾力克技能点获得"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《艾力克》获取了1点");
            }
            if (pc.AInt["序章·好奇的少女技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：阅读完《纳克德手稿·译本》序章获取了1点");
            }
            if (pc.AInt["北国搬运技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《北国搬运累积10次》获取了1点");
            }
            if (pc.AInt["西国搬运技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《西国搬运累积10次》获取了1点");
            }
            if (pc.AInt["南国搬运技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《南国搬运累积10次》获取了1点");
            }
            if (pc.AInt["南部矿区搬运完成技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《南部矿区搬运累积10次》获取了1点");
            }
            if (pc.AInt["西部渔场搬运完成技能点获取"] == 1)
            {
                questsbouns += 1;
                SkillHandler.SendSystemMessage(pc, "额外的技能点：通过《西部渔场搬运累积10次》获取了1点");
            }
            if (pc.Skills != null)
            {
                List<uint> ids = new List<uint>();
                foreach (var item in pc.Skills.Values)
                    ids.Add(item.ID);
                foreach (var item in ids)
                    pc.Skills.Remove(item);

            }
            if (pc.Skills2 != null)
            {
                List<uint> ids = new List<uint>();
                foreach (var item in pc.Skills2.Values)
                    ids.Add(item.ID);
                foreach (var item in ids)
                    pc.Skills2.Remove(item);

            }
            if (pc.Skills2_1 != null)
            {
                List<uint> ids = new List<uint>();
                foreach (var item in pc.Skills2_1.Values)
                    ids.Add(item.ID);
                foreach (var item in ids)
                    pc.Skills2_1.Remove(item);

            }
            if (pc.Skills2_2 != null)
            {
                List<uint> ids = new List<uint>();
                foreach (var item in pc.Skills2_2.Values)
                    ids.Add(item.ID);
                foreach (var item in ids)
                    pc.Skills2_2.Remove(item);

            }
            if (pc.Skills3 != null)
            {
                List<uint> ids = new List<uint>();
                foreach (var item in pc.Skills3.Values)
                    ids.Add(item.ID);
                foreach (var item in ids)
                    pc.Skills3.Remove(item);

            }
            pc.SkillPoint = (ushort)((pc.JobLevel3 - 1) + questsbouns);
            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendPlayerInfo();
            SagaMap.Skill.SkillHandler.Instance.CastPassiveSkills(pc);
            ShowEffect(pc, 4131);
            Wait(pc, 4000);
            Say(pc, 131, "技能恢復初始值了$R;");

        }
        void 洗素质点(ActorPC pc)
        {
            ResetStatusPoint(pc);
            ShowEffect(pc, 4131);
            Wait(pc, 4000);
            Say(pc, 131, "素质点恢復初始值了$R;");
        }
        void 转职服務(ActorPC pc)
        {
            int fee = 10000;
            if (pc.CInt["不是第一次更换职业"] == 1)
                Say(pc, 131, "你想更换职业吗？$R$R目前阶段一次需要10,000G哦。", "水琴");
            else
            {
                Say(pc, 131, "你想更换职业吗？$R$R第一次免费哦。", "水琴");
                fee = 0;
            }
            switch (Select(pc, "請選擇您要作為的職業(職業名暫定)", "", "勇者[原劍+騎]", "斥候[原賊+弓]", "魔导师[原wiz+元素]", "吟遊詩人[原光與暗]", "离开"))
            {

                case 1:
                    if (pc.Gold >= fee)
                    {
                        pc.Gold -= fee;
                        ChangePlayerJob(pc, PC_JOB.GLADIATOR);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已經成為『勇者』了。", "娜娜依");
                        PlaySound(pc, 4012, false, 100, 50);
                        if (pc.CInt["不是第一次更换职业"] != 1)
                            pc.CInt["不是第一次更换职业"] = 1;
                        洗素质点(pc);
                        SagaMap.Skill.SkillHandler.Instance.CastPassiveSkills(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够呢"); return;
                    }
                    break;
                case 2:
                    if (pc.Account.GMLevel < 200)
                    {
                        if (pc.CInt["斥候转职完成"] != 1)
                        {
                            Say(pc, 131, "你似乎没有达成转职要求呢"); return;
                        }
                    }
                    if (pc.Gold >= fee)
                    {
                        pc.Gold -= fee;
                        ChangePlayerJob(pc, PC_JOB.HAWKEYE);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已經成為『斥候』了。", "娜娜依");
                        PlaySound(pc, 4012, false, 100, 50);
                        if (pc.CInt["不是第一次更换职业"] != 1)
                            pc.CInt["不是第一次更换职业"] = 1;
                        洗素质点(pc);
                        SagaMap.Skill.SkillHandler.Instance.CastPassiveSkills(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够呢"); return;
                    }
                    break;
                case 3:
                    if (pc.Account.GMLevel < 200)
                    {
                        if (pc.CInt["魔导师转职完成"] != 1)
                        {
                            Say(pc, 131, "你似乎没有达成转职要求呢"); return;
                        }
                    }
                    if (pc.Gold >= fee)
                    {
                        pc.Gold -= fee;
                        pc.EP = 0;
                        ChangePlayerJob(pc, PC_JOB.ASTRALIST);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已經成為『魔导师』了。", "水琴");
                        PlaySound(pc, 4012, false, 100, 50);
                        if (pc.CInt["不是第一次更换职业"] != 1)
                            pc.CInt["不是第一次更换职业"] = 1;
                        洗素质点(pc);
                        SagaMap.Skill.SkillHandler.Instance.CastPassiveSkills(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够呢"); return;
                    }
                    break;
                case 4:
                    if (pc.Gold >= fee)
                    {
                        pc.Gold -= fee;
                        ChangePlayerJob(pc, PC_JOB.CARDINAL);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已經成為『吟遊詩人』了。", "水琴");
                        PlaySound(pc, 4012, false, 100, 50);
                        if (pc.CInt["不是第一次更换职业"] != 1)
                            pc.CInt["不是第一次更换职业"] = 1;
                        洗素质点(pc);
                        SagaMap.Skill.SkillHandler.Instance.CastPassiveSkills(pc);
                    }
                    else
                    {
                        Say(pc, 131, "钱不够呢"); return;
                    }
                    break;
                case 5:
                    break;
            }
        }
    }
}

