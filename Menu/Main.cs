using BepInEx;
using BepInEx.Configuration;
using GorillaLocomotion;
using GorillaNetworking;
using GorillaTag;
using HarmonyLib;
using Oculus.Platform.Models;
using Photon.Pun;
using PlayFab;
using StupidTemplate.Classes;
using StupidTemplate.Menu;
using StupidTemplate.Mods;
using StupidTemplate.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;
using static StupidTemplate.Menu.Buttons;
using static StupidTemplate.Settings;

namespace StupidTemplate.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static void Prefix()
        {
            try
            {
                DougAndMattShit.SetBug();
            }
            catch { }
            if (GetIndex("PC EMULATION").enabled)
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    ControllerInputPoller.instance.leftGrab = true;
                    ControllerInputPoller.instance.leftControllerIndexFloat = 1f;
                }

                if (Mouse.current.rightButton.isPressed)
                {
                    ControllerInputPoller.instance.rightGrab = true;
                    ControllerInputPoller.instance.rightControllerIndexFloat = 1f;
                }

                if (UnityInput.Current.GetKey(KeyCode.W))
                {
                    Player.Instance.transform.position += Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                    Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                if (UnityInput.Current.GetKey(KeyCode.S))
                {
                    Player.Instance.transform.position -= Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                    Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                if (UnityInput.Current.GetKey(KeyCode.A))
                {
                    Player.Instance.Turn(-1.45f);
                }

                if (UnityInput.Current.GetKey(KeyCode.D))
                {
                    Player.Instance.Turn(1.45f);
                }

                if (UnityInput.Current.GetKey(KeyCode.Space))
                {
                    Player.Instance.transform.position += new Vector3(0f, 0.10f, 0f);
                }
            }

            try
            {
                GameObject.Find("motdtext").GetComponent<Text>().text = "HEY THANKS FOR USING <color=#ff9400>" + PluginInfo.Name + " V:" + PluginInfo.Version + "</color> THIS MENU IS VERY SIMPLE BUT THANKS FOR USING IT!\n\nALSO THIS MENU IS PRIMARLY FOR GHOST TROLLING DONT USE IT IF YOU WANT A WHOLE BUNCH OF OP SHIT YOU WILL BE DISSAPOINTED";
                GameObject.Find("COC Text").GetComponent<Text>().text = "IT IS PRETTY SIMPLE TO USE THIS MENU IF YOU GET BANNED 504BRANDON TAKES NO RESPONSIBILITY!\nANYTHING THAT IS <color=#ff9400>THIS COLOR</color> MEANS THAT IS THE BUTTON TO USE FOR EXAMPLE [<color=#ff9400>B</color>] MEANS TO USE B FOR THE MOD\nANYTHING THAT IS <color=#570000>THIS COLOR</color> OR [<color=#570000>D</color>] BY IT MEANS DETECTED\nANYTHING THAT IS <color=#ffcc00>THIS COLOR</color> OR HAS [<color=#ffcc00>D?/EX</color>] BY IT MEANS THE MOD IS POSSIBLY DETECTED AND OR IS EXPERIMENTAL\n\nTHATS ABOUT IT HAPPY <color=#570000>ILLEGAL</color> MODDING";//COC writing
                GameObject.Find("CodeOfConduct").GetComponent<Text>().text = "HOW TO USE <color=#ff9400>504MC</color>";//COC Title
            }
            catch { }

            if (!hasLoaded)
            {
                /*if (PlayerPrefs.GetInt("buttonLayout") >= 0)
                {
                    buttonLayout = PlayerPrefs.GetInt("buttonLayout");
                }*/

                Global.DoSettingsShit();

                if (FileUtils.ReadTXTFile("MODS").Contains("Should Save Mods"))
                {
                    foreach (string mod in FileUtils.ReadTXTFile("MODS").Split("\n"))
                    {
                        try
                        {
                            Toggle(mod);
                        }
                        catch { }
                    }
                }

                /*if (FileUtils.ReadTXTFile("themes/RGB_THEME") != null && FileUtils.ReadTXTFile("themes/RGB_THEME") != "")
                {
                    string[] color1 = FileUtils.ReadTXTFile("themes/RGB_THEME").Split("\n")[0].Split(",");
                    string[] color2 = FileUtils.ReadTXTFile("themes/RGB_THEME").Split("\n")[1].Split(",");

                    newBackroundColor = new ExtGradient
                    {
                        colors = new GradientColorKey[]
                        {
                            new GradientColorKey(new Color(int.Parse(color1[0]), int.Parse(color1[1]), int.Parse(color1[2])), 0.25f),
                            new GradientColorKey(new Color(int.Parse(color2[0]), int.Parse(color2[1]), int.Parse(color2[2])), 1f),
                        }
                    };
                }

                if (FileUtils.ReadTXTFile("themes/RGB_THEME_BORDER") != null && FileUtils.ReadTXTFile("themes/RGB_THEME_BORDER") != "")
                {
                    string[] color1 = FileUtils.ReadTXTFile("themes/RGB_THEME_BORDER").Split("\n")[0].Split(",");
                    string[] color2 = FileUtils.ReadTXTFile("themes/RGB_THEME_BORDER").Split("\n")[1].Split(",");

                    BackroundBorderColor = new ExtGradient
                    {
                        colors = new GradientColorKey[]
                        {
                            new GradientColorKey(new Color(int.Parse(color1[0]), int.Parse(color1[1]), int.Parse(color1[2])), 0.25f),
                            new GradientColorKey(new Color(int.Parse(color2[0]), int.Parse(color2[1]), int.Parse(color2[2])), 1f),
                        }
                    };
                }

                if (FileUtils.ReadTXTFile("themes/RGB_THEME_BUTTON") != null && FileUtils.ReadTXTFile("themes/RGB_THEME_BUTTON") != "")
                {
                    string[] color1 = FileUtils.ReadTXTFile("themes/RGB_THEME_BUTTON").Split("\n")[0].Split(",");
                    string[] color2 = FileUtils.ReadTXTFile("themes/RGB_THEME_BUTTON").Split("\n")[1].Split(",");

                    newButtonColors[0] = new Color(int.Parse(color1[0]), int.Parse(color1[1]), int.Parse(color1[2]));
                    newButtonColors[1] = new Color(int.Parse(color2[0]), int.Parse(color2[1]), int.Parse(color2[2]));
                }*/

                descriptionText = "Click a mod!";
            }

            hasLoaded = true;

            if (DateTime.Now.Day == 1 && DateTime.Now.Month == 4 || GetIndex("April Fools").enabled)
            {
                foreach (ButtonInfo[] buttons in Buttons.buttons)
                {
                    foreach (ButtonInfo button in buttons)
                    {
                        button.overlapText = "???";
                    }
                }
            }

            try // just in case were banned lmao
            {
                if (OwnerIDs.Contains(PhotonNetwork.LocalPlayer.UserId) || AdminIDs.Contains(PhotonNetwork.LocalPlayer.UserId))
                    Buttons.buttons[0][Buttons.buttons[0].Length - 1] = new ButtonInfo { buttonText = "Admin Panel", method =() => SettingsMods.AdminShit(), isTogglable=false};
            } catch { }

            /*if (buttonLayout == 3 && Time.time > timeTilYouCanClickAgain && menu != null)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1)
                {
                    Toggle("NextPage", false);
                    timeTilYouCanClickAgain = Time.time + 0.5f;
                }

                if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.1)
                {
                    Toggle("PreviousPage", false);
                    timeTilYouCanClickAgain = Time.time + 0.5f;
                }
            }*/

            // Initialize Menu
            try
            {
                bool toOpen = (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) || (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton && !GetIndex("Watch Menu").enabled);
                bool keyboardOpen = false;

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenu();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                    }
                    else
                    {
                        Rigidbody comp = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        if (rightHanded)
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        else
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }

                        if (GetIndex("No menu fall").enabled)
                            UnityEngine.Object.Destroy(menu);
                        else
                            UnityEngine.Object.Destroy(menu, 2);

                        menu = null;

                        UnityEngine.Object.Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            // Constant
            try
            {
                // Execute Enabled mods
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            GorillaComputer.instance.pressedMaterial.color = newButtonColors[1];
        }

        // Functions
        public static void CreateMenu()
        {
            // Menu Holder
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            // Menu Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

            if (PNGTheme == 0)
            {
                ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
                colorChanger.colorInfo = newBackroundColor;
                colorChanger.Start();
            }

            if (PNGTheme > 0)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = FileUtils.LoadTheme(FileUtils.ReadTXTFile("themes/THEMES").Split("\n")[PNGTheme], "menubg" + PNGTheme + ".png");
            }

            GameObject MenuBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(MenuBorder.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(MenuBorder.GetComponent<BoxCollider>());
            MenuBorder.transform.parent = menu.transform;
            MenuBorder.transform.position = new Vector3(0.05f, 0f, 0f);
            MenuBorder.transform.rotation = Quaternion.identity;
            MenuBorder.transform.localScale = new Vector3(0.09f, 1.05f, menuSize.z + 0.05f);
            MenuBorder.GetComponent<Renderer>().forceRenderingOff = !GetIndex("Menu Border").enabled;

            if (BorderPNGTheme == 0)
            {
                ColorChanger colorChangerBorder = MenuBorder.AddComponent<ColorChanger>();
                colorChangerBorder.colorInfo = BackroundBorderColor;
                colorChangerBorder.Start();
            }

            if (BorderPNGTheme > 0)
            {
                MenuBorder.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                MenuBorder.GetComponent<Renderer>().material.color = Color.white;
                MenuBorder.GetComponent<Renderer>().material.mainTexture = FileUtils.LoadTheme(FileUtils.ReadTXTFile("themes/THEMES").Split("\n")[BorderPNGTheme], "menuborder" + BorderPNGTheme + ".png");
            }

            // Canvas
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            // Title and FPS
            string pageNumText = " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";

            if (menuNameHehe == "Rexon Free")
                pageNumText = " [P" + pageNumber + "]";
            if (menuNameHehe == "ModderX.1.0" || menuNameHehe.Contains("Shiba"))
                pageNumText = "";

            Text text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = menuNameHehe + pageNumText;
            text.fontSize = 1;
            text.color = textColors[0];
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            if (longMenu)
                component.position = new Vector3(0.06f, 0f, 0.165f);
            else
                component.position = new Vector3(0.06f, 0f, 0.082f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                fpsObject.color = textColors[0];
                fpsObject.fontSize = 1;
                fpsObject.supportRichText = true;
                fpsObject.fontStyle = FontStyle.Italic;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.28f, 0.02f);
                component2.position = new Vector3(component.position.x, component.position.y, component.position.z - 0.02f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Buttons
            // Disconnect
            if (disconnectButton)
            {
                GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    disconnectbutton.layer = 2;
                }
                UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
                disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectbutton.transform.parent = menu.transform;
                disconnectbutton.transform.rotation = Quaternion.identity;
                disconnectbutton.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
                if (longMenu)
                    disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
                else
                    disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.35f);
                disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                disconnectbutton.AddComponent<Classes.Button>().relatedText = "Disconnect";

                Text discontext = new GameObject
                {
                    transform =
                            {
                                parent = canvasObject.transform
                            }
                }.AddComponent<Text>();
                discontext.text = "Disconnect";
                discontext.font = currentFont;
                discontext.fontSize = 1;
                discontext.color = textColors[0];
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;

                RectTransform rectt = discontext.GetComponent<RectTransform>();
                rectt.localPosition = Vector3.zero;
                rectt.sizeDelta = new Vector2(0.2f, 0.03f);
                if (longMenu)
                    rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
                else
                    rectt.localPosition = new Vector3(0.064f, 0f, 0.13f);
                rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            Text descriptionObject = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            descriptionObject.text = descriptionText;
            descriptionObject.font = currentFont;
            descriptionObject.fontSize = 1;
            descriptionObject.color = textColors[0];
            descriptionObject.alignment = TextAnchor.MiddleCenter;
            descriptionObject.resizeTextForBestFit = true;
            descriptionObject.resizeTextMinSize = 0;

            RectTransform rectt2 = descriptionObject.GetComponent<RectTransform>();
            rectt2.localPosition = Vector3.zero;
            rectt2.sizeDelta = new Vector2(0.3f, 0.04f);
            rectt2.localPosition = new Vector3(0.064f, 0f, -0.2f);
            rectt2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (!longMenu)
                rectt2.localPosition += new Vector3(0f, 0f, 0.092f);

                // Page Buttons
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                if (longMenu)
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                else
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.50f);
                gameObject.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
                gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";

                text = new GameObject
                {
                    transform =
                        {
                            parent = canvasObject.transform
                        }
                }.AddComponent<Text>();
                text.font = currentFont;
                text.text = "<";
                text.fontSize = 1;
                text.color = textColors[0];
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                component.localPosition = new Vector3(0.064f, 0.195f, 0f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.transform.rotation = Quaternion.identity;
                if (longMenu)
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.9f);
                else
                    gameObject.transform.localScale = new Vector3(0.09f, 0.2f, 0.50f);
                gameObject.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
                gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";

                text = new GameObject
                {
                    transform =
                        {
                            parent = canvasObject.transform
                        }
                }.AddComponent<Text>();
                text.font = currentFont;
                text.text = ">";
                text.fontSize = 1;
                text.color = textColors[0];
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                component.localPosition = new Vector3(0.064f, -0.195f, 0f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
            {
                if (longMenu)
                    CreateButton(i * 0.1f, activeButtons[i]);
                else
                    CreateButton(i * 0.1f + 0.20f, activeButtons[i]);
            }
            
            //if (isOutdated)
            //NotifiLib.SendNotification("<color=#570000>MENU IS OUTDATED UPDATE TO </color> [<color=#ff9400>" + GetHttp("") + "</color>] IF YOU DO NOT WANT TO RISK GETTING <color=#570000>BANNED</color>");
        }

        public static void CreateButton(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.GetComponent<Renderer>().material.color = Color.black;
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;
            if (GetIndex("Rainbow Menu").enabled)
            {
                ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
                colorChanger.colorInfo = newBackroundColor;
                colorChanger.Start();
            }

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
                gameObject.GetComponent<Renderer>().material.color = newButtonColors[1];

                if (GetIndex("Rainbow Menu").enabled)
                    gameObject.GetComponent<ColorChanger>().colorInfo = new ExtGradient {colors = GetSolidGradient(newButtonColors[1])};
            }
            else
            {
                text.color = textColors[0];
                gameObject.GetComponent<Renderer>().material.color = newButtonColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            try
            {
                TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
            }
            catch { }

            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }

                if (GetIndex("Bark Menu").enabled /*|| GetIndex("Joystick Menu").enabled*/)
                {
                    menu.transform.transform.position = GorillaTagger.Instance.headCollider.transform.position + new Vector3(0f, 0.025f, 0.55f);
                    menu.transform.rotation = Quaternion.Euler(-90, 100, 0);
                }
            }
            else
            {
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText, bool shouldSave = true)
        {
            int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;

            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }

            if (buttonText == "NextPage")
            {
                pageNumber++;
                if (pageNumber > lastPage)
                {
                    pageNumber = 0;
                }
            }

            if (buttonText == "Disconnect")
                PhotonNetwork.Disconnect();

            ButtonInfo target = GetIndex(buttonText);
            if (target != null)
            {
                if (target.isTogglable)
                {
                    target.enabled = !target.enabled;
                    if (target.enabled)
                    {
                        NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                        if (target.enableMethod != null)
                        {
                            try { target.enableMethod.Invoke(); } catch { }
                        }
                    }
                    else
                    {
                        NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                        if (target.disableMethod != null)
                        {
                            try { target.disableMethod.Invoke(); } catch { }
                        }
                    }
                }
                else
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                    if (target.method != null)
                    {
                        try { target.method.Invoke(); } catch { }
                    }
                }
            }

            if (buttonText != "NextPage" && buttonText != "PreviousPage" && buttonText != "Disconnect")
            {
                if (target.isTogglable && shouldSave && shouldSaveMods)
                    Global.SaveMods();

                descriptionText = target.buttonText + ": " + target.toolTip;
            }

            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Menu.Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

        public static string GetHttp(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = "";
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            return html;
        }

        // Variables
        // Important
        // Objects
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject reference;
        public static GameObject canvasObject;

        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;
        public static Text descriptionObject;
        public static String descriptionText;

        // Data
        public static bool hasLoaded = false;
        public static int pageNumber = 0;
        public static int buttonsType = 0;
        public static float timeTilYouCanClickAgain = 0;
    }
}