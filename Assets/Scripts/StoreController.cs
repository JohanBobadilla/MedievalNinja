using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public float zoomCamera = 2f;
    public Text price;
    public Text indicationTitle;
    public Text indicationButton;
    private int armorIndex;
    private int tempArmorIndex;
    private int faceIndex;
    private int tempFaceIndex;
    private PlayerLoader playerLoader;
    private PlayerMovement playerMovement;
    private SavedGame currentStatus;
    private bool changingArmor;

    private void OnEnable()
    {
        SetUpParameters();
    }

    private void SetUpParameters()
    {
        playerLoader = playerLoader == null ? FindObjectOfType<PlayerLoader>(true) : playerLoader;
        playerMovement = playerMovement == null ? FindObjectOfType<PlayerMovement>(true) : playerMovement;
        playerMovement.SetMovementStatus(false);
        playerMovement.SetCameraZoom(zoomCamera);
        currentStatus = playerLoader.GetPlayerStatus();
        armorIndex = currentStatus.armorSetIndex;
        faceIndex = currentStatus.faceSetIndex;
        tempArmorIndex = currentStatus.armorSetIndex;
        tempFaceIndex = currentStatus.faceSetIndex;
        changingArmor = true;
        SetTexts();
    }

    private void SetTexts()
    {
        if (changingArmor)
        {
            indicationTitle.text =  "Use Arrows to Select Armor Set";
            indicationButton.text = "Change Face (Space)";
        }
        else
        {
            indicationTitle.text =  "User Arrows to Select Face Set";
            indicationButton.text = "Change Armor (Space)";
        }
    }

    private void OnDisable() {
        playerMovement.SetMovementStatus(true);
        playerMovement.ResetCamera();
        playerLoader.LoadFaceSet(tempFaceIndex);
        playerLoader.LoadArmorSet(tempArmorIndex);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            changingArmor = changingArmor ? false : true;
            SetTexts();
        }

        if (changingArmor)
        {
            ChangeArmor();
        }
        else
        {
            ChangeFace();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PurchaseSet();
        }
    }

    public void PurchaseSet()
    {
        if (price.text == "Owned") 
        {
            tempArmorIndex = changingArmor ? armorIndex : tempArmorIndex;
            tempFaceIndex = !changingArmor ? faceIndex : tempFaceIndex;       
            return; 
        } 

        if (changingArmor)
        {
            UnlockedSet set = new UnlockedSet { index = armorIndex };
            bool purchased = playerLoader.AddArmorSet(Db.instance.armorSets[armorIndex].price, set);
            price.text = purchased ? "Owned" : price.text;
            tempArmorIndex = purchased ? armorIndex : tempArmorIndex;
        }
        else
        {
            UnlockedSet set = new UnlockedSet { index = faceIndex };
            bool purchased = playerLoader.AddFaceSet(Db.instance.armorSets[armorIndex].price, set);
            price.text = purchased ? "Owned": price.text;
            tempFaceIndex = purchased ? faceIndex : tempFaceIndex;       
        }
    }

    public void ChangeArmor()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            armorIndex--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow))
        {
            armorIndex++;
        }

        if (armorIndex > Db.instance.armorSets.Count - 1)
        {
            armorIndex = 0;
        }

        if (armorIndex < 0)
        {
            armorIndex = Db.instance.armorSets.Count - 1;
        }

        if (currentStatus.armorSets.Find(set => set.index == armorIndex) != null)
        {
            price.text = "Owned";
            playerLoader.LoadArmorSet(armorIndex);
        }
        else
        {
            price.text = Db.instance.armorSets[armorIndex].price.ToString();
            playerLoader.LoadArmorSet(armorIndex);
        }
    }

    public void ChangeFace()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !Input.GetKeyDown(KeyCode.RightArrow))
        {
            faceIndex--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !Input.GetKeyDown(KeyCode.LeftArrow))
        {
            faceIndex++;
        }

        if (faceIndex > Db.instance.faceSets.Count - 1)
        {
            faceIndex = 0;
        }

        if (faceIndex < 0)
        {
            faceIndex = Db.instance.faceSets.Count - 1;
        }

        if (currentStatus.faceSets.Find(set => set.index == faceIndex) != null)
        {
            price.text = "Owned";
            playerLoader.LoadFaceSet(faceIndex);
        }
        else
        {
            price.text = Db.instance.faceSets[faceIndex].price.ToString();
            playerLoader.LoadFaceSet(faceIndex);
        }

    }
}
