using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using AForge.Controls;
using AForge.Video.DirectShow;
using WebcamCapturer.Controls.Core;

namespace WebcamCapturer.Controls.Wpf
{
    public partial class WebcamCaptureWindow : Window//, IWebcamCaptureView
    {
        //    private FilterInfoCollection _videoDevices;
        //    private IEnumerable<string> _supportedFrameSizes;
        //    private System.Drawing.Image _snapShotImage;

        //    public FilterInfoCollection VideoDevices
        //    {
        //        get => _videoDevices;
        //        set
        //        {
        //            _videoDevices = value;
        //            comboBox1.Items.Clear();
        //            foreach (FilterInfo videoDevice in _videoDevices)
        //            {
        //                comboBox1.Items.Add(videoDevice.Name);
        //            }
        //        }
        //    }

        //    public VideoCaptureDevice SelectedVideoDevice { get; set; }

        //    public VideoSourcePlayer VideoSourcePlayer => videoSourcePlayer;

        //    public Image SnapShotImage
        //    {
        //        get => pictureBox1.Image;
        //        set => pictureBox1.Image = value;
        //    }

        //    public IEnumerable<string> SupportedFrameSizes
        //    {
        //        get => _supportedFrameSizes;
        //        set
        //        {
        //            _supportedFrameSizes = value;
        //            CbVideoResolution.Items.Clear();
        //            foreach (var supportedFrameSize in _supportedFrameSizes)
        //            {
        //                CbVideoResolution.Items.Add(supportedFrameSize);
        //            }
        //        }
        //    }

        //    public void EnableConnectionControls(bool enable)
        //    {
        //        comboBox1.Enabled = enable;
        //        CbVideoResolution.Enabled = enable;
        //        BtnConnect.Enabled = enable;
        //        BtnDisconnect.Enabled = !enable;
        //        BtnSnapshot.Enabled = !enable;
        //    }

        //    public event EventHandler<Image> SaveSnapShot;

        //    public event EventHandler Disconnect;
        //    public event EventHandler Connect;
        //    public event EventHandler SnapShot;
        //    public event EventHandler Load;
        //    public event EventHandler<VideoCaptureDevice> DeviceSelected;

        //    public WebcamCaptureWindow()
        //    {
        //        InitializeComponent();
        //        comboBox1.SelectedIndexChanged += OnDevicesComboBoxSelectedIndexChanged;
        //        BtnConnect.Click += OnBtnConnectClick;
        //        BtnDisconnect.Click += OnBtnDisconnectClick;
        //        BtnSnapshot.Click += OnBtnSnapshotClick;
        //        BtnSave.Click += OnBtnSaveClick;
        //    }

        //    private void OnBtnSaveClick(object sender, EventArgs e)
        //    {
        //        SaveSnapShot?.Invoke(this, pictureBox1.Image);
        //    }

        //    private void OnBtnDisconnectClick(object sender, EventArgs e)
        //    {
        //        Disconnect?.Invoke(this, EventArgs.Empty);
        //    }

        //    private void OnBtnSnapshotClick(object sender, EventArgs e)
        //    {
        //        SnapShot?.Invoke(this, EventArgs.Empty);
        //    }

        //    private void OnDevicesComboBoxSelectedIndexChanged(object sender, EventArgs e)
        //    {
        //        DeviceSelected?.Invoke(this, new VideoCaptureDevice(VideoDevices[comboBox1.SelectedIndex].MonikerString));
        //    }

        //    private void OnBtnConnectClick(object sender, EventArgs e)
        //    {
        //        Connect?.Invoke(this, EventArgs.Empty);
        //    }
    }
}

