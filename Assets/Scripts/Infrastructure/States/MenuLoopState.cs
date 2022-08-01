using Infrastructure.States;
using Services;
using System;
using UI;

public class MenuLoopState : IState
{
    private GameStateMachine _gameStateMachine;
    private readonly AllServices _services;
    private MenuService _menuService;

    public MenuLoopState(GameStateMachine gameStateMachine, AllServices services)
    {
        _gameStateMachine = gameStateMachine;
        _services = services;
    }

    public void Enter()
    {
        _menuService = _services.Single<MenuService>();
        _menuService.OnStartClicked += EnterGame;
        _menuService.OnExitClicked += ExitGame;
    }

    private void ExitGame() => _gameStateMachine.Enter<ExitState>();

    private void EnterGame() => _gameStateMachine.Enter<LoadLevelState, string>("GameScene");

    public void Exit() => _menuService.OnStartClicked -= EnterGame;
}
