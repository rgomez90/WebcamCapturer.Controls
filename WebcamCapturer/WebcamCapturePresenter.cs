using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WebcamCapturer.Core
{
    public class WebcamCapturePresenter
    {
        private readonly object _sync = new object();
        private Bitmap _actualFrame;
        private VideoCapture _videoCapture;
        private readonly IWebcamCaptureView _view;

        public WebcamCapturePresenter(IWebcamCaptureView view)
        {
            _view = view;
            _videoCapture=new VideoCapture(-1);
            _videoCapture.AutoFocus = true;

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
            _videoCapture.Release();
            _view.EnableConnectionControls(true);
        }

        

        private void OnViewConnect(object sender, EventArgs e)
        {
            if (_view.SelectedVideoDevice == null)
            {
                _view.Message("No device selected!");
                return;
            }

            _view.EnableConnectionControls(false);
            using (Mat image = new Mat()) // Frame image buffer
            {
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (true)
                {
                    _videoCapture.Read(image); // same as cvQueryFrame
                    if (image.Empty()) break;

                    NewFrame?.Invoke(this, image.ToBitmap());
                    Cv2.WaitKey(30);
                }
            }
        }

        public event EventHandler<Bitmap> NewFrame; 

        private void OnViewDeviceSelected(object sender, string e)
        {
            if (_view.VideoDevices.Count == 0) return;
            _view.SelectedVideoDevice = e;
            _view.SupportedFrameSizes.Clear();
        }

        private void OnViewDisconnect(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void OnViewLoad(object sender, EventArgs e)
        {
       
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