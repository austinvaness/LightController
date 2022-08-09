﻿using LightController.Config;
using LightController.Dmx;
using LightController.Midi;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace LightController
{
    public class SceneManager
    {
        private List<Scene> scenes;
        private Scene activeScene;
        private MidiInput midiDevice;
        private DmxProcessor dmx;
        private System.Windows.Controls.ComboBox sceneList;

        public SceneManager(List<Scene> scenes, string midiDevice, string defaultScene, DmxProcessor dmx, System.Windows.Controls.ComboBox sceneList)
        {
            this.scenes = scenes;
            this.dmx = dmx;
            this.sceneList = sceneList;

            MidiDeviceList midiDevices = new MidiDeviceList();

            if (string.IsNullOrWhiteSpace(midiDevice))
            {
                if(midiDevices.TryGetFirstDevice(out this.midiDevice))
                {
                    this.midiDevice.NoteEvent += MidiDevice_NoteEvent;
                    this.midiDevice.Input.Start();
                }
            }
            else if (midiDevices.TryGetDevice(midiDevice, out this.midiDevice))
            {
                this.midiDevice.NoteEvent += MidiDevice_NoteEvent;
                this.midiDevice.Input.Start();
            }

            foreach (Scene s in scenes)
                s.Init();

            if (!string.IsNullOrWhiteSpace(defaultScene))
            {
                Scene scene = scenes.Find(x => x.Name == defaultScene.Trim());
                if (scene != null)
                {
                    LogFile.Info("Activating scene " + scene.Name);
                    activeScene = scene;
                    UpdateSceneUI(activeScene.Name);
                    dmx.SetInputs(scene.Inputs);
                }
            }

        }

        public Task ActivateSceneAsync()
        {
            if (activeScene != null)
                return activeScene.ActivateAsync();
            return Task.CompletedTask;
        }

        public Task UpdateAsync()
        {
            if (activeScene != null)
                return activeScene.UpdateAsync();
            return Task.CompletedTask;
        }

        private async void MidiDevice_NoteEvent(MidiNote note)
        {
            Scene newScene = scenes.Find(s => s.MidiNote == note);
            if (newScene != null)
            {
                LogFile.Info("Activating scene " + newScene.Name);

                foreach (Scene s in scenes)
                    await s.DeactivateAsync();

                await newScene.ActivateAsync();
                activeScene = newScene;
                UpdateSceneUI(activeScene.Name);
                dmx.SetInputs(newScene.Inputs);
            }
        }

        private void UpdateSceneUI(string name)
        {
            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                for (int i = 0; i < sceneList.Items.Count; i++)
                {
                    if (sceneList.Items[i] as string == name)
                    {   
                        sceneList.SelectedIndex = i;
                    }
                }
            });
        }
    }
}
