using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _aboutButton;
        [SerializeField] private Button _closeAboutButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _aboutPanel;

        private MenuService _menuService;

        private void OnEnable()
        {
            _menuService = AllServices.Container.Single<MenuService>();
            _startButton.onClick.AddListener(_menuService.StartNewGame);
            _aboutButton.onClick.AddListener(OpenAbout);
            _closeAboutButton.onClick.AddListener(CloseAbout);
            _exitButton.onClick.AddListener(_menuService.Exit);
        }

        private void OpenAbout() => _aboutPanel.SetActive(true);

        private void CloseAbout() => _aboutPanel.SetActive(false);

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(_menuService.StartNewGame);
            _aboutButton.onClick.RemoveListener(OpenAbout);
            _closeAboutButton.onClick.RemoveListener(CloseAbout);
            _exitButton.onClick.RemoveListener(_menuService.Exit);
        }
    }
}
