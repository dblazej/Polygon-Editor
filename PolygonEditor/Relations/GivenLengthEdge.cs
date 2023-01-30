namespace PolygonEditor
{
    class GivenLengthEdge : Relation
    {
        public Edge edge;
        private readonly int length;

        public GivenLengthEdge(Edge edge, int lineLength)
        {
            this.edge = edge;
            length = lineLength;
            this.edge.AddRelation(this);
            CreateRelation();
        }
        public override void UpdateRelation(Shape? movingShape, Stack<(Relation, Shape)> relationsStack)
        {
            if ((int)Functions.DistanceBetweenPoints(new Point(edge.Vertex1.X, edge.Vertex1.Y), new Point(edge.Vertex2.X, edge.Vertex2.Y)) == this.length) return;

            Vertex movingVertex = (movingShape is Edge edge1 && edge1.FromVertex == edge.Vertex1) ? edge.Vertex2 : edge.Vertex1;
            Vertex otherVertex = edge.Vertex1 == movingVertex ? edge.Vertex2 : edge.Vertex1;
            double edgeA = (edge.Vertex2.X == edge.Vertex1.X) ? edge.Vertex2.X : (edge.Vertex1.Y - edge.Vertex2.Y) / (double)(edge.Vertex1.X - edge.Vertex2.X);
            double? edgeB = (edge.Vertex2.X == edge.Vertex1.X) ? null : edge.Vertex1.Y - edgeA * edge.Vertex1.X;
            if (edgeB == null || (edgeB != null && Math.Abs(edgeA) > 20))
            {
                int newY = otherVertex.Y + this.length;
                if (Math.Abs(movingVertex.Y - otherVertex.Y + this.length) > Math.Abs(movingVertex.Y - otherVertex.Y - this.length)) newY = otherVertex.Y - this.length;

                movingVertex.X = otherVertex.X;
                movingVertex.Y = newY; 
                movingVertex.GetOtherEdge(this.edge).AddRelationsToStack(relationsStack);
                return;
            }
            int newX1 = (int)(otherVertex.X + (this.length / Math.Sqrt(1 + edgeA * edgeA)));
            int newX2 = (int)(otherVertex.X - (this.length / Math.Sqrt(1 + edgeA * edgeA)));
            // determine in which direction we want to 'move'
            int newX = Math.Abs(movingVertex.X - newX1) > Math.Abs(movingVertex.X - newX2) ? newX2 : newX1;
            movingVertex.X = newX;
            if (edgeB == null) return;
            movingVertex.Y = (int)(edgeA * newX + edgeB);
            movingVertex.GetOtherEdge(this.edge).AddRelationsToStack(relationsStack);
        }
        public override void Remove()
        {
            edge.RemoveRelation(this);
            base.Remove();
        }
    }
}