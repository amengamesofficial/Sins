using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UI = UnityEngine.UI;
using UIToolkit = UnityEngine.UIElements;

public class MenuUIToolkit : MonoBehaviour
{
    UIToolkit.VisualElement root;

    public GameObject[] playerUIs;
   

    private void Awake()
    {


        UIToolkit.UIDocument document = GetComponent<UIToolkit.UIDocument>();
        root = document.rootVisualElement;



        AddEventToButtons();
       
        ChangeFPS();
        ChangeGraphic();
        ChangeAudio();


        PresetSettings();

        
    }

    void PresetSettings()
    {
        // Initial FPS
        if (PlayerPrefs.GetInt("FPS") == 0)
        {
            root.Q<UIToolkit.RadioButtonGroup>("FPS").value = 0;
            Application.targetFrameRate = 30;
        }
        else
        {
            root.Q<UIToolkit.RadioButtonGroup>("FPS").value = 1;
            Application.targetFrameRate = 60;
        }


        // Initial Graphic
        if (PlayerPrefs.GetInt("Graphic") == 0)
        {
            QualitySettings.SetQualityLevel(0);
            root.Q<UIToolkit.RadioButtonGroup>("Graphic").value = 0;
        }
        else if(PlayerPrefs.GetInt("Graphic") == 1)
        {
            QualitySettings.SetQualityLevel(1);
            root.Q<UIToolkit.RadioButtonGroup>("Graphic").value = 1;
        }
        else
        {
            QualitySettings.SetQualityLevel(2);
            root.Q<UIToolkit.RadioButtonGroup>("Graphic").value = 2;
        }


        // Initial Audio
        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            root.Q<UIToolkit.RadioButtonGroup>("Audio").value = 0;
            AudioListener.pause = false;
        } 
        else
        {
            root.Q<UIToolkit.RadioButtonGroup>("Audio").value = 1;
            AudioListener.pause = true;
        }


        // Initial UI Size
        PlayerPrefs.GetFloat("UISize", 1f);
        root.Q<UIToolkit.Slider>("UIChanger").value = PlayerPrefs.GetFloat("UISize", 1f);
        foreach (GameObject temp in playerUIs)
        {
            temp.GetComponent<UI.CanvasScaler>().scaleFactor = PlayerPrefs.GetFloat("UISize", 1f);
        }
        
    }


    void AddEventToButtons()
    {
        root.Q<UIToolkit.Button>("ResumeButton").clicked += ResumeGame;
        root.Q<UIToolkit.Button>("SettingsButton").clicked += ShowSettings;
        root.Q<UIToolkit.Button>("BackToMenuButton").clicked += ShowMenu;
        root.Q<UIToolkit.Button>("ChangeUISizeButton").clicked += ChangeUISize;
        root.Q<UIToolkit.Button>("Save").clicked += ShowSettings;
        root.Q<UIToolkit.Button>("LevelChooser").clicked += LevelChooser;

    }

    public void LevelChooser()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelChooser");
    }


    public void ShowMenu()
    {

        foreach (UIToolkit.VisualElement vE in root.Children())
        {
            if (vE.name == "Menu") vE.style.display = DisplayStyle.Flex;
            else vE.style.display = DisplayStyle.None;
        }
        Time.timeScale = 0f;
        AudioListener.pause = true;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        root.Q<UIToolkit.VisualElement>("Menu").style.display = DisplayStyle.None;

        if(PlayerPrefs.GetInt("Audio") == 0) AudioListener.pause = false;
        else AudioListener.pause = true;
    }

    public void ShowSettings()
    {
        
        foreach(UIToolkit.VisualElement vE in root.Children())
        {
            if(vE.name == "SettingsPanel") vE.style.display = DisplayStyle.Flex;
            else vE.style.display = DisplayStyle.None;
        }
    }

    void ChangeUISize()
    {

        
        root.Q<UIToolkit.Slider>("UIChanger").value = PlayerPrefs.GetFloat("UISize");

        

        foreach (UIToolkit.VisualElement vE in root.Children())
        {
            if (vE.name == "ChangeUIButtonPanel") vE.style.display = DisplayStyle.Flex;
            else vE.style.display = DisplayStyle.None;
        }

        var changebutton = root.Q<UIToolkit.Slider>("UIChanger");
        changebutton.RegisterValueChangedCallback(evt =>

        {
            float amount = evt.newValue;

            

            foreach(GameObject temp in playerUIs)
            {
                if(temp != null)
                    temp.GetComponent<UI.CanvasScaler>().scaleFactor = amount;
            }

            
            PlayerPrefs.SetFloat("UISize",amount);

        });
    }

    void ChangeFPS()
    {

        var fpsButton = root.Q<UIToolkit.RadioButtonGroup>("FPS");
        fpsButton.RegisterValueChangedCallback(evt =>
        {
            int index = evt.newValue;
            if (index == 0) Application.targetFrameRate = 30;
            else Application.targetFrameRate = 60;
            PlayerPrefs.SetInt("FPS", index);

        });
    }

    void ChangeGraphic()
    {
        var fpsButton = root.Q<UIToolkit.RadioButtonGroup>("Graphic");
        fpsButton.RegisterValueChangedCallback(evt =>
        {
            int index = evt.newValue;
            if (index == 0) QualitySettings.SetQualityLevel(0);
            else if (index == 1) QualitySettings.SetQualityLevel(1);
            else QualitySettings.SetQualityLevel(2);
            PlayerPrefs.SetInt("Graphic", index);

        });
    }

    void ChangeAudio()
    {
        var fpsButton = root.Q<UIToolkit.RadioButtonGroup>("Audio");
        fpsButton.RegisterValueChangedCallback(evt =>
        {
            int index = evt.newValue;
            PlayerPrefs.SetInt("Audio", index);

        });
    }

    
}
