namespace PolygonEditor
{
    abstract class Shape
    {
        public List<Relation> relations = new();

        public abstract void Move(int dX, int dY, Stack<(Relation, Shape)> relationsStack, bool addRelations = true);
        public void AddRelation(Relation relation) => this.relations.Add(relation);
        public void RemoveRelation(Relation relation) => this.relations.Remove(relation);
        public virtual Relation? TypeOfRelation(Type relationType)
            => this.relations.Find(relation => relation.GetType() == relationType);
        public virtual void AddRelationsToStack(Stack<(Relation, Shape)> relationsStack)
            => this.relations.ForEach(relation => relationsStack.Push(new (relation, this)));
        public void RemoveRelations()
        {
            foreach (var relation in this.relations.ToArray())
                relation.Remove();
        }
        public virtual void Remove() => this.RemoveRelations();
    }
}