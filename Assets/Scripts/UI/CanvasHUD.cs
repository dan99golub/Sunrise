using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net;

public class CanvasHUD : MonoBehaviour
{
    public GameObject PanelStart;
    public GameObject PanelStop;

    public Button buttonHost, buttonServer, buttonClient, buttonStop;
    public TMP_Text _text;
    public Text serverText;
    public Text clientText;


    private void Start()
    {
        ValueChangeCheck();


        buttonHost.onClick.AddListener(StartHost);
        buttonServer.onClick.AddListener(StartServer);
        buttonClient.onClick.AddListener(StartClient);
        buttonStop.onClick.AddListener(StopConnectend);


        SetupCanvas();
    }


    public void ValueChangeCheck()
    {
        String host = Dns.GetHostName();
        IPAddress ip;
        if (Application.isMobilePlatform)
        {

            ip = Dns.GetHostByName(host).AddressList[0];
        }
        else
        {
            ip = Dns.GetHostByName(host).AddressList[1];
        }
        Debug.Log(ip.ToString());
        NetworkManager.singleton.networkAddress = ip.ToString();
    }

    public void StartHost()
    {
        _text.text = "OnHost";
        NetworkManager.singleton.StartHost();
        SetupCanvas();
    }

    public void StartServer()
    {
        _text.text = "OnStart";
        NetworkManager.singleton.StartServer();
        SetupCanvas();
    }

    public void StartClient()
    {
        _text.text = "OnClient";
        ValueChangeCheck();
        NetworkManager.singleton.StartClient();
        SetupCanvas();
    }

    public void StopConnectend()
    {
        _text.text = "StopClietn";
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }

        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }

        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }

        SetupCanvas();
    }

    public void SetupCanvas()
    {


        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (NetworkClient.active)
            {
                PanelStart.SetActive(false);
                PanelStop.SetActive(true);
                clientText.text = "Connecting to " + NetworkManager.singleton.networkAddress + "..";
            }
            else
            {
                PanelStart.SetActive(true);
                PanelStop.SetActive(false);
            }
        }
        else
        {
            PanelStart.SetActive(false);
            PanelStop.SetActive(true);


            if (NetworkServer.active)
            {
                serverText.text = "Server: active. Transport: " + Transport.activeTransport;
            }
            if (NetworkClient.isConnected)
            {
                clientText.text = "Client: address=" + NetworkManager.singleton.networkAddress;
            }
        }
    }
}
