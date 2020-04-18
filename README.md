# Polygon-2d-with-holes-triangulator

This project is copied from:
https://github.com/nickgravelyn/Triangulator

Added classes: MathHelper and Vector2 

(which was originaly in Microsoft.Xna.Framework now you dont need that framework to run this project)

## Original project desctiption:

Triangulator is an implementation of Dave Eberly's ear clipping algorithm as described here: http://www.geometrictools.com/Documentation/TriangulationByEarClipping.pdf. The project allows you to simply input a list of vertices and get back the required vertices (in order) and indices needed to construct a VertexBuffer and IndexBuffer for rendering the particular shape. The library is able to cut holes inside of polygons without error (the only caveat to this is that the library assumes that the hole to be cut lies completely within the shape so erroneous data given as a hole will result in invalid output data).

Note: Triangulator is set up to write a good amount of verbose output in Debug mode. This will affect performance. For optimal performance, make sure you are building the library in Release mode or modify the source to remove the logging functionality.
