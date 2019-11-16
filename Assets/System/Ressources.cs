using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressources : MonoBehaviour
{
    #region Icons
    [SerializeField] Sprite infoIcon;
    [SerializeField] Sprite warnIcon;
    [SerializeField] Sprite errIcon;
    [SerializeField] Sprite publicIcon;
    [SerializeField] Sprite privateIcon;
    [SerializeField] Sprite skullIcon;
    [SerializeField] Sprite wheelIcon;

    public static Sprite InfoIcon;
    public static Sprite WarnIcon;
    public static Sprite ErrIcon;
    public static Sprite PublicIcon;
    public static Sprite PrivateIcon;
    public static Sprite SkullIcon;
    public static Sprite WheelIcon;
    #endregion Icons

    #region Colors
    public static Color SystemInfoColor = new Color(0.45f, 0.65f, 0.8f, 0.3f);
    public static Color SystemWarnColor = new Color(1f, 0.9f, 0f, 0.4f);
    public static Color SystemErrColor = new Color(1f, 0f, 0f, 0.5f);
    public static Color PlayerPublicColor = new Color(0.2f, 0.2f, 0.2f, 0.25f);
    public static Color PlayerPrivateColor = new Color(0.9f, 0f, 1f, 0.25f);
    public static Color KillFeedColor = new Color(1f, 0f, 0f, 0.3f);
    #endregion Colors

    private void Start()
    {
        #region Icons
        InfoIcon = infoIcon;
        WarnIcon = warnIcon;
        ErrIcon = errIcon;
        PublicIcon = publicIcon;
        PrivateIcon = privateIcon;
        SkullIcon = skullIcon;
        WheelIcon = wheelIcon;
        #endregion Icons
    }
}
