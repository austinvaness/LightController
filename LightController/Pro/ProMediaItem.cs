﻿using LightController.Color;
using MediaToolkit.Tasks;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LightController.Pro
{
    [ProtoContract(UseProtoMembersOnly = true)]
    public class ProMediaItem
    {
        private const double FrameInterval = 0.1;

        [ProtoMember(1)]
        private MediaFrame[] data;
        private Dictionary<int, MediaFrame[]> resizedData = new Dictionary<int, MediaFrame[]>();
        private string details = "";

        public double Length => data.Length * FrameInterval;
        public string Name { get; private set; }
        public int? Id { get; private set; }
        public bool HasMotion { get; private set; }

        public ProMediaItem()
        {
        }

        public void SetDetails(string name, int? id, bool hasMotion)
        {
            Name = name;
            Id = id;
            HasMotion = hasMotion;

            StringBuilder sb = new StringBuilder();
            if (Id.HasValue)
                sb.Append(Id.Value).Append(" - ");
            sb.Append(Name);
            if (!HasMotion)
                sb.Append(" (thumbnail)");
            details = sb.ToString();
        }

        public ColorRGB[] GetData(int size, double time)
        {
            MediaFrame[] frames;
            if (!resizedData.TryGetValue(size, out frames))
                frames = resizedData[size] = ResizeData(data, size);

            int index = (int)(time / FrameInterval);
            if (index >= data.Length)
                index = data.Length - 1;
            if(index < 0)
                index = 0;

            return frames[index].Data;
        }

        private MediaFrame[] ResizeData(MediaFrame[] data, int size)
        {
            MediaFrame[] newData = new MediaFrame[data.Length];
            for (int i = 0; i < data.Length; i++)
                newData[i] = data[i].ResizeData(size);
            return newData;
        }

        public static Task<ProMediaItem> GetItemAsync(string mediaFolder, string cacheFolder, string file, double length, IProgress<double> progress, CancellationToken cancelToken)
        {
            Directory.CreateDirectory(cacheFolder);
            string cacheFile = Path.Combine(cacheFolder, file + ".bin");
            if(File.Exists(cacheFile))
            {
                progress.Report(double.NaN);
                return LoadItemAsync(cacheFile, cancelToken);
            }
            return CreateItemAsync(Path.Combine(mediaFolder, file), cacheFile, length, progress, cancelToken);
        }

        private static async Task<ProMediaItem> CreateItemAsync(string mediaPath, string cacheFile, double fileLength, IProgress<double> progress, CancellationToken cancelToken)
        {
            GetThumbnailOptions options = new GetThumbnailOptions
            {
                OutputFormat = OutputFormat.Image2,
                PixelFormat = MediaToolkit.Tasks.PixelFormat.Rgba,
                FrameSize = new FrameSize(854, 480)
            };

            List<MediaFrame> frames = new List<MediaFrame>(); 
            for (double time = 0; time < fileLength || time == 0; time += FrameInterval)
            {
                if (fileLength > 0)
                    progress.Report(time / fileLength);
                else
                    progress.Report(double.NaN);

                options.SeekSpan = TimeSpan.FromSeconds(time);

                GetThumbnailResult thumbnailResult = await MainWindow.Instance.Ffmpeg.ExecuteAsync(new FfTaskGetThumbnail(
                  mediaPath,
                  options
                ));
                
                cancelToken.ThrowIfCancellationRequested();

                if (thumbnailResult.ThumbnailData.Length > 0)
                {
                    MediaFrame frame = await Task.Run(() => MediaFrame.CreateFrame(thumbnailResult.ThumbnailData, time, cancelToken));
                    frames.Add(frame);
                }
            }

            progress.Report(double.NaN);

            cancelToken.ThrowIfCancellationRequested();

            if (frames.Count == 0)
            {
                throw new Exception("Error while reading media file: Unable to get any frames from the media.");
            }

            ProMediaItem result = new ProMediaItem();
            result.data = frames.ToArray();
            using (FileStream stream = File.Create(cacheFile))
            {
                await Task.Run(() => Serializer.Serialize<ProMediaItem>(stream, result));
            }

            return result;
        }

        private static async Task<ProMediaItem> LoadItemAsync(string cacheFile, CancellationToken cancelToken)
        {
            using (FileStream stream = File.OpenRead(cacheFile))
            {
                cancelToken.ThrowIfCancellationRequested();
                return await Task.Run(() => Serializer.Deserialize<ProMediaItem>(stream));
            }
        }

        public override string ToString()
        {
            return details;
        }
    }
}
