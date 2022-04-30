using System;
using System.Collections.Generic;
using System.Text;
using SignalSamplePart;
using TypicalValueExtractionPart;
using WorkConditionClassifierPart;
using ScoreRatingPart;

namespace EscalatorHealthRating
{
    public class EscalatorHealthRatingSystem_v1 : EscalatorHealthRatingSystem
    {
        // 原始信号数据
        private SignalData_VibrationSignal signaldata_motor;
        private SignalData_VibrationSignal signaldata_reducer;
        private SignalData_VibrationSignal signaldata_bolt;

        // 子分析系统
        private TypicalValueAnalysisSystem_v2 tvas_v2;      // 特征分析系统
        private WorkConditionClassifierSystem_v1 wccs_v1;   // 工况判断分析系统
        private HealthScoreRatingSystem_v1 hsrs_v1;         // 健康评分系统

        // 扶梯整体评分
        private int score;

        public EscalatorHealthRatingSystem_v1()
        {
            signaldata_motor = new SignalData_VibrationSignal();
            signaldata_reducer = new SignalData_VibrationSignal();
            signaldata_bolt = new SignalData_VibrationSignal();

            tvas_v2 = new TypicalValueAnalysisSystem_v2();
            wccs_v1 = new WorkConditionClassifierSystem_v1();
            hsrs_v1 = new HealthScoreRatingSystem_v1();

            score = 0;
        }
        private void SetData(SignalData_VibrationSignal[] signaldatas)      // 输入信号数据（以SignalData_VibrationSignal格式）
        {
            signaldata_motor = signaldatas[0];
            signaldata_reducer = signaldatas[1];
            signaldata_bolt = signaldatas[2];
        }
        private void RatingAnalysis()                                       // 系统整体评分
        {
            // step1. 特征分析
            Array tv_motor = tvas_v2.SystemWorking(signaldata_motor.GetSignalData_Time(), signaldata_motor.GetSignalData_Amplitude());          // motor部件振动信号的特征值
            Array tv_reducer = tvas_v2.SystemWorking(signaldata_reducer.GetSignalData_Time(), signaldata_reducer.GetSignalData_Amplitude());    // reducer部件振动信号的特征值
            Array tv_bolt = tvas_v2.SystemWorking(signaldata_bolt.GetSignalData_Time(), signaldata_bolt.GetSignalData_Amplitude());             // bolt部件振动信号的特征值

            // step2. 工况分析
            Array[] tvs = { tv_motor, tv_reducer, tv_bolt };                // 部件特征值集合
            Array labels = wccs_v1.SystemWorking(tvs);

            // step3. 总体评分
            score = hsrs_v1.SystemWorking((int[])labels);
        }
        private int GetScore()                                              // 获取整体评分
        {
            return this.score;
        }
        public override int SystemWorking(SignalData_VibrationSignal[] signaldatas)     // 系统运作
        {
            this.SetData(signaldatas);
            this.RatingAnalysis();
            return this.GetScore();
        }
    }
}
