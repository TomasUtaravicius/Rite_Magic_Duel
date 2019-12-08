/*
 * Advaced Gesture Recognition - Unity Plug-In
 *
 * Copyright (c) 2019 MARUI-PlugIn (inc.)
 * This software is free to use for non-commercial purposes.
 * You may use this software in part or in full for any project
 * that does not pursue financial gain, including free software
 * and projectes completed for evaluation or educational purposes only.
 * Any use for commercial purposes is prohibited.
 * You may not sell or rent any software that includes
 * this software in part or in full, either in it's original form
 * or in altered form.
 * If you wish to use this software in a commercial application,
 * please contact us at support@marui-plugin.com to obtain
 * a commercial license.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 * PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY
 * OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GestureController : MonoBehaviour
{
    public enum Gesture { Sandtimer = 0, Circle, ThunderBolt, Fish, SwishAndFlick, NONE }

    public VRInputModule vRInputModule;
    public TrailController trailController;
    public PhotonView photonView;
    private bool isGestureControllerReady = true;

    // The file from which to load gestures on startup.
    [SerializeField] public string LoadGesturesFile;

    //Reference to player right hand.
    [SerializeField] private GameObject rightControllerReference;

    //Reference to plaeyer head.
    [SerializeField] private GameObject playerHeadReference;

    // File where to save recorded gestures.
    // For example: "Assets/GestureRecognition/my_custom_gestures.dat"
    [SerializeField] public string SaveGesturesFile;

    [SerializeField] public SpellManager spellManager;

    // The gesture recognition object:
    // You can have as many of these as you want simultaneously.
    private GestureRecognition gestureRecognition = new GestureRecognition();

    // The text field to display instructions.
    public Text HUDText;

    // The game object associated with the currently active controller (if any):
    private GameObject active_controller = null;

    // ID of the gesture currently being recorded,
    // or: -1 if not currently recording a new gesture,
    // or: -2 if the AI is currently trying to learn to identify gestures
    // or: -3 if the AI has recently finished learning to identify gestures
    private int recording_gesture = -1;

    // Handle to this object/script instance, so that callbacks from the plug-in arrive at the correct instance.
    private GCHandle me;

    // Initialization:
    private void Start()
    {
        Invoke("LoadTheFile", 0.4f);

        me = GCHandle.Alloc(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            StartTraining();
        }
        if (vRInputModule != null && vRInputModule.rightController.GetHairTriggerUp())
        {
            trailController.TurnOffTrail();
        }

        //If the gesture is already loaded,do not record.
        if (spellManager.bufferedGesture != Gesture.NONE || !isGestureControllerReady)
        {
            return;
        }

        //1 Second cooldown after the gesture was perfomed to disable gesture spamming.

        if (vRInputModule.rightController.GetHairTriggerUp())
        {
            Invoke("ResetGestureAvailability", 1f);
        }

        //Get trigger value of the right controller
        float trigger_right = vRInputModule.rightController.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
        // If the user is not yet dragging (pressing the trigger) on either controller, he hasn't started a gesture yet.
        if (active_controller == null && isGestureControllerReady)
        {
            // If the user presses controller's trigger, we start a new gesture.
            if (trigger_right > 0.3)
            {
                active_controller = rightControllerReference;
            }

            // If we arrive here, the user is not pressing controller's trigger:
            // nothing to do.
            else
            {
                return;
            }
            // If we arrive here: trigger was pressed, so we start the gesture.
            GameObject hmd = playerHeadReference;
            Vector3 hmdPosition = hmd.transform.localPosition;
            Quaternion hmdRotation = hmd.transform.localRotation;
            gestureRecognition.startStroke(hmdPosition, hmdRotation, recording_gesture);

            trailController.TurnOnTrail();
        }

        if (active_controller == null)
        {
            // If we arrive here, the user is pressing neither controller's trigger:
            // nothing to do.
            return;
        }

        // If we arrive here, the user is currently dragging with one of the controllers.
        // Check if the user is still dragging or if he let go of the trigger button.
        if (trigger_right > 0.3 && isGestureControllerReady)
        {
            // The user is still dragging with the controller: continue the gesture.
            Vector3 position = active_controller.transform.localPosition;
            position.z += 0.5f;
            Quaternion rotation = active_controller.transform.localRotation;
            gestureRecognition.contdStroke(position, rotation);

            return;
        }

        //if we arrive here, the user let go of the trigger, ending a gesture.
        active_controller = null;

        //Gesture performing has ended.
        if (spellManager.bufferedGesture == Gesture.NONE)
        {
            String gestureName = "";
            double[] gestureRecogntionResult = gestureRecognition.endStrokeAndGetAllProbabilities();

           
            int gesture_id = -1;

            for (int i = 0; i < gestureRecogntionResult.GetLength(0); i++)
            {
                //gesture enum to string
                gestureName = ((Gesture)i).ToString();
                Debug.Log(gestureName + "  " + gestureRecogntionResult[i]);

                if (gestureRecogntionResult[i] > 0.9)
                {
                    gesture_id = i;
                    break;
                }
            }
            if (gesture_id < 0)
            {
                // Error trying to identify any gesture
                Debug.LogWarning("Gesture failed");
            }
            else
            {
                if (spellManager.canCastSpells)
                {
                    spellManager.SetBufferedGesture((Gesture)gesture_id); //int to enum
                    isGestureControllerReady = false;
                }
            }
        }
    }

    // Callback function to be called by the gesture recognition plug-in during the learning process.
    public static void trainingUpdateCallback(double performance, IntPtr ptr)
    {
        // Get the script/scene object back from metadata.
        GCHandle obj = (GCHandle)ptr;
        GestureController me = (obj.Target as GestureController);
        // Update the performance indicator with the latest estimate.
        Debug.LogError(performance);
    }

    // Callback function to be called by the gesture recognition plug-in when the learning process was finished.
    public static void trainingFinishCallback(double performance, IntPtr ptr)
    {
        Debug.LogError("Gesture training finished");
        // Get the script/scene object back from metadata.
        GCHandle obj = (GCHandle)ptr;
        GestureController me = (obj.Target as GestureController);
        // Update the performance indicator with the latest estimate.
        // Signal that training was finished.
        me.recording_gesture = -3;
        // Save the data to file.
        me.gestureRecognition.saveToFile(me.SaveGesturesFile);
        Debug.LogError("Training finished");
    }

    private void LoadTheFile()
    {
#if UNITY_EDITOR
        gestureRecognition.loadFromFile(LoadGesturesFile);
#else
        gr.loadFromFile(Application.streamingAssetsPath + "/GestureSet5Gestures180Samples.dat");
#endif
    }

    private void StartTraining()
    {
        Debug.Log("Start training");
        gestureRecognition.setTrainingUpdateCallback(trainingUpdateCallback);
        gestureRecognition.setTrainingUpdateCallbackMetadata((IntPtr)me);
        gestureRecognition.setTrainingFinishCallback(trainingFinishCallback);
        gestureRecognition.setTrainingFinishCallbackMetadata((IntPtr)me);
        gestureRecognition.startTraining();
    }

    private void ResetGestureAvailability()
    {
        isGestureControllerReady = true;
    }

}