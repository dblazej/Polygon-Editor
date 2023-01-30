namespace PolygonEditor
{
    class Vertex : Shape
    {
        private Point point;
        public int X
        {
            get => this.point.X;
            set => this.point.X = value;
        }
        public int Y
        {
            get => this.point.Y;
            set => this.point.Y = value;
        }
        public List<Edge> Edges { get; private set; } = new List<Edge>();
        public Vertex(Point point) => this.point = point;
        public override void Move(int dX, int dY, Stack<(Relation, Shape)> relationsStack, bool addRelations = true)
        {
            this.X += dX;
            this.Y += dY;
            if (addRelations) this.Edges.ForEach(edge => { edge.FromVertex = this; edge.AddRelationsToStack(relationsStack);});
        }
        public void AddEdge(Edge edge) => this.Edges.Add(edge);
        public void RemoveEdge(Edge edge) => this.Edges.Remove(edge);
        public Edge GetOtherEdge(Edge edge)
        {
            Edge otherEdge = this.Edges.First(_edge => _edge != edge);
            otherEdge.FromVertex = this;
            return otherEdge;
        }
    }
}