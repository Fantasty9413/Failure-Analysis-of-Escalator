using System;
using System.Collections.Generic;
using System.Text;

namespace SignalSamplePart
{
    public abstract class SpectrumAnalysisSystem
    {
        protected int Length;
        protected int Fs;
        protected double[] time;
        protected double[] amplitude;
        public abstract void SystemWorking(double[] time, double[] amplitude);
    }
}
