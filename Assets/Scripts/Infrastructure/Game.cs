using Configs.Game;
using Infrastructure.States;
using Logic;
using Services;

namespace Infrastructure
{
    public class Game
    {
        public GameConfig Config;
        public GameStateMachine StateMachine;

        public Game(GameConfig config, ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            Config = config;
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container, Config);            
        }
    }
}
