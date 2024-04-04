﻿using static StupidTemplate.Settings;
using static StupidTemplate.Menu.Main;
using StupidTemplate.Classes;
using UnityEngine;
using GorillaLocomotion;
using UnityEngine.UIElements;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using StupidTemplate.Notifications;
using StupidTemplate.Menu;
using UnityEngine.InputSystem;
using GorillaGameModes;

namespace StupidTemplate.Mods
{
    internal class AdvantageShit
    {
        public static VRRig player;
        public static GameObject GunThingie;
        public static void TagGun()
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
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                    }
                }
            }
            else
            {
                RigShit.fixRig();
                Object.Destroy(GunThingie);
            }

            if (player != null && !GorillaTagger.Instance.offlineVRRig.enabled && PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("INFECTION") && !player.mainSkin.material.name.Contains("fected"))
            {
                var taggingPosition = player.transform.position - new Vector3(0f, 3.7f, 0f);

                GorillaTagger.Instance.offlineVRRig.transform.position = taggingPosition;

                try
                {
                    GorillaTagger.Instance.myVRRig.transform.position = taggingPosition;
                }
                catch
                {
                }

                GorillaLocomotion.Player.Instance.rightControllerTransform.position = player.transform.position;

                SafetyShit.RpcFlush();
            }
            else
            {
                if (player.mainSkin.material.name.Contains("fected") && NotifiLib.PreviousNotifi != "You cant tag somone thats tagged silly")
                    NotifiLib.SendNotification("You cant tag somone thats tagged silly");

                player = null;
            }
        }
        public static void tagAll()
        {
            VRRig rando = RigManager.GetRandomVRRig(false);
            if (PhotonNetwork.CurrentRoom.CustomProperties.ToString().Contains("INFECTION") && !rando.mainSkin.material.name.Contains("fected"))
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = RigManager.GetRandomVRRig(false).transform.position - new Vector3(0f, 3.7f, 0f);
                GorillaLocomotion.Player.Instance.rightControllerTransform.position = rando.transform.position;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}