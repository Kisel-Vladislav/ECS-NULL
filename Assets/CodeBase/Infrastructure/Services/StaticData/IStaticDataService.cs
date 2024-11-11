using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public void LoadPlayer();
        public PlayerStaticData ForPlayer();

        public void LoadWeapon();
        WeaponSettings ForWeapon();

        public void LoadAgents();
        AgentStaticData ForAgent(TeamType teamType);
    }
}