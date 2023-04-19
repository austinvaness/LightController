﻿using LightController.Config;
using LightController.Dmx;
using LightController.Pro;
using MediaToolkit.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using LightController.Color;

namespace LightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int DmxUpdateRate = 33;
        private const int InputsUpdateRate = 50;
        private const int ErrorTimeout = 5000;

        private ProPresenter pro;
        private IMediaToolkitService ffmpeg;
        private ConfigFile config;
        private SceneManager sceneManager;
        private DmxProcessor dmx;
        private Timer dmxTimer; // Runs on different thread
        private Timer inputsTimer; // Runs on different thread
        private bool inputActivated = false;
        private string customConfig;
        private DebugData debug = new DebugData();

        public static MainWindow Instance { get; private set; }

        public string ApplicationData { get; private set; }
        public ProPresenter Pro => pro;
        public IMediaToolkitService Ffmpeg => ffmpeg;

        public MainWindow()
        {
            Instance = this;

            InitializeComponent();
            DataContext = debug;

            InitAppData();
            InitFfmpeg();

#if DEBUG
            debugPanel.Visibility = Visibility.Visible;
#endif

            CommandLineOptions args = new CommandLineOptions(Environment.GetCommandLineArgs());
            LogFile.Info("Command: " + args.ToString());

            try
            {
                string configFile;
                if (args.TryGetFlagArg("config", 0, out configFile) && File.Exists(configFile))
                    customConfig = configFile;
                else
                    configFile = Path.Combine(ApplicationData, "config.yml");

                config = ConfigFile.Load(configFile);
            }
            catch (Exception e)
            {
                LogFile.Error(e, "An error occurred while reading the config file!");
                ErrorBox.Show("An error occurred while reading the config file, please check your config.");
            }

            pro = new ProPresenter(config.ProPresenter, mediaList);
            dmx = new DmxProcessor(config.Dmx);

            string defaultScene;
            if(!args.TryGetFlagArg("scene", 0, out defaultScene))
                defaultScene = config.DefaultScene;
            sceneManager = new SceneManager(config.Scenes, config.MidiDevice, defaultScene, dmx, config.DefaultTransitionTime, sceneList);

            // Update fixture list
            dmx.AppendToListbox(fixtureList);

            // https://stackoverflow.com/a/12797382
            dmxTimer = new Timer(UpdateDmx, null, DmxUpdateRate, Timeout.Infinite);
            inputsTimer = new Timer(UpdateInputs, null, InputsUpdateRate, Timeout.Infinite);

        }

        private void InitFfmpeg()
        {
            string appLocation = typeof(MainWindow).Assembly.Location;
            if (string.IsNullOrEmpty(appLocation))
                throw new Exception("Unable to find ffmpeg location");
            string ffmpegPath = Path.Combine(Path.GetDirectoryName(appLocation), "ffmpeg.exe");
            if (!File.Exists(ffmpegPath))
                throw new Exception("Unable to find ffmpeg.exe");
            ffmpeg = MediaToolkitService.CreateInstance(ffmpegPath);
        }

        private void InitAppData()
        {
            AssemblyName mainAssemblyName = typeof(MainWindow).Assembly.GetName();
            string appname = mainAssemblyName.Name;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("No /AppData/Local/ folder exists!");
            path = Path.Combine(path, appname);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            ApplicationData = path;

            LogFile.Init(Path.Combine(ApplicationData, "Logs", appname + ".log"));
            if(mainAssemblyName.Version != null)
                LogFile.Info("Started application - v" + mainAssemblyName.Version);
            else
                LogFile.Info("Started application");
        }

        // This runs on a different thread
        private async void UpdateInputs(object state)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                if (!inputActivated)
                {
                    await sceneManager.ActivateSceneAsync();
                    inputActivated = true;
                }

                await sceneManager.UpdateAsync();

                sw.Stop();
#if DEBUG
                double percent = sw.ElapsedMilliseconds / (double)InputsUpdateRate;
                debug.InputUpdateUsage = percent.ToString("P");
#endif
                inputsTimer.Change(Math.Max(0, InputsUpdateRate - sw.ElapsedMilliseconds), Timeout.Infinite);
            }
            catch (Exception ex)
            {
                LogFile.Error(ex, "An error occurred while updating inputs!");
                inputsTimer.Change(ErrorTimeout, Timeout.Infinite);
            }
        }

        // This runs on a different thread
        private void UpdateDmx(object state)
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                dmx.Write();

                sw.Stop();
#if DEBUG
                double percent = sw.ElapsedMilliseconds / (double)DmxUpdateRate;
                debug.DmxUpdateUsage = percent.ToString("P");
#endif
                dmxTimer.Change(Math.Max(0, DmxUpdateRate - sw.ElapsedMilliseconds), Timeout.Infinite);
            }
            catch(Exception ex)
            {
                LogFile.Error(ex, "An error occurred while updating dmx!");
                dmxTimer.Change(ErrorTimeout, Timeout.Infinite);
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            LogFile.Info("Restarting application");
            string currentScene = sceneManager?.ActiveSceneName;
            string fileName = Process.GetCurrentProcess().MainModule.FileName;
            StringBuilder sb = new StringBuilder();

            if (currentScene != null && !currentScene.Contains('"'))
            {
                sb.Append("-scene ");
                if (currentScene.Contains(' '))
                    sb.Append('"').Append(currentScene).Append('"');
                else
                    sb.Append(currentScene);
            }

            if(customConfig != null)
            {
                if (sb.Length > 0)
                    sb.Append(' ');
                sb.Append("-config ");
                if (customConfig.Contains(' '))
                    sb.Append('"').Append(customConfig).Append('"');
                else
                    sb.Append(customConfig);
            }

            if(sb.Length > 0)
                Process.Start(fileName, sb.ToString());
            else
                Process.Start(fileName);
            Process.GetCurrentProcess().Kill();
        }

        private void btnOpenConfig_Click(object sender, RoutedEventArgs e)
        {
            config.Open();
        }

        private void btnLoadConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "YAML files (.yml)|*.yml;*.yaml",
                Multiselect = false,
            };
            if (openFileDialog.ShowDialog() == true && File.Exists(openFileDialog.FileName))
            {
                customConfig = openFileDialog.FileName;
                btnRestart_Click(null, null);
            }
        }

        private void btnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            config.Save();
        }

        private void ListBox_DisableMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Disables selection
            e.Handled = true;
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            string logs = Path.Combine(ApplicationData, "Logs");
            if(Directory.Exists(logs))
                Process.Start("explorer.exe", "\"" + logs + "\"");
        }

        private void DebugDmx_Click(object sender, RoutedEventArgs e)
        {
            dmx.WriteDebug();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (dmx == null)
                return;

            LogFile.Info("Closed application.");

            inputsTimer.Change(Timeout.Infinite, Timeout.Infinite);
            sceneManager.Disable();

            Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#if !DEBUG
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult != MessageBoxResult.Yes)
                e.Cancel = true;
#endif
        }

        public void Shutdown()
        {
            dmx.TurnOff();
            dmx.Write();
        }


        public class DebugData
        {
            public string InputUpdateUsage { get; set; }
            public string DmxUpdateUsage { get; set; }
        }
    }
}
