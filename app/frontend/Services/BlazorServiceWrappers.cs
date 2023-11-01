// Copyright (c) Microsoft. All rights reserved.

namespace ClientApp.Services;

public class LocalStorageServiceImplementation(ILocalStorageService localStorageService) : ILocalStorageServiceWrapper
{
    public double Length => localStorageService.Length;

    public void Clear()
    {
        localStorageService.Clear();
    }

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null)
    {
        return localStorageService.GetItem<TValue>(key, options);
    }

    public string? Key(double index)
    {
        return localStorageService.Key(index);
    }

    public void RemoveItem(string key)
    {
        localStorageService.RemoveItem(key);
    }

    public void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null)
    {
        localStorageService.SetItem<TValue>(key, value, options);
    }
}

public class SessionStorageServiceImplementation(ISessionStorageService sessionStorageService) : ISessionStorageServiceWrapper
{
    public double Length => sessionStorageService.Length;

    public void Clear()
    {
        sessionStorageService.Clear();
    }

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null)
    {
        return sessionStorageService.GetItem<TValue>(key, options);
    }

    public string? Key(double index)
    {
        return sessionStorageService.Key(index);
    }

    public void RemoveItem(string key)
    {
        sessionStorageService.RemoveItem(key);
    }

    public void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null)
    {
        sessionStorageService.SetItem<TValue>(key, value, options);
    }
}

public class SpeechRecognitionServiceImplementation(ISpeechRecognitionService speechRecognitionService) : ISpeechRecognitionServiceWrapper
{
    public void CancelSpeechRecognition(bool isAborted)
    {
        speechRecognitionService.CancelSpeechRecognition(isAborted);
    }

    public ValueTask DisposeAsync()
    {
        return speechRecognitionService.DisposeAsync();
    }

    public Task InitializeModuleAsync(bool logModuleDetails = true)
    {
        return speechRecognitionService.InitializeModuleAsync(logModuleDetails);
    }

    public IDisposable RecognizeSpeech(string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEventWrapper>? onError = null, Action? onStarted = null, Action? onEnded = null)
    {
        return speechRecognitionService.RecognizeSpeech(language, onRecognized, ev => { if (onError is not null) { onError(new SpeechRecognitionErrorEventWrapper(ev.Error, ev.Message)); } }, onStarted, onEnded);
    }
}

public class SpeechSynthesisServiceImplementation(ISpeechSynthesisService speechSynthesisService) : ISpeechSynthesisServiceWrapper
{
    public bool Paused => speechSynthesisService.Paused;

    public bool Pending => speechSynthesisService.Pending;

    public bool Speaking => speechSynthesisService.Speaking;

    public void Cancel()
    {
        speechSynthesisService.Cancel();
    }

    public async ValueTask<SpeechSynthesisVoiceWrapper[]> GetVoicesAsync()
    {
        // TODO: Eilon. Implement this properly
        return
            (await speechSynthesisService
                .GetVoicesAsync())
                .Select(v => new SpeechSynthesisVoiceWrapper { })
                .ToArray();
    }

    public void Pause()
    {
        speechSynthesisService.Pause();
    }

    public void Resume()
    {
        speechSynthesisService.Resume();
    }

    public void Speak(SpeechSynthesisUtteranceWrapper utterance)
    {
        speechSynthesisService.Speak(
            new SpeechSynthesisUtterance
            {
                Lang = utterance.Lang,
                Pitch = utterance.Pitch,
                Rate = utterance.Rate,
                Text = utterance.Text,
                Voice = utterance.Voice is null ? null : new SpeechSynthesisVoice
                {
                    Default = utterance.Voice.Default,
                    Lang = utterance.Voice.Lang,
                    LocalService = utterance.Voice.LocalService,
                    Name = utterance.Voice.Name
                },
                Volume = utterance.Volume
            });
    }
}

public class SpeechSynthesisServiceExtensionsImplementation : ISpeechSynthesisServiceExtensions
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
