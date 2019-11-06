using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    List<Behaviour> componentsToDisable = new List<Behaviour>();

    [SerializeField]
    List<GameObject> gameObjectsToDisable = new List<GameObject>();

    private void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (Behaviour component in componentsToDisable)
            {
                component.enabled = false;
            }

            foreach (GameObject gameObject in gameObjectsToDisable)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
