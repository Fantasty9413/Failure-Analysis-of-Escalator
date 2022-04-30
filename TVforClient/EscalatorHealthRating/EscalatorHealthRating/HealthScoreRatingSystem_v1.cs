using System;
using System.Collections.Generic;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using HealthScoreRating;


namespace ScoreRatingPart
{
    class HealthScoreRatingSystem_v1 : HealthScoreRatingSystem
    {
        private WorkConditionRatingSystem_motor wcrs_motor;
        private WorkConditionRatingSystem_reducer wcrs_reducer;
        private WorkConditionRatingSystem_bolt wcrs_bolt;
        private int ratingparts_nums;                               // 评估部件数量
        private MWNumericArray _grades;                             // 评分值
        private MWNumericArray _weight;                             // 评分权值

        public HealthScoreRatingSystem_v1() : base()
        {
            wcrs_motor = new WorkConditionRatingSystem_motor();
            wcrs_reducer = new WorkConditionRatingSystem_reducer();
            wcrs_bolt = new WorkConditionRatingSystem_bolt();
            ratingparts_nums = 3;
            _grades = new MWNumericArray(1, ratingparts_nums);
            int[] weight = { 1, 1, 1 };
            _weight = new MWNumericArray(1, ratingparts_nums, weight);
        }
        private void SetWeight(int[] weight)                              // 设置评分权重
        {
            if (weight.Length != ratingparts_nums)
            {
                throw new Exception("Error: 权重设置出错！");
            }
            _weight = new MWNumericArray(1, ratingparts_nums, weight);
        }
        private void SetWeight(int weight1, int weight2, int weight3)     // 设置评分权重
        {
            int[] weight = new int[ratingparts_nums];
            weight[0] = weight1;
            weight[1] = weight2;
            weight[2] = weight3;
            _weight = new MWNumericArray(1, ratingparts_nums, weight);
        }
        private Array GetWorkConditionGrades(int[] Labels)                // 获取各个部件的工况评分。输入是工况标签数组，输出是评分数组。两者都遵从motor、reducer和bolt的顺序。
        {
            if (Labels.Length != ratingparts_nums)
            {
                throw new Exception("Error: 工况标签出错！");
            }
            var grades = new int[ratingparts_nums];
            int Label_motor = Labels[0];        // motor部件工况标签
            int Label_reducer = Labels[1];      // reducer部件工况标签
            int Label_bolt = Labels[2];         // bolt部件工况标签
            grades.SetValue((int)wcrs_motor.WorkConditionRating(Label_motor), 0);        // 获取motor部件工况评分
            grades.SetValue((int)wcrs_reducer.WorkConditionRating(Label_reducer), 1);    // 获取reducer部件工况评分
            grades.SetValue((int)wcrs_bolt.WorkConditionRating(Label_bolt), 2);          // 获取bolt部件工况评分
            return grades;
        }
        public int GetHealthScore(int[] Labels)                           // 获取扶梯的整体健康评分——使用默认评分权重1：1：1
        {
            // 获取各个部件的工况评分分数
            Array grades = this.GetWorkConditionGrades(Labels);
            _grades = new MWNumericArray(grades);

            // 设置各个部件的评分权重
            int[] weight = { 1, 1, 1 };
            this.SetWeight(weight);

            // 获取总体的健康评分
            MWArray[] _result;
            _result = this.hsr.HealthScoreRating(2, _grades, _weight);

            // 结果数据类型转换
            MWNumericArray _Flag, _Score;
            _Flag = (MWNumericArray)_result[0];
            _Score = (MWNumericArray)_result[1];

            int Flag, Score;
            Flag = _Flag.ToScalarInteger();
            Score = _Score.ToScalarInteger();

            // 输出结果
            if (Flag == 1)
            {
                return Score;
            }
            else
            {
                throw new Exception("Error: 健康评分获取失败！");
            }
        }
        public int GetHealthScore(int[] Labels, int[] Weights)            // 获取扶梯的整体健康评分——自行设置权重。权重数组遵从motor、reducer和bolt的顺序。
        {
            // 获取各个部件的工况评分分数
            Array grades = this.GetWorkConditionGrades(Labels);
            _grades = new MWNumericArray(grades);

            // 设置各个部件的评分权重
            int[] weight = Weights;
            this.SetWeight(weight);

            // 获取总体的健康评分
            MWArray[] _result;
            _result = this.hsr.HealthScoreRating(2, _grades, _weight);

            // 结果数据类型转换
            MWNumericArray _Flag, _Score;
            _Flag = (MWNumericArray)_result[0];
            _Score = (MWNumericArray)_result[1];

            int Flag, Score;
            Flag = _Flag.ToScalarInteger();
            Score = _Score.ToScalarInteger();

            // 输出结果
            if (Flag == 1)
            {
                return Score;
            }
            else
            {
                throw new Exception("Error: 健康评分获取失败！");
            }
        }
        public int SystemWorking(int[] Labels)                            // 对GetHealthScore(int[] Labels)做封装，同一接口
        {
            return this.GetHealthScore(Labels);
        }
    }
}
