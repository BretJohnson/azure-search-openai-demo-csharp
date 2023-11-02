// Copyright (c) Microsoft. All rights reserved.

namespace MauiBlazor.Services;

// NOTE: These service implementations are incomplete. Only a few parts are implemented
// for basic app functionality (for now).

public class MauiSessionStorageService : ISessionStorageService
{
    public double Length => 0;

    public void Clear()
    {
    }

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null)
    {
        return default;
    }

    public string? Key(double index)
    {
        return default;
    }

    public void RemoveItem(string key)
    {
    }

    public void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null)
    {
    }
}

public class MauiSpeechSynthesisService : ISpeechSynthesisService
{
    public bool Paused => throw new NotImplementedException();

    public bool Pending => throw new NotImplementedException();

    public bool Speaking => throw new NotImplementedException();

    public void Cancel()
    {
        throw new NotImplementedException();
    }

    public ValueTask<SpeechSynthesisVoice[]> GetVoicesAsync()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Resume()
    {
        throw new NotImplementedException();
    }

    public void Speak(SpeechSynthesisUtterance utterance)
    {
        throw new NotImplementedException();
    }
}

public class MauiLocalStorageService : ILocalStorageService
{
    public double Length => 0;

    public void Clear()
    {
    }

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null)
    {
        return Preferences.Default.Get<TValue>(key, default);
    }

    public string? Key(double index)
    {
        return default;
    }

    public void RemoveItem(string key)
    {
    }

    public void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null)
    {
        Preferences.Default.Set<TValue>(key, value);
    }
}

public class MauiSpeechRecognitionService : ISpeechRecognitionService
{
    public void CancelSpeechRecognition(bool isAborted)
    {
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public Task InitializeModuleAsync(bool logModuleDetails = true)
    {
        return Task.CompletedTask;
    }

    public IDisposable RecognizeSpeech(string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEvent>? onError = null, Action? onStarted = null, Action? onEnded = null)
    {
        return default;
    }
}
