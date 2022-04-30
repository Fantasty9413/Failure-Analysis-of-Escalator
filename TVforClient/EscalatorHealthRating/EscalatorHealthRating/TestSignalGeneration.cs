using System;
using System.Collections.Generic;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using ToolBox;

namespace TestTool
{
    public class TestSignalGenerator
    {
		private double[] time;
		private double[] amplitude;
		protected int Length;
		protected int Fs;
		private string DataFilaName;															// 测试数据路径
		private void TestSignalGeneration_Rand(int Length = 8192, int Fs = 10000)				// 随机生成数据
		{
			this.Length = Length;
			this.Fs = Fs;
			time = new double[this.Length];
			amplitude = new double[this.Length];

			Random rd = new Random();
			for (int i = 0; i < this.Length; i++)
			{
				time[i] = (i) / (double)this.Fs;
				amplitude[i] = 100 * rd.NextDouble();
			}
		}
		private void TestSignalGeneration_Load(int Length = 8192, int Fs = 10000)				// 从本地文件中导入数据
		{
			this.Length = Length;
			this.Fs = Fs;
			this.time = new double[this.Length];
			this.amplitude = new double[this.Length];

			TB tb = new TB();
			MWNumericArray time, amplitude;
			MWArray[] temp;
			temp = tb.ImportData(2, Length, this.DataFilaName);
			time = (MWNumericArray)temp[0];
			amplitude = (MWNumericArray)temp[1];

			this.time = (double[])time.ToVector(MWArrayComponent.Real);
			this.amplitude = (double[])amplitude.ToVector(MWArrayComponent.Real);
		}
		public TestSignalGenerator()
		{
			DataFilaName = new string("test_data.mat");

			// 生成测试数据
			//this.TestSignalGeneration_Rand();
			this.TestSignalGeneration_Load();
		}
		public double[] GetSignalData_Time()													// 获取测试数据--时间序列
		{
			double[] time = new double[Length];
			this.time.CopyTo(time, 0);
			return time;
		}
		public double[] GetSignalData_Amplitude()                                               // 获取测试数据--幅值序列
		{
			double[] amplitude = new double[Length];
			this.amplitude.CopyTo(amplitude, 0);
			return amplitude;
		}
	}
}
