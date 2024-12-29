using System.Net;
using System.Net.Sockets;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private NetworkManager m_NetworkManager;
    public static GameManager Instance;
    void Awake()
    {
        m_NetworkManager = GetComponent<NetworkManager>();
        Instance = this;
        if(!PlayerPrefs.HasKey("Mode")) return;
        string mode = PlayerPrefs.GetString("Mode");

        if(mode == "Host")
        {
            m_NetworkManager.StartHost();
        }
        else if(mode == "Client")
        {
            string ip = PlayerPrefs.GetString("IP");
            m_NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address = ip;
            m_NetworkManager.StartClient();
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
        {
            StartButtons();
        }
        else 
        {
            StatusLabels();
        }
        GUILayout.EndArea();
    }
    void StartButtons()
    {
        if (GUILayout.Button("Host"))
        {
            m_NetworkManager.StartHost();
        }
        
        if (GUILayout.Button("Client")) 
        {
            m_NetworkManager.StartClient();
        }
        
        if (GUILayout.Button("Server")) 
        {
            m_NetworkManager.StartServer();
        }
    }
    string localIP = null;
    void StatusLabels()
    {
        var mode = m_NetworkManager.IsHost ?
            "Host" : m_NetworkManager.IsServer ? "Server" : "Client";
        GUILayout.Label("Transport: " +
            m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
        
        if(localIP == null)
        {
            localIP = GetLocalIPAddress();
        }
        
        GUILayout.Label("Local IP: " + localIP);
    }

    public string GetLocalIPAddress()
    {
        string ipAddress = "0.0.0.0";
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddress = ip.ToString();
                Debug.Log("local ip: " + ip);
                return ip.ToString();
            }
        }
        return ipAddress;
    }
    
    public int score1 = 0, score2 = 0;
}
