using System.Collections.Generic;
using System;

[Serializable]
public class SavedGame
{
    public List<UnlockedSet> armorSets;
    public List<UnlockedSet> faceSets;
    public int armorSetIndex;
    public int faceSetIndex;
    public int gems;

    public SavedGame()
    {
        UnlockedSet unlockedSet = new UnlockedSet{ index = 0 };
        armorSets = new List<UnlockedSet>();
        faceSets = new List<UnlockedSet>();
        armorSets.Add(unlockedSet);
        faceSets.Add(unlockedSet);
        armorSetIndex = 0;
        faceSetIndex = 0;
        gems = 5000;
    }
}
