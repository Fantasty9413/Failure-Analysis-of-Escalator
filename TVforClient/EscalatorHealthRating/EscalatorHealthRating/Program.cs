using System;
using TestTool;
using SignalSamplePart;
using TypicalValueExtractionPart;
using WorkConditionClassifierPart;
using ScoreRatingPart;
using EscalatorHealthRating;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            /**************************************************************************************************/
            ///*模块测试*/    // 取消注释

            //// 测试信号模拟器
            //TestSignalGenerator tsg = new TestSignalGenerator();
            //double[] originalsignal_time = tsg.GetSignalData_Time();
            //double[] originalsignal_amp = tsg.GetSignalData_Amplitude();

            //// 频谱分析测试
            //SpectrumAnalysisSystem_V1 sas_v1 = new SpectrumAnalysisSystem_V1();
            //sas_v1.SystemWorking(originalsignal_time, originalsignal_amp);
            //var temp = sas_v1.GetSpec("pow");

            //// 信号采集系统
            //SignalData_VibrationSignal ssy_vs = new SignalData_VibrationSignal();
            //ssy_vs.SignalDataSample(originalsignal_time, originalsignal_amp);
            //double[] samplesignal_time = ssy_vs.GetSignalData_Time();
            //double[] samplesignal_amp = ssy_vs.GetSignalData_Amplitude();

            //// 特征分析系统
            //TypicalValueAnalysisSystem_v2 tvas_v2 = new TypicalValueAnalysisSystem_v2();
            //Array tv = tvas_v2.SystemWorking(samplesignal_time, samplesignal_amp);

            //// 工况判断分析系统
            //Array tv_motor = tv;
            //Array tv_reducer = tv;
            //Array tv_bolt = tv;
            //Array[] tvs = { tv_motor, tv_reducer, tv_bolt };
            //WorkConditionClassifierSystem_v1 wccs_v1 = new WorkConditionClassifierSystem_v1();
            //Array labels = wccs_v1.SystemWorking(tvs);

            //// 健康评分系统
            //HealthScoreRatingSystem_v1 hsrs_v1 = new HealthScoreRatingSystem_v1();
            //int healthscor = 0;
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            //healthscor = hsrs_v1.SystemWorking((int[])labels);
            /**************************************************************************************************/


            /**************************************************************************************************/
            /*整体测试*/

            //// step0. 模拟数据生成
            //// 测试信号模拟
            //TestSignalGenerator tsg = new TestSignalGenerator();
            //double[] originalsignal_time = tsg.GetSignalData_Time();
            //double[] originalsignal_amp = tsg.GetSignalData_Amplitude();
            //// 模拟数据采集后读入
            //// 通道1采集电机振动信号
            //double[] signal_channel1_time = originalsignal_time;        // 时间序列信号
            //double[] signal_channel1_amp = originalsignal_amp;          // 幅值序列信号
            //// 通道1采集电机振动信号
            //double[] signal_channel2_time = originalsignal_time;
            //double[] signal_channel2_amp = originalsignal_amp;
            //// 通道1采集电机振动信号
            //double[] signal_channel5_time = originalsignal_time;
            //double[] signal_channel5_amp = originalsignal_amp;


            //// step1.初始化
            //// 创建signal对象
            //SignalData_VibrationSignal SD_motor = new SignalData_VibrationSignal();
            //SignalData_VibrationSignal SD_reducer = new SignalData_VibrationSignal();
            //SignalData_VibrationSignal SD_bolt = new SignalData_VibrationSignal();
            //SignalData_VibrationSignal[] SDs = new SignalData_VibrationSignal[3] { SD_motor, SD_reducer, SD_bolt };
            //// 创建score
            //int score = 0;
            //// 创建扶梯健康评分系统
            //EscalatorHealthRatingSystem_v1 ehrs_v1 = new EscalatorHealthRatingSystem_v1();

            //// step2.评估系统运行测试
            //// 扶梯健康评分系统测试
            //int N = 500;                // 测试循环次数
            //int[] scores = new int[500];// 存放所有分数，用于测试展示
            //for (int i = 0; i < N; i++)      // 模拟持续采集数据并进行扶梯诊断分析
            //{
            //    // 输入--信号数据采集
            //    SD_motor.SignalDataSample(signal_channel1_time, signal_channel1_amp);
            //    SD_reducer.SignalDataSample(signal_channel2_time, signal_channel2_amp);
            //    SD_bolt.SignalDataSample(signal_channel5_time, signal_channel5_amp);
            //    SDs = new SignalData_VibrationSignal[3] { SD_motor, SD_reducer, SD_bolt };
            //    // 输出--分析结果
            //    score = ehrs_v1.SystemWorking(SDs);
            //    scores[i] = score;
            //}

            //Console.WriteLine("扶梯的健康分数为： " + score.ToString());
            /**************************************************************************************************/
        }
    }
}
