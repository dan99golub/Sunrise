using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomDataView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _numberClientLabel;

    private int _numberClient;

    private string NameRoom => _inputField.text;

    public void AddOrRemoveValue(int count)
    {
        _numberClient += count;
        _numberClientLabel.text = _numberClient.ToString();
    }

    public (int, string) GetDataRoom()
    {
        return (_numberClient, NameRoom);
    }

    
}
