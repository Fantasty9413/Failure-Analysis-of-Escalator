using System;
using System.Collections.Generic;
using System.Text;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using ComputationMethod;

namespace TypicalValueExtractionPart
{
    public class TypicalValueAnalysisSystem_v2 : TypicalValueAnalysisSystem
    {
        private FTM ftm;                    
        private MWNumericArray TD_t;
        private MWNumericArray TD_amp;
        private MWNumericArray FD_f;
        private MWNumericArray FD_amp;
        private double[] tv;                   // 特征值数组

        public TypicalValueAnalysisSystem_v2(int SignalLength = 8192, int SampleFrequency = 10000): base(SignalLength, SampleFrequency)
        {
            ftm = new FTM();

            TD_t = new MWNumericArray();
            TD_amp = new MWNumericArray();

            FD_f = new MWNumericArray();
            FD_amp = new MWNumericArray();

            tv = new double[16];
        }

        private void FFT()                                                                  // FFT变换，获取频域数据
        {
            MWArray[] temp;
            temp = ftm.fft_ss(2, TD_amp, this.Fs);
            FD_amp = (MWNumericArray)temp[0];
            FD_f = (MWNumericArray)temp[1];
        }
        private Tuple<double, double, double, double> Analysis_pv()                         // 特征值分析——pv
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
        private double Analysis_kv()                                                        // 特征值分析——kv
        {
            MWNumericArray kv;
            kv = (MWNumericArray)ftm.TV_kv(TD_amp);

            double _kv = new double();
            _kv = kv.ToScalarDouble();

            return _kv;
        }
        private double Analysis_ppv()                                                       // 特征值分析——ppv
        {
            MWNumericArray ppv;
            ppv = (MWNumericArray)ftm.TV_ppv(TD_amp, TD_t);

            double _ppv = new double();
            _ppv = ppv.ToScalarDouble();

            return _ppv;
        }
        private double Analysis_tfv()                                                       // 特征值分析——tfv
        {
            MWNumericArray tfv;
            tfv = (MWNumericArray)ftm.TV_tfv(FD_amp);

            double _tfv = new double();
            _tfv = tfv.ToScalarDouble();

            return _tfv;
        }
        private Array Analysis_Nbf()                                                        // 特征值分析——Nbf
        {
            MWNumericArray Nbf;
            int bf = 20;
            Nbf = (MWNumericArray)ftm.TV_Nbf(FD_amp, FD_f, bf);

            Array _Nbf = new Array[3];
            _Nbf = Nbf.ToVector(MWArrayComponent.Real);

            return _Nbf;
        }
        private Tuple<Array, Array> Analysis_pvifds()                                       // 特征值分析——pvifds
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

        public override void SetData(double[] time, double[] amplitude)                     // 输入待分析数据
        {
            time.CopyTo(this.time, 0);
            amplitude.CopyTo(this.amplitude, 0);
            TD_t = new MWNumericArray(1, this.Length, this.time);
            TD_amp = new MWNumericArray(1, this.Length, this.amplitude);
        }
        private void TVAnalysis()                                                           // 特征分析
        {
            // step1.fft变化
            this.FFT();

            // step2.特征分析
            double[] tv = new double[16];

            // 获取 pv
            var pv = this.Analysis_pv();
            tv.SetValue(pv.Item1, 0);
            tv.SetValue(pv.Item2, 1);
            tv.SetValue(pv.Item3, 2);
            tv.SetValue(pv.Item4, 3);

            // 获取 kv
            double kv = this.Analysis_kv();
            tv.SetValue(kv, 4);

            // 获取 ppv
            double ppv = this.Analysis_ppv();
            tv.SetValue(kv, 5);

            // 获取 tfv
            double tfv = this.Analysis_tfv();
            tv.SetValue(tfv, 6);

            // 获取 Nbf
            var Nbf = this.Analysis_Nbf();
            tv.SetValue(Nbf.GetValue(0), 7);
            tv.SetValue(Nbf.GetValue(1), 8);
            tv.SetValue(Nbf.GetValue(2), 9);

            // 获取 pvifds
            var pvifds = this.Analysis_pvifds();
            tv.SetValue(pvifds.Item1.GetValue(0), 10);
            tv.SetValue(pvifds.Item1.GetValue(1), 11);
            tv.SetValue(pvifds.Item1.GetValue(2), 12);
            tv.SetValue(pvifds.Item2.GetValue(0), 13);
            tv.SetValue(pvifds.Item2.GetValue(1), 14);
            tv.SetValue(pvifds.Item2.GetValue(2), 15);

            // step3.特征存储
            tv.CopyTo(this.tv, 0);
        }
        public override Array GetTV()                                                       // 输出特征分析结果
        {
            return tv;
        }
        public Array SystemWorking(double[] time, double[] amplitude)                       // 分析系统工作（读入数据、分析、输出结分析果）
        {
            this.SetData(time, amplitude);
            this.TVAnalysis();
            return this.GetTV();
        }
    }
}
