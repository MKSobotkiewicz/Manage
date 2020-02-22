using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manage.Units
{
    public interface IDestructable:IDisposable
    {
        int HitPoints();
        int Armor();

        bool Damage(int value);
    }
}
