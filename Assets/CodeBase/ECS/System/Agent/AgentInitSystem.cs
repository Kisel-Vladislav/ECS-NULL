using CodeBase.ECS.Component.Agent;
using CodeBase.ECS.Data;
using CodeBase.Infrastructure.Factory;
using Leopotam.Ecs;

namespace CodeBase.ECS.System.Agent
{
    public class AgentInitSystem : IEcsInitSystem
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEntityViewFactory _entityViewFactory;
        private readonly EcsWorld _world;
        private readonly SceneData _sceneData;

        public void Init()
        {
            foreach (var enemyView in _sceneData.Enemy)
            {
                var enemyEntity = _entityFactory.CreateAgent(TeamType.Enemy);
                _entityViewFactory.CreateAgentView(ref enemyEntity, enemyView);

                InitializeWeaponForAgent(ref enemyEntity);
                enemyEntity.Get<CheckDetectionZone>();
            }

            foreach (var allyView in _sceneData.Ally)
            {
                var allyEntity = _entityFactory.CreateAgent(TeamType.Ally);
                _entityViewFactory.CreateAgentView(ref allyEntity, allyView);

                InitializeWeaponForAgent(ref allyEntity);
                allyEntity.Get<CheckDetectionZone>();
            }
        }

        private void InitializeWeaponForAgent(ref EcsEntity agentEntity)
        {
            var weapon = _entityFactory.SetupWeapon(ref agentEntity);
            _entityViewFactory.SetupWeapon(ref agentEntity);
        }
    }
}
