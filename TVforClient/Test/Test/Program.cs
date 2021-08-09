using System;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using mtest;

namespace Singal
{
    class signal
    {
        private int Length;
        private double Fs;
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
            MWNumericArray amplitude = new MWNumericArray(1, 8000, s1.time);
            MWArray result;
            result = c.mtest(time, amplitude);
            Console.WriteLine("the result is " + result.ToString());
            //s1.displaydata();
        }
    }
}
