using System;
using System.Collections.Generic;
using System.Text;
using WorkConditionClassifier;
using MathWorks.MATLAB.NET.Arrays;

namespace WorkConditionClassifierPart
{
    public class WorkConditionClassifier_complicated : WorkConditionClassifier
    {
        private int Tv_Size;              // 特征值数据长度
        private string modelsuffixname;   // 模型后缀名
        private MWNumericArray _tv;
        private MWCharArray _modelsuffixname;
        public WorkConditionClassifier_complicated() : base("complicated") 
        {
            Tv_Size = 32;
            _tv = new MWNumericArray(1, Tv_Size);
             modelsuffixname = "complicated";
            _modelsuffixname = new MWCharArray(modelsuffixname);
        }
        public override int GetWorkConditionLabel(double[] tv)      // 单通道机制，不实用，只是对父类的重写。
        {
            return -1;
        }
        public int GetWorkConditionLabel(double[] tv_channel_2, double[] tv_channel_5)      // 获取预测工况（结果以标签形式展示）
        {
            //throw new NotImplementedException();

            // 获取预测工况结果

            // step 0.构造特征值输入
            double[] tv = new double[Tv_Size];
            tv_channel_2.CopyTo(tv, 0);
            tv_channel_5.CopyTo(tv, Tv_Size / 2);
            _tv = new MWNumericArray(1, Tv_Size, tv);

            // step1. 获取MWArray[]型的结果
            MWArray[] _result;
            _result = this.wcc.WorkConditionClassifier(2, _tv, _modelsuffixname);

            // step2. MWArray[]型变量转MWNumericArray
            MWNumericArray _Flag, _Label;
            _Flag = (MWNumericArray)_result[0];
            _Label = (MWNumericArray)_result[1];

            // step3. MWNumericArray型转int型
            int Flag, Label;
            Flag = _Flag.ToScalarInteger();     // 工况判断分类函数调用信息标志
            Label = _Label.ToScalarInteger();   // 工况判断分类函数调用结果--潜在工况对应标志

            // 输出结果
            if (Flag == 1)
            { 
                return Label; 
            }
            else
            {
                throw new Exception("Error: 预测判断工况信息失败！");
            }
        }
    }
}