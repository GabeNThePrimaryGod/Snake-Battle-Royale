using UnityEngine;
using UnityEngine.UI;

public enum KillFeedSprite { Default, Wheel }

public class KillFeedManager : MonoBehaviour
{
    [SerializeField] Transform killFeedLocation;
    [SerializeField] GameObject killFeedElementPrefab;

    public void AddEntry(string killerName, string killedName)
    {
        /*TEMPORAIRE*/

        if (killerName == killedName)
            AddEntry("", killedName, Ressources.SkullIcon);
        else
            AddEntry(killerName, killedName, Ressources.WheelIcon);
    }

    public void AddEntry(string killerName, string killedName, Sprite sprite)
    {
        GameManager.LocalPlayer.UI.Chat.AddKillFeedEntry(Ressources.SkullIcon, killerName, sprite, killedName);

        // permet de placer la nouvelle entry a l'index 0 du kill feed
        int childCount = killFeedLocation.childCount;
        for (int i = 0; i < childCount; i++)
            killFeedLocation.GetChild(i).SetSiblingIndex(i + 1);

        GameObject newEntry = Instantiate(killFeedElementPrefab, killFeedLocation);
        newEntry.transform.SetSiblingIndex(0);      // met le nouvel element au plus bas du kill feed
        KillFeedElement newEntryElement = newEntry.GetComponent<KillFeedElement>(); 

        newEntryElement.WeapondImage.sprite = sprite;
        newEntryElement.KillerText.text = killerName;
        newEntryElement.KilledText.text = killedName;
    }
}
