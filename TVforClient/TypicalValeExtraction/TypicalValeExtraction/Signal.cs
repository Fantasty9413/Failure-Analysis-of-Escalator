using System;

public class Signal
{
	protected int Length;
	protected int Fs;
	protected double[] time;
	protected double[] amplitude;

	public Signal(int Length = 8192, int Fs = 10000)
	{
        this.Length = Length;
		this.Fs = Fs;
		time = new double[Length];
		amplitude = new double[Length];
	}

	// set sample data
	// 放入采样数据
	public void Sample(double[] time, double[] amplitude)
	{
		int index = 0;
		while (index < Length)
		{
			this.time[index] = time[index];
			this.amplitude[index] = amplitude[index];
			index++;
		}
	}
}
