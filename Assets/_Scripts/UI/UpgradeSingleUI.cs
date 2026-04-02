using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSingleUI : MonoBehaviour
{
    private const string UPGRADE_LEVEL_TEXT = "LV. ";

    private UpgradeSO upgrade;

    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeLevelText;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Button upgradeButton;

    [SerializeField] private float popScale;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float settleDuration;
    [SerializeField] private float moveDirectionY;
    [SerializeField] private float moveDuration;
    [SerializeField] private float shrinkScale;

    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.localPosition;
    }

    private void OnEnable()
    {
        ActionManager.OnUISelected += HandleSelectedMovement;
    }

    private void OnDisable()
    {
        ActionManager.OnUISelected -= HandleSelectedMovement;
    }

    private void PopUp()
    {
        gameObject.SetActive(true);

        transform.localScale = Vector3.zero;

        transform.DOScale(popScale, scaleDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOScale(1f, settleDuration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                upgradeButton.gameObject.SetActive(true);
            }).SetUpdate(true);
        }).SetUpdate(true);
    }

    public void SetSingleUpgrade(UpgradeSO upgrade)
    {
        this.upgrade = upgrade;
        upgradeNameText.text = upgrade.upgradeName;
        upgradeLevelText.text = UPGRADE_LEVEL_TEXT + upgrade.upgradeLevel;
        upgradeImage.sprite = upgrade.upgradeSprite;

        upgradeButton.gameObject.SetActive(false);

        PopUp();
    }

    public void OnUpgradeSelected()
    {
        upgrade.UpgradeTier();

        ActionManager.OnUISelected(this);
    }

    private void HandleSelectedMovement(UpgradeSingleUI upgradeSingleUI)
    {
        upgradeButton.gameObject.SetActive(false);

        if (this == upgradeSingleUI)
        {
            upgrade.Apply(Player.instance.gameObject);

            ActionManager.OnGamePausedCancelled?.Invoke();
            SelectedAnimation();
        }
        else
            Shrink();
    }

    private void SelectedAnimation()
    {
        transform.DOScale(popScale, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            transform.DOMoveY(moveDirectionY, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                ResetPosition();
            }).SetUpdate(true);
        }).SetUpdate(true);
    }

    private void Shrink()
    {
        transform.DOScale(shrinkScale, moveDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            ResetPosition();
        }).SetUpdate(true);
    }

    private void ResetPosition()
    {
        transform.localPosition = initialPos;
        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }
}