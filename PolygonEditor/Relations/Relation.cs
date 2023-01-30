namespace PolygonEditor
{
    abstract class Relation
    {
        private static int count = 0;
        public int identificator;
        public bool toRemoved = false;

        public Relation()
        {
            identificator = ++Relation.count;
        }
        public abstract void UpdateRelation(Shape? movingShape, Stack<(Relation, Shape)> relationsStack);
        protected void CreateRelation()
        {
            var relationsStack = new Stack<(Relation, Shape)>();
            this.UpdateRelation(null, relationsStack);
            try
            {
                Functions.FixRelationsStack(relationsStack);
            }
            catch (CannotSetException)
            {
                MessageBox.Show("Polygon with this relation cannot exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public virtual void Remove() => this.toRemoved = true;
    }
}