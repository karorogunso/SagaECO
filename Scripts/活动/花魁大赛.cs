
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S66666666 : Event
    {
        public S66666666()
        {
            this.EventID = 66666666;
        }

        public override void OnEvent(ActorPC pc)
        {
            string name = "";

            switch (Select(pc, "请选择", "", "开启对指定玩家投票", "总结本轮得分", "离开"))
            {
                case 1:
                    name = InputBox(pc, "请输入这位选手的编号or代号", InputType.PetRename);
                    pc.TStr["n_playername"] = name;
                    pc.TInt["c_playername"] = 0;
                    pc.TInt["t_playername"] = 0;
                    pc.TInt["c2_playername"] = 0;
                    pc.TInt["t2_playername"] = 0;
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                    {
                        if (item.Character.MapID == 91000999)
                        {
                            Activator a = new Activator(pc, item.Character, name);
                            a.Activate();
                            SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage(item.Character.Name + "开始打分...");
                        }
                    }
                    break;
                case 2:
                    string s = "※裁判总结了本次打分，选手 " + pc.TStr["n_playername"].ToString() + " 的总分为：" + pc.TInt["t_playername"].ToString() +
    " 打分人数：" + pc.TInt["c_playername"].ToString() + "人，最终平均分为：" + ((double)pc.TInt["t_playername"] / (double)pc.TInt["c_playername"]).ToString();
                    foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                        if (item.Character.MapID == 91000999)
                            SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage(s);
                    Announce(91000999, s);
                    break;
                case 3:
                    break;
            }
        }


        class Activator : MultiRunTask
        {
            ActorPC pc;
            ActorPC player;
            string playername;
            public Activator(ActorPC pc, ActorPC player,string name)
            {
                this.pc = pc;
                this.player = player;
                this.playername = name;
                this.dueTime = 1000;
                period = 5000;
            }
            public override void CallBack()
            {
                try
                {
                    if (pc == player)
                    {
                        //if (SkillEvent.Instance.Select(pc, "[管理员打分]是否现在打分？（选择否5秒后再次出现）", "", "打分", "5秒后再提示我") == 1)
                        {
                            int c = int.Parse(SkillEvent.Instance.InputBox(pc, "请输入[服装]给分（0-20）", InputType.Bank));
                            pc.TInt["c_playername"]++;
                            pc.TInt["t_playername"] += c;
                            foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                                if (item.Character.MapID == 91000999)
                                    SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage("※裁判 给 " + playername + " " + "打了 " + c.ToString() + "[服饰分]分");

                            c = int.Parse(SkillEvent.Instance.InputBox(pc, "请输入[表演]给分（0-20）", InputType.Bank));
                            pc.TInt["t_playername"] += c;
                            foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                                if (item.Character.MapID == 91000999)
                                    SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage("※裁判 给 " + playername + " " + "打了 " + c.ToString() + "[表演分]分");
                            this.Deactivate();
                        }
                    }
                    else
                    {
                        int c3 = 0;
                        int c = SkillEvent.Instance.Select(player, "请决定对 " + playername + " 的给分【服饰分】", "", "1分", "2分", "3分", "4分", "5分！！(最高分）");
                        c3 += c;
                        pc.TInt["c_playername"]++;
                        pc.TInt["t_playername"] += c;
                        SagaMap.Network.Client.MapClient.FromActorPC(player).SendSystemMessage("你给了 " + playername + " " + c.ToString() + "[服饰分]分");
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                            if (item.Character.MapID == 91000999)
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage(/*player.Name +*/ "一颗糖果 给 " + playername + " " + "打了 " + c.ToString() + "[服饰分]分");
                        c = SkillEvent.Instance.Select(player, "请决定对 " + playername + " 的给分【表演分】", "", "1分", "2分", "3分", "4分", "5分！！(最高分）");
                        c3 += c;
                        pc.TInt["t_playername"] += c;
                        SagaMap.Network.Client.MapClient.FromActorPC(player).SendSystemMessage("你给了 " + playername + " " + c.ToString() + "[表演分]分");
                        foreach (var item in SagaMap.Manager.MapClientManager.Instance.OnlinePlayer)
                            if (item.Character.MapID == 91000999)
                                SagaMap.Network.Client.MapClient.FromActorPC(item.Character).SendSystemMessage(/*player.Name +*/ "一颗糖果 给 " + playername + " " + "打了 " + c.ToString() + "[表演分]分");
                        SagaMap.Skill.SkillHandler.Instance.ActorSpeak(player, c3.ToString() + "分！！");

                        this.Deactivate();
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    this.Deactivate();
                }
            }
        }
    }
}

