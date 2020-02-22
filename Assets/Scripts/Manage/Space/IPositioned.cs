using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Manage.Space
{
    public interface IPositioned
    {
        Vector3 Position();
        IPositioned Move(Vector3 vector);
        IPositioned Rotate(Vector3 eulerAngles);
    }
}
