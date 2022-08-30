using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Mirror.Discovery
{
    public class CustomDiscoveryHUD : MonoBehaviour
    {
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private GameObject _gamePanel;

        [Header("Button")]
        [SerializeField] private Button _findButton;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _serverButton;
        [SerializeField] private Button _stopButton;

        [Header("Labels")]
        [SerializeField] TMP_Text _coutServersLabel;


        [Header("NetworkDiscovery")]
        [SerializeField] private NetworkDiscovery networkDiscovery;

        [SerializeField] private Transform _scrollView;
        [SerializeField] private Room _roomPrefab;

        private List<ServerResponse> _responses = new List<ServerResponse>();

        private void OnEnable()
        {
            _findButton.onClick.AddListener(FindServer);
            _hostButton.onClick.AddListener(CreateHost);
            _serverButton.onClick.AddListener(CreateSrver);
            _stopButton.onClick.AddListener(StopGame);
        }

        private void OnDisable()
        {
            _findButton.onClick.RemoveListener(FindServer);
            _hostButton.onClick.RemoveListener(CreateHost);
            _serverButton.onClick.RemoveListener(CreateSrver);
            _stopButton.onClick.RemoveListener(StopGame);
        }

        private void FindServer()
        {
            networkDiscovery.StartDiscovery();
        }

        private void CreateHost()
        {
            SwitchPanel(false);
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }

        private void CreateSrver()
        {
            SwitchPanel(false);
            NetworkManager.singleton.StartServer();
            networkDiscovery.AdvertiseServer();
        }

        private void SpawnRoom(ServerResponse info)
        {
            if (!_responses.Contains(info))
            {
                var newRoom = Instantiate(_roomPrefab, _scrollView);
                newRoom.ShowServerData(info);
                newRoom.SetHandler(Connect);
                _responses.Add(info);
            }
        }

        private void Connect(ServerResponse info)
        {

            SwitchPanel(false);

            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            SpawnRoom(info);
        }

        private void SwitchPanel(bool isActive)
        {
            _startPanel.SetActive(isActive);
            _gamePanel.SetActive(!isActive);
        }

        void StopGame()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {

                NetworkManager.singleton.StopHost();
                networkDiscovery.StopDiscovery();

            }

            else if (NetworkClient.isConnected)
            {

                NetworkManager.singleton.StopClient();
                networkDiscovery.StopDiscovery();

            }

            else if (NetworkServer.active)
            {

                NetworkManager.singleton.StopServer();
                networkDiscovery.StopDiscovery();

            }
            SwitchPanel(true);
        }
    }
}
