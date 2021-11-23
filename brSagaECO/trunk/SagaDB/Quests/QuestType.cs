
namespace SagaDB.Quests
{
    /// <summary>
    /// 任务类别
    /// </summary>
    public enum QuestType
    {
        /// <summary>
        /// 击退
        /// </summary>
        HUNT=1,
        /// <summary>
        /// 收集
        /// </summary>
        GATHER,
        /// <summary>
        /// 攻略
        /// </summary>
        CAPTURE,
        /// <summary>
        /// 搬运
        /// </summary>
        TRANSPORT,
        /// <summary>
        /// 特殊
        /// </summary>
        SPECIAL,
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum QuestStatus
    {
        /// <summary>
        /// 进行中
        /// </summary>
        OPEN = 1,
        /// <summary>
        /// 完成
        /// </summary>
        COMPLETED = 2,
        /// <summary>
        /// 失败
        /// </summary>
        FAILED = 3,
    }

    /// <summary>
    /// 任务难度
    /// </summary>
    public enum QuestDifficulty
    {
        TOO_EASY,
        NORMAL,
        BEST_FIT,
        TOO_HARD,
    }
}