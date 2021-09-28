using System;
using BasicFrequencyAnaysis;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

public class Escalator_BFA
{
	private BFA bfa;
	private readonly string[] BF_Type;
	private double[] SP;                // structure parameters
	private double[] BF;				// basic frequency

	public Escalator_BFA()
	{
		bfa = new BFA();
		this.BF_Type = new string[9];
		this.SP = new double[20];
		this.BF = new double[20];

		string[] bf_type = {"f_motor", "f_m", "f_d", "f_md", "f_hd", "f_r", "f_dp", "f_sp", "f_hp"};		// basic frequency type
		bf_type.CopyTo(this.BF_Type, 0);
	}

	public void Analysis(double[] SP)	// analysis for all basic frequency
	{
		SP.CopyTo(this.SP, 0);
		MWNumericArray _SP = new MWNumericArray(1, 20, this.SP);

		MWNumericArray _BF = new MWNumericArray();
		_BF = (MWNumericArray)bfa.BF_Structure(_SP);

		this.BF = (double[])_BF.ToVector(MWArrayComponent.Real);
	}
	public double GetBf(string type)	// get basic frequenct of expected type
	{
		int index = Array.IndexOf(this.BF_Type, type);
		return (index != -1 ? BF[index] : 0);
	}
}

