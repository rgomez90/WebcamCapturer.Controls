using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using WebcamCapturer.Controls.Core;

namespace WebcamCapturer.Controls.WinForms
{
    public class WebcamCapturePresenter
    {
        private IWebcamCaptureView _view;

        public WebcamCapturePresenter(IWebcamCaptureView view)
        {
            _view = view;
            view.Load += OnViewLoad;
            view.Connect += OnViewConnect;
            view.DeviceSelected += OnViewDeviceSelected;
            view.SnapShot += OnViewSnapshot;
            view.Disconnect += OnViewDisconnect;
        }

        private void OnViewDisconnect(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void OnViewSnapshot(object sender, EventArgs e)
        {
            var snapShot = _view.VideoSourcePlayer.GetCurrentVideoFrame();
            _view.SnapShotImage = snapShot;
        }

        private void OnViewLoad(object sender, EventArgs e)
        {
            _view.VideoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        private void OnViewDeviceSelected(object sender, VideoCaptureDevice e)
        {
            if (_view.VideoDevices.Count == 0) return;
            _view.SelectedVideoDevice = e;
            _view.SupportedFrameSizes = EnumeratedSupportedFrameSizes(e);
        }

        private static IEnumerable<string> EnumeratedSupportedFrameSizes(VideoCaptureDevice videoDevice)
        {
            var videoCapabilities = videoDevice.VideoCapabilities;
            foreach (VideoCapabilities videoCapability in videoCapabilities)
            {
                yield return $"{videoCapability.FrameSize.Width} x {videoCapability.FrameSize.Height}";
            }
        }

        private void OnViewConnect(object sender, EventArgs e)
        {
            if (_view.SelectedVideoDevice == null)
            {
                MessageBox.Show("No device selected!");
                return;
            }
            _view.EnableConnectionControls(false);
            _view.VideoSourcePlayer.VideoSource = _view.SelectedVideoDevice;
            _view.VideoSourcePlayer.Start();
        }

        private void Disconnect()
        {
            _view.VideoSourcePlayer.SignalToStop();
            _view.VideoSourcePlayer.WaitForStop();
            _view.VideoSourcePlayer.VideoSource = null;
            _view.EnableConnectionControls(true);
        }
    }
}
