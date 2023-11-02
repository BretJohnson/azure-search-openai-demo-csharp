// Copyright (c) Microsoft. All rights reserved.

namespace MauiBlazor.Services;

// NOTE: These service implementations are incomplete. Only a few parts are implemented
// for basic app functionality (for now).

public class MauiSessionStorageService : ISessionStorageServiceWrapper
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


public class MauiSpeechSynthesisService : ISpeechSynthesisServiceWrapper
{
    public bool Paused => false;

    public bool Pending => false;

    public bool Speaking => false;

    public void Cancel()
    {
    }

    public ValueTask<SpeechSynthesisVoiceWrapper[]> GetVoicesAsync()
    {
        return ValueTask.FromResult<SpeechSynthesisVoiceWrapper[]>(null);
    }

    public void Pause()
    {
    }

    public void Resume()
    {
    }

    public void Speak(SpeechSynthesisUtteranceWrapper utterance)
    {
    }
}

public class MauiLocalStorageService : ILocalStorageServiceWrapper
{
    public double Length => 0;

    public void Clear()
    {
    }

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null)
    {
        return Preferences.Default.Get<TValue>(key, default(TValue));
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

public class MauiSpeechRecognitionService : ISpeechRecognitionServiceWrapper
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

    public IDisposable RecognizeSpeech(string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEventWrapper>? onError = null, Action? onStarted = null, Action? onEnded = null)
    {
        return default;
    }
}

public class MauiSpeechSynthesisServiceExtensions : ISpeechSynthesisServiceExtensions
{
    public void OnUtteranceEnded(ISpeechSynthesisServiceWrapper service, string text, double elapsedTimeSpokenInMilliseconds)
    {
        throw new NotImplementedException();
    }

    public void OnVoicesChanged(ISpeechSynthesisServiceWrapper service, Func<Task> onVoicesChanged)
    {
        throw new NotImplementedException();
    }

    public void Speak(ISpeechSynthesisServiceWrapper service, SpeechSynthesisUtteranceWrapper utterance, Action<double> onUtteranceEnded)
    {
        throw new NotImplementedException();
    }

    public void UnsubscribeFromVoicesChanged(ISpeechSynthesisServiceWrapper service)
    {
        throw new NotImplementedException();
    }

    public Task VoicesChangedAsync(ISpeechSynthesisServiceWrapper service, string guid)
    {
        throw new NotImplementedException();
    }
}
