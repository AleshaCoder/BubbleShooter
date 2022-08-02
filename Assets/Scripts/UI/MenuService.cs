using Services;
using System;

namespace UI
{
    public class MenuService : IService
    {
        public event Action OnStartClicked;
        public event Action OnExitClicked;

        public void StartNewGame() => OnStartClicked?.Invoke();
        public void Exit() => OnExitClicked?.Invoke();
    }
}
