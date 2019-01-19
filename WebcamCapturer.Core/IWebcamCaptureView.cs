using System;
using System.ComponentModel;
using System.Drawing;

namespace WebcamCapturer.Core
{
    public interface IWebcamCaptureView
    {
        event EventHandler Connect;

        event EventHandler<string> DeviceSelected;

        event EventHandler Disconnect;

        event EventHandler Load;

        event EventHandler<string> ResolutionSelected;

        event EventHandler SaveSnapShot;

        event EventHandler SnapShot;

        Image ActualCamImage { get; set; }

        string SelectedVideoDevice { get; set; }

        Image SnapShotImage { get; set; }

        BindingList<string> SupportedFrameSizes { get; set; }

        BindingList<string> VideoDevices { get; set; }

        void EnableConnectionControls(bool b);

        string GetExportPath();

        void Message(string message);
    }
}