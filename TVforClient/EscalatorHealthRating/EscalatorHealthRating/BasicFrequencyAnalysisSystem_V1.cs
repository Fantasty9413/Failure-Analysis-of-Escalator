using System;
using System.Collections.Generic;
using System.Text;
using BasicFrequencyAnaysis;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace TypicalValueExtractionPart
{
    public class BasicFrequencyAnalysisSystem_V1 : BasicFrequencyAnalysisSystem
    {
		private BFA bfa;
		private readonly string[] BF_Type;
		private double[] SP;                // structure parameters
		private double[] BF;                // basic frequency
		public BasicFrequencyAnalysisSystem_V1()
		{
			bfa = new BFA();
			this.BF_Type = new string[9];
			this.SP = new double[20];
			this.BF = new double[20];

			string[] bf_type = { "f_motor", "f_m", "f_d", "f_md", "f_hd", "f_r", "f_dp", "f_sp", "f_hp" };      // basic frequency type
			bf_type.CopyTo(this.BF_Type, 0);
		}

		private void SetData(double[] SP)	// 设置结构参数
        {
			SP.CopyTo(this.SP, 0);
        }
		private void BFAnalysis()
        {
			MWNumericArray _SP = new MWNumericArray(1, 20, this.SP);

			MWNumericArray _BF = new MWNumericArray();
			_BF = (MWNumericArray)bfa.BF_Structure(_SP);

			this.BF = (double[])_BF.ToVector(MWArrayComponent.Real);
		}
        public override void SystemWorking(double[] SP)		// 基频分析，输入结构参数后系统分析运算一次，之后直接通过Get方法获取
        {
			this.SetData(SP);
			this.BFAnalysis();
        }
		public double GetBf(string type)					// 获取基频值
		{
			int index = Array.IndexOf(this.BF_Type, type);
			return (index != -1 ? BF[index] : 0);
		}
	}
}
