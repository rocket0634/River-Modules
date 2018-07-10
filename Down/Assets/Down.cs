using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KMHelper;

public class Down : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMBombModule Module;
    public KMAudio Audio;

    public KMSelectable buttonLeft, buttonUp, buttonRight;
    public Texture2D cat, ledge;
    public TextMesh Commands;
    public TextMesh Names;
    public KMModSettings modSettings;
    public Renderer textRenderer;
    private List<string> defaultNames = new List<string>();
    private List<string> defaultCommands = new List<string>();

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool Active = false;
    private bool Solved = false;
    private int strikeGet;

    void Start()
    {
        moduleId = moduleIdCounter++;
        Module.OnActivate += delegate() { Active = true; };

        buttonLeft.OnInteract += delegate { ButtonPress(buttonLeft); return false; };
        buttonRight.OnInteract += delegate { ButtonPress(buttonRight); return false; };
        buttonUp.OnInteract += delegate { ButtonPress(buttonUp); return false; };
    }

    private void Database()
    {
        
    }

    void Update()
    {
        strikeGet = Bomb.GetStrikes();
    }

    private bool ButtonPress(KMSelectable button)
    {
        if (Active && !Solved)
        {
            button.AddInteractionPunch();
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if (button == buttonUp)
            {
                Module.HandlePass();
                Solved = true;
                return true;
            }
            else if (button == buttonLeft)
            {
                Debug.LogFormat("{0}", strikeGet.ToString());
            }
            else
            {
                Module.HandleStrike();
            }
        }
        return false;
    }
}
