﻿using LightController.Color;
using LightController.Pro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LightController.Config.Input
{
    [YamlTag("!propresenter_input")]
    public class ProPresenterInput : InputBase
    {
        private ProPresenter pro = null;
        private ProMediaItem media;
        private ColorRGB[] colors;
        private byte maxColorValue;
        private byte minColorValue;
        private int pixelWidth;
        private object colorLock = new object();
        private InputIntensity minIntensity = new InputIntensity();
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private Dictionary<int, int> idToIndex = new Dictionary<int, int>();

        public bool HasMotion { get; set; } = true;

        public string MinIntensity
        {
            get
            {
                return minIntensity.ToString();
            }
            set
            {
                minIntensity = InputIntensity.Parse(value);
            }
        }

        public ProPresenterInput() { }


        public override void Init()
        {
            pro = MainWindow.Instance.Pro;

            int count = 0;
            foreach(int id in FixtureIds.EnumerateValues())
            {
                idToIndex[id] = count;
                count++;
            }
            pixelWidth = count;
        }


        public override async Task StartAsync(Midi.MidiNote note)
        {
            if(cts != null)
                cts.Cancel();

            int? id = null;
            if (note != null && note.Intensity.HasValue && note.Intensity.Value > 0)
                id = note.Intensity.Value;

            // Initialize info about current background

            using (var myCts = new CancellationTokenSource())
            {
                cts = myCts;

                try
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    ProMediaItem newMedia = await pro.GetCurrentMediaAsync(HasMotion, cts.Token, id);
                    media = newMedia;
                    if (!HasMotion)
                    {
                        lock (colorLock)
                        {
                            colors = media.GetData(pixelWidth, 0);
                            maxColorValue = colors.Select(x => x.Max()).Max();
                            minColorValue = colors.Select(x => x.Max()).Min();
                        }
                    }
                    LogFile.Info($"{(HasMotion ? "Media" : "Thumbnail")} generation took {sw.ElapsedMilliseconds}ms");
                }
                catch (HttpRequestException)
                {
                    LogFile.Error("Unable to communicate with ProPresenter");
                }
                catch (OperationCanceledException)
                {
                    LogFile.Info($"Canceled {(HasMotion ? "media" : "thumbnail")} generation");
                }

                if (cts == myCts)
                    cts = null;

            }

        }

        public override async Task UpdateAsync()
        {
            // Update the current color based on the background frame and estimated time
            if (media == null || !HasMotion)
                return;

            double time = await pro.AsyncGetTransportLayerTime(Layer.Presentation);

            lock(colorLock)
            {
                colors = media.GetData(pixelWidth, time);
                maxColorValue = colors.Select(x => x.Max()).Max();
                minColorValue = colors.Select(x => x.Max()).Min();
            }
        }

        public override ColorHSV GetColor(int fixtureId)
        {
            int index = idToIndex[fixtureId];
            ColorHSV result;
            lock(colorLock)
            {
                if (colors == null)
                {
                    result = new ColorHSV(0, 1, 1);
                }
                else 
                {
                    if (index >= colors.Length)
                        index = colors.Length - 1;
                    result = (ColorHSV)colors[index];
                }
            }
            return result;
        }

        public override double GetIntensity(int fixtureid, ColorHSV color)
        {
            // intensity provided by user
            double targetMaxIntensity = intensity.Value ?? 1;
            double targetMinIntensity = minIntensity.Value ?? 0;

            // intensity of the media 
            double maxChannelValue;
            double minChannelValue;
            lock (colorLock)
            {
                maxChannelValue = maxColorValue / 255d;
                minChannelValue = minColorValue / 255d;
            }

            double thisIntensity = color.Value;

            if(minChannelValue == maxChannelValue || targetMinIntensity == targetMaxIntensity)
                return targetMaxIntensity;

            // https://math.stackexchange.com/a/914843
            // Input: [minChannelValue-maxChannelValue]
            // Output: [targetMinIntensity-targetMaxIntensity]
            double target = targetMinIntensity + (((targetMaxIntensity - targetMinIntensity) / (maxChannelValue - minChannelValue)) * (thisIntensity - minChannelValue));
            return target;
        }

    }
}
