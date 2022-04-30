using System;
using System.Collections.Generic;
using System.Text;

namespace SignalSamplePart
{
    public class SignalData_VibrationSignal : SignalData
    {
        protected int Length;           // 信号长度
        protected int Fs;               // 采样频率
        protected double[] time;        // 时间序列
        protected double[] amplitude;   // 幅值序列

        public SignalData_VibrationSignal(int SignalLength = 8192, int SampleFrequency = 10000)
        {
            this.Length = SignalLength;
            this.Fs = SampleFrequency;
            time = new double[Length];
            amplitude = new double[Length];
        }
        public override void SignalDataSample(double[] time, double[] amplitude)         // 信号采样
        {
            int index = 0;
            while (index < Length)
            {
                this.time[index] = time[index];
                this.amplitude[index] = amplitude[index];
                index++;
            }
        }
        public double[] GetSignalData_Time()                                         // 获取信号的时间序列
        {
            double[] time = new double[Length];
            this.time.CopyTo(time, 0);
            return time;

        }
        public double[] GetSignalData_Amplitude()                                    // 获取信号的幅值序列
        {
            double[] amplitude = new double[Length];
            this.amplitude.CopyTo(amplitude, 0);
            return amplitude;
        }
    }
}
