using System;
using System.Collections.Generic;
using System.Text;
using HealthScoreRating;


namespace ScoreRatingPart
{
    abstract class HealthScoreRatingSystem
    {
        public HSR hsr;                         // health score rating
        public HealthScoreRatingSystem()        // 构造函数
        {
            hsr = new HSR();
        }
        public int GetHealthScore()             // 获取扶梯健康评分
        {
            return 0;
        }
    }
}
