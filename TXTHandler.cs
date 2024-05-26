﻿using UnityEngine;
using BepInEx;
using StupidTemplate.Classes;
using System.Linq;
using StupidTemplate.Mods;
using static StupidTemplate.Settings;
using static StupidTemplate.Menu.Main;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Diagnostics;
using ExitGames.Client.Photon;
using GorillaNetworking;
using static UnityEngine.UI.GridLayoutGroup;
using GorillaLocomotion;

namespace StupidTemplate
{
    internal class TXTHandler
    {
        static void VerifyThing()
        {
            if (!Directory.Exists("504Brandon"))
            {
                Directory.CreateDirectory("504Brandon");
            }
        }

        public static string ReadTXTFile(string name = "coolness")
        {
            VerifyThing();

            if (File.Exists("504Brandon/" + name + ".txt"))
            {
                return File.ReadAllText("504Brandon/" + name + ".txt");
            }
            else
            {
                MakeTXTFile(name);
                return null;
            }
        }

        public static void MakeTXTFile(string name = "coolness", string contents = "", bool shouldOpen = false)
        {
            VerifyThing();
            File.WriteAllText("504Brandon/" + name + ".txt", contents);

            if (shouldOpen)
                OpenTXTFile(name);
        }

        public static void OpenTXTFile(string name = "coolness")
        {
            VerifyThing();

            string filePath = System.IO.Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, "504Brandon/" + name + ".txt");
            filePath = filePath.Split("BepInEx\\")[0] + "504Brandon/" + name + ".txt";
            try
            {
                Process.Start(filePath);
            }
            catch
            {
                UnityEngine.Debug.Log("Could not open process " + filePath);
            }
        }
    }
}