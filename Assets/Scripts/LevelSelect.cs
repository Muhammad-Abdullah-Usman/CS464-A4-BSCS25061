using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] LevelLoader loader;

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;
            Button button = levelButtons[i];
            button.interactable = level <= SaveData.Unlocked;
            TMP_Text label = button.GetComponentInChildren<TMP_Text>();
            if (SaveData.HasBest(level))
                label.SetText("Level {0}\n{1:1} s", level, SaveData.BestTime(level));
            else
                label.SetText("Level {0}", level);
            button.onClick.AddListener(() => loader.Load(level));
        }
    }
}