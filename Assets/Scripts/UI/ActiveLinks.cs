using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ActiveLinks : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TextMeshProUGUI textMessage;

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMessage, eventData.position, eventData.pressEventCamera);
        if (linkIndex == -1)
            return;
        TMP_LinkInfo linkInfo = textMessage.textInfo.linkInfo[linkIndex];
        string selectedLink = linkInfo.GetLinkID();
        if (selectedLink != "")
        {
            Debug.LogFormat("Open link {0}", selectedLink);
            Application.OpenURL(selectedLink);
        }
    }
}
