using System;
using System.Collections.Generic;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;

namespace ScoreRatingPart
{
    class WorkConditionRatingSystem_bolt : WorkConditionRatingSystem
    {
        private MWNumericArray _label;
        private string modelsuffixname;   // 模型后缀名
        private MWCharArray _modelsuffixname;
        public WorkConditionRatingSystem_bolt() : base("bolt", 10)
        {
            _label = new MWNumericArray(1);
            modelsuffixname = "bolt";
            _modelsuffixname = new MWCharArray(modelsuffixname);
        }
        private bool SetModelSuffixName(string modelsuffixname)                 // 设置模型后缀名，不要单独使用
        {
            if (modelsuffixname == "bolt" || modelsuffixname == "complicated")
            {
                this.modelsuffixname = modelsuffixname;
                return true;
            }
            else
            {
                return false;
            }
        }
        private double getsinglegrade(int Label, string modelsuffixname)        // 扶梯工况评分--单次评分--可以更改对应模型
        {
            // 设置label获取来源模型的模型后缀名
            SetModelSuffixName(modelsuffixname);

            // 获取单次评分
            // step 0.构造输入
            int label = Label;
            _label = new MWNumericArray(label);
            _modelsuffixname = new MWCharArray(modelsuffixname);

            // step1. 获取MWArray[]型的结果
            MWArray[] _result;
            _result = this.hsr.WorkConditionRating(2, _label, _modelsuffixname);

            // step2. MWArray[]型变量转MWNumericArray
            MWNumericArray _Flag, _Grade;
            _Flag = (MWNumericArray)_result[0];
            _Grade = (MWNumericArray)_result[1];

            // step3. MWNumericArray型转int型
            int Flag, Grade;
            Flag = _Flag.ToScalarInteger();
            Grade = _Grade.ToScalarInteger();

            // 输出结果
            if (Flag == 1)
            {
                return Grade;
            }
            else
            {
                throw new Exception("Error: 工况评分获取失败！");
            }
        }
        public override double GetSingleGrade(int Label_bolt)                   // 使用单一模型(bolt模型)获取的Label结果来评分。重写父类的方法
        {
            double Grade;
            Grade = this.getsinglegrade(Label_bolt, "bolt");
            return Grade;
        }
        private double GetSingleGrade(int Label_bolt, int Label_complicated)    // 同时使用单一模型和混合模型获取的Label结果来评分。重载的方法
        {
            double grade_bolt, grade_complicated;

            grade_bolt = this.getsinglegrade(Label_bolt, "bolt");

            grade_complicated = this.getsinglegrade(Label_complicated, "complicated");

            return Math.Max(grade_bolt, grade_complicated);
        }
        public double WorkConditionRating(int Label_bolt, int Label_complicated)// 重载父类的评分方法
        {
            double grade = 0;
            grade = this.GetSingleGrade(Label_bolt, Label_complicated);

            this.UpdateGradeQueue(grade);

            double avggrade = 0;
            avggrade = this.GetAverageGrade();

            return avggrade;
        }
    }
}
