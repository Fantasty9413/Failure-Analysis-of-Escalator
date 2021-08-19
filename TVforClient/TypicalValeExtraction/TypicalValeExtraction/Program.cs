using System;

namespace TypicalValeExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            Signal singal = new Signal();
            SignalData singalData = new SignalData();
            singal.Sample(singalData.time, singalData.amplitude);

            VibrationAnalysis va = new VibrationAnalysis(singalData.time, singalData.amplitude, 8092);

            double tfv = va.Analysis_tfv();
            Console.WriteLine(tfv.ToString());              // 通频值

            var pvifds = va.Analysis_pvifds();
            Console.WriteLine(pvifds.Item1.ToString());     // 幅值
            Console.WriteLine(pvifds.Item2.ToString());     // 频率

            double kv = va.Analysis_kv();                   // 波形陡峭度
            Console.WriteLine(kv.ToString());

            var pv = va.Analysis_pv();
            Console.WriteLine("pv_max: " + pv.Item1.ToString());    // 峰值max
            Console.WriteLine("t_max: " + pv.Item2.ToString());
            Console.WriteLine("pv_min: " + pv.Item3.ToString());    // 峰值min
            Console.WriteLine("t_min: " + pv.Item4.ToString());

            double ppv = va.Analysis_ppv();
            Console.WriteLine("ppv: " + ppv.ToString());     // 峰峰值

            // bug部分 注释掉
            va.Setbf(20);       // 设置基频值
            var Nbf = va.Analysis_Nbf();
            Console.WriteLine(Nbf.ToString());      // 1、2、3倍频值

            VibrationAnalysis va2 = new VibrationAnalysis();
            va2.SetData(singalData.time, singalData.amplitude);     

            double tfv2 = va2.Analysis_tfv();
            Console.WriteLine("tfv: " + tfv2.ToString());

            SignalData signalData2 = new SignalData();
            va2.SetData(signalData2.time, signalData2.amplitude);       // 分析完一组数据后，分析第二组数据
            tfv2 = va2.Analysis_tfv();
            Console.WriteLine("tfv: " + tfv2.ToString());
        }
    }
}
