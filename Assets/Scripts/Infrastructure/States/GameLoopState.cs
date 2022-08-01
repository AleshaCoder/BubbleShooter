using Services;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private const string MapPath = "Assets/Resourses/Map.txt";
        private readonly AllServices _services;

        public GameLoopState(GameStateMachine stateMachine, AllServices services)
        {
            _services = services;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            IMapLoader loader =_services.Single<IMapLoader>();
            IMapInstatiator mapInstatiator = _services.Single<IMapInstatiator>();

            Map map = loader.Load(MapPath);
            mapInstatiator.Instantiate(map);
        }
    }
}
