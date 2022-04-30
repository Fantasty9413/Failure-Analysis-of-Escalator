using System;
using System.Collections.Generic;
using System.Text;

namespace TypicalValueExtractionPart
{
    public abstract class TypicalValueAnalysisSystem
    {
		protected int Length;
		protected int Fs;
		protected double[] time;
		protected double[] amplitude;

		public TypicalValueAnalysisSystem(int SignalLength = 8192, int SampleFrequency = 10000)
        {
			this.Length = SignalLength;
			this.Fs = SampleFrequency;
			time = new double[Length];
			amplitude = new double[Length];
		}
		public abstract void SetData(double[] s_x, double[] s_y);	// 输出待分析数据
		public abstract Array GetTV();								// 输出获取特征值
	}
}
