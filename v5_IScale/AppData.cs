﻿using iParkingv5.Objects.ScaleObjects;
using Kztek.Scale_net6.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v5_IScale
{
    public static class AppData
    {
        public static WeighingDetailCollection WeighingDetailCollection = new WeighingDetailCollection();
        public static WeighingFormCollection WeighingFormCollection = new WeighingFormCollection();
        public static ScaleConfig ScaleConfig = new ScaleConfig();
    }
}
