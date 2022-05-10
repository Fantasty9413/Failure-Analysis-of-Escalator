using System;
using System.Collections.Generic;
using System.Text;
using SpectrumAnalysis;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace SignalSamplePart
{
    public class SpectrumAnalysisSystem_V1 : SpectrumAnalysisSystem
    {
        private SA sa;                    // Analysis method
        private readonly string[] Spec_Type;
        private MWNumericArray _time;
        private MWNumericArray _amplitude;
        private Array[,] spectrums;
        public SpectrumAnalysisSystem_V1(int Length = 8192, int Fs = 10000)
        {
            this.Length = Length;
            this.Fs = Fs;
            this.time = new double[this.Length];
            this.amplitude = new double[this.Length];

            sa = new SA();
            string[] spec_type = { "time", "fre", "pow", "enve", "ceps" };
            this.Spec_Type = new string[5];
            spec_type.CopyTo(this.Spec_Type, 0);

            spectrums = new Array[spec_type.Length, 2];
        }
        private void SetData(double[] time, double[] amplitude)                 // 输入信号数据序列
        {
            time.CopyTo(this.time, 0);
            amplitude.CopyTo(this.amplitude, 0);
            _time = new MWNumericArray(1, this.Length, this.time);
            _amplitude = new MWNumericArray(1, this.Length, this.amplitude);
        }
        private Tuple<Array, Array> GetSpectrum(string type)                    // 单个谱的谱图分析
        {
            MWNumericArray _Spec_y, _Spec_x;
            _Spec_y = _amplitude;               // 纵坐标
            _Spec_x = _time;                    // 横坐标

            MWArray[] temp;

            int index = Array.IndexOf(this.Spec_Type, type);

            switch (index)
            {
                case 0:
                    _Spec_y = _amplitude;
                    _Spec_x = _time;
                    break;
                case 1:
                    temp = sa.FreSpec(2, _amplitude, Fs);
                    _Spec_y = (MWNumericArray)temp[0];
                    _Spec_x = (MWNumericArray)temp[1];
                    break;
                case 2:
                    temp = sa.PowSpec(2, _amplitude, Fs);
                    _Spec_y = (MWNumericArray)temp[0];
                    _Spec_x = (MWNumericArray)temp[1];
                    break;
                case 3:
                    temp = sa.EnveSpec(2, _amplitude, Fs);
                    _Spec_y = (MWNumericArray)temp[0];
                    _Spec_x = (MWNumericArray)temp[1];
                    break;
                case 4:
                    temp = sa.CepsSpec(2, _amplitude, Fs);
                    _Spec_y = (MWNumericArray)temp[0];
                    _Spec_x = (MWNumericArray)temp[1];
                    break;
                default:

                    break;
            }

            Array Spec_y = new Array[this.Length];
            Array Spec_x = new Array[this.Length];

            Spec_y = _Spec_y.ToVector(MWArrayComponent.Real);       // 纵坐标
            Spec_x = _Spec_x.ToVector(MWArrayComponent.Real);       // 横坐标

            Tuple<Array, Array> result = new Tuple<Array, Array>(Spec_x, Spec_y);
            return result;
        }
        private void SpectrumAnalysis()
        {
            for(int i=0; i<Spec_Type.Length; i++)
            {
                Tuple<Array, Array> temp = this.GetSpectrum(Spec_Type[i]);
                spectrums[i, 0] = temp.Item1;       // 横坐标序列
                spectrums[i, 1] = temp.Item2;       // 纵坐标序列
            }
        }
        public override void SystemWorking(double[] time, double[] amplitude)
        {
            this.SetData(time, amplitude);
            this.SpectrumAnalysis();
        }
        public Tuple<Array, Array> GetSpec(string type)                             // 获取谱分析结果
        {
            int index = Array.IndexOf(this.Spec_Type, type);
            if (index==-1) throw new Exception("Error: 谱图类型不存在！");
            Tuple<Array, Array> result = new Tuple<Array, Array>(this.spectrums[index, 0], this.spectrums[index, 1]);
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

            Tuple<Array, Array> result = new Tuple<Array, Array>(Spec_x, Spec_y);
            return result;
        }
    }
}
