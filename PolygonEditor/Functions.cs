namespace PolygonEditor
{
    class Functions
    {
        public static double EdgeDistance(Point p, Point edgeA, Point edgeB)
        {
            double pX = edgeB.X - edgeA.X;
            double pY = edgeB.Y - edgeA.Y;
            double tmp = ((p.X - edgeA.X) * pX + (p.Y - edgeA.Y) * pY) / ((pX * pX) + (pY * pY));

            if (tmp > 1) tmp = 1;
            else if (tmp < 0) tmp = 0;

            double dX = edgeA.X + tmp * pX - p.X;
            double dY = edgeA.Y + tmp * pY - p.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }
        public static (double, double?) LineEquationOfPoints(Point p1, Point p2)
        {
            if (p2.X == p1.X) return new(p2.X, null);
            double tmp = (p1.Y - p2.Y) / (double)(p1.X - p2.X);
            return new(tmp, p1.Y - tmp * p1.X);
        }
        public static double DistanceBetweenPoints(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(p2.X - p1.X), 2) + Math.Pow(Math.Abs(p2.Y - p1.Y), 2));
        }
        public static void FixRelationsStack(Stack<(Relation, Shape)> relationsStack)
        {
            int counter = 0;
            int currentRelationIdentificator = 0;
            while (true)
            {
                if (relationsStack.Count == 0) break;

                var relation = relationsStack.Pop();
                // Check if there is not duplicated relation
                if (relation.Item1.identificator == currentRelationIdentificator) continue;

                currentRelationIdentificator = relation.Item1.identificator;
                // Fix relation
                relation.Item1.UpdateRelation(relation.Item2, relationsStack);
                // Check if this is not infinity loop
                if (++counter >= 40) 
                {
                    relation.Item1.Remove();
                    relation.Item2.Remove();
                    throw new CannotSetException();
                } 
            }
        }
    }
}