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
    public class S50015001 : Event
    {
        public S50015001()
        {
            this.EventID = 50015001;
        }
        string s;
        private SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(80010000);
        public override void OnEvent(ActorPC pc)
        {
            if (pc.CInt["决斗积分"] < 1) pc.CInt["决斗积分"] = 1000;
            if (SInt["竞技场_决斗开始"] == 0) clear();
            if (!CheckHomeExist()) SInt["竞技场_主场"] = 0;
            if (!map.Actors.ContainsKey((uint)SInt["竞技场_主场"])) clear();
            if (!CheckCanApply()) s = "对决已经开始";
            
            ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
            if(pc == home && SInt["竞技场_决斗开始"] != 2)
            {
                Say(pc, 131, "您的比赛正在等待对手中..");
                switch(Select(pc,"怎么办呢","","我要放弃等待","没事"))
                {
                    case 1:
                        clear();
                        break;
                }
                return;
            }
            if (SInt["竞技场_客场"] == 0 && SInt["竞技场_主场"] == 0) s = "开放中..";
            Say(pc, 131, "欢迎来到一测竞技场_，$R$R我是这里的道长，$R有什么可以帮到你的吗？", "道长");
            switch (Select(pc, "欢迎来到一测竞技场_", "", "寻找对手【当前：" + s + "】", "请告诉我「比赛规则」", "查看「我的积分」", "查看「排行榜」", "请送我「离开这里」", "你跟外面的那个人长得一样耶", "没事"))
            {
                case 1:
                    try
                    {
                        if (SInt["竞技场_决斗开始"] == 2)
                        {
                            Say(pc, 131, "当前对决正在进行中，$R请等待下一场。", "道长");
                            return;
                        }
                        if (CheckHomeExist())
                        {
                            SInt["竞技场_客场"] = (int)pc.ActorID;
                            Say(pc, 131, "申请对决成功！$R您为 「客场」。$R$R您将于玩家 " + home.Name + "挑战，$R请做好赛前准备。$R$R本场对决将在10秒后开始。");
                            Announce(80010000, "玩家 " + pc.Name + " 申请了对决！他将与玩家 " + home.Name + " 进行对决，双方对决将在10秒后开始。");
                            //开始比赛
                            SInt["竞技场_决斗开始"] = 3;
                            Timer timer = AddTimer("竞技场_决斗计时"+System.DateTime.Now.Second.ToString() + System.DateTime.Now.Minute.ToString(), 100, 10000, pc, false, false);
                            timer.OnTimerCall += Timer_OnTimerCall;
                            timer.Activate();
                            
                        }
                        else
                        {
                            SInt["竞技场_主场"] = (int)pc.ActorID;
                            Say(pc, 131, "申请对决成功！$R您为 「主场」。$R$R请等待客场玩家参与。$R若60秒内无人参与，本场对决将取消。");
                            Announce(80010000, "玩家 " + pc.Name + " 申请了对决！正在等待60内有玩家挑战他。");
                            SInt["竞技场_决斗开始"] = 1;
                            //设置等待计时器
                            SInt["竞技场_等待计时器"] = 0;
                            Timer time = AddTimer("竞技场_等待计时" + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Minute.ToString(), 1000, 0, pc, false, true);
                            time.OnTimerCall += Time_OnTimerCall;
                            time.Activate();
                        }
                    }
                    catch(Exception ex)
                    {
                        SInt["竞技场_客场"] = 0;
                        SagaLib.Logger.ShowError(ex);
                        Say(pc, 131, "请重试一遍");
                        return;
                    }
                    break;
                case 2:
                    Say(pc, 131, "这里是一对一决斗场，$R如果有「两位玩家在60秒内」申请了决斗，$R决斗将会开始，时限为「180秒」。$R$R在决斗期间， $R只有决斗双方玩家可以相互攻击，$R其他玩家作为旁观者。$R$R为了排除干扰，$R决斗玩家在决斗期间无法看见其他玩家。", "道长");
                    switch (Select(pc, "还有什么想问的吗？", "", "询问「胜利条件」", "询问「关于积分」", "没有什么要问的了"))
                    {
                        case 1:
                            Say(pc, 131, "胜利条件有：$R1、将对手击杀$R2、超时时血较多的一方。 $R$R※如果超时后血量相等将判为主场胜利。", "道长");
                            break;
                        case 2:
                            Say(pc, 131, "积分？那是什么？可以吃吗？", "道长");
                            break;
                    }
                    break;
                case 3:
                    Say(pc, 131, "您的积分为" + pc.CInt["决斗积分"].ToString());
                    break;
                case 4:
                    break;
                case 5:
                    Say(pc, 131, "慢走~");
                    pc.CInt["FFBGM"] = 1168;
                    Warp(pc, 90001999, 35, 15);
                    break;
                case 6:
                    removePCmode();
                    pc.CInt["决斗积分"] = 1000;
                    clear();
                    break;
            }
        }

        private void Timer_OnTimerCall(Timer timer, ActorPC pc)
        {
            try
            {
                SInt["竞技场_决斗计时_180秒"]++;
                if (!map.Actors.ContainsKey((uint)SInt["竞技场_主场"]))
                {
                    guestwin();
                    timer.Deactivate();
                }
                if (!map.Actors.ContainsKey((uint)SInt["竞技场_客场"]))
                {
                    homewin();
                    timer.Deactivate();
                }
                ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
                ActorPC guest = (ActorPC)map.GetActor((uint)SInt["竞技场_客场"]);
                if (!CheckCanApply())
                {
                    if (SInt["竞技场_决斗计时_180秒"] == 1)
                    {

                        short[] pos = new short[2];
                        pos[0] = SagaLib.Global.PosX8to16(43, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(31, map.Height);
                        map.MoveActor(SagaMap.Map.MOVE_TYPE.START, home, pos, 2, 5000, true, MoveType.WARP2);
                        pos[0] = SagaLib.Global.PosX8to16(22, map.Width);
                        pos[1] = SagaLib.Global.PosY8to16(31, map.Height);
                        map.MoveActor(SagaMap.Map.MOVE_TYPE.START, guest, pos, 2, 5000, true, MoveType.WARP2);
                        home.Mode = PlayerMode.KNIGHT_NORTH;
                        guest.Mode = PlayerMode.KNIGHT_SOUTH;
                        this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, home, true);
                        this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, guest, true);
                        ChangeBGM(home, getrandomBGM(), true, 100, 50);
                        ChangeBGM(guest, getrandomBGM(), true, 100, 50);
                        SInt["竞技场_决斗开始"] = 2;
                        SInt["竞技场_主场参加者积分押金"] = calcScore(home);
                        SInt["竞技场_客场参加者积分押金"] = calcScore(guest);
                        home.CInt["决斗积分"] -= SInt["竞技场_主场参加者积分押金"];
                        guest.CInt["决斗积分"] -= SInt["竞技场_客场参加者积分押金"];
                    }
                }
                if (SInt["竞技场_决斗计时_180秒"] >= 1800)
                {

                    float HomeHP = (float)home.HP / (float)home.MaxHP;
                    float GuestHP = (float)guest.HP / (float)guest.HP;
                    if (HomeHP > GuestHP)
                        homewin();
                    else guestwin();
                    clear();
                    timer.Deactivate();
                }
                if (home.Buff.Dead || home.MapID != 80010000 || home.HP == 0 || home == null)
                {
                    SagaLib.Logger.ShowError("sadasd??3");
                    guestwin();
                    timer.Deactivate();

                }
                if (guest.Buff.Dead || guest.MapID != 80010000 || guest.HP == 0 || guest == null)
                {
                    SagaLib.Logger.ShowError("sadasd??2");
                    homewin();
                    timer.Deactivate();
                }
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                timer.Deactivate();
            }
        }
        void homewin()
        {
            map = SagaMap.Manager.MapManager.Instance.GetMap(80010000);
            ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
            home.CInt["决斗积分"] += SInt["竞技场_主场参加者积分押金"];
            home.CInt["决斗积分"] += SInt["竞技场_客场参加者积分押金"];

            if (map.Actors.ContainsKey((uint)SInt["竞技场_客场"]))
            {
                ActorPC guest = (ActorPC)map.GetActor((uint)SInt["竞技场_客场"]);
                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你战胜了玩家 " + guest.Name);
                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你获得了 " + SInt["竞技场_客场参加者积分押金"].ToString() + " 积分，当前积分为：" + home.CInt["决斗积分"].ToString());

                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你败给了玩家 " + home.Name);
                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你失去了 " + SInt["竞技场_客场参加者积分押金"].ToString() + " 积分，当前积分为：" + guest.CInt["决斗积分"].ToString());
            }
            else
            {
                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你战胜了！");
                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你获得了 " + SInt["竞技场_客场参加者积分押金"].ToString() + " 积分，当前积分为：" + home.CInt["决斗积分"].ToString());
            }
            Announce(80010000, "玩家： " + home.Name + " 获得了胜利！决斗结束，现在可以再次申请决斗了。");
            removePCmode();
            clear();
        }
        void guestwin()
        {
            map = SagaMap.Manager.MapManager.Instance.GetMap(80010000);
            ActorPC guest = (ActorPC)map.GetActor((uint)SInt["竞技场_客场"]);
            guest.CInt["决斗积分"] += SInt["竞技场_客场参加者积分押金"];
            guest.CInt["决斗积分"] += SInt["竞技场_主场参加者积分押金"];

            if (map.Actors.ContainsKey((uint)SInt["竞技场_主场"]))
            {
                ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你战胜了玩家 " + home.Name);
                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你获得了 " + SInt["竞技场_主场参加者积分押金"].ToString() + " 积分，当前积分为：" + guest.CInt["决斗积分"].ToString());

                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你败给了玩家 " + guest.Name);
                SagaMap.Network.Client.MapClient.FromActorPC(home).SendSystemMessage("你失去了 " + SInt["竞技场_主场参加者积分押金"].ToString() + " 积分，当前积分为：" + home.CInt["决斗积分"].ToString());
            }
            else
            {
                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你战胜了！");
                SagaMap.Network.Client.MapClient.FromActorPC(guest).SendSystemMessage("你获得了 " + SInt["竞技场_主场参加者积分押金"].ToString() + " 积分，当前积分为：" + guest.CInt["决斗积分"].ToString());
            }
            Announce(80010000, "玩家： " + guest.Name + " 获得了胜利！现在可以再次申请决斗了。");
            removePCmode();
            clear();
        }
        void removePCmode()
        {
            map = SagaMap.Manager.MapManager.Instance.GetMap(80010000);

            if (map.Actors.ContainsKey((uint)SInt["竞技场_主场"]))
            {
                ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
                home.Mode = PlayerMode.NORMAL;
                this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, home, true);
                ChangeBGM(home, 1155, true, 100, 50);
            }
            if (map.Actors.ContainsKey((uint)SInt["竞技场_客场"]))
            {
                ActorPC guest = (ActorPC)map.GetActor((uint)SInt["竞技场_客场"]);
                guest.Mode = PlayerMode.NORMAL;
                this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, guest, true);
                ChangeBGM(guest, 1155, true, 100, 50);
            }
        }
        private void Time_OnTimerCall(Timer timer, ActorPC pc)
        { 
            SInt["竞技场_等待计时器"]++;
            if (!CheckHomeExist())
            {
                timer.Deactivate();
                Announce(80010000, "主场玩家取消了决斗等待。");
                clear();
            }
            if (SInt["竞技场_决斗开始"] == 2)
                timer.Deactivate();
            if(SInt["竞技场_等待计时器"] >= 60)
            {
                Announce(80010000, "60秒内无人应答决斗，将重新开启决斗申请。");
                SInt["竞技场_主场"] = 0;
                clear();
            }
        }
        int calcScore(ActorPC loser)
        {
            int Score = 0;
            if (loser.CInt["决斗积分"] < 3000)
                Score = 200;
            if (loser.CInt["决斗积分"] < 2000)
                Score = 90;
            if (loser.CInt["决斗积分"] < 1500)
                Score = 60;
            if (loser.CInt["决斗积分"] < 1200)
                Score = 30;
            if (loser.CInt["决斗积分"] < 1000)
                Score = 20;
            if (loser.CInt["决斗积分"] < 500)
                Score = 2;
            return Score;
        }
        void clear()
        {
            SInt["竞技场_主场"] = 0;
            SInt["竞技场_客场"] = 0;
            SInt["竞技场_等待计时器"] = 0;
            SInt["竞技场_决斗计时_180秒"] = 0;
            SInt["竞技场_决斗开始"] = 0;
        }
        bool CheckCanApply()
        {
            if (CheckHomeExist() && CheckGuestExist())
            {
                return false;
            }
            else
                return true;
        }
        uint getrandomBGM()
        {
            uint id = 1156;
            switch(SagaLib.Global.Random.Next(0,5))
            {
                case 0:
                    id = 1156;
                    break;
                case 1:
                    id = 1147;
                    break;
                case 2:
                    id = 1066;
                    break;
                case 3:
                    id = 1064;
                    break;
                case 4:
                    id = 1161;
                    break;
                case 5:
                    id = 1027;
                    break;
            }
            return id;
        }
        bool CheckHomeExist()
        {
            if(!map.Actors.ContainsKey((uint)SInt["竞技场_主场"])) return false;
            if (SInt["竞技场_主场"] > 0)
            {
                ActorPC home = (ActorPC)map.GetActor((uint)SInt["竞技场_主场"]);
                if (home == null) return false;
                if (home.MapID != 80010000) return false;
                //if (home.Buff.Dead) return false;
                s = home.Name + " 正在等待挑战";
                return true;
            }
            else return false;
        }
        bool CheckGuestExist()
        {
            if (!map.Actors.ContainsKey((uint)SInt["竞技场_客场"])) return false;
            if (SInt["竞技场_客场"] > 0)
            {
                ActorPC guest = (ActorPC)map.GetActor((uint)SInt["竞技场_客场"]);
                if (guest == null) return false;
                if (guest.MapID != 80010000) return false;
                //if (home.Buff.Dead) return false;
                return true;
            }
            else return false;
        }
    }
}

