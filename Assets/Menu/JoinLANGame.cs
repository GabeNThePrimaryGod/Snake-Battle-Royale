using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JoinLANGame : MonoBehaviour
{
    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        networkManager.StopMatchMaker();
    }

    bool isAdresseValid = false;
    bool isPortValid = false;

    private string netAdress;
    public string NetAdress
    {
        get => netAdress;
        set
        {
            netAdress = value;

            string[] netAdresseAndPort = netAdress.Split(':');

            string[] spliedAdresse = netAdresseAndPort[0].Split('.');
            bool adresseIsCorrect = true;

            foreach (string element in spliedAdresse)
            {
                int tempInt = 256;
                if (int.TryParse(element, out tempInt) && tempInt < 256)
                { }
                else
                {
                    isAdresseValid = false;
                    Debug.LogWarning("Incorect adresse");
                    adresseIsCorrect = false;
                    break;
                }
            }
            
            if (adresseIsCorrect && spliedAdresse.Length == 4)
            {
                isAdresseValid = true;
                Debug.Log("Net adresse : " + netAdresseAndPort[0]);
                networkManager.networkAddress = netAdresseAndPort[0];
            }

            int port = 0;

            if (int.TryParse(netAdresseAndPort[1], out port) && port < 2556)
            {
                isPortValid = true;
                Debug.Log("Net Port : " + port);
                networkManager.networkPort = port;
            }
            else
            {
                isPortValid = false;
                Debug.LogWarning("Incorect Port");
            }
        }
    }

    public void StartHost()
    {
        networkManager.StartHost();
    }

    public void StartClient()
    {
        networkManager.StartClient();
    }
}
