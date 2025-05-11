/// <summary>
/// ホールに関する定数を管理
/// </summary>
public class ConstHole
{
    /// <summary>
    /// 穴の様々な要素に対する成長率
    /// </summary>
    public static readonly float GrowthRate = 1.5f;
    public static readonly float initialSpeed = 2f;
    /// <summary>
    /// スケールからカメラ距離を計算するための乗数
    /// </summary>
    //public static readonly float multiplierOrthoSize = 5f;
    //public static readonly float durationOrthoSize = 0.8f;

    /// <summary>
    /// 穴の最大レベル
    /// </summary>
    public static readonly int limitLevel = 15;
}
