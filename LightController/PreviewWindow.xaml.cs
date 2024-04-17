﻿using LightController.Controls;
using LightController.Dmx;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace LightController
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        private const double FixtureItemHeight = 70;
        private const double FixtureItemWidth = 50;
        private readonly Dictionary<int, FixturePreview> fixtures = new Dictionary<int, FixturePreview>();
        private readonly DispatcherTimer dispatcherTimer;

        public PreviewWindow()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Update;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        public void Init(IEnumerable<DmxFixture> fixtures)
        {
            int fixturesPerRow = (int)Math.Max(fixtureScrollBox.ActualWidth / FixtureItemWidth, 1);
            int currentColumn = 0;
            int rows = 0;
            int currentRow = 0;
            StackPanel currentPanel = null;
            foreach(DmxFixture fixture in fixtures)
            {
                currentColumn++;
                if(currentColumn > fixturesPerRow)
                {
                    currentColumn = 1;
                    currentRow++;
                }

                if(currentRow >= rows)
                {
                    fixtureGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(FixtureItemHeight, GridUnitType.Pixel)
                    });
                    currentPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                    };
                    fixtureGrid.Children.Add(currentPanel);
                    Grid.SetRow(currentPanel, currentRow);
                    rows++;
                }

                LightFixtureControl fixtureControl = new LightFixtureControl();
                currentPanel.Children.Add(fixtureControl);
                fixtureControl.FixtureName = fixture.FixtureId.ToString();
                this.fixtures[fixture.FixtureId] = new FixturePreview(fixture.FixtureId, fixtureControl);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            foreach(FixturePreview preview in fixtures.Values)
                preview.Update();
        }

        public void UpdatePreviewColor(int fixtureId, System.Windows.Media.Color rgb, double intensity)
        {
            FixturePreview preview = fixtures[fixtureId];
            preview.Intensity = intensity;
            preview.Color = rgb;
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private class FixturePreview
        {
            public int FixtureId { get; }
            public LightFixtureControl Control { get; }

            private System.Windows.Media.Color color;
            private object colorLock = new object();
            public System.Windows.Media.Color Color
            {
                get
                {
                    lock(colorLock)
                    {
                        return color;
                    }
                }
                set
                {

                    lock (colorLock)
                    {
                        this.color = value;
                    }
                }
            }

            private float intensity;
            public double Intensity
            {
                get
                {
                    return Interlocked.CompareExchange(ref intensity, 0, 0);
                }
                set
                {
                    Interlocked.Exchange(ref intensity, (float)value);
                }
            }


            public FixturePreview(int fixtureId, LightFixtureControl control)
            {
                FixtureId = fixtureId;
                Control = control;
            }

            public void Update()
            {
                float intensity = (float)Intensity;

                var newColor = Color;
                newColor.ScR *= intensity;
                newColor.ScG *= intensity;
                newColor.ScB *= intensity;
                newColor.ScA = 1;

                Control.Color = new SolidColorBrush(newColor);
                Control.Percent = (int)(intensity * 100);
            }
        }
    }
}
