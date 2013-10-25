using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaMap.Localization;

namespace SagaMap.Localization.Languages
{
    public class Chinese : Strings
    {
        public Chinese()
        {
            this.ATCOMMAND_NO_ACCESS = "你没有访问这条命令的权限！";
            this.ATCOMMAND_ITEM_PARA = "用法: !item 物品ID";
            this.ATCOMMAND_ITEM_NO_SUCH_ITEM = "没有这件物品！";            
            this.PLAYER_LOG_IN = "玩家：{0} 已经登录";
            this.CLIENT_CONNECTING = "客户端(版本号:{0}) 正在尝试连接中...";
            this.NEW_CLIENT = "新客户端： {0}";
            this.INITIALIZATION = "开始初始化……";
            this.ACCEPTING_CLIENT = "开始接受客户端。";

            this.ITEM_ADDED = "得到{1}个[{0}]";
            this.ITEM_DELETED = "失去{1}个[{0}]";
        }
    }
}
