using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;
using System.Collections.Generic;

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

        public void LoadBuildsView();
        List<BuildPanelItemData> ForBuilds(BuildGroupType buildGroupType);
        Build ForBuild(BuildTypeId buildTypeid);
        void LoadBuilds();
    }
}