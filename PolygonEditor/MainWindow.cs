namespace PolygonEditor
{
    public partial class MainWindow : Form
    {
        // variables
        private bool drawPolygon = false;
        private bool drawing = false;
        private bool moving = false;
        private bool selecting = false;
        private bool bresenham = false;
        private bool secondEdge = false;
        private bool removedParallel= false;
        private Point currentPoint;
        private Polygon? currentPolygon;
        private List<Relation> practicallyRemovedParallel = new();
        private ParallelEdges? practicallyFinishedParallel = null;
        private readonly List<Relation> relations = new();
        private readonly List<Polygon> polygons = new();
        // functions
        private void PaintStartCanvas()
        {
            var polygon1 = new Polygon(new Point(100, 400));
            polygon1.AddEdge(new Point(300, 300));
            polygon1.AddEdge(new Point(500, 400));
            polygon1.AddEdge(new Point(100, 400));
            polygon1.UpdateLastVertex(new Point(100, 400));
            polygon1.FinishDrawing();
            this.polygons.Add(polygon1);

            var polygon2 = new Polygon(new Point(70, 70));
            polygon2.AddEdge(new Point(170, 80));
            polygon2.AddEdge(new Point(180, 180));
            polygon2.AddEdge(new Point(70, 170));
            polygon2.AddEdge(new Point(70, 70));
            polygon2.UpdateLastVertex(new Point(70, 70));
            polygon2.FinishDrawing();
            this.polygons.Add(polygon2);

            var polygon3 = new Polygon(new Point(400, 50));
            polygon3.AddEdge(new Point(500, 60));
            polygon3.AddEdge(new Point(510, 160));
            polygon3.AddEdge(new Point(450, 200));
            polygon3.AddEdge(new Point(400, 150));
            polygon3.AddEdge(new Point(400, 50));
            polygon3.UpdateLastVertex(new Point(400, 50));
            polygon3.FinishDrawing();
            this.polygons.Add(polygon3);

            var parallelEdges1 = new ParallelEdges();
            parallelEdges1.AddShape(polygon1.Edges[3]);
            parallelEdges1.AddShape(polygon3.Edges[1]);
            var parallelEdges2 = new ParallelEdges();
            parallelEdges2.AddShape(polygon1.Edges[3]);
            parallelEdges2.AddShape(polygon2.Edges[1]);
            relations.Add(parallelEdges1);
            relations.Add(parallelEdges2);
            relations.Add(new GivenLengthEdge(polygon1.Edges[3], 400));
            relations.Add(new GivenLengthEdge(polygon1.Edges[1], 223));
            relations.Add(new GivenLengthEdge(polygon3.Edges[1], 100));
        }
        private void ChangeButtonsEnabledProperty(bool addPolygon = false, bool addVertex = false, bool addRelationParallel = false, bool addRelationFixed = false, 
            bool removePolygon = false, bool removeVertex = false, bool removeRelationParallel = false, bool removeRelationFixed = false)
        {
            this.AddPolygonButton.Enabled = addPolygon;
            this.AddVertexButton.Enabled = addVertex;
            this.AddRelationParallelButton.Enabled = addRelationParallel;
            this.AddRelationGivenLengthbutton.Enabled = addRelationFixed;
            this.RemovePolygonButton.Enabled = removePolygon;
            this.RemoveVertexButton.Enabled = removeVertex;
            this.RemoveRelationParallelButton.Enabled = removeRelationParallel;
            this.RemoveRelationGivenLengthbutton.Enabled = removeRelationFixed;
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            PaintStartCanvas();
            ChangeButtonsEnabledProperty(true);
        }
        private void AddPolygonButton_Click(object sender, EventArgs e)
        {
            drawPolygon = true;
            drawing = false;
            moving = false;
            selecting = false;
            if (this.currentPolygon != null)
            {
                this.currentPolygon.ClickOffShape();
                this.currentPolygon = null;
                ChangeButtonsEnabledProperty(true);
            }
            this.canvas.Invalidate();
        }
        private void AddVertexButton_Click(object sender, EventArgs e)
        {
            if (this.currentPolygon != null)
            {
                this.currentPolygon.AddVertexOnEdge();
                drawing = false;
                moving = false;
                selecting = false;
                this.currentPolygon.ClickOffShape();
                this.currentPolygon = null;
                ChangeButtonsEnabledProperty(true);
                this.canvas.Invalidate();
            }
        }
        private void RemovePolygonButton_Click(object sender, EventArgs e)
        {
            if (this.currentPolygon != null)
            {
                this.currentPolygon.Remove();
                this.polygons.Remove(this.currentPolygon);
                drawing = false;
                moving = false;
                selecting = false;
                this.currentPolygon.ClickOffShape();
                this.currentPolygon = null;
                ChangeButtonsEnabledProperty(true);
                this.canvas.Invalidate();
            }
        }
        private void RemoveVertexButton_Click(object sender, EventArgs e)
        {
            if (this.currentPolygon != null)
            {
                this.currentPolygon.RemoveCurrentVertex();
                drawing = false;
                moving = false;
                selecting = false;
                this.currentPolygon.ClickOffShape();
                this.currentPolygon = null;
                ChangeButtonsEnabledProperty(true);
                this.canvas.Invalidate();
            }
        }
        private void AddRelationParallelButton_Click(object sender, EventArgs e)
        {
            if (currentPolygon == null) return;
            secondEdge = true;
            ParallelEdges currentParallelRelation = new();
            currentParallelRelation.AddShape(currentPolygon.ClickedShape as Edge);
            this.relations.Add(currentParallelRelation);
            this.practicallyFinishedParallel = currentParallelRelation;
            currentPolygon.ClickOffShape();
            currentPolygon = null;  
            selecting = false;
            ChangeButtonsEnabledProperty();
            this.canvas.Invalidate();
        }
        private void AddRelationGivenLengthbutton_Click(object sender, EventArgs e)
        {
            if (currentPolygon == null || currentPolygon.ClickedShape == null) return;
            Relation? currentGivenLengthEdgeRelation = currentPolygon.ClickedShape.TypeOfRelation(typeof(GivenLengthEdge));
            if (currentGivenLengthEdgeRelation != null)
            {
                currentGivenLengthEdgeRelation.Remove();
                this.relations.Remove(currentGivenLengthEdgeRelation);
            }

            Edge edge = (Edge)this.currentPolygon.ClickedShape;
            int edgeLength = (int)Functions.DistanceBetweenPoints(new Point(edge.Vertex1.X, edge.Vertex1.Y), new Point(edge.Vertex2.X, edge.Vertex2.Y));
            EdgeLength edgeLengthForm = new();
            edgeLengthForm._edgeLength = edgeLength.ToString();
            edgeLengthForm.ShowDialog();
            if(edgeLengthForm.outcome != "")
            {
                Relation newFixedEdgeRelation = new GivenLengthEdge(edge, Int32.Parse(edgeLengthForm.outcome));
                this.relations.Add(newFixedEdgeRelation);  
            }
            currentPolygon.ClickOffShape();
            currentPolygon = null;
            selecting = false;
            ChangeButtonsEnabledProperty(true);
            this.canvas.Invalidate();
        }
        private void RemoveRelationParallelButton_Click(object sender, EventArgs e)
        {
            if (this.currentPolygon == null) return;
            if (this.currentPolygon.ClickedShape == null) return;
            foreach(Relation r in this.currentPolygon.ClickedShape.relations)
            {
                if(r.GetType() == typeof(ParallelEdges)) practicallyRemovedParallel.Add(r);
            }
            if (practicallyRemovedParallel.Count > 0)
            {
                removedParallel = true;
                ChangeButtonsEnabledProperty(false);
            }
            else ChangeButtonsEnabledProperty(true);
            currentPolygon.ClickOffShape();
            currentPolygon = null;
            selecting = false;
            this.canvas.Invalidate();
        }
        private void RemoveRelationGivenLengthbutton_Click(object sender, EventArgs e)
        {
            if (this.currentPolygon == null) return;
            if (this.currentPolygon.ClickedShape == null) return;
            Relation? currentGivenLengthEdgeRelation = this.currentPolygon.ClickedShape.TypeOfRelation(typeof(GivenLengthEdge));
            if(currentGivenLengthEdgeRelation != null)
            {
                currentGivenLengthEdgeRelation.Remove();
                this.relations.Remove(currentGivenLengthEdgeRelation);
            }
            currentPolygon.ClickOffShape();
            currentPolygon = null;
            selecting = false;
            ChangeButtonsEnabledProperty(true);
            this.canvas.Invalidate();
        }
        private void BresenhamRadioButton_Click(object sender, EventArgs e)
        {
            if(!bresenham)
            {
                bresenham = true;
                this.canvas.Invalidate();
            }
        }
        private void LibraryRadioButton_Click(object sender, EventArgs e)
        {
            if (bresenham)
            {
                bresenham = false;
                this.canvas.Invalidate();
            }
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.polygons.Clear();
            this.relations.Clear();
            drawPolygon = false;
            drawing = false;
            moving = false;
            selecting = false;
            secondEdge = false;
            PaintStartCanvas();
            ChangeButtonsEnabledProperty(true);
            if (this.currentPolygon != null)
            {
                this.currentPolygon.ClickOffShape();
                this.currentPolygon = null;
                ChangeButtonsEnabledProperty(true);
            }
            this.canvas.Invalidate();
        }
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            var bitmap = new Bitmap(this.canvas.Width, this.canvas.Height);
            this.relations.RemoveAll(relation => relation.toRemoved);
            this.polygons.ForEach(polygon => polygon.DrawPolygon(bitmap, e, bresenham));
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (drawing && drawPolygon)
                {
                    if (this.currentPolygon == null) return;
                    if (this.currentPolygon.PracticallyFinished)
                    {
                        this.currentPolygon.FinishDrawing();
                        drawing = false;
                        drawPolygon = false;
                        currentPolygon.ClickOffShape();
                        this.currentPolygon = null;
                        ChangeButtonsEnabledProperty(true);
                    }
                    else this.currentPolygon.AddEdge(this.currentPoint);
                }
                else if (!drawing && !selecting && !moving)
                {
                    if (drawPolygon)
                    {
                        drawing = true;
                        ChangeButtonsEnabledProperty();
                        this.currentPoint = e.Location;
                        Polygon drawingPolygon = new(this.currentPoint);
                        drawingPolygon.AddEdge(this.currentPoint);
                        this.currentPolygon = drawingPolygon;
                        this.polygons.Add(currentPolygon);
                        ChangeButtonsEnabledProperty();
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (drawing)
                {
                    if (this.currentPolygon == null) return;
                    this.currentPolygon.Remove();
                    this.polygons.Remove(this.currentPolygon);
                    drawing = false;
                    this.currentPolygon = null;
                    ChangeButtonsEnabledProperty(true);
                }
                else if (selecting)
                {
                    selecting = false;
                    if (this.currentPolygon == null) return;
                    this.currentPolygon.ClickOffShape();
                    this.currentPolygon = null;
                    ChangeButtonsEnabledProperty(true);
                }
                else if (secondEdge)
                {
                    secondEdge = false;
                    ChangeButtonsEnabledProperty(true);
                    if (this.practicallyFinishedParallel == null) return;
                    if (!this.practicallyFinishedParallel.finished)
                    {
                        this.practicallyFinishedParallel.Remove();
                        this.relations.Remove(this.practicallyFinishedParallel);
                    }
                }
            }
            this.canvas.Invalidate();
        }
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (selecting)
                {
                    if (this.currentPolygon == null) return;
                    if (this.currentPolygon.GetAppropriateShape(e.Location)?.Item1 == this.currentPolygon.ClickedShape)
                    {
                        selecting = false;
                        moving = true;
                        this.currentPolygon.StartMoving(e.Location);
                        this.canvas.Cursor = Cursors.Hand;
                        ChangeButtonsEnabledProperty();
                    }
                    else
                    {
                        selecting = false;
                        this.currentPolygon.ClickOffShape();
                        this.currentPolygon = null;
                        ChangeButtonsEnabledProperty(true);
                    }
                }
                if (!drawing && !selecting && !moving)
                {
                    ((Shape, Polygon), double)? mostAppropriateShape = null;
                    foreach (var polygon in this.polygons.AsEnumerable().Reverse())
                    {
                        var appropriateShape = polygon.GetAppropriateShape(e.Location);

                        if (appropriateShape != null)
                        {
                            if (mostAppropriateShape == null || appropriateShape.Value.Item2 < mostAppropriateShape.Value.Item2)
                                mostAppropriateShape = new ((appropriateShape.Value.Item1, polygon), appropriateShape.Value.Item2);
                        }
                    }
                    if (mostAppropriateShape != null)
                    {
                        this.currentPolygon = mostAppropriateShape.Value.Item1.Item2;
                        this.currentPolygon.ClickShape(mostAppropriateShape.Value.Item1.Item1);
                        moving = true;
                        ChangeButtonsEnabledProperty();
                        this.currentPolygon.StartMoving(e.Location);
                        this.canvas.Cursor = Cursors.Hand;
                        if (secondEdge)
                        {
                            if (this.currentPolygon.ClickedShape == null) return;
                            if (this.practicallyFinishedParallel == null) return;
                            if (this.currentPolygon.ClickedShape.GetType() == typeof(Edge))
                                this.practicallyFinishedParallel.AddShape((Edge)this.currentPolygon.ClickedShape);
                            secondEdge = false;
                            if (!this.practicallyFinishedParallel.finished)
                            {
                                this.practicallyFinishedParallel.Remove();
                                this.relations.Remove(this.practicallyFinishedParallel);
                                moving = false;
                                selecting = false;
                                this.canvas.Cursor = Cursors.Default;
                                this.currentPolygon.ClickOffShape();
                                currentPolygon = null;
                                ChangeButtonsEnabledProperty(true);
                                MessageBox.Show("Selected shape cannot be added to parallel relation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        if(removedParallel)
                        {
                            if (this.currentPolygon == null) return;
                            if (this.currentPolygon.ClickedShape == null) return;
                            List<Relation> practicallyRemovedParallel2 = new();
                            foreach (Relation r in this.currentPolygon.ClickedShape.relations)
                            {
                                if (r.GetType() == typeof(ParallelEdges)) practicallyRemovedParallel2.Add(r);
                            }

                            bool sucessRemoved = false;
                            foreach (Relation r2 in practicallyRemovedParallel2)
                            {
                                foreach (Relation r1 in practicallyRemovedParallel)
                                {
                                    if(r2 == r1)
                                    {
                                        sucessRemoved = true;
                                        r1.Remove();
                                        this.relations.Remove(r1);
                                    }
                                }
                            }
                            if(!sucessRemoved)
                                MessageBox.Show("Cannot delete parallel relation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.canvas.Cursor = Cursors.Default;
                            ChangeButtonsEnabledProperty(true);
                            currentPolygon.ClickOffShape();
                            currentPolygon = null;
                            practicallyRemovedParallel.Clear();
                            removedParallel = false;
                            selecting = false;
                            moving = false;
                        }
                    }
                }
            }
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (moving && e.Button == MouseButtons.Right)
            {
                if (this.currentPolygon == null) return;
                if (this.currentPolygon.ClickedShape == null) return;
                this.currentPolygon.EndMoving();
                this.canvas.Cursor = Cursors.Default;
                moving = false;
                selecting = true;
                ChangeButtonsEnabledProperty(addPolygon: true, removePolygon: true, addVertex: this.currentPolygon.ClickedShape is Edge, 
                    removeVertex: this.currentPolygon.ClickedShape is Vertex, addRelationFixed: this.currentPolygon.ClickedShape is Edge,
                    addRelationParallel: this.currentPolygon.ClickedShape is Edge,
                    removeRelationFixed: this.currentPolygon.ClickedShape.TypeOfRelation(typeof(GivenLengthEdge)) != null,
                    removeRelationParallel: this.currentPolygon.ClickedShape.TypeOfRelation(typeof(ParallelEdges)) != null);
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.currentPoint = e.Location;

            if (this.currentPolygon == null) return;
            if (moving) this.currentPolygon.UpdateMoving(this.currentPoint);
            else if (drawing) this.currentPolygon.UpdateLastVertex(this.currentPoint);
            else if (selecting)
            {
                bool found = false;
                foreach (var polygon in this.polygons.AsEnumerable().Reverse())
                {
                    var fittestPolygon = polygon.GetAppropriateShape(e.Location);

                    if (fittestPolygon != null)
                    {
                        this.canvas.Cursor = Cursors.Hand;
                        break;
                    }
                }
                if (!found) this.canvas.Cursor = Cursors.Default;
            }
            this.canvas.Invalidate();
        }
    }
}