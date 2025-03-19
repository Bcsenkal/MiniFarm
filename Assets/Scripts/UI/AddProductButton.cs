using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using Managers;

public class AddProductbutton : ProductionButton
{
    private StringBuilder stringBuilder = new StringBuilder();
    private Image requiredProductIcon;
    private TextMeshProUGUI requiredProductAmount;

    protected override void Awake()
    {
        base.Awake();
        requiredProductIcon = transform.GetChild(1).GetComponent<Image>();
        requiredProductAmount = requiredProductIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        button.onClick.AddListener(AddProduction);
    }

    protected void AddProduction()
    {
        currentSelection.AddProduction();
        AudioManager.Instance.PlayPositiveButtonClick();
    }

    // Since all productions only need one material, I used index of 0, but in case of multiple materials, it can be changed as list of images and for loop
    protected override void SetSelection(Building building)
    {
        base.SetSelection(building);
        if(currentSelection == null) return;
        button.interactable = !currentSelection.IsMaxCapacity() && currentSelection.HasEnoughMaterials();
        buttonText.text = currentSelection.IsMaxCapacity() ? "MAX" : "+1";
        requiredProductIcon.sprite = currentSelection.GetBuildingData().inputProducts[0].product.sprite;
        stringBuilder.Clear();
        stringBuilder.Append("x");
        stringBuilder.Append(currentSelection.GetBuildingData().inputProducts[0].amount);
        requiredProductAmount.text = currentSelection.GetBuildingData().inputProducts[0].amount.ToString();
    }
}
