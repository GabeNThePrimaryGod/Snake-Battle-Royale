using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChat : MonoBehaviour
{
    [SerializeField] Transform chatContent;
    [SerializeField] InputField inputField;

    [Header("Prefabs : ")] 
    [SerializeField] GameObject systemElementPrefab;
    [SerializeField] GameObject killFeedElementPrefab;
    [SerializeField] GameObject playerElementPrefab;

    public void OnInputFieldValueChanged(string querry)
    {

    }

    public void OnInputFieldEndEdit(string querry)
    {
        AddPlayerEntry(Ressources.PublicIcon, GameManager.LocalPlayer.PlayerName, querry);
        inputField.text = "";
    }

    private void ReOrganizeChat()
    {
        int childCount = chatContent.childCount;
        for (int i = 0; i < childCount; i++)
            chatContent.GetChild(i).SetSiblingIndex(i + 1);
    }

    public void AddSystemEntry(Sprite type, string message, Color backgroundColor)
    {
        ReOrganizeChat();
        GameObject chatElement = Instantiate(systemElementPrefab, chatContent);
        chatElement.transform.SetSiblingIndex(0);

        SystemChatMessage chatMessage = chatElement.GetComponent<SystemChatMessage>();
        chatMessage.Type.sprite = type;
        chatMessage.Message.text = message;
        chatMessage.Background.color = backgroundColor;
    }

    public void AddSystemEntry(Sprite type, string message)
    {
        if (type == Ressources.InfoIcon)
            AddSystemEntry(type, message, Ressources.SystemInfoColor);
        else if(type == Ressources.WarnIcon)
            AddSystemEntry(type, message, Ressources.SystemWarnColor);
        else if (type == Ressources.ErrIcon)
            AddSystemEntry(type, message, Ressources.SystemErrColor);
        else
            AddSystemEntry(type, message, Ressources.SystemErrColor);
    }


    public void AddKillFeedEntry(Sprite type, string killer, Sprite killType, string killed, Color backgroundColor)
    {
        ReOrganizeChat();
        GameObject chatElement = Instantiate(killFeedElementPrefab, chatContent);
        chatElement.transform.SetSiblingIndex(0);

        KillFeedChatMessage chatMessage = chatElement.GetComponent<KillFeedChatMessage>();
        chatMessage.Type.sprite = type;
        chatMessage.Killer.text = killer;
        chatMessage.KillType.sprite = killType;
        chatMessage.Killed.text = killed;
        chatMessage.Background.color = backgroundColor;
    }

    public void AddKillFeedEntry(Sprite type, string killer, Sprite killType, string killed)
    {
        AddKillFeedEntry(type, killer, killType, killed, Ressources.KillFeedColor);
    }


    public void AddPlayerEntry(Sprite type, string sender, string message, Color backgroundColor)
    {
        ReOrganizeChat();
        GameObject chatElement = Instantiate(playerElementPrefab, chatContent);
        chatElement.transform.SetSiblingIndex(0);

        PlayerChatMessage chatMessage = chatElement.GetComponent<PlayerChatMessage>();
        chatMessage.Type.sprite = type;
        chatMessage.Sender.text = sender;
        chatMessage.Message.text = message;
        chatMessage.Background.color = backgroundColor;

    }

    public void AddPlayerEntry(Sprite type, string sender, string message)
    {
        if (type == Ressources.PrivateIcon)
            AddPlayerEntry(type, sender, message, Ressources.PlayerPrivateColor);
        else
            AddPlayerEntry(type, sender, message, Ressources.PlayerPublicColor);
    }
}