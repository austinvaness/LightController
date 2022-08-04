﻿using LightController.Color;
using LightController.Config;
using LightController.Config.Input;
using LightController.Dmx;
using LightController.Pro;
using MediaToolkit.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace LightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string FfmpegFilePath = @"C:\Bin\ffmpeg.exe";
        private const int UpdateRate = 40;

        private ProPresenter pro;
        private IMediaToolkitService ffmpeg;
        private ConfigFile config;
        private SceneManager sceneManager;
        private DmxProcessor dmx;
        private Timer timer;
        private Stopwatch timerWatch = new Stopwatch();

        public static MainWindow Instance { get; private set; }

        public string ApplicationData { get; }
        public ProPresenter Pro => pro;
        public IMediaToolkitService Ffmpeg => ffmpeg;

        public MainWindow()
        {
            Instance = this;

            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("No /AppData/Local/ folder exists!");
            path = Path.Combine(path, typeof(MainWindow).Assembly.GetName().Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            ApplicationData = path;

            ffmpeg = MediaToolkitService.CreateInstance(FfmpegFilePath);

            config = ConfigFile.Load();

            pro = new ProPresenter(config.ProPresenter);
            dmx = new DmxProcessor(config.Dmx);
            sceneManager = new SceneManager(config.Scenes, config.MidiDevice, config.DefaultScene, dmx);


            // TODO: Update inputs on 10hz (100ms) and Update dmx on 30hz (33ms) (Or just one timer?)
            // https://stackoverflow.com/a/12797382
            timer = new Timer(Update, null, UpdateRate, Timeout.Infinite);
        }

        // For debug
        private void GenerateConfig()
        {
            config = new ConfigFile
            {
                // Config settings
                DefaultScene = "Worship",
                ProPresenter = new ProPresenterConfig()
                {
                    ApiUrl = "http://localhost:1025/v1/",
                    MediaAssetsPath = @"C:\Users\austin.vaness\Documents\ProPresenter\Media\Assets"
                },
                Dmx = new Config.Dmx.DmxConfig()
                {
                    DmxDevice = null,
                    Fixtures = new List<Config.Dmx.DmxDeviceProfile>()
                    {
                        new Config.Dmx.DmxDeviceProfile()
                        {
                            Name = "Spotlight",
                            DmxLength = 5,
                            AddressMapStrings = new[] { "intensity", "red", "green", "blue" }
                        },
                        new Config.Dmx.DmxDeviceProfile()
                        {
                            Name = "RGBW",
                            DmxLength = 4,
                            AddressMapStrings = new[] { "red", "green", "blue", "white" }
                        },
                        new Config.Dmx.DmxDeviceProfile()
                        {
                            Name = "Lightbar",
                            DmxLength = 13,
                            AddressMapStrings = new[] { "red", "green", "blue", "white", "amber", null, "intensity" }
                        },
                    },
                    Addresses = new List<Config.Dmx.DmxDeviceAddress>()
                    {
                        new Config.Dmx.DmxDeviceAddress()
                        {
                            Name = "Spotlight",
                            StartAddress = 1,
                            Count = 5,
                        },
                        new Config.Dmx.DmxDeviceAddress()
                        {
                            Name = "RGBW",
                            StartAddress = 255,
                        },
                        new Config.Dmx.DmxDeviceAddress()
                        {
                            Name = "Lightbar",
                            StartAddress = 30,
                            Count = 14,
                        }
                    }

                },
                Scenes = new List<Scene>()
                {
                    new Scene()
                    {
                        Name = "Worship",
                        MidiNote = new Midi.MidiNote()
                        {
                            Channel = 0,
                            Note = 0,
                        },
                        Inputs = new List<InputBase>()
                        {
                            new ColorInput()
                            {
                                RGB = new ColorRGB(255, 255, 255),
                                FixtureRange = "1-20",
                                IntensityMode = "50%"
                            }
                        }
                    }
                },
            };
            config.Save();
        }

        private void Update(object state)
        {
            timerWatch.Restart();

            sceneManager.Update();
            dmx.Write();

            timer.Change(Math.Max(0, UpdateRate - timerWatch.ElapsedMilliseconds), Timeout.Infinite);
            timerWatch.Stop();
        }

        private async void btnCheckContent_Click(object sender, RoutedEventArgs e)
        {
            /*const int targetMs = 500;
            const double marginTop = 50;
            const double marginLeft = 50;
            const double width = 1;
            const double height = 50;

            Stopwatch sw = new Stopwatch();
            for (int x = 0; x < 300; x++)
            {
                sw.Restart();
                canvas.Children.Clear();

                var status = await pro.AsyncGetTransportStatus(Layer.Presentation);
                if (!status.audio_only && !string.IsNullOrWhiteSpace(status.name) && status.duration > 1)
                {
                    string path = System.IO.Path.Combine(Media, status.name);
                    if (File.Exists(path))
                    {
                        double time = await pro.AsyncGetTransportLayerTime(Layer.Presentation);
                        time = Math.Min(Math.Round(time), status.duration - 1);

                        GetThumbnailOptions options = new GetThumbnailOptions
                        {
                            SeekSpan = TimeSpan.FromSeconds(time),
                            OutputFormat = OutputFormat.Image2,
                            PixelFormat = MediaToolkit.Tasks.PixelFormat.Rgba,
                            FrameSize = new FrameSize(854, 480)
                        };

                        GetThumbnailResult result = await service.ExecuteAsync(new FfTaskGetThumbnail(
                          path,
                          options
                        ));

                        string filePath = @"C:\Users\austi\Desktop\Test\image.jpg";
                        if (!File.Exists(filePath))
                        {
                            try
                            {
                                await File.WriteAllBytesAsync(filePath, result.ThumbnailData);
                            }
                            catch { }
                        }

                        if (result.ThumbnailData.Length > 0)
                        {
                            ColorRGB[] colorData = await Task.Run(() => MediaLibrary.ReadImage(result.ThumbnailData, 14, 1));
                            for (int i = 0; i < colorData.Length; i++)
                            {
                                ColorRGB color = colorData[i];
                                var winColor = System.Windows.Media.Color.FromRgb(color.Red, color.Green, color.Blue);
                                var rect = new Rectangle
                                {
                                    Fill = new SolidColorBrush(winColor),
                                    Width = width,
                                    Height = height,
                                };
                                Canvas.SetTop(rect, marginTop);
                                Canvas.SetLeft(rect, marginLeft + (i * width));
                                canvas.Children.Add(rect);
                            }
                        }
                        label.Content = $"Time: {time}s took {sw.ElapsedMilliseconds}ms";
                    }
                    else
                    {
                        label.Content = "File does not exist!";
                    }
                }
                else
                {
                    label.Content = "Bad response from ProPresenter";
                }

                sw.Stop();
                long ms = targetMs - sw.ElapsedMilliseconds;
                if (ms > 0)
                    await Task.Delay((int)ms);
                break;
            }*/
            
        }
    }
}
