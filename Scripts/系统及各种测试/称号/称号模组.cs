using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item;
namespace SagaScript
{
    public abstract class 称号模组 : Event
    {
        uint titleID;

        public 称号模组()
        {
        }

        protected void Init(uint eventID,uint titleID)
        {
            this.EventID = eventID;
            this.titleID = titleID;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(PlayerTitleFactory.Instance.PlayerTitles.ContainsKey(titleID))
            {
                PlayerTitle title = PlayerTitleFactory.Instance.PlayerTitles[titleID];
                switch (Select(pc, "是否使用【" + title.titlename + "】称号？前缀："+title.firstname, "", "使用该称号[显示前缀]", "使用该称号[隐藏前缀]", "取消目前的称号", "什么也不干"))
                {
                    case 1:
                        pc.ShowFirstName = 1;
                        pc.PlayerTitleID = (ushort)title.id;
                        SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                        ShowEffect(pc, 5019);
                        break;
                    case 2:
                        pc.ShowFirstName = 2;
                        pc.PlayerTitleID = (ushort)title.id;
                        SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                        ShowEffect(pc, 5019);
                        break;
                    case 3:
                        pc.PlayerTitleID = 0;
                        pc.ShowFirstName = 0;
                        SagaMap.PC.StatusFactory.Instance.CalcStatus(pc);
                        break;
                }
            }

        }
    }
}
