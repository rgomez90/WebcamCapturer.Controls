using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WebcamCapturer.Core
{
    public class WebcamCapturePresenter
    {
        private readonly object _sync = new object();
        private Bitmap _actualFrame;
        private FilterInfoCollection _devices;
        private VideoCaptureDevice _selectedVideoDevice;
        private readonly IWebcamCaptureView _view;

        public WebcamCapturePresenter(IWebcamCaptureView view)
        {
            _view = view;
            view.Load += OnViewLoad;
            view.Connect += OnViewConnect;
            view.DeviceSelected += OnViewDeviceSelected;
            view.SnapShot += OnViewSnapshot;
            view.Disconnect += OnViewDisconnect;
            view.SaveSnapShot += OnViewSaveSnapShot;
            view.ResolutionSelected += OnViewResolutionSelected;
        }

        private void OnViewResolutionSelected(object sender, string e)
        {
            var frameSize = e.Split('x');
            var widht = int.Parse(frameSize[0]);
            var height = int.Parse(frameSize[1]);
            var selectedResolution = _selectedVideoDevice.VideoCapabilities
                .SingleOrDefault(x => x.FrameSize.Height == height && x.FrameSize.Width == widht);
            if (selectedResolution == null)
            {
                _view.Message("Application Error");
            }

            _selectedVideoDevice.VideoResolution = selectedResolution;
        }

        private void OnViewSaveSnapShot(object sender, EventArgs e)
        {
            var path = _view.GetExportPath();
            if (path == null) return;
            try
            {
                _view.SnapShotImage.Save(path);
            }
            catch (Exception exception)
            {
                _view.Message("Error saving image.");
            }
        }

        private void Disconnect()
        {
            _selectedVideoDevice.SignalToStop();
            _selectedVideoDevice.WaitForStop();
            _view.EnableConnectionControls(true);
        }

        private IEnumerable<string> EnumeratedSupportedFrameSizes(string e)
        {
            _selectedVideoDevice = null;
            foreach (FilterInfo filterInfo in _devices)
            {
                if (filterInfo.Name == e)
                {
                    _selectedVideoDevice = new VideoCaptureDevice(filterInfo.MonikerString);
                    break;
                }
            }

            if (_selectedVideoDevice == null)
            {
                yield break;
            }

            var videoCapabilities = _selectedVideoDevice.VideoCapabilities;
            foreach (VideoCapabilities videoCapability in videoCapabilities)
            {
                yield return $"{videoCapability.FrameSize.Width} x {videoCapability.FrameSize.Height}";
            }
        }

        private void OnVideoDeviceNewFrame(object sender, NewFrameEventArgs eventargs)
        {
            lock (_sync)
            {
                Bitmap image = (Bitmap) eventargs.Frame.Clone();
                if (_actualFrame != null)
                {
                    _actualFrame.Dispose();
                    _actualFrame = null;
                }

                _actualFrame = image;
            }

            _view.ActualCamImage = _actualFrame;
        }

        private void OnViewConnect(object sender, EventArgs e)
        {
            if (_view.SelectedVideoDevice == null)
            {
                _view.Message("No device selected!");
                return;
            }

            _view.EnableConnectionControls(false);
            _selectedVideoDevice.NewFrame += OnVideoDeviceNewFrame;
            _selectedVideoDevice.Start();
        }

        private void OnViewDeviceSelected(object sender, string e)
        {
            if (_view.VideoDevices.Count == 0) return;
            _view.SelectedVideoDevice = e;
            _view.SupportedFrameSizes.Clear();
            foreach (var frameSize in EnumeratedSupportedFrameSizes(e))
            {
                _view.SupportedFrameSizes.Add(frameSize);
            }
        }

        private void OnViewDisconnect(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void OnViewLoad(object sender, EventArgs e)
        {
            _devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in _devices)
            {
                _view.VideoDevices.Add(filterInfo.Name);
            }
        }

        private void OnViewSnapshot(object sender, EventArgs e)
        {
            lock (_sync)
            {
                try
                {
                    _view.SnapShotImage = (Image) _actualFrame.Clone();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }
    }
}