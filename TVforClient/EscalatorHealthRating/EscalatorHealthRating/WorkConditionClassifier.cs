using System;
using System.Collections.Generic;
using System.Text;
using WorkConditionClassifier;

namespace WorkConditionClassifierPart
{
    public abstract class WorkConditionClassifier
    {
        private string PartName;
        public WCC wcc;        // workconditionclassifier
        public WorkConditionClassifier(string partname)
        {
            PartName = partname;
            wcc = new WCC();
        }
        public abstract int GetWorkConditionLabel(double[] tv);
    }
}