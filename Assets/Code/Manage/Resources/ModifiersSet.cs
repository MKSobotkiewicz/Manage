using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Resources
{
    public class ModifiersSet
    {
        public ResourceModifierList BuildCostModifiers { get; private set; }
        public ResourceModifierSpeedList BuildSpeedModifiers { get; private set; }
        public ResourceModifierList ProductionModifiers { get; private set; }
        public ResourceModifierList ProductionCostModifiersPerTick { get; private set; }
        public ResourceModifierList ProductionCostModifiers { get; private set; }
        public ResourceModifierSpeedList ProductionSpeedModifiers { get; private set; }

        public ModifiersSet()
        {
            Debug.Logger.Log(Debug.Logger.InfoLevel.InfoLevel2, "Object of type ModifiersSet created.");
            BuildCostModifiers = new ResourceModifierList();
            ProductionModifiers = new ResourceModifierList();
            ProductionCostModifiers = new ResourceModifierList();
            ProductionCostModifiersPerTick = new ResourceModifierList();
            ProductionSpeedModifiers = new ResourceModifierSpeedList();
            BuildSpeedModifiers = new ResourceModifierSpeedList();
        }
    }
}
