# WebcamCapturer.Controls

Provide some controls for WinForms and WPF to show/record/snapshot video from a webcam using [AForge.NET](https://github.com/andrewkirillov/AForge.NET).

## Installation

Install using Nuget `Install-Package WebcamCapturer.Core` or download [latest release]() build it yourself!

If you also want to use the built-in control, you also need to install:

* `Install-Package WebcamCapturer.Controls.WinForms` if working with WinForms
* `Install-Package WebcamCapturer.Controls.WPF` if working with WPF

## Usage

For now the library exposes an `IWebcamCapturerView` interface and a `WebcamCapturerPresenter`, both reside in `WebcamCapturer.Core`.

If you use the built-in controls, instantiate a `new WebcamCapturerPresenter`,  inject the correct view (Winforms or WPF) to the presenter.

If you want to use your own form, your control has to implement the `IWebcamCapturerView`. 
This way it can be injected into the presenter.

### WinForms

```csharp
var view = new WebcamCaptureForm();
var presenter = new WebcamCapturePresenter(view);
presenter.Show();
```

### WPF

```csharp
var view = new WebcamCapturerWindow()
var presenter = new WebcamCapturePresenter(view);
presenter.Show();
```
