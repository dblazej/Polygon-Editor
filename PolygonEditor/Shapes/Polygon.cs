using System.Drawing.Drawing2D;
using System.Text;

namespace PolygonEditor
{
    class Polygon : Shape
    {
        protected Point recentPoint;
        protected bool isMoving = false;
        public bool finished;

        public SortedDictionary<int, Edge> Edges { get; private set; } = new SortedDictionary<int, Edge>();
        public SortedDictionary<int, Vertex> Vertices { get; private set; } = new SortedDictionary<int, Vertex>();
        private Vertex FirstVertex => this.Vertices[1];
        public Shape? ClickedShape { get; protected set; } = null;
        public bool PracticallyFinished { get; private set; } = false;
        public int DX { get; private set; }
        public int DY { get; private set; }
        public Polygon(Point firstVertex)
        {
            this.Vertices.Add(1, new Vertex(firstVertex));
        }
        public override void Move(int dX, int dY, Stack<(Relation, Shape)> relationsStack, bool addRelationsToFix = true)
        {
            foreach (var vertex in this.Vertices.Values)
                vertex.Move(dX, dY, relationsStack);
        }
        public void ClickShape(Shape shape) => this.ClickedShape = shape;
        public void ClickOffShape() => this.ClickedShape = null;
        public void StartMoving(Point _recent)
        {
            this.isMoving = true;
            this.recentPoint = _recent;
        }
        public void EndMoving() => this.isMoving = false;
        public void UpdateMoving(Point p)
        {
            if (!this.isMoving || this.ClickedShape == null) throw new Exception("Moving shape not set");

            DX = p.X - this.recentPoint.X;
            DY = p.Y - this.recentPoint.Y;
            this.recentPoint = p;
            var relationsStack = new Stack<(Relation, Shape)>();
            if (this.ClickedShape == null) return;
            this.ClickedShape.Move(DX, DY, relationsStack);
            Functions.FixRelationsStack(relationsStack);
        }
        public override void AddRelationsToStack(Stack<(Relation, Shape)> relationsStack)
        {
            if (this.ClickedShape == null) return;
            this.relations.ForEach(relation => relationsStack.Push( new (relation, this.ClickedShape)));
        }
        public void UpdateLastVertex(Point vertex)
        {
            this.Vertices.Last().Value.X = vertex.X;
            this.Vertices.Last().Value.Y = vertex.Y;
            this.PracticallyFinished = this.Edges.Count > 2 && Functions.DistanceBetweenPoints(new Point(FirstVertex.X, FirstVertex.Y), vertex) < 16;
        }
        public void AddEdge(Point vertex)
        {
            var newVertex = new Vertex(vertex);
            Vertices.Add(this.Vertices.Last().Key + 1, newVertex);
            Edges.Add(this.Edges.Count == 0 ? 1 : this.Edges.Last().Key + 1, new Edge() { Vertex1 = this.Edges.Count == 0 ? this.FirstVertex : this.Edges.Last().Value.Vertex2, Vertex2 = newVertex });
        }
        public void FinishDrawing()
        {
            if (this.PracticallyFinished)
            {
                this.finished = true;
                this.PracticallyFinished = false;

                // Delete last vertex because is supposed to be StartPoint.
                this.Vertices.Remove(this.Vertices.Count);
                // Set last edge second vertex as StartPoint
                var lastEdge = this.Edges.Last();
                lastEdge.Value.Vertex2 = this.FirstVertex;
            }
        }
        public void DrawPolygon(Bitmap bm, PaintEventArgs e, bool bresenham)
        {
            // If polygon is completed fill it with some color 
            if (this.finished)
            {
                List<Point> points = new();
                foreach (var vertex in this.Vertices)
                    points.Add(new Point(vertex.Value.X, vertex.Value.Y));

                e.Graphics.FillPolygon(new SolidBrush(this.ClickedShape is Polygon ? Color.Blue : Color.AliceBlue), points.ToArray());
            }
            // Draw vertices
            foreach (var vertex in this.Vertices.Values)
            {
                int radius = 5;
                e.Graphics.FillEllipse(new SolidBrush(this.ClickedShape == vertex ? Color.Blue : Color.Black),
                        new Rectangle(vertex.X - radius, vertex.Y - radius, radius + radius, radius + radius));
            }
            // Draw lines
            foreach (var edge in this.Edges.Values)
            {
                Bresenham.DrawLine(bm, e, new Point(edge.Vertex1.X, edge.Vertex1.Y), new Point(edge.Vertex2.X, edge.Vertex2.Y), 
                    bresenham, this.ClickedShape == edge ? Color.Blue : Color.Black);

                // Draw relations Ids
                if(edge.relations.Count > 0)
                {
                    StringBuilder edgeId = new();
                    foreach (var relation in edge.relations)
                    {
                        if(relation.GetType() == typeof(ParallelEdges))
                        {
                            if(((ParallelEdges)relation).edge1 != null && ((ParallelEdges)relation).edge2 != null)
                                edgeId.AppendFormat("{0} ", relation.identificator);
                        }
                    }
                    if(edge.TypeOfRelation(typeof(GivenLengthEdge)) != null) edgeId.Append('L');
                    Point centerPoint = new((edge.Vertex1.X + edge.Vertex2.X) / 2, (edge.Vertex1.Y + edge.Vertex2.Y) / 2);
                    e.Graphics.DrawString(edgeId.ToString(), new Font("Consolas", 9, FontStyle.Bold), Brushes.Gray, centerPoint.X + 5, centerPoint.Y + 5);
                }
            }
            // Draw circle around start point to finish polygon
            if (this.PracticallyFinished) e.Graphics.DrawEllipse(new(Color.Blue, 1), this.FirstVertex.X - 20, this.FirstVertex.Y - 20, 40, 40);
        }
        public (Shape, double)? GetAppropriateShape(Point point)
        {
            double distance;
            // vertex clicked
            foreach (var vertex in this.Vertices.Values)
            {
                distance = Functions.DistanceBetweenPoints(point, new Point(vertex.X, vertex.Y));
                if (distance < 20) return new (vertex, distance);
            }
            // edge clicked
            foreach (var edge in this.Edges.Values)
            {
                distance = Functions.EdgeDistance(point, new Point(edge.Vertex1.X, edge.Vertex1.Y), new Point(edge.Vertex2.X, edge.Vertex2.Y));
                if (distance < 20) return new (edge, distance);
            }
            // polygon is clicked
            List<Point> points = new();
            foreach (var vertex in this.Vertices)
                points.Add(new Point(vertex.Value.X, vertex.Value.Y));

            var polygon = new GraphicsPath();
            polygon.AddPolygon(points.ToArray());
            if (polygon.IsVisible(point)) return new (this, 21);

            return null;
        }
        public void AddVertexOnEdge()
        {
            if (this.ClickedShape is not Edge) return;
            this.ClickedShape.RemoveRelations();
            var currentEdgeIndex = this.Edges.First((edge) => edge.Value == this.ClickedShape).Key;
            var currentEdge = (Edge)this.ClickedShape;
            var vertex1 = currentEdge.Vertex1;
            var vertex2 = currentEdge.Vertex2;

            var newVertices = new SortedDictionary<int, Vertex>();
            int newVertexIndex = this.Vertices.First((vertex) => vertex.Value == currentEdge.Vertex1).Key + 1;
            foreach (var vertex in this.Vertices)
            {
                if (vertex.Key >= newVertexIndex) newVertices.Add(vertex.Key + 1, vertex.Value);
                else if (vertex.Key < newVertexIndex) newVertices.Add(vertex.Key, vertex.Value);
            }

            var newVertex = new Vertex(new Point((vertex1.X + vertex2.X) / 2, (vertex1.Y + vertex2.Y) / 2));
            newVertices.Add(newVertexIndex, newVertex);

            var newEdges = new SortedDictionary<int, Edge>();
            foreach (var edge in this.Edges)
            {
                if (edge.Key < currentEdgeIndex) newEdges.Add(edge.Key, edge.Value);
                else if (edge.Key > currentEdgeIndex) newEdges.Add(edge.Key + 1, edge.Value);
            }
            currentEdge.Vertex2.RemoveEdge(currentEdge);
            currentEdge.Vertex1.RemoveEdge(currentEdge);
            // split existing line
            newEdges.Add(currentEdgeIndex, new Edge() { Vertex1 = vertex1, Vertex2 = newVertex });
            newEdges.Add(currentEdgeIndex + 1, new Edge() { Vertex1 = newVertex, Vertex2 = currentEdge.Vertex2 });
            this.Vertices = newVertices;
            this.Edges = newEdges;
            // choose created vertex
            this.ClickedShape = newVertex;
        }
        public void RemoveCurrentVertex()
        {
            if (this.ClickedShape is not Vertex || this.Vertices.Count <= 3) return;
            this.Vertices.Remove(this.Vertices.First((vertex) => vertex.Value == this.ClickedShape).Key);
            // remove relations from edges with choosen vertex
            foreach (var edge in this.Edges.Values)
                if (edge.Vertex1 == this.ClickedShape || edge.Vertex2 == this.ClickedShape)
                    edge.Remove();

            var edgeWithSelectedVertexAsFirst =
                this.Edges.First((edge) => edge.Value.Vertex1 == this.ClickedShape);
            int edgeBeforeKey = edgeWithSelectedVertexAsFirst.Key > 1 ? edgeWithSelectedVertexAsFirst.Key - 1 : this.Edges.Count;
            // remove edge which first vertex is the one we want to delete
            this.Edges.Remove(edgeWithSelectedVertexAsFirst.Key);
            // change edge before
            this.Edges[edgeBeforeKey].Vertex2 = edgeWithSelectedVertexAsFirst.Value.Vertex2;
            // sort keys
            var newVertices = new SortedDictionary<int, Vertex>();
            int i = 1;
            foreach (var vertex in this.Vertices.Values)
                newVertices.Add(i++, vertex);
            this.Vertices = newVertices;
            var newEdges = new SortedDictionary<int, Edge>();
            i = 1;
            foreach (var edge in this.Edges.Values)
                newEdges.Add(i++, edge);
            this.Edges = newEdges;

            this.ClickOffShape();
        }
        public override void Remove()
        {
            foreach (var edge in this.Edges.Values)
                edge.Remove();
            base.Remove();
        }
    }
}