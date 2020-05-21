/*MIT License

Copyright(c) 2020 Dawid Dyrcz

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

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
            this.AxisX = Vector3.Normalize(axisX);

            //Calculate axisZ
            this.AxisZ = Vector3.Normalize(Vector3.Cross(axisX, axisY));

            //Correct axisY to be sure that all vectors are orthodonal. 
            this.AxisY = Vector3.Normalize(Vector3.Cross(this.AxisZ, axisX));
        }

        public static CoordinateSystem From3Points(Vector3 origin, Vector3 pointOnTheAxisX, Vector3 pointOnTheXYPlane)
        {
            return new CoordinateSystem(origin, pointOnTheAxisX - origin, pointOnTheXYPlane - origin);
        }

        public Matrix4 GetTransformationMatrix()
        {
            var rotation =  new Matrix4(new Vector4(AxisX.X, AxisY.X, AxisZ.X, 0f),
                                       new Vector4(AxisX.Y, AxisY.Y, AxisZ.Y, 0f),
                                       new Vector4(AxisX.Z, AxisY.Z, AxisZ.Z, 0f),
                                       Vector4.UnitW
                                       );
            var translation = Matrix4.CreateTranslation(-1*Origin);
            return translation * rotation;
        }

        public override string ToString()
        {
            return "Coordinate system: Origin: " + this.Origin.ToString() + " AxisX:" + this.AxisX.ToString() + " AxisY:" + this.AxisY.ToString();
        }
    }
}
