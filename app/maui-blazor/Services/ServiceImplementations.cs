// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Threading;
using CommunityToolkit.Maui.Media;

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

    public void Clear() =>
        Preferences.Default.Clear();

    public TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null) =>
        Preferences.Default.Get<TValue>(key, default);

    public string? Key(double index) => default;

    public void RemoveItem(string key) =>
        Preferences.Default.Remove(key);

    public void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null) =>
        Preferences.Default.Set<TValue>(key, value);
}

public class MauiSpeechRecognitionService : ISpeechRecognitionService
{
    private readonly ISpeechToText _speechToText;

    private SpeechRecognitionOperation? _current;

    public MauiSpeechRecognitionService(ISpeechToText speechToText)
    {
        _speechToText = speechToText;
    }

    public void CancelSpeechRecognition(bool isAborted)
    {
        _current?.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        _current?.Dispose();
        return ValueTask.CompletedTask;
    }

    public Task InitializeModuleAsync(bool logModuleDetails = true)
    {
        return Task.CompletedTask;
    }

    public IDisposable RecognizeSpeech(string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEvent>? onError = null, Action? onStarted = null, Action? onEnded = null)
    {
        return _current = new SpeechRecognitionOperation(_speechToText, language, onRecognized, onError, onStarted, onEnded);
    }

    private class SpeechRecognitionOperation : IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ISpeechToText _speechToText;
        private readonly string _language;
        private readonly Action<string> _onRecognized;
        private readonly Action<SpeechRecognitionErrorEvent>? _onError;
        private readonly Action? _onStarted;
        private readonly Action? _onEnded;

        public SpeechRecognitionOperation(ISpeechToText speechToText, string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEvent>? onError, Action? onStarted, Action? onEnded)
        {
            _speechToText = speechToText;
            _language = language;
            _onRecognized = onRecognized;
            _onError = onError;
            _onStarted = onStarted;
            _onEnded = onEnded;

            StartAsync();
        }

        private async void StartAsync()
        {
            _onStarted?.Invoke();

            try
            {
                var isGranted = await _speechToText.RequestPermissions(_cts.Token);
                if (!isGranted)
                {
                    _onError?.Invoke(new SpeechRecognitionErrorEvent("Permissions Error", "Permissions were not granted."));
                    _onEnded?.Invoke();
                    return;
                }

                var culture = CultureInfo.GetCultureInfo(_language);
                var last = "";
                var recognitionResult = await _speechToText.ListenAsync(culture, new Progress<string>(rec => {
                    var current = last;
                    last = rec;
                    // nothing changed, so skip
                    if (rec.Length <= current.Length)
                        return;
                    // new words added, trim and pass just the new words
                    if (rec.Length >= current.Length)
                        rec = rec.Substring(current.Length);
                    _onRecognized?.Invoke(rec);
                }), _cts.Token);

                if (!recognitionResult.IsSuccessful)
                {
                    _onError?.Invoke(new SpeechRecognitionErrorEvent("Unknown Error",
                        $"Unable to recognize speech. Got: '{recognitionResult.Text}'. Ex: {recognitionResult.Exception}"));
                }
            }
            catch (Exception ex)
            {
                _onError?.Invoke(new SpeechRecognitionErrorEvent("Unknown Error", ex.Message));
            }

            _onEnded?.Invoke();
        }

        public void Dispose()
        {
            _cts.Cancel();
        }
    }
}
