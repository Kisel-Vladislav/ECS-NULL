using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;
using CodeBase.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<TeamType, AgentStaticData> _agents;

        private PlayerStaticData _player;
        private WeaponSettings _weapon;
        private List<BuildPanelItemData> _buildsView;

        public AgentStaticData ForAgent(TeamType teamType) => _agents[teamType];
        public List<BuildPanelItemData> ForBuilds(BuildGroupType buildGroupType) => 
            _buildsView.Where(x => x.BuildGroupType == buildGroupType)
                       .ToList();
        public PlayerStaticData ForPlayer() => _player;
        public WeaponSettings ForWeapon() => _weapon;

        public void LoadAgents() => _agents = Resources.LoadAll<AgentStaticData>("StaticData/Agent")
                                                       .ToDictionary(x => x.Team, x => x);
        public void LoadBuildsView() => _buildsView = Resources.LoadAll<BuildPanelItemData>("StaticData/Build")
                                                               .ToList();
        public void LoadPlayer() => _player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerData");
        public void LoadWeapon() => _weapon = Resources.Load<WeaponSettings>("StaticData/Weapon/Pistol");
    }
}
