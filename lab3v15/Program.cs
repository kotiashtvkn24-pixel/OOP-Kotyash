using System;
using System.Runtime.CompilerServices;

class VideoRecorder : IDisposable
{
    private string _outputFile;
    private bool _isRecording;
    private bool _isResourceAllocated;
    private bool _disposed = false;

    public string OutputFile
    {
        get { return _outputFile; }
    }

    public bool IsRecording
    {
        get { return _isRecording; }
    }

    public VideoRecorder(string outputFile)
    {
        _outputFile = outputFile;
        _isResourceAllocated = true;
        Console.WriteLine("Ресурс виділено: " + _outputFile);
    }

    public void StartRecording()
    {
        if (_isResourceAllocated && !_isRecording)
        {
            _isRecording = true;
            Console.WriteLine("Запис розпочато: " + _outputFile);
        }
    }

    public void StopRecording()
    {
        if (_isRecording)
        {
            _isRecording = false;
            Console.WriteLine("Запис зупинено: " + _outputFile);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                Console.WriteLine("Звільнення керованих ресурсів");
            }

            if (_isResourceAllocated)
            {
                StopRecording();
                Console.WriteLine("Звільнення некерованого ресурсу: " + _outputFile);
                _isResourceAllocated = false;
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~VideoRecorder()
    {
        Dispose(false);
    }
}

class Program
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    static void CreateWithoutDispose()
    {
        VideoRecorder recorder = new VideoRecorder("video3.mp4");
        recorder.StartRecording();
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("--- Сценарій 1: using ---");
        using (VideoRecorder r1 = new VideoRecorder("video1.mp4"))
        {
            r1.StartRecording();
            r1.StopRecording();
        }

        Console.WriteLine();
        Console.WriteLine("--- Сценарій 2: явний виклик Dispose() ---");
        VideoRecorder r2 = new VideoRecorder("video2.mp4");
        r2.StartRecording();
        r2.Dispose();

        Console.WriteLine();
        Console.WriteLine("--- Сценарій 3: без Dispose(), працює деструктор ---");
        CreateWithoutDispose();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine();
        Console.WriteLine("Кінець програми");
    }
}