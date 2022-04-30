using System;
using WorkConditionClassifierAlgorithm;
using ScoreRatingAlgorithm;

namespace TypicalValeExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            Signal singal = new Signal();
            //SignalData singalData = new SignalData();         // 随机生成的测试数据
            SignalData singalData = new SignalData("test_data.mat");     // 导入的真实数据

            singal.Sample(singalData.time, singalData.amplitude);

            VibrationAnalysis va = new VibrationAnalysis(singalData.time, singalData.amplitude, 8192);

            double tfv = va.Analysis_tfv();
            Console.WriteLine("tfv: " + tfv.ToString());            // 通频值

            var pvifds = va.Analysis_pvifds();
            Console.WriteLine("pvifds_amp: " + pvifds.Item1.GetValue(0) + " " + pvifds.Item1.GetValue(1) + " " + pvifds.Item1.GetValue(2));     // 幅值
            Console.WriteLine("pvifds_f: " + pvifds.Item2.GetValue(0) + " " + pvifds.Item2.GetValue(1) + " " + pvifds.Item2.GetValue(2));     // 频率

            double kv = va.Analysis_kv();                   // 波形陡峭度
            Console.WriteLine("kv: " + kv.ToString());

            var pv = va.Analysis_pv();
            Console.WriteLine("pv_max: " + pv.Item1.ToString());    // 峰值max
            Console.WriteLine("t_max: " + pv.Item2.ToString());
            Console.WriteLine("pv_min: " + pv.Item3.ToString());    // 峰值min
            Console.WriteLine("t_min: " + pv.Item4.ToString());

            double ppv = va.Analysis_ppv();
            Console.WriteLine("ppv: " + ppv.ToString());     // 峰峰值

            // bug部分 注释掉
            va.Setbf(16.474);       // 设置基频值
            var Nbf = va.Analysis_Nbf();
            Console.WriteLine("Nbf: " + Nbf.GetValue(0) + " " + Nbf.GetValue(1) + " " + Nbf.GetValue(2));      // 1、2、3倍频值

            // 测试下一组数据
            //VibrationAnalysis va2 = new VibrationAnalysis();
            //va2.SetData(singalData.time, singalData.amplitude);     

            //double tfv2 = va2.Analysis_tfv();
            //Console.WriteLine("tfv: " + tfv2.ToString());

            //SignalData signalData2 = new SignalData();
            //va2.SetData(signalData2.time, signalData2.amplitude);       // 分析完一组数据后，分析第二组数据
            //tfv2 = va2.Analysis_tfv();
            //Console.WriteLine("tfv: " + tfv2.ToString());

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            // 基频部分测试
            double[] sp = { 0.65, 18.99, 19, 71, 21, 38.1, 133.33, 31.75, 41, 19, 894.6, 100, 861.34 };     // 参数输入
            Escalator_BFA bfa = new Escalator_BFA();
            bfa.Analysis(sp);
            //double bf = bfa.GetBf("f_motor");
            //Console.WriteLine("BF: " + bf.ToString());
            //Console.WriteLine(" ");
            string[] bf_type = { "f_motor", "f_m", "f_d", "f_md", "f_hd", "f_r", "f_dp", "f_sp", "f_hp" };
            for (int i = 0; i < bf_type.Length; i++)
            {
                double bf = bfa.GetBf(bf_type[i]);
                Console.Write(bf_type[i] + ": " + bf.ToString() + "   ");
                if ((i + 1) % 3 == 0)
                {
                    Console.Write("\n");
                }
            }

            // 谱分析
            SpecAnalysis sa = new SpecAnalysis();
            sa.SetData(singalData.time, singalData.amplitude);      // 输入待谱分析的数据值

            string[] spec_type = {"time", "fre", "pow", "enve", "ceps" };       // 频谱类型
            for (int i = 0; i < spec_type.Length; i++)
            {
                //var spec_result = sa.GetSpec(spec_type[i]);
                var spec_result = sa.GetSpec(spec_type[i], 1, 250);             // 得结果数据
                Console.WriteLine(" ");
                Console.WriteLine(spec_type[i]);
                Console.WriteLine("spec_y: " + " " + spec_result.Item1.GetValue(0) + " " + spec_result.Item1.GetValue(1) + " " + spec_result.Item1.GetValue(2));    // y轴数据
                Console.WriteLine("spec_x: " + " " + spec_result.Item2.GetValue(0) + " " + spec_result.Item2.GetValue(1) + " " + spec_result.Item2.GetValue(2));    // x轴数据
            }


            //var spec = sa.GetSpec("time");
            //Console.WriteLine("spec_y: " + " " + spec.Item1.GetValue(0) + " " + spec.Item1.GetValue(1) + " " + spec.Item1.GetValue(2));
            //Console.WriteLine("spec_x: " + " " + spec.Item2.GetValue(0) + " " + spec.Item2.GetValue(1) + " " + spec.Item2.GetValue(2));

            // 测试GetTv
            Array tv = va.GetTv();      // 一次性获取所有特征值
            Console.WriteLine(tv.ToString());

            // 测试GetWorkConditionLabel
            int Label = new int();
            double[] tv_channel_1 = (double[])tv;
            double[] tv_channel_2 = (double[])tv;
            double[] tv_channel_5 = (double[])tv;

            WorkConditionClassifier_motor wcc_motor = new WorkConditionClassifier_motor();                      // 获取motor部件的工况标签
            Label = wcc_motor.GetWorkConditionLabel(tv_channel_1);

            WorkConditionClassifier_reducer wcc_reducer = new WorkConditionClassifier_reducer();                // 获取reducer部件的工况标签
            Label = wcc_reducer.GetWorkConditionLabel(tv_channel_2);

            WorkConditionClassifier_bolt wcc_bolt = new WorkConditionClassifier_bolt();                         // 获取bolt部件的工况标签--使用单一模型
            Label = wcc_bolt.GetWorkConditionLabel(tv_channel_5);

            WorkConditionClassifier_complicated wcc_complicated = new WorkConditionClassifier_complicated();    // 获取bolt部件的工况标签--使用混合模型 
            Label = wcc_complicated.GetWorkConditionLabel(tv_channel_2, tv_channel_5);

            // 测试WorkConditionRating
            double avggrade = new double();

            WorkConditionRatingSystem wcrs = new WorkConditionRatingSystem_bolt();

            WorkConditionRatingSystem_motor wcrs_motor = new WorkConditionRatingSystem_motor();
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);       // 模拟连续采集数据连续评分
            avggrade = wcrs_motor.WorkConditionRating(2);
            avggrade = wcrs_motor.WorkConditionRating(2);
            avggrade = wcrs_motor.WorkConditionRating(3);
            avggrade = wcrs_motor.WorkConditionRating(3);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(1);
            avggrade = wcrs_motor.WorkConditionRating(3);
            avggrade = wcrs_motor.WorkConditionRating(2);

            WorkConditionRatingSystem_reducer wcrs_reducer = new WorkConditionRatingSystem_reducer();
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);       // 模拟连续采集数据连续评分
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 3);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 3);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 1);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 3);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 3);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);
            avggrade = wcrs_reducer.WorkConditionRating(3 + 2);

            wcrs = new WorkConditionRatingSystem_bolt();
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(9);
            avggrade = wcrs.WorkConditionRating(10);
            avggrade = wcrs.WorkConditionRating(11);

            WorkConditionRatingSystem_bolt wcrs_bolt = new WorkConditionRatingSystem_bolt();
            avggrade = wcrs_bolt.WorkConditionRating(9, 1);
            avggrade = wcrs_bolt.WorkConditionRating(9, 1);
            avggrade = wcrs_bolt.WorkConditionRating(10, 1);
            avggrade = wcrs_bolt.WorkConditionRating(11, 1);
            avggrade = wcrs_bolt.WorkConditionRating(11, 7);

            // 测试WorkConditionRating
            HealthScoreRatingSystem_v1 hsrs = new HealthScoreRatingSystem_v1();
            int[] Labels = { 1, 4, 9 };
            int healthscore = 0;
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);
            healthscore = hsrs.GetHealthScore(Labels);


            // 按任意键退出
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}
