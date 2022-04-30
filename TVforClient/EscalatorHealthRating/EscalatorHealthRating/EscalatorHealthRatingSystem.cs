using System;
using System.Collections.Generic;
using System.Text;
using SignalSamplePart;

namespace EscalatorHealthRating
{
    public abstract class EscalatorHealthRatingSystem
    {
        public abstract int SystemWorking(SignalData_VibrationSignal[] signaldatas);
    }
}
