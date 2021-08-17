using System;

// generate signal data for test
// 产生模拟数据用于测试
public class SignalData
{
	public double[] time;
	public double[] amplitude;
	protected int Length;
	protected int Fs;

	public SignalData(int Length = 8092, int Fs = 10000)
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
}
