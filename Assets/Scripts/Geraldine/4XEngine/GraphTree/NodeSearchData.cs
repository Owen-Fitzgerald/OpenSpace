namespace Geraldine._4XEngine.GraphTree
{
    [System.Serializable]
    public struct NodeSearchData
    {
        public int distance;

        public int nextWithSamePriority;

        public int pathFrom;

        public int heuristic;

        public int searchPhase;

        public readonly int SearchPriority => distance + heuristic;
    }
}
