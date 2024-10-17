namespace CodeBase.ECS.Component.Agent
{
    public struct TeamComponent
    {
        public TeamType Team;
    }
    public enum TeamType
    {
        Player,
        Enemy,
        Ally,
    }
}
