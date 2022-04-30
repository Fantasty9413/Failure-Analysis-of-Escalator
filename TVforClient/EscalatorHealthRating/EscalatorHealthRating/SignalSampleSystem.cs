using System;
using System.Collections.Generic;
using System.Text;

namespace SignalSamplePart
{
    public abstract class SignalData
    {
        public abstract void SignalDataSample(double[] s_x, double[] s_y);        // 信号采样
    }
}
