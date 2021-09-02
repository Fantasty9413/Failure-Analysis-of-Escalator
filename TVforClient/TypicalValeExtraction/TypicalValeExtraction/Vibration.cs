using System;
using ComputationMethod;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

public class VibrationAnalysis: Signal
{
    private FTM ftm;                    // Analysis method
    private MWNumericArray TD_t;
    private MWNumericArray TD_amp;
    private MWNumericArray FD_f;
    private MWNumericArray FD_amp;
    private MWNumericArray bf;          // basic frequency for Nbf analysis

    public VibrationAnalysis(int Length = 8192, int Fs = 10000):base(Length, Fs) 
    {
        ftm = new FTM();
        bf = 20;
    }
    public VibrationAnalysis(double[] time, double[] amplitude, int Length = 8192, int Fs = 10000):base(Length, Fs)
    {
        Sample(time, amplitude);
        TD_t = new MWNumericArray(1, this.Length, this.time);
        TD_amp = new MWNumericArray(1, this.Length, this.amplitude);

        ftm = new FTM();

        MWArray[] temp;
        temp = ftm.fft_ss(2, TD_amp, this.Fs);
        FD_amp = (MWNumericArray)temp[0];
        FD_f = (MWNumericArray)temp[1];

        bf = 20;
    }

    public void SetData(double[] time, double[] amplitude)
    {
        Sample(time, amplitude);
        TD_t = new MWNumericArray(1, this.Length, this.time);
        TD_amp = new MWNumericArray(1, this.Length, this.amplitude);

        MWArray[] temp;
        temp = ftm.fft_ss(2, TD_amp, this.Fs);
        FD_amp = (MWNumericArray)temp[0];
        FD_f = (MWNumericArray)temp[1];
    }

    public double Analysis_tfv()
    {
        MWNumericArray tfv;
        tfv = (MWNumericArray)ftm.TV_tfv(FD_amp);

        double _tfv = new double();
        _tfv = tfv.ToScalarDouble();

        return _tfv;
    }

    public Tuple<Array, Array> Analysis_pvifds()
    {
        MWNumericArray pvifds_amp, pvifds_f;
        MWArray[] temp;
        temp = ftm.TV_pvifds(2, FD_amp, FD_f);
        pvifds_amp = (MWNumericArray)temp[0];
        pvifds_f = (MWNumericArray)temp[1];

        Array _pvifds_amp = new Array[3];
        Array _pvifds_f = new Array[3];
        _pvifds_amp = pvifds_amp.ToVector(MWArrayComponent.Real);
        _pvifds_f = pvifds_f.ToVector(MWArrayComponent.Real);

        Tuple<Array, Array> result = new Tuple<Array, Array>(_pvifds_amp, _pvifds_f);
        return result;
    }

    public double Analysis_kv()
    {
        MWNumericArray kv;
        kv = (MWNumericArray)ftm.TV_kv(TD_amp);

        double _kv = new double();
        _kv = kv.ToScalarDouble();

        return _kv;
    }

    public Tuple<double, double, double, double> Analysis_pv()
    {
        MWNumericArray pv_max, t_max, pv_min, t_min;
        MWArray[] temp;
        temp = ftm.TV_pv(4, TD_amp, TD_t);
        pv_max = (MWNumericArray)temp[0];
        t_max = (MWNumericArray)temp[1];
        pv_min = (MWNumericArray)temp[2];
        t_min = (MWNumericArray)temp[3];

        double _pv_max, _t_max, _pv_min, _t_min;
        _pv_max = pv_max.ToScalarDouble();
        _t_max = t_max.ToScalarDouble();
        _pv_min = pv_min.ToScalarDouble();
        _t_min = t_min.ToScalarDouble();

        Tuple<double, double, double, double> result = new Tuple<double, double, double, double>(_pv_max, _t_max, _pv_min, _t_min);
        return result;
    }

    public double Analysis_ppv()
    {
        MWNumericArray ppv;
        ppv = (MWNumericArray)ftm.TV_ppv(TD_amp, TD_t);

        double _ppv = new double();
        _ppv = ppv.ToScalarDouble();

        return _ppv;
    }

    public void Setbf(double bf)
    {
        this.bf = bf;
    }

    public Array Analysis_Nbf()
    {
        MWNumericArray Nbf;
        Nbf = (MWNumericArray)ftm.TV_Nbf(FD_amp, FD_f, bf);

        Array _Nbf = new Array[3];
        _Nbf = Nbf.ToVector(MWArrayComponent.Real);

        return _Nbf;
    }
}
