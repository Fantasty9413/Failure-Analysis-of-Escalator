using System;
using ToolBox;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

// generate signal data for test
// 产生模拟数据用于测试
public class SignalData
{
	public double[] time;
	public double[] amplitude;
	protected int Length;
	protected int Fs;

	public SignalData(int Length = 8192, int Fs = 10000)
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

	public SignalData(string DataFileName, int Length = 8192, int Fs = 10000)
	{
		this.Length = Length;
		this.Fs = Fs;
		this.time = new double[this.Length];
		this.amplitude = new double[this.Length];

		TB tb = new TB();
		MWNumericArray time, amplitude;
		MWArray[] temp;
		temp = tb.ImportData(2, Length);
		time = (MWNumericArray)temp[0];
		amplitude = (MWNumericArray)temp[1];

		this.time = (double[])time.ToVector(MWArrayComponent.Real);
		this.amplitude = (double[])amplitude.ToVector(MWArrayComponent.Real);
	}
}
