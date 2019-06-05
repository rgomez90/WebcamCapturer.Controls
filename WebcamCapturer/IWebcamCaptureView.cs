using System;
using System.ComponentModel;

namespace WebcamCapturer
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
        
        Bitmap ActualCamImage { get; set; }

        string SelectedVideoDevice { get; set; }

        Bitmap SnapShotImage { get; set; }

        BindingList<string> SupportedFrameSizes { get; set; }

        BindingList<string> VideoDevices { get; set; }

        void EnableConnectionControls(bool b);

        string GetExportPath();

        void Message(string message);
    }
}