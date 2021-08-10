using System;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using mtest;
using TypicalValue;

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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Singal.signal s1 = new Singal.signal();
            s1.displayinformation();

            Class1 c = new Class1();
            double ans = new double();
            MWNumericArray time = new MWNumericArray(1, 8000, s1.time);
            MWNumericArray amplitude = new MWNumericArray(1, 8000, s1.amplitude);
            MWNumericArray Fs = new MWNumericArray(s1.Fs);
            MWArray result;
            result = c.mtest(time, amplitude);
            Console.WriteLine("the result is " + result.ToString());

            Class2 c2 = new Class2();
            MWArray FD_fre, FD_amp;
            MWArray[] result2;
            MWNumericArray[] result3;
            result2 = c2.fft_ss(2, amplitude, Fs);
            // result3 = (MWNumericArray[])(c2.fft_ss(2, amplitude, Fs));
            FD_amp = result2[0];
            FD_fre = result2[1];
            Console.WriteLine(FD_amp.ToString());
            // Console.WriteLine(result3.ToString());
            // Console.WriteLine("the result is " + FD_amp.ToString());
            // Console.WriteLine("the result is " + FD_fre.ToString());
            //s1.displaydata();

            Array a1;
            a1 = ((MWNumericArray)(FD_amp)).ToVector(MWArrayComponent.Real);
        }
    }
}
