using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoader : MonoBehaviour
{
    [Header("BodyParts")]
    public SpriteRenderer rightBoot;
    public SpriteRenderer leftBoot;
    public SpriteRenderer rightLeg;
    public SpriteRenderer leftLeg;
    public SpriteRenderer pelvis;
    public SpriteRenderer torso;
    public SpriteRenderer rightShoulder;
    public SpriteRenderer leftShoulder;
    public SpriteRenderer rightForearm;
    public SpriteRenderer leftForearm;
    public SpriteRenderer rightGlove;
    public SpriteRenderer leftGlove;
    public SpriteRenderer hood;
    public SpriteRenderer face;

    [Header("StoredInformation")]

    private SavedGame savedGame;
    public Text gemsText;

    void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedGame"))
        {
            string savedGameJson = PlayerPrefs.GetString("SavedGame");
            savedGame = JsonUtility.FromJson<SavedGame>(savedGameJson);
        }
        else
        {
            savedGame = new SavedGame();
        }

        LoadArmorSet(savedGame.armorSetIndex);
        LoadFaceSet(savedGame.faceSetIndex);
        gemsText.text = savedGame.gems.ToString();
        SaveGame();
    }

    public void LoadArmorSet(int armorIndex)
    {
        rightBoot.sprite = Db.instance.armorSets[armorIndex].rightBoot;
        leftBoot.sprite = Db.instance.armorSets[armorIndex].leftBoot;
        rightLeg.sprite = Db.instance.armorSets[armorIndex].rightLeg;
        leftLeg.sprite = Db.instance.armorSets[armorIndex].leftLeg;
        pelvis.sprite = Db.instance.armorSets[armorIndex].pelvis;
        torso.sprite = Db.instance.armorSets[armorIndex].torso;
        rightShoulder.sprite = Db.instance.armorSets[armorIndex].rightShoulder;
        leftShoulder.sprite = Db.instance.armorSets[armorIndex].leftShoulder;
        rightForearm.sprite = Db.instance.armorSets[armorIndex].rightForearm;
        leftForearm.sprite = Db.instance.armorSets[armorIndex].leftForearm;
        rightGlove.sprite = Db.instance.armorSets[armorIndex].rightGlove;
        leftGlove.sprite = Db.instance.armorSets[armorIndex].leftGlove;
        hood.sprite = Db.instance.armorSets[armorIndex].hood;
        savedGame.armorSetIndex = armorIndex;

    }

    public void LoadFaceSet(int faceIndex)
    {
        face.sprite = Db.instance.faceSets[faceIndex].face;
        savedGame.faceSetIndex = faceIndex; 
    }

    public void SaveGame()
    {
        string savedGameJson = JsonUtility.ToJson(savedGame);
        PlayerPrefs.SetString("SavedGame", savedGameJson);  
    }

    public bool AddArmorSet(int amount, UnlockedSet set)
    {
        if (savedGame.gems - amount < 0)
        {
            return false;
        }
        else
        {
            savedGame.gems -= amount;
            gemsText.text = savedGame.gems.ToString();
            savedGame.armorSets.Add(set);
            SaveGame();
            return true;
        }
    }

    public bool AddFaceSet(int amount, UnlockedSet set)
    {
        if (savedGame.gems - amount < 0)
        {
            return false;
        }
        else
        {
            savedGame.gems -= amount;
            gemsText.text = savedGame.gems.ToString();
            savedGame.faceSets.Add(set);
            SaveGame();
            return true;
        }
    }

    public SavedGame GetPlayerStatus()
    {
        return savedGame;
    }
}
