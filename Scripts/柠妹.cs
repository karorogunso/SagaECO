using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaMap;
using SagaDB.Actor;
using SagaMap.Skill;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace 柠妹
{
    public partial class 柠妹 : Event
    {
        public 柠妹()
        {
            this.EventID = 66666666;
        }
        public override void OnEvent(ActorPC pc)
        {
            Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            ActorPC lee = (ActorPC)map.GetActor((uint)pc.TInt["触发的虚拟玩家ID"]);
            
            if (pc.AInt["第一次与柠妹对话"] == 0)
            {
                Say(pc, 0, "你好呀冒险家！$R初次见面，我叫柠妹！$R$R希望以后能多多关照呀。", "柠妹");
                Say(pc, 0, "我准备在这里开商店！$R希望你以后多来光顾哦。", "柠妹");
                pc.AInt["第一次与柠妹对话"] = 1;
                pc.AInt["柠妹好感度"] = 1;
                SkillHandler.Instance.ShowEffectOnActor(lee, 8055);
                Logger log = new Logger("柠妹记录.txt");
                string text = "\r\n-第一次对话玩家名：" + pc.Name;
                text += "\r\n-玩家账号ID：" + pc.Account.AccountID;
                log.WriteLog(text);
            }
            else
            {
                Say(pc,0,"现在商店还没开张哦，$R也许可以干点别的事情呢？", "柠妹");
                switch(Select(pc,"怎么办呢？","","购买物品(未开张)","出售物品(未开张)","在她的留言板下给她留言","互动","离开"))
                {
                    case 1:
                        Say(pc, 0, "不能太心急！$R现在还没有开张呢。$R$R等开张了再来呀！~", "柠妹");
                        break;
                    case 2:
                        Say(pc, 0, "不能太心急！$R现在还没有开张呢。$R$R等开张了再来呀！~", "柠妹");
                        break;
                    case 3:
                        OpenBBS(pc, 10, 0);
                        break;
                    case 4:
                        switch(Select(pc,"要如何互动呢？（更多功能开发中）","","掀裙子","举高高","阿空是谁？","离开"))
                        {
                            case 1:
                                if(pc.AInt["柠妹掀裙子被抛飞"] == 1)
                                {
                                    Say(pc, 0, "嗯？？？$R$R（她使劲地盯着你的一举一动）", "柠妹");
                                    Say(pc, 0, "你暂时不会想再去掀她裙子了。");
                                    return;
                                }
                                Say(pc,0,"你的好感度还不足以安全地做此互动！$R$R如果强行做的话，$R可能会降低好感度。$R$R你真的要这样做吗？", "柠妹");
                                if(Select(pc,"怎么办呢？","","强行掀裙子","离开") == 1)
                                {
                                    Warp(pc, 10054000, 0, 0);
                                    Say(pc, 0, "就在你碰到她裙子的那一刹那，$R你被她反手抓住胳膊抛进里海里。$R$R$R※提示：使用手机可以回城。");
                                    pc.AInt["柠妹掀裙子被抛飞"] = 1;
                                    Logger log = new Logger("柠妹记录.txt");
                                    string text = "\r\n-掀裙子被抛飞的玩家名：" + pc.Name;
                                    text += "\r\n-玩家账号ID：" + pc.Account.AccountID;
                                    log.WriteLog(text);
                                }
                                break;
                            case 2:
                                Say(pc, 0, "……$R$R举高高似乎不应该对她使用？");
                                break;
                            case 3:
                                Say(pc, 0, "阿空是我的哥哥~$R$R他好像很怕被举高高，$R所以不太敢露面呢。", "柠妹");
                                Say(pc, 0, "下次有机会我叫他出来试试呀~", "柠妹");
                                break;
                        }
                        break;
                }
            }
        }
    }
}

