namespace PolygonEditor
{
    class Edge : Shape
    {
        private Vertex vertex1;
        private Vertex vertex2;

        public Edge()
        {
            vertex1 = new Vertex(new Point(0, 0));
            vertex2 = new Vertex(new Point(0, 0));
        }
        public Vertex Vertex1
        {
            get => this.vertex1;
            set
            {
                this.vertex1?.RemoveEdge(this);
                this.vertex1 = value;
                this.vertex1?.AddEdge(this);
            }
        }
        public Vertex Vertex2
        {
            get => this.vertex2;
            set
            {
                this.vertex2?.RemoveEdge(this);
                this.vertex2 = value;
                this.vertex2?.AddEdge(this);
            }
        }
        public Vertex? FromVertex { get; set; }
        public override void Move(int dX, int dY, Stack<(Relation, Shape)> relationsStack, bool addRelationsToFix = true)
        {
            this.Vertex1.Move(dX, dY, relationsStack, false);
            this.Vertex2.Move(dX, dY, relationsStack, false);

            if (addRelationsToFix)
            {
                this.AddRelationsToStack(relationsStack);
                this.vertex1.GetOtherEdge(this)?.AddRelationsToStack(relationsStack);
                this.vertex2.GetOtherEdge(this)?.AddRelationsToStack(relationsStack);
            }
        }
    }
}