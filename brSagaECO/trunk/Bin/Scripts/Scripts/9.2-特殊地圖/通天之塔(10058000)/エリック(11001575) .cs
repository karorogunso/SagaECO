using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10058000
{
    public class S11001575 : Event
    {
        public S11001575()
        {
            this.EventID = 11001575;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "おぉ！アンタか！$R;" +
            "今日はなんのようだい？$R;", "エリック");
           switch (Select(pc, "今日はなんのようだい？", "", "『降竜石』を譲って欲しい", "もう一度、あの話を聞きたい", "｢たてがみ｣の加工（１０万Ｇ）", "なんでもない"))
           {
               case 1:
                   Say(pc, 131, "５０００Ｇになるが……。$R;" + 
                   "それでもいいかい？$R;", "エリック");
                   if (Select(pc, "『降竜石』を譲って貰う？", "", "５０００Ｇを支払う", "やっぱりやめとく") == 1)
                   {

                       if (pc.Gold >= 5000)
                       {
                           Say(pc, 131, "『降竜石』を入手した！$R;", "エリック");
                           pc.Gold -= 5000;
                           GiveItem(pc, 10011605, 1);


                           Say(pc, 131, "へっへ、毎度～♪$R;" +
                           "$P使い方は簡単だ。$R;" +
                           "塔の前で『降竜石』をかかげる。$R;" +
                           "それだけでいい。$R;" +
                           "$P話に出てきた石より質は劣るが$R;" +
                           "竜を呼び寄せるには十分なシロモノだ。$R;" +
                           "$Pあんた、いい買い物したぜ！$R;" +
                           "$R健闘を祈ってるよ～。$R;", "エリック");
                       }
                                        
                   }
                   break;
               case 2:
                   Say(pc, 131, "もう一度話しが聞きたいだと？$R;" +
                   "$P……。$R;" +
                   "$P…………。$R;" +
                   "$P………………。$R;" +
                   "ま、いいだろう。$R;", "エリック");

                   Say(pc, 131, "最近の話だ。$R;" +
                   "$Pそりゃもう凄腕の冒険者達が$R;" +
                   "この塔に住む白い竜を退治しようと$R;" +
                   "この塔に入ったんだ。$R;" +
                   "$P……まぁ、激闘の末$R;" +
                   "白い竜を退治することができたんだ。$R;" +
                   "$Rだが、その後、誰もが予想しえない$R;" +
                   "事態が起こったのさ！$R;" +
                   "$P急に辺りが暗くなったと思ったら$R;" +
                   "大きな紫色のたてがみに$R;" +
                   "紫色の鱗で覆われた$R;" +
                   "巨大な竜が、仲間の竜を引き連れ$R;" +
                   "冒険者達を見下ろしてやがったのさ。$R;", "エリック");

                   Say(pc, 131, "白い竜との戦いの後で$R;" +
                   "疲労していたこともあったが$R;" +
                   "その竜の力は圧倒的、いや$R;" +
                   "人間が計り知れるものでは$R;" +
                   "なかったと表現した方が正確だろうな。$R;" +
                   "$P……その戦いで$R;" +
                   "リーダー格の冒険者は命を落とし$R;" +
                   "命からがら竜から逃れた他の冒険者は$R;" +
                   "恐怖のあまり$R;" +
                   "二度と剣が握れなくなったと聞くぜ。$R;" +
                   "$Rただ、一人を除いてな。$R;" +
                   "$Pそいつは、竜に殺された$R;" +
                   "リーダー格の冒険者---$R;" +
                   "幼馴染の仇を取るため$R;" +
                   "何度もこの塔を登った。$R;" +
                   "$R何度も何度もね。$R;" +
                   "$Pだが、行けども行けども$R;" +
                   "その竜はそいつの前に一度も$R;" +
                   "姿を現さなかった。$R;" +
                   "$R……どうしてだと思う？$R;" +
                   "$Pへへっ、わかるわけないよな。$R;" +
                   "$Pそいつが、最初で最後に送った$R;" +
                   "首飾りの石が$R;" +
                   "その竜を呼んでいたなんてよ。$R;" +
                   "$P……と、まぁ、こんな話だ。$R;", "エリック");
                   Say(pc, 131, "他にようはあるかい？$R;", "エリック");
                   break;
                   case 3:
                   if (pc.Gold < 100000)
                   {
                   	Say(pc, 131, "制作费要100000G没钱你来干什么？$R;", "エリック");
                   	return;
                   }
                   //黄金に輝く紫竜のたてがみ
                      if (CountItem(pc, 10031152) > 0)
                      {
                      switch (Select(pc, "上質な紫竜のたてがみ？", "", "1個","5個","10個", "なんでもない"))
                      {
                      	case 1:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031152) > 0)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031152, 1);
                      		Say(pc, 0, "一か八か$R;", "エリック");
                      		
                      		    if(SagaLib.Global.Random.Next(0, 999) <8)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      		}

                      		break;
                        case 2:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031152) > 4)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031152, 5);
                      		Say(pc, 0, "これならいけるかも$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 999) <50)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "黄金に輝く紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		break;
                      	case 3:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031152) > 9)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031152, 10);
                      		Say(pc, 0, "失敗する気がしない$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 999) <800)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "黄金に輝く紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		
                      		
                      		break;
                      }
                      }
                   //上質な紫竜のたてがみ
                   if (CountItem(pc, 10031151) > 0)
                      {
                      switch (Select(pc, "上質な紫竜のたてがみ？", "", "1個","5個","10個", "なんでもない"))
                      {
                      	case 1:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031151) > 0)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031151, 1);
                      		Say(pc, 0, "失敗しても恨むなよ$R;", "エリック");
                      		
                      		    if(SagaLib.Global.Random.Next(0, 999) <1)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      		}

                      		break;
                        case 2:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031151) > 4)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031151, 5);
                      		Say(pc, 0, "結構厳しいが・・・$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 999) <5)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		break;
                      	case 3:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031151) > 9)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031151, 10);
                      		Say(pc, 0, "一か八か$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 999) <8)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		
                      		
                      		break;
                      }
                      }
                   //紫竜のたてがみ
                      if (CountItem(pc, 10031150) > 0)
                      {
                      switch (Select(pc, "紫竜のたてがみ？", "", "1個","5個","10個", "なんでもない"))
                      {
                      	case 1:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031150) > 0)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031150, 1);
                      		Say(pc, 0, "成功しないものと思ってくれ$R;", "エリック");
                      		
                      		    if(SagaLib.Global.Random.Next(0, 9999) <1)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      		}

                      		break;
                        case 2:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031150) > 4)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031150, 5);
                      		Say(pc, 0, "難しいが・・・$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 9999) <5)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		break;
                      	case 3:
                      		if (Select(pc, "真的要制作？", "", "是", "否")==1)
                      		{
                      			
                      			if (CountItem(pc, 10031150) > 9)
                      			{
                      				pc.Gold -= 100000;
                      				TakeItem(pc, 10031150, 10);
                      		Say(pc, 0, "失敗しても恨むなよ$R;", "エリック");
                      		    if(SagaLib.Global.Random.Next(0, 999) <1)
                      		    {
                      		    	GiveItem(pc, 50057652, 1);
                      		    	Say(pc, 0, "成功$R;", "エリック");
                      		    	return;
                      		    }
                      		        Say(pc, 0, "失败$R;", "エリック");
                      		        return;
                      		    }
                      			Say(pc, 0, "紫竜のたてがみ不够$R;", "エリック");
                      		}
                      		
                      		
                      		break;
                      }
                      }
                      
                      
           
        
                       Say(pc, 131, "どれを加工するんだい？$R;", "エリック");

                       Say(pc, 131, "……。$R;" +
                       "$Pん？$R;" +
                       "なにも無いじゃないか。$R;", "エリック");
           
                       break;
           }
        }
    }
}