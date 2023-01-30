namespace PolygonEditor
{
    class ParallelEdges : Relation
    {
        public Edge? edge1;
        public Edge? edge2;
        public bool finished;

        public override void UpdateRelation(Shape? movingShape, Stack<(Relation, Shape)> relationsStack)
        {
            (double, double?) lineEquation;
            Edge? otherEdge;
            if (edge1 == null) return;
            if (edge1 == movingShape)
            {
                lineEquation = Functions.LineEquationOfPoints(new Point(this.edge1.Vertex1.X, this.edge1.Vertex1.Y), new Point(this.edge1.Vertex2.X, this.edge1.Vertex2.Y));
                otherEdge = edge2;
            }
            else
            {
                if (this.edge2 == null) return;
                lineEquation = Functions.LineEquationOfPoints(new Point(this.edge2.Vertex1.X, this.edge2.Vertex1.Y), new Point(this.edge2.Vertex2.X, this.edge2.Vertex2.Y)); 
                otherEdge = edge1;
            }
            // Edges are parallel
            if(otherEdge == null) return;
            if (Math.Abs(lineEquation.Item1 - Functions.LineEquationOfPoints(new Point(otherEdge.Vertex1.X, otherEdge.Vertex1.Y), new Point(otherEdge.Vertex2.X, otherEdge.Vertex2.Y)).Item1) <= 0.01) return;

            int newX;
            int newY;
            Vertex vertexToMove = otherEdge.Vertex2.GetOtherEdge(otherEdge).relations.FindAll(relation => relation.GetType() is not null).Count == 0
                ? otherEdge.Vertex2 : otherEdge.Vertex1;
            Vertex otherVertex = otherEdge.Vertex1 == vertexToMove ? otherEdge.Vertex2 : otherEdge.Vertex1;

            if (lineEquation.Item2 == null || (lineEquation.Item2 != null && Math.Abs(lineEquation.Item1) > 20))
            {
                newY = Int32.MaxValue; // we want to change X 
                newX = otherVertex.X;
            }
            else
            {
                var newB = otherVertex.Y - lineEquation.Item1 * otherVertex.X;
                newY = (int)(lineEquation.Item1 * vertexToMove.X + newB);
                newX = (int)((vertexToMove.Y - newB) / lineEquation.Item1);
            }
            if (Math.Abs(newX - vertexToMove.X) < Math.Abs(newY - vertexToMove.Y))
            {
                vertexToMove.X = newX;
                vertexToMove.Y = vertexToMove.Y;
            }
            else
            {
                vertexToMove.X = vertexToMove.X;
                vertexToMove.Y = newY;
            }
            vertexToMove.GetOtherEdge(otherEdge)?.AddRelationsToStack(relationsStack);
        }
        public void AddShape(Edge? edge)
        {
            if(edge == null) return;
            if (edge1 == null) edge1 = edge;
            else
            {
                edge2 = edge;
                if (edge1.Vertex1 == edge2.Vertex1 || edge1.Vertex1 == edge2.Vertex2 
                    || edge1.Vertex2 == edge2.Vertex1 || edge1.Vertex2 == edge2.Vertex2)
                {
                    edge2 = null;
                    return;
                }
            }
            edge.AddRelation(this);
            if (edge1 != null && edge2 != null)
            {
                finished = true;
                CreateRelation();
            }
        }
        public override void Remove()
        {
            edge1?.RemoveRelation(this);
            edge2?.RemoveRelation(this);
            base.Remove();
        }
    }
}