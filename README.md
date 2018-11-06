# WebcamCapturer.Controls

Provide some controls for WinForms and WPF to show/record/snapshot video from a webcam using [AForge.NET](https://github.com/andrewkirillov/AForge.NET).

## Installation

Download [latest release]() or download source and build it yourself!

## Usage

For now the library exposes an `IWebcamCapturerView` interface and a `WebcamCapturerPresenter`.
Just create a `new WebcamCapturerPresenter` and inject the correct view (Winforms or WPF) to the presenter.

### WinForms

```csharp
var presenter = new WebcamCapturePresenter(new WebcamCaptureForm());
presenter.Show();
```

### WPF

```csharp
var presenter = new WebcamCapturePresenter(new WebcamCapturerWindow());
presenter.Show();
```