using System;
using System.Collections.Generic;
using System.Text;

namespace WorkConditionClassifierPart
{
    public class WorkConditionClassifierSystem_v1 : WorkConditionClassifierSystem
    {
        // 工况分析器WCC
        private WorkConditionClassifier_motor wcc_motor;
        private WorkConditionClassifier_reducer wcc_reducer;
        private WorkConditionClassifier_bolt wcc_bolt;
        // 部件振动信号特征值
        private Array tv_motor;         // 原始数据来源于channel-1
        private Array tv_reducer;       // 原始数据来源于channel-2
        private Array tv_bolt;          // 原始数据来源于channel-5
        // 工况结果标签
        private int label_motor;
        private int label_reducer;
        private int label_bolt;
        // 输出结果
        private Array labels;

        public WorkConditionClassifierSystem_v1()
        {
            wcc_motor = new WorkConditionClassifier_motor();
            wcc_reducer = new WorkConditionClassifier_reducer();
            wcc_bolt = new WorkConditionClassifier_bolt();
            tv_motor = new Array[16];
            tv_reducer = new Array[16];
            tv_bolt = new Array[16];
            label_motor = 0;
            label_reducer = 0;
            label_bolt = 0;
            labels = new int[3] { 0, 0, 0};
        }
        private void SetTv(Array[] tvs)         // 输入特征值
        {
            tv_motor = tvs[0];       // motor部件信号的特征值
            tv_reducer = tvs[1];     // reducer部件信号的特征值
            tv_bolt = tvs[2];        // bolt部件信号的特征值
        }

        private void WCCAnalysis()              // WCC分析--工况判断
        {
            label_motor = wcc_motor.GetWorkConditionLabel((double[])tv_motor);
            label_reducer = wcc_reducer.GetWorkConditionLabel((double[])tv_reducer);
            label_bolt = wcc_bolt.GetWorkConditionLabel((double[])tv_bolt);
        }

        private Array GetLabels()               // 获取标签结果--获取工况判断结果
        {
            labels.SetValue(label_motor, 0);
            labels.SetValue(label_reducer, 1);
            labels.SetValue(label_bolt, 2);
            return labels;
        }
        public override Array SystemWorking(Array[] tvs)
        {
            this.SetTv(tvs);
            this.WCCAnalysis();
            return this.GetLabels();
        }
    }
}
