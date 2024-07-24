using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Cinemachine;
using UltimatePlayer;
using System;

public class InterfaceManager : MonoBehaviour
{
    public float timeAnimation = 0.16f;
    public GameObject inventory;
    public static event EventHandler<CanMovedPlayerEvent> OnCanMovedChanged;

    private PlayerInputSystem playerInputSystem;
    private RectTransform rectTransformInterface;
    private CinemachineBrain brainCamera;
    
    private void Awake()
    {
        
        brainCamera = Camera.main.GetComponent<CinemachineBrain>();
        rectTransformInterface = inventory.GetComponent<RectTransform>();
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();
    }

    private void OnEnable()
    {
        playerInputSystem.UI.Inventory.performed += OpenCloseInventory;
    }

    private void OnDisable()
    {
        playerInputSystem.UI.Inventory.performed -= OpenCloseInventory;
    }

    private void OpenCloseInventory(InputAction.CallbackContext context)
    {
        if (!inventory.active)
        {
            brainCamera.enabled = false;
            OnCanMovedChanged?.Invoke(this, new CanMovedPlayerEvent(false));
            rectTransformInterface.sizeDelta = new Vector2(-4, 10);
            inventory.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(0.1f)
                    .Append(rectTransformInterface.DOSizeDelta(new Vector2(1000, rectTransformInterface.sizeDelta.y), timeAnimation))
                    .OnComplete(() => rectTransformInterface.DOSizeDelta(new Vector2(rectTransformInterface.sizeDelta.x, 650), timeAnimation));
        }
        else
        {
            rectTransformInterface.DOSizeDelta(new Vector2(rectTransformInterface.sizeDelta.x, 10), timeAnimation).OnComplete(() => 
            rectTransformInterface.DOSizeDelta(new Vector2(-4, rectTransformInterface.sizeDelta.y), timeAnimation).OnComplete(() =>
            inventory.SetActive(false)));
            brainCamera.enabled = true;
            OnCanMovedChanged?.Invoke(this, new CanMovedPlayerEvent(true));
        }
    }
}
