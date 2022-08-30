using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror.Discovery;

public class Room : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameRoomLabel;
    [SerializeField] private TMP_Text _numberClientLabel;
    [SerializeField] private Button _connectionButton;

    private ServerResponse _serverResponse;

    public delegate void HandleServerData(ServerResponse response);

    public void ShowServerData(ServerResponse response)
    {
        _nameRoomLabel.text = response.NameRoom;
        _numberClientLabel.text = response.NumberClients;
        _serverResponse = response;
        _connectionButton.onClick.AddListener(HandleData);
    }

    public void SetHandler(HandleServerData handler)
    {
        _handler = handler;
    }

    private HandleServerData _handler;

    private void HandleData()
    {
        _handler.Invoke(_serverResponse);
        _connectionButton.onClick.RemoveListener(HandleData);
        Destroy(gameObject);
    }
}
