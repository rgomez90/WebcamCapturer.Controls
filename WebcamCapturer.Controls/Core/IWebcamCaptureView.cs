using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using AForge.Controls;
using AForge.Video.DirectShow;

namespace WebcamCapturer.Controls.Core
{
    internal interface IWebcamCaptureView
    {
        event EventHandler<Image> SaveSnapShot;
        event EventHandler Disconnect;
        event EventHandler Connect;
        event EventHandler SnapShot;
        event EventHandler Load;

        event EventHandler<VideoCaptureDevice> DeviceSelected;

        FilterInfoCollection VideoDevices { get; set; }
        VideoCaptureDevice SelectedVideoDevice { get; set; }
        VideoSourcePlayer VideoSourcePlayer { get; }
        Image SnapShotImage { get; set; }
        IEnumerable<string> SupportedFrameSizes { get; set; }
        void EnableConnectionControls(bool b);
    }
}