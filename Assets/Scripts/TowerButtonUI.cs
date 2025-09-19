using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TowerButtonUI : MonoBehaviour
{
    public TowerData tower;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI towerCostText;
    public Image towerIcon;

    private Button button;

    void OnEnable()
    {
        GameManager.instance.onMoneyChange.AddListener(OnMoneyChanged);
        // Subscribe to the cost change event
        CannonTowerChecker checker = FindObjectOfType<CannonTowerChecker>();
        if (checker != null)
        {
            checker.onTowerCostChanged.AddListener(UpdateUI);
        }
    }

    void OnDisable()
    {
        GameManager.instance.onMoneyChange.RemoveListener(OnMoneyChanged);
        // Unsubscribe from the cost change event
        CannonTowerChecker checker = FindObjectOfType<CannonTowerChecker>();
        if (checker != null)
        {
            checker.onTowerCostChanged.RemoveListener(UpdateUI);
        }
    }

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        towerNameText.text = tower.displayName;
        towerCostText.text = $"${tower.cost}";
        towerIcon.sprite = tower.icon;

        OnMoneyChanged();
    }

    public void OnClick()
    {
        GameManager.instance.towerPlacement.SelectTowerToPlace(tower);
    }

    void OnMoneyChanged()
    {
        button.interactable = GameManager.instance.money >= tower.cost;
    }

    void UpdateUI()
    {
        towerCostText.text = $"${tower.cost}";
        OnMoneyChanged();
    }

}
