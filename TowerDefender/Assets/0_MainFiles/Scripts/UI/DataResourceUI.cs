using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DataResourceUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float delayTooltip = 0.4f;
    [SerializeField] private ResourceObject dataObject;
    [SerializeField] private Image icon;
    [SerializeField] private RectTransform tooltip;
    [SerializeField] private TextMeshProUGUI textCountResource;
    [SerializeField] private TextMeshProUGUI textTooltipName;
    [SerializeField] private TextMeshProUGUI textTooltipDiscription;

    private Coroutine coroutineTooltip;
    TotalSave totalSaveInstance;

    private void OnEnable()
    {
        if (totalSaveInstance == null) { Start(); }

        if (dataObject != null)
        {
            textCountResource.text = totalSaveInstance.saveData.countResources[dataObject.ID].ToString();
            textTooltipName.text = dataObject.name;
            textTooltipDiscription.text = dataObject.Description;
        }
    }

    void Start()
    {       
        totalSaveInstance = TotalSave.Instance;
        icon.color = dataObject.Color;
        tooltip.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SortQueue();
        if (dataObject != null && tooltip != null)
        {
            coroutineTooltip = StartCoroutine(OpenTooltip(delayTooltip));
        }        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (coroutineTooltip != null) { StopCoroutine(coroutineTooltip); }
        tooltip.gameObject.SetActive(false);
    }

    IEnumerator OpenTooltip(float delay)
    {
        yield return new WaitForSeconds(delay);

        tooltip.sizeDelta = new Vector2(0, tooltip.sizeDelta.y);
        tooltip.gameObject.SetActive(true);        
        tooltip.DOSizeDelta(new Vector2(350, tooltip.sizeDelta.y), 0.3f);
    }

    private void SortQueue()
    {
        var cellTransform = this.gameObject.transform.parent;
        cellTransform.SetAsLastSibling();
    }
}