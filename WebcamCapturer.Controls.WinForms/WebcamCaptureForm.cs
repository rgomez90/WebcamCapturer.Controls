using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WebcamCapturer.Core;

namespace WebcamCapturer.Controls.WinForms
{
    public partial class WebcamCaptureForm : Form, IWebcamCaptureView
    {
        private BindingList<string> _supportedFrameSizes = new BindingList<string>();

        private BindingList<string> _videoDevices = new BindingList<string>();

        public WebcamCaptureForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndexChanged += OnDevicesComboBoxSelectedIndexChanged;
            CbVideoResolution.SelectedIndexChanged += OnCbResolutionsSelectedIndexChanged;
            BtnConnect.Click += OnBtnConnectClick;
            BtnDisconnect.Click += OnBtnDisconnectClick;
            BtnSnapshot.Click += OnBtnSnapshotClick;
            BtnSave.Click += OnBtnSaveClick;
            bindingSourceResolutions.DataSource = SupportedFrameSizes;
            bindingSourceVideoSources.DataSource = VideoDevices;
        }

        public event EventHandler Connect;

        public event EventHandler<string> DeviceSelected;

        public event EventHandler Disconnect;

        public event EventHandler<string> ResolutionSelected;

        public event EventHandler SaveSnapShot;

        public event EventHandler SnapShot;

        public Image ActualCamImage
        {
            get => PbCamImage.Image;
            set => PbCamImage.Image = value;
        }

        public string SelectedVideoDevice { get; set; }

        public Image SnapShotImage
        {
            get => pictureBox1.Image;
            set => pictureBox1.Image = value;
        }

        public BindingList<string> SupportedFrameSizes
        {
            get => _supportedFrameSizes;
            set
            {
                _supportedFrameSizes = value;
                CbVideoResolution.Items.Clear();
                foreach (var supportedFrameSize in _supportedFrameSizes)
                {
                    CbVideoResolution.Items.Add(supportedFrameSize);
                }
            }
        }

        public BindingList<string> VideoDevices
        {
            get => _videoDevices;
            set
            {
                _videoDevices = value;
                comboBox1.Items.Clear();
                foreach (var videoDevice in _videoDevices)
                {
                    comboBox1.Items.Add(videoDevice);
                }
            }
        }

        public void EnableConnectionControls(bool enable)
        {
            comboBox1.Enabled = enable;
            CbVideoResolution.Enabled = enable;
            BtnConnect.Enabled = enable;
            BtnDisconnect.Enabled = !enable;
            BtnSnapshot.Enabled = !enable;
        }

        public string GetExportPath()
        {
            throw new NotImplementedException();
        }

        public void Message(string message)
        {
            MessageBox.Show(message);
        }

        private void OnBtnConnectClick(object sender, EventArgs e)
        {
            Connect?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnDisconnectClick(object sender, EventArgs e)
        {
            Disconnect?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnSaveClick(object sender, EventArgs e)
        {
            SaveSnapShot?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnSnapshotClick(object sender, EventArgs e)
        {
            SnapShot?.Invoke(this, EventArgs.Empty);
        }

        private void OnCbResolutionsSelectedIndexChanged(object sender, EventArgs e)
        {
            ResolutionSelected?.Invoke(this, (string)CbVideoResolution.SelectedItem);
        }

        private void OnDevicesComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceSelected?.Invoke(this, (string)comboBox1.SelectedItem);
        }
    }
}
