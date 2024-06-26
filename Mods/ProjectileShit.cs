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
using StupidTemplate.Menu;
using HarmonyLib;
namespace StupidTemplate.Mods
{
    internal class ProjectileShit
    {
        public static int projectileSpeedCycle = 0;
        public static float projectileSpeed = 0f;
        public static int projColorCycle = 0;
        public static float projDelay = 0f;
        public static float projShootDelay = 0.15f;
        public static int projShootDelayCycle = 0;
        public static Color32 projColor = Color.white;
        public static int projCycle = 0;
        public static VRRig player;
        public static GameObject GunThingie;
        public static void Projectile(string projectileName, Vector3 position, Vector3 velocity, Color color, bool noDelay = false)
        {
            ControllerInputPoller.instance.leftControllerGripFloat = 1f;
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject, 0.1f);
            gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            gameObject.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            gameObject.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            int[] array = new int[] {
                32,
                204,
                231,
                240,
                249,
                252
            };
            gameObject.AddComponent<GorillaSurfaceOverride>().overrideIndex = array[Array.IndexOf(fullProjectileNames, projectileName)];
            gameObject.GetComponent<Renderer>().enabled = false;
            if (Time.time > projDelay)
            {
                try
                {
                    Vector3 velocity2 = GorillaTagger.Instance.GetComponent<Rigidbody>().velocity;
                    string[] array2 = new string[] {
                        "LMACE.",
                        "LMAEX.",
                        "LMAGD.",
                        "LMAHQ.",
                        "LMAIE.",
                        "LMAIO."
                    };
                    SnowballThrowable component = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body/shoulder.L/upper_arm.L/forearm.L/hand.L/palm.01.L/TransferrableItemLeftHand/" + fullProjectileNames[Array.IndexOf(fullProjectileNames, projectileName)] + "LeftAnchor").transform.Find(array2[Array.IndexOf(fullProjectileNames, projectileName)]).GetComponent<SnowballThrowable>(); 
                    Vector3 position2 = component.transform.position;
                    component.randomizeColor = true;
                    component.transform.position = position;
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = velocity;
                    GorillaTagger.Instance.offlineVRRig.LeftThrowableProjectileColor = color;
                    GorillaTagger.Instance.offlineVRRig.RightThrowableProjectileColor = color;
                    GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/EquipmentInteractor").GetComponent<EquipmentInteractor>().ReleaseLeftHand();
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = velocity2;
                    component.transform.position = position2;
                    component.randomizeColor = false;

                    // component.projectilePrefab.tag = "CupidBow_Projectile";

                    SafetyShit.RpcFlush();
                }
                catch { }
                if (projShootDelay > 0f && !noDelay)
                {
                    projDelay = Time.time + projShootDelay;
                }
            }
        }
        public static string[] fullProjectileNames = new string[] {
            "Snowball",
            "WaterBalloon",
            "LavaRock",
            "ThrowableGift",
            "ScienceCandy",
            "FishFood"
        };
        public static void ProjectileSpammer()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Color color;

                if (fullProjectileNames[ProjectileType] == "WaterBalloon")
                    color = colorChangeables[UnityEngine.Random.Range(0, colorChangeablesAmmount)];
                else
                    color = projColor;

                Projectile(fullProjectileNames[ProjectileType], Player.Instance.rightControllerTransform.position, Vector3.down, color, false);
            }
        }
        public static void MiniGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Color color;

                if (fullProjectileNames[ProjectileType] == "WaterBalloon")
                    color = colorChangeables[UnityEngine.Random.Range(0, colorChangeablesAmmount)];
                else
                    color = projColor;

                Projectile(fullProjectileNames[ProjectileType], Player.Instance.rightControllerTransform.position, Player.Instance.rightControllerTransform.forward - Player.Instance.rightControllerTransform.up * 8.5f, color, false);
            }
        }
        public static void ProjectileRain()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Vector3 RandomRainPos = Vector3.zero;

                if (GetIndex("Random Rain Position").enabled)
                    RandomRainPos = UnityEngine.Random.insideUnitSphere * RainRangeMultiplyer;

                Projectile(fullProjectileNames[ProjectileType], GorillaTagger.Instance.offlineVRRig.headMesh.transform.position + (Vector3.up * 2) + RandomRainPos, Vector3.down, Color.blue, false);
            }
        }
        public static void Urine()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Projectile("Snowball", GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, -0.15f, 0f), GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f, new Color32(255, 255, 0, 255), false);
            }
        }
        public static void Feces()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Projectile("FishFood", GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, -0.5f, 0f), new Vector3(0f, -1f, 0f), projColor, false);
            }
        }
        public static void Semen()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Projectile("Snowball", GorillaTagger.Instance.bodyCollider.transform.position + new Vector3(0f, -0.15f, 0f), GorillaTagger.Instance.bodyCollider.transform.forward * 8.33f, new Color32(255, 255, 255, 255), false);
            }
        }
        public static void Vomit()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Projectile("Snowball", GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward * 0.1f + GorillaTagger.Instance.headCollider.transform.up * -0.15f, GorillaTagger.Instance.headCollider.transform.forward * 8.33f, new Color32(0, 255, 0, 255), false);
            }
        }
        public static void ProjectileGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit) && GunThingie == null)
                {
                    GunThingie = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(GunThingie.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(GunThingie.GetComponent<SphereCollider>());
                    GunThingie.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                    ColorChanger colorChanger = GunThingie.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = newBackroundColor;
                    colorChanger.Start();
                }
                GunThingie.transform.position = raycastHit.point;

                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
                {
                    VRRig possibly = raycastHit.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                    {
                        player = possibly;
                    }
                    GunThingie.GetComponent<ColorChanger>().colorInfo = new ExtGradient
                    {
                        colors = new GradientColorKey[] { new GradientColorKey(Color.green, 1f) } //what are you doing...?
                    };
                }
                else
                {
                    GunThingie.GetComponent<ColorChanger>().colorInfo = newBackroundColor;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(GunThingie);
            }

            if (player != null)
            {
                Projectile("Snowball", player.rightHandTransform.position, player.rightHandTransform.forward - (player.rightHandTransform.up * 7.5f), projColor, false);
            }
        }
        public static void FecesGun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                RaycastHit raycastHit;
                if (Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit) && GunThingie == null)
                {
                    GunThingie = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    UnityEngine.Object.Destroy(GunThingie.GetComponent<Rigidbody>());
                    UnityEngine.Object.Destroy(GunThingie.GetComponent<SphereCollider>());
                    GunThingie.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                    ColorChanger colorChanger = GunThingie.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = newBackroundColor;
                    colorChanger.Start();
                }
                GunThingie.transform.position = raycastHit.point;

                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
                {
                    VRRig possibly = raycastHit.collider.GetComponentInParent<VRRig>();
                    if (possibly && possibly != GorillaTagger.Instance.offlineVRRig)
                    {
                        player = possibly;
                    }
                    GunThingie.GetComponent<ColorChanger>().colorInfo = new ExtGradient
                    {
                        colors = new GradientColorKey[] { new GradientColorKey(Color.green, 1f) } //what are you doing...?
                    };
                }
                else
                {
                    GunThingie.GetComponent<ColorChanger>().colorInfo = newBackroundColor;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(GunThingie);
            }

            if (player != null)
            {
                Projectile("FishFood", player.transform.position + new Vector3(0f, -0.3f, 0f), new Vector3(0f, -1f, 0f), projColor, false);
            }
        }
        public static void UngrabableProjectiles()
        {
            foreach (SnowballThrowable snowball in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>())
            {
                if (snowball.IsMine())
                {
                    snowball.OnRelease(null, null);
                }
            }
        }
        public static void SlowProjectiles()
        {
            foreach (SnowballThrowable snowball in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>())
            {
                if (snowball.IsMine())
                {
                    snowball.maxLinSpeed = 0.3f;
                    snowball.maxWristSpeed = 0.3f;
                    snowball.linSpeedMultiplier = 0.3f;
                }
            }
        }
        public static void FastProjectiles()
        {
            foreach (SnowballThrowable snowball in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>())
            {
                if (snowball.IsMine())
                {
                    snowball.maxLinSpeed = 95f;
                    snowball.maxWristSpeed = 95f;
                    snowball.linSpeedMultiplier = 95f;
                }
            }
        }
        public static void UpProjectiles()
        {
            foreach (SnowballThrowable snowball in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>())
            {
                if (snowball.IsMine())
                    snowball.linSpeedMultiplier = float.PositiveInfinity;

                snowball.maxWristSpeed = 75f;
            }
        }
        public static void FixProjectiles() // ngl i just assumed these numbers
        {
            foreach (SnowballThrowable snowball in UnityEngine.Object.FindObjectsOfType<SnowballThrowable>())
            {
                snowball.maxLinSpeed = 10f;
                snowball.maxWristSpeed = 10f;
                snowball.linSpeedMultiplier = 10f;
                snowball.GetComponent<Rigidbody>().velocity = snowball.GetComponent<Rigidbody>().velocity.normalized;
            }
        }

        public static void SnowFloor()
        {
            try
            {
                GameObject.Find("pit ground").GetComponent<GorillaSurfaceOverride>().overrideIndex = 32;
            }
            catch
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> <color=white> SNOW FLOOR FAILED TO WORK ARE YOU IN FOREST?</color>");
                GetIndex("Snow Floor [FOREST]").enabled = false;
            }
        }

        public static void MetalFloor()
        {
            try
            {
                GameObject.Find("pit ground").GetComponent<GorillaSurfaceOverride>().overrideIndex = 18;
            }
            catch
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> <color=white> SNOW FLOOR FAILED TO WORK ARE YOU IN FOREST?</color>");
                GetIndex("Snow Floor [FOREST]").enabled = false;
            }
        }

        public static void DisableSnowFloor()
        {
            GameObject.Find("pit ground").GetComponent<GorillaSurfaceOverride>().overrideIndex = 7;
        }
    }
}