using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:尼庫爾勒(11000097) X:120 Y:30
namespace SagaScript.M10031000
{
    public class S11000097 : Event
    {
        public S11000097()
        {
            this.EventID = 11000097;

            this.questTransportSource = "请您帮我转交给对方,$R;" +
                "那就拜托了!;";
            this.questTransportDest = "收好了，真的谢谢您啊;";
            this.questTransportCompleteSrc = "这么快就将物品转交给对方了?$R;" +
                "非常谢谢阿!$R;" +
                "$R请去任务服务台领取报酬吧！;";
            this.questTransportCompleteDest = "辛苦了;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {
                Say(pc, 131, "$R$P电路机械开始自然自语了?$P凭依者也没有呢?$R;" +
                    "$P这样不可能的事情…$R;" +
                    "$R也许问一下除了活动木偶以外$R拥有其他力量的人可能会比较好$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.開始維修) &&
                !Neko_02_cmask.Test(Neko_02.聽取建議))
            {
                Neko_02_cmask.SetValue(Neko_02.聽取建議, true);
                Say(pc, 131, "喂，电路机械的维修进行的好吗?$R;" +
                    "$R什么?$P电路机械开始自然自语了?$P凭依者也没有呢?$R;" +
                    "$R不是产生了错觉吧?$R;" +
                    "$P嗯…难道那是真的?$R;" +
                    "$R从来没听说过那样的事啊…$R;" +
                    "$R嗯…$R我不是活动木偶的专家$R所以不是全部都知道$R但是…$R;" +
                    "$P嗯…$R想听听看活动木偶专家的意见阿$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.得知維修方法) && 
                !Neko_02_cmask.Test(Neko_02.開始維修))
            {
                Say(pc, 131, "准备一下电路机械的材料吧$R还有四种类的强化结晶也要…$R;" +
                    "$R强化结晶会引起神秘的作用$R安装部件会自己开始恢复$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
            Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話) && 
            !Neko_02_cmask.Test(Neko_02.得知維修方法))
            {
                Neko_02_cmask.SetValue(Neko_02.得知維修方法, true);
                Say(pc, 131, "活动木偶需要维修$R;" +
                    "$P想要维修坏掉的电路机械?$R;" +
                    "$R维修活动木偶很简单的$R但是你能做到吗?$R;" +
                    "$P嗯，好的!$R;" +
                    "$P因为你是我的朋友$R所以会特别教你的$R;" +
                    "$R不可以教给别人啊!$R;" +
                    "$P首先要准备活动木偶电路机械的材料$R;" +
                    "$P『洋铁的躯干』和『洋铁的心』$R;" +
                    "$P用那个替换碎部件$R;" +
                    "$R虽然安装部件是困难的$R但是不要太在意$R;" +
                    "$P然后是唐卡秘传的维修方法!$R;" +
                    "$P4种类的强化结晶全部需要$R;" +
                    "$R『生命的结晶』$R『力量的结晶』$R『魔力的结晶』还有最后是$R『会心一击的结晶』$R;" +
                    "$P使用强化结晶的话$R结晶4个会引起神秘的作用$R安装部件会自己开始恢复$R;");
                return;
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "妈妈在故乡担心着呢…$R;" +
                    "好久没去故乡了，要不要回去呢?$R;");
                return;
            }
            Say(pc, 131, "听说火之精灵就藏在这附近$R;" +
                "找一找看怎么样?$R;");
        }
    }
}
