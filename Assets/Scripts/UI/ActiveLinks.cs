using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ActiveLinks : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI textMessage;

        public void OnPointerClick(PointerEventData eventData)
            => TryOpenLink(eventData);
        private void TryOpenLink(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMessage, eventData.position, eventData.pressEventCamera);
            if (linkIndex == -1)
                return;
            TMP_LinkInfo linkInfo = textMessage.textInfo.linkInfo[linkIndex];
            string selectedLink = linkInfo.GetLinkID();
            if (selectedLink != "")
                Application.OpenURL(selectedLink);
        }
    }
}
