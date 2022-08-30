using Mirror.Discovery;
using System;
using System.Net;
using UnityEngine;

public class NewNetworkDiscovery : NetworkDiscovery
{
    [SerializeField] private RoomDataView _roomData;
    protected override ServerResponse ProcessRequest(ServerRequest request, IPEndPoint endpoint)
    {
        try
        {

            return new ServerResponse
            {
                serverId = ServerId,
                uri = transport.ServerUri(),
                NameRoom = _roomData.GetDataRoom().Item2,
                NumberClients = _roomData.GetDataRoom().Item1.ToString()
            };


        }
        catch (NotImplementedException)
        {
            Debug.LogError($"Transport {transport} does not support network discovery");
            throw;
        }
    }

    protected override void ProcessResponse(ServerResponse response, IPEndPoint endpoint)
    {

        response.EndPoint = endpoint;


        UriBuilder realUri = new UriBuilder(response.uri)
        {
            Host = response.EndPoint.Address.ToString()
        };
        response.uri = realUri.Uri;

        Debug.Log(response.NumberClients);
        Debug.Log(response.NameRoom);
        OnServerFound.Invoke(response);
    }
}

