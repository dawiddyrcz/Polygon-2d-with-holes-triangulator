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
using System.Drawing;
using System.Windows.Forms;

using PolygonWithHolesTriangulator;

namespace Triangulator_Example
{
    public partial class MainForm : Form
    {
        List<Vector2> inputVectors = new List<Vector2>()
        {
            new Vector2(50,50),
            new Vector2(70,10),
            new Vector2(120,60),
            new Vector2(300,50),
            new Vector2(350,40),
            new Vector2(400,70),
            new Vector2(450,50),
            new Vector2(500,40),
            new Vector2(350,200),
            new Vector2(350,250),
            new Vector2(300,300),
            new Vector2(250,250),
            new Vector2(200,350),
            new Vector2(50,300),
            new Vector2(100,250),
            new Vector2(100,200),
            new Vector2(20,100),

        };

        List<Vector2> holeVectors = new List<Vector2>()
        {
            new Vector2(150,150),
            new Vector2(150,220),
            new Vector2(220,220),

        };

        List<Vector2> holeVectors2 = new List<Vector2>()
        {
            new Vector2(240,140),
            new Vector2(280,140),
            new Vector2(280,170),
            new Vector2(260,200),
            new Vector2(240,170),

        };

        List<Vector2> holeVectors3 = new List<Vector2>()
        {
            new Vector2(180,90),
            new Vector2(250,90),
            new Vector2(250,105),
            new Vector2(180,105),

        };


        List<Vector2> resultVertices = new List<Vector2>();
        List<int> ResultIndices = new List<int>();

        public MainForm()
        {
            InitializeComponent();

            var cutted = Triangulator.CutHoleInShape(inputVectors.ToArray(), holeVectors.ToArray());
            var cutted2 = Triangulator.CutHoleInShape(cutted, holeVectors2.ToArray());
            var cutted3 = Triangulator.CutHoleInShape(cutted2, holeVectors3.ToArray());

            Triangulator.Triangulate(cutted3, WindingOrder.Clockwise,
                out Vector2[] result, out int[] indices);
            
            resultVertices = new List<Vector2>(result);
            ResultIndices = new List<int>(indices);
        }

        private Point FromVector2(Vector2 vector, int offsetX = 0)
        {
            return new Point((int)vector.X+ offsetX, (int)vector.Y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawResults(e);
            DrawVectors(e, inputVectors);
            DrawVectors(e, holeVectors);
            DrawVectors(e, holeVectors2);
            DrawVectors(e, holeVectors3);
        }

        private void DrawResults(PaintEventArgs e)
        {
            var pen = new Pen(Color.Red);

            for (int i = 0; i < ResultIndices.Count; i = i + 3)
            {
                e.Graphics.DrawLine(pen, FromVector2(resultVertices[ResultIndices[i]]), FromVector2(resultVertices[ResultIndices[i+1]]));
                e.Graphics.DrawLine(pen, FromVector2(resultVertices[ResultIndices[i+1]]), FromVector2(resultVertices[ResultIndices[i+2]]));
                e.Graphics.DrawLine(pen, FromVector2(resultVertices[ResultIndices[i+2]]), FromVector2(resultVertices[ResultIndices[i]]));
            }
        }

        private void DrawVectors(PaintEventArgs e, List<Vector2> vectors)
        {
            var pen = new Pen(Color.Blue);

            if (vectors.Count >= 2)
            {
                for (int i = 1; i < vectors.Count; i++)
                {
                    e.Graphics.DrawLine(pen, FromVector2(vectors[i - 1]), FromVector2(vectors[i]));
                }

                e.Graphics.DrawLine(pen, FromVector2(vectors[0]), FromVector2(vectors[vectors.Count - 1]));
            }
        }
        
    }
}
