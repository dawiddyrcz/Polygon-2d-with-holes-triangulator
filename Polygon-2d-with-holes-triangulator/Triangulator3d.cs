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

using System.Collections.Generic;

namespace PolygonWithHolesTriangulator
{
    public static class Triangulator3d
    {
        //Not works!!!
        public static void Triangulate(List<Vector3> shapeVertexes,
            List<List<Vector3>> collectionOfholesVetexes,
            out Vector3[] outputVertexes, out int[] outputIndices
            )
        {

            //Not works when change coordinate system. 2d algorythm have error probably with winding orders

            var coordinateSystem = CoordinateSystem.From3Points(shapeVertexes[0], shapeVertexes[1], shapeVertexes[2]);
            var matrixTo2d = coordinateSystem.GetTransformationMatrix();

          //  var cs = new CoordinateSystem(new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(-1, 0, 0));
           //   var matrixTo2d = cs.GetTransformationMatrix();

            // var matrixTo2d =   Matrix4.Identity;

            var matrixTo3d = Matrix4.Invert(matrixTo2d);

            var transformedShapeVertexes = ConvertTo2d(shapeVertexes, ref matrixTo2d);
            //transformedShapeVertexes = Triangulator.EnsureWindingOrder(transformedShapeVertexes, WindingOrder.CounterClockwise);

            if (collectionOfholesVetexes.Count != 0)
            {
                foreach (var holeVertexes in collectionOfholesVetexes)
                {
                    var transformedHoleVertexes = ConvertTo2d(holeVertexes, ref matrixTo2d);
                    //transformedHoleVertexes = Triangulator.EnsureWindingOrder(transformedHoleVertexes, WindingOrder.Clockwise);
                    transformedShapeVertexes = Triangulator.CutHoleInShape(transformedShapeVertexes, transformedHoleVertexes);
                }
            }

            
            Triangulator.Triangulate(transformedShapeVertexes, WindingOrder.Clockwise,
                    out Vector2[] outputVertexesArray, out int[] indices);

            outputIndices = indices;
            outputVertexes = ConvertTo3d(outputVertexesArray, ref matrixTo3d);
        }

        private static Vector2[] ConvertTo2d(List<Vector3> inputList, ref Matrix4 matrix)
        {
            var output = new List<Vector2>(inputList.Count + 1);

            foreach (var point in inputList)
            {
                var transFormedPoint = Vector4.Transform(new Vector4(point.X, point.Y, point.Z, 1.0f), matrix);
                output.Add(new Vector2(transFormedPoint.X, transFormedPoint.Y));
            }

            return output.ToArray();
        }

        private static Vector3[] ConvertTo3d(Vector2[] inputList, ref Matrix4 matrix)
        {
            var output = new List<Vector3>(inputList.Length + 1);

            foreach (var point in inputList)
            {
                var transFormedPoint = Vector4.Transform(new Vector4(point.X, point.Y, 0.0f, 1.0f), matrix);
                output.Add(new Vector3(transFormedPoint.X, transFormedPoint.Y, transFormedPoint.Z));
            }

            return output.ToArray();
        }
        
    }
}
