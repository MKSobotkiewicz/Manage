using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage.Control
{
    public static class MoveOrderPositions
    {
        public static List<Vector3> GetPositions(List<Vector3> currentPositionOfUnits,Vector3 orderPosition,float unitSpread)
        {
            Vector3 averagePostionOfUnits = new Vector3(0,0,0);

            foreach (var position in currentPositionOfUnits)
            {
                averagePostionOfUnits += position;
            }
            averagePostionOfUnits /= currentPositionOfUnits.Count;

            List<Vector3> orderPositions = new List<Vector3>();

            float i = -((currentPositionOfUnits.Count * unitSpread )/ 2-(unitSpread/2));
            foreach (var position in currentPositionOfUnits)
            {
                orderPositions.Add(orderPosition +Vector3.Cross(new Vector3(0,1,0), (averagePostionOfUnits-orderPosition ).normalized)*i);
                i += unitSpread;
            }

            return orderPositions;
        }
    }
}