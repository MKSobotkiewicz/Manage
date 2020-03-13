using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ResourcePlantType
    {
        public ResourceValueList ResourceProductionCosts { get; private set; }
        public ResourceValueList ResourceProductionCostsPerTick { get; private set; }
        public ResourceValueList ResourceProduction { get; private set; }
        public ResourceValueList ResourceBuildCosts { get; private set; }
        public ulong ResourceProductionSpeedPerTick { get; private set; }
        public ulong BuildSpeedPerTick { get; private set; }

        private Info _Info { get; set; }

        public ResourcePlantType(Info info,
                                 ResourceValueList resourceProductionCosts,
                                 ResourceValueList resourceProductionCostsPerTick,
                                 ResourceValueList resourceProduction,
                                 ResourceValueList resourceBuildCosts,
                                 ulong resourceProductionSpeedPerTick,
                                 ulong buildSpeedPerTick)
        {

            _Info = info;
            ResourceProductionCosts = resourceProductionCosts;
            ResourceProductionCostsPerTick = resourceProductionCostsPerTick;
            ResourceProduction = resourceProduction;
            ResourceBuildCosts = resourceBuildCosts;
            ResourceProductionSpeedPerTick = resourceProductionSpeedPerTick;
            BuildSpeedPerTick = buildSpeedPerTick;
        }

        public Info Info()
        {
            return _Info;
        }
    }
}
