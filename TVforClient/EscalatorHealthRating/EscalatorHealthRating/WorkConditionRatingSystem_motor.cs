using System;
using System.Collections.Generic;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;

namespace ScoreRatingPart
{
    class WorkConditionRatingSystem_motor : WorkConditionRatingSystem
    {
        private MWNumericArray _label;
        private string modelsuffixname;   // 模型后缀名
        private MWCharArray _modelsuffixname;
        public WorkConditionRatingSystem_motor() : base("motor", 10)
        {
            _label = new MWNumericArray(1);
            modelsuffixname = "motor";
            _modelsuffixname = new MWCharArray(modelsuffixname);
        }
        public override double GetSingleGrade(int Label_motor)
        {
            //throw new NotImplementedException();

            // step 0.构造输入
            int label = Label_motor;
            _label = new MWNumericArray(label);

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
    }
}
