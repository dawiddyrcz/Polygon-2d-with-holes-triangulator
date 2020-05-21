/*
*Copyright (C) Dawid Dyrcz 2020 - All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace PolygonWithHolesTriangulator
{
    public class CoordinateSystem
    {
        public Vector3 Origin { get; private set; } = new Vector3(0, 0, 0);
        public Vector3 AxisX { get; private set; } = new Vector3(1, 0, 0);
        public Vector3 AxisY { get; private set; } = new Vector3(0, 1, 0);
        public Vector3 AxisZ { get; private set; } = new Vector3(0, 0, 1);

        public CoordinateSystem()
        {

        }

        public CoordinateSystem(Vector3 origin, Vector3 axisX, Vector3 axisY)
        {
            this.Origin = origin;
            this.AxisX = axisX;
            
            //var axisZ
        }
    }
}
