using System;
using System.Collections.Generic;
using System.Text;
using HealthScoreRating;

namespace ScoreRatingPart
{
    abstract class WorkConditionRatingSystem
    {
        private string PartName;        // 部件名称
        private int QueueLen;           // 分数队列长度
        public Queue<double> WorkConditionGradesQueue;  // 分数队列
        public HSR hsr;     // health score rating
        public WorkConditionRatingSystem(string partname, int queuelen = 10)
        {
            PartName = partname;
            hsr = new HSR();
            // 初始化分数队列
            QueueLen = queuelen;
            WorkConditionGradesQueue = new Queue<double>(QueueLen);
            for (int i = 0; i < QueueLen; i++)
            {
                WorkConditionGradesQueue.Enqueue(0);
            }
        }
        public abstract double GetSingleGrade(int Label);    // 获取单次分数
        public void UpdateGradeQueue(double grade)           // 更新分数队列
        {
            this.WorkConditionGradesQueue.Dequeue();
            this.WorkConditionGradesQueue.Enqueue(grade);
        }
        public double GetAverageGrade()                      // 获取平均分数
        {
            if (WorkConditionGradesQueue.Count != QueueLen)
            {
                throw new Exception("分数队列长度错误！");
            }

            double GradeSum = 0;
            foreach (double grade in WorkConditionGradesQueue)
            {
                GradeSum += grade;
            }

            return GradeSum / QueueLen;
        }
        public double WorkConditionRating(int Label)         // 工况评分。整体方法封装。
        {
            double grade = 0;
            grade = this.GetSingleGrade(Label);

            this.UpdateGradeQueue(grade);

            double avggrade = 0;
            avggrade = this.GetAverageGrade();

            return avggrade;
        }
    }
}
