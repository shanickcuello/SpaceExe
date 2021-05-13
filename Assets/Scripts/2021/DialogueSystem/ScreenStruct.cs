using System.Collections.Generic;

[System.Serializable]
public struct ScreenList
{
    public List<StorytellingScreen> list;
}

[System.Serializable]
public struct StorytellingScreen
{
    public string imagePath;
    public bool hasButton;
    public bool lastScreen;
    public string dialogueText;
    public float xPositionText;
    public float yPositionText;
    public int sizeText;
    public bool BlackColorText;
}