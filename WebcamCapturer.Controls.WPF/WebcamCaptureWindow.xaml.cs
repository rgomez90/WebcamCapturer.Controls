using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using WebcamCapturer.Core;
using Image = System.Drawing.Image;

namespace WebcamCapturer.Controls.WPF
{
    /// <summary>
    /// Interaction logic for WebcamCaptureWindow.xaml
    /// </summary>
    public partial class WebcamCaptureWindow : Window, IWebcamCaptureView, INotifyPropertyChanged
    {
        private Image _actualCamImage;

        private string _selectedVideoDevice;

        private Image _snapShotImage;

        public WebcamCaptureWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
        }

        public event EventHandler Connect;

        public event EventHandler<string> DeviceSelected;

        public event EventHandler Disconnect;

        public event EventHandler Load;

        public event EventHandler<string> ResolutionSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SaveSnapShot;

        public event EventHandler SnapShot;

        public Image ActualCamImage
        {
            get => _actualCamImage;
            set
            {
                _actualCamImage = value;
                this.Dispatcher.Invoke(() => { ImgActualImage.Source = GetImage(_actualCamImage); });
                OnPropertyChanged();
            }
        }

        public string SelectedVideoDevice
        {
            get => _selectedVideoDevice;
            set
            {
                _selectedVideoDevice = value;
                OnPropertyChanged();
            }
        }

        public Image SnapShotImage
        {
            get => _snapShotImage;
            set
            {
                _snapShotImage = value;
                ImgSnapShotImage.Source = GetImage(_snapShotImage);
                OnPropertyChanged();
            }
        }

        public BindingList<string> SupportedFrameSizes { get; set; } = new BindingList<string>();

        public BindingList<string> VideoDevices { get; set; } = new BindingList<string>();

        public void EnableConnectionControls(bool b)
        {

        }

        public string GetExportPath()
        {
            var dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                return dialog.FileName;
            }

            return null;
        }

        public void Message(string message)
        {
            MessageBox.Show(message);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private BitmapImage GetImage(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
        }

        private void OnBtnConnectClick(object sender, RoutedEventArgs e)
        {
            Connect?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnDisconnectClick(object sender, RoutedEventArgs e)
        {
            Disconnect?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnSaveClick(object sender, RoutedEventArgs e)
        {
            SaveSnapShot?.Invoke(this, EventArgs.Empty);
        }

        private void OnBtnSnapShotClick(object sender, RoutedEventArgs e)
        {
            SnapShot?.Invoke(this, EventArgs.Empty);
        }

        private void OnCbVideoDevicesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceSelected?.Invoke(this, (string)CbVideoDevices.SelectedItem);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Load?.Invoke(this, EventArgs.Empty);
        }

        private void OnCbResolutionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox ?? throw new ArgumentException(nameof(sender));
            ResolutionSelected?.Invoke(this, (string) cb.SelectedItem);
        }
    }
}
