using UnityEngine;
using UnityEngine.UI;

public class KillFeedManager : MonoBehaviour
{
    [SerializeField] Transform killFeedLocation;
    [SerializeField] GameObject killFeedElementPrefab;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite tailKillSprite;
    public static Sprite TailKillSprite;

    private void Start()
    {
        TailKillSprite = tailKillSprite;
    }

    public void AddEntry(string killerName, string killedName)
    {
        Debug.Log(killerName + " Killed " + killedName);

        // permet de placer la nouvelle entry a l'index 0 du kill feed
        int childCount = killFeedLocation.childCount;
        for (int i = 0; i < childCount; i++)
            killFeedLocation.GetChild(i).SetSiblingIndex(i + 1);

        GameObject newEntry = Instantiate(killFeedElementPrefab, killFeedLocation);
        KillFeedElement newEntryElement = newEntry.GetComponent<KillFeedElement>();
        newEntry.transform.SetSiblingIndex(0);
       
        if (killerName != killedName)
        {
            newEntryElement.KillerText.text = killerName;
            newEntryElement.WeapondImage.sprite = KillFeedManager.TailKillSprite;
        }  
        else
        {
            newEntryElement.KillerText.text = "";
            newEntryElement.WeapondImage.sprite = defaultSprite;
        }


        newEntryElement.KilledText.text = killedName;
    }

    public void AddEntry(string killerName, string killedName, Image weapondImage)
    {
        GameObject newEntry = Instantiate(killFeedElementPrefab);
        KillFeedElement newElement = newEntry.GetComponent<KillFeedElement>();
        newElement.KillerText.text = killerName;
        newElement.KilledText.text = killedName;
        newElement.WeapondImage = weapondImage;
    }
}
