using System;
using SpectrumAnalysis;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

public class SpecAnalysis : Signal
{
    private SA sa;                    // Analysis method
    private readonly string[] Spec_Type;
    private MWNumericArray TD_t;
    private MWNumericArray TD_amp;

    public SpecAnalysis(int Length = 8192, int Fs = 10000) : base(Length, Fs)
    {
        sa = new SA();

        string[] spec_type = {"time", "fre", "pow", "enve", "ceps" };
        this.Spec_Type = new string[5];
        spec_type.CopyTo(this.Spec_Type, 0);
    }

    public SpecAnalysis(double[] time, double[] amplitude, int Length = 8192, int Fs = 10000) : base(Length, Fs)
    {
        sa = new SA();

        string[] spec_type = { "time", "fre", "pow", "enve", "ceps" };
        spec_type.CopyTo(this.Spec_Type, 0);

        Sample(time, amplitude);
        TD_t = new MWNumericArray(1, this.Length, this.time);
        TD_amp = new MWNumericArray(1, this.Length, this.amplitude);
    }

    public void SetData(double[] time, double[] amplitude)
    {
        Sample(time, amplitude);
        TD_t = new MWNumericArray(1, this.Length, this.time);
        TD_amp = new MWNumericArray(1, this.Length, this.amplitude);
    }

    public Tuple<Array, Array> GetSpec(string type)
    {
        MWNumericArray _Spec_y, _Spec_x;
        _Spec_y = TD_amp;
        _Spec_x = TD_t;

        MWArray[] temp;

        int index = Array.IndexOf(this.Spec_Type, type);
        
        switch(index)
        {
            case 0:
                _Spec_y = TD_amp;
                _Spec_x = TD_t;
                break;
            case 1:
                temp = sa.FreSpec(2, TD_amp, Fs);
                _Spec_y = (MWNumericArray)temp[0];
                _Spec_x = (MWNumericArray)temp[1];
                break;
            case 2:
                temp = sa.PowSpec(2, TD_amp, Fs);
                _Spec_y = (MWNumericArray)temp[0];
                _Spec_x = (MWNumericArray)temp[1];
                break;
            case 3:
                temp = sa.EnveSpec(2, TD_amp, Fs);
                _Spec_y = (MWNumericArray)temp[0];
                _Spec_x = (MWNumericArray)temp[1];
                break;
            case 4:
                temp = sa.CepsSpec(2, TD_amp, Fs);
                _Spec_y = (MWNumericArray)temp[0];
                _Spec_x = (MWNumericArray)temp[1];
                break;
            default:

                break;
        }

        Array Spec_y = new Array[this.Length];
        Array Spec_x = new Array[this.Length];

        Spec_y = _Spec_y.ToVector(MWArrayComponent.Real);
        Spec_x = _Spec_x.ToVector(MWArrayComponent.Real);

        Tuple<Array, Array> result = new Tuple<Array, Array>(Spec_y, Spec_x);
        return result;
    }
    public Tuple<Array, Array> GetSpec(string type, int begin, int end)
    {
        int len_result = end - begin + 1;

        var spec_result = GetSpec(type);

        double[] Spec_y = new double[len_result];
        double[] Spec_x = new double[len_result];

        for (int i = 0; i < len_result; i++)
        {
            Spec_y[i] = (double)spec_result.Item1.GetValue(begin + i);
            Spec_x[i] = (double)spec_result.Item2.GetValue(begin + i);
        }

        Tuple<Array, Array> result = new Tuple<Array, Array>(Spec_y, Spec_x);
        return result;
    }
}
