using System;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using mtest;
using TypicalValue;
using TypeValue;
using Singal;

namespace Singal
{
    class signal
    {
        public int Length;
        public double Fs;
        public double[] time;
        public double[] amplitude;
        public signal()
        {
            Length = 8000;
            Fs = 10000;
            time = new double[Length];
            amplitude = new double[Length];
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                time[i] = (i) / Fs;
                amplitude[i] = 100 * rd.NextDouble();
            }
        }

        public void displayinformation()
        {
            Console.WriteLine("Length of signal is " + Length.ToString());
            Console.WriteLine("Sample Frequence of signal is " + Fs.ToString());
        }

        public void displaydata()
        {
            int i = 0;
            Console.WriteLine("The amplitude of signal is ");
            while (i < Length)
            {
                Console.Write(amplitude[i++].ToString() + " ");
            }
            Console.WriteLine(" ");

        }
    }
}

namespace Test
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //    Singal.signal s1 = new Singal.signal();
        //    s1.displayinformation();

        //    Class1 c = new Class1();
        //    double ans = new double();
        //    MWNumericArray time = new MWNumericArray(1, 8000, s1.time);
        //    MWNumericArray amplitude = new MWNumericArray(1, 8000, s1.amplitude);
        //    MWNumericArray Fs = new MWNumericArray(s1.Fs);
        //    MWArray result;
        //    result = c.mtest(time, amplitude);
        //    Console.WriteLine("the result is " + result.ToString());

        //    Class2 c2 = new Class2();
        //    MWArray FD_fre, FD_amp;
        //    MWArray[] result2;
        //    MWNumericArray[] result3;
        //    result2 = c2.fft_ss(2, amplitude, Fs);
        //    // result3 = (MWNumericArray[])(c2.fft_ss(2, amplitude, Fs));
        //    FD_amp = result2[0];
        //    FD_fre = result2[1];
        //    Console.WriteLine(FD_amp.ToString());
        //    // Console.WriteLine(result3.ToString());
        //    // Console.WriteLine("the result is " + FD_amp.ToString());
        //    // Console.WriteLine("the result is " + FD_fre.ToString());
        //    //s1.displaydata();

        //    Array a1, temp;
        //    a1 = ((MWNumericArray)(FD_amp)).ToVector(MWArrayComponent.Real);
        //    temp = ((MWNumericArray)(FD_amp)).ToVector(MWArrayComponent.Real);


        //}

        static void Main(string[] args)
        {
            TV tv = new TV();
            signal s_test = new signal();

            MWNumericArray TD_t = new MWNumericArray(1, 8000, s_test.time);
            MWNumericArray TD_amp = new MWNumericArray(1, 8000, s_test.amplitude);
            MWNumericArray Fs = new MWNumericArray(s_test.Fs);

            MWArray[] result;
            MWNumericArray FD_amp, FD_f;
            result = tv.fft_ss(2, TD_amp, Fs);
            FD_amp = (MWNumericArray)result[0];
            FD_f = (MWNumericArray)result[1];
            // Console.WriteLine("FD_amp: " + FD_amp.ToString());
            Array.Clear(result, 0, result.Length);

            // TV_tfv
            MWNumericArray tfv;
            tfv = (MWNumericArray)tv.TV_tfv(FD_amp);

            // TV_pvifds
            MWNumericArray pvifds_amp, pvifds_f;
            result = tv.TV_pvifds(2, FD_amp, FD_f);
            pvifds_amp = (MWNumericArray)result[0];
            pvifds_f = (MWNumericArray)result[1];
            Array.Clear(result, 0, result.Length);

            // TV_kv
            MWNumericArray kv;
            kv = (MWNumericArray)tv.TV_kv(TD_amp);

            // TV_pv
            MWNumericArray pv_max, t_max, pv_min, t_min;
            result = tv.TV_pv(4, TD_amp, TD_t);
            pv_max = (MWNumericArray)result[0];
            t_max = (MWNumericArray)result[1];
            pv_min = (MWNumericArray)result[2];
            t_min = (MWNumericArray)result[3];
            Array.Clear(result, 0, result.Length);

            // TV_ppv
            MWNumericArray ppv;
            ppv = (MWNumericArray)tv.TV_ppv(TD_amp, TD_t);

            // TV_Nbf
            MWNumericArray Nbf;
            MWNumericArray bf = 20;
            Nbf = (MWNumericArray)tv.TV_Nbf(FD_amp, FD_f, bf);

        }
    }
}
