﻿using static StupidTemplate.Settings;
using static StupidTemplate.Menu.Main;
using StupidTemplate.Classes;
using UnityEngine;
using GorillaLocomotion;
using UnityEngine.UIElements;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using StupidTemplate.Notifications;
using System;
using System.Runtime.InteropServices;
using Random = UnityEngine.Random;
using static FlagCauldronColorer;

namespace StupidTemplate.Mods
{
    internal class RandomShit
    {
        static GameObject newQuitBox;
        public static void fuckLeaderBoard()
        {
            String[] Names = {"HACKED", "THIS IS MINE", "L LEMMING", "ERROR", "HIDE AWAY", "404", "SEROXEN", "RATTED", "L", "504MINICHEATSONTOPONG", "LEMMING", "PBBV", "STATUE", "DAISY09", "RUN", "ECHO", "Name", "NULL", "gorilla", "???", "HIM", ""};

            int RandomNumber = Random.Range(0, Names.Length);

            Global.SetName(Names[RandomNumber]);
        }

        public static void MakeQuitBoxPlatform()
        {
            GameObject oldStinkyQuitBox = UnityEngine.GameObject.Find("QuitBox");

            if (newQuitBox == null)
            {
                newQuitBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newQuitBox.transform.localScale = oldStinkyQuitBox.transform.localScale + new Vector3(0.10f, 0.10f, 0.10f);
                newQuitBox.transform.position = oldStinkyQuitBox.transform.position;

                ColorChanger colorChanger = newQuitBox.AddComponent<ColorChanger>();
                colorChanger.colorInfo = newBackroundColor;
                colorChanger.Start();
            }
        }

        public static void DeleteQuitBoxPlatform()
        {
            UnityEngine.GameObject.Destroy(newQuitBox);
            newQuitBox = null;
        }

        public static void Nametag()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig != GorillaTagger.Instance.offlineVRRig)
                {
                    string ColorShit = vrrig.playerColor.r * 9f + ", " + vrrig.playerColor.g * 9f + ", " + vrrig.playerColor.b * 9f;

                    vrrig.playerText.resizeTextMaxSize = int.MaxValue;
                    vrrig.playerText.text = RigManager.GetPlayerFromVRRig(vrrig).NickName + "\n" + ColorShit + "\nPlayer Token: " + RigManager.GetPlayerFromVRRig(vrrig).UserId;
                }
            }
        }

        public static void ImpossibleColor()
        {
            PlayerPrefs.SetFloat("redValue", -2147483648);
            PlayerPrefs.SetFloat("greenValue", -2147483648);
            PlayerPrefs.SetFloat("blueValue", -2147483648);
            GorillaTagger.Instance.UpdateColor(-2147483648, -2147483648, -2147483648);
            PlayerPrefs.Save();

            GorillaTagger.Instance.myVRRig.RPC("InitializeNoobMaterial", RpcTarget.All, new object[] { -2147483648, -2147483648, -2147483648, true });
        }
    }
}
