// Copyright (c) Microsoft. All rights reserved.

namespace SharedWebComponents.Services;

// NOTE: These interfaces, types, and methods are here to mimic the patterns used in the Blazor WebAssembly
// app. These come from various 'blazorators' files at https://github.com/IEvangelist/blazorators, but
// are simplified to just the definitions.

public interface ILocalStorageServiceWrapper
{
    double Length { get; }
    void Clear();
    TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null);
    string? Key(double index);
    void RemoveItem(string key);
    void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null);
}

public interface ISessionStorageServiceWrapper
{
    double Length { get; }
    void Clear();
    TValue? GetItem<TValue>(string key, JsonSerializerOptions? options = null);
    string? Key(double index);
    void RemoveItem(string key);
    void SetItem<TValue>(string key, TValue value, JsonSerializerOptions? options = null);
}

public interface ISpeechRecognitionServiceWrapper : IAsyncDisposable
{
    Task InitializeModuleAsync(bool logModuleDetails = true);
    void CancelSpeechRecognition(bool isAborted);
    IDisposable RecognizeSpeech(string language, Action<string> onRecognized, Action<SpeechRecognitionErrorEventWrapper>? onError = null, Action? onStarted = null, Action? onEnded = null);
}

public interface ISpeechSynthesisServiceWrapper
{
    bool Paused { get; }
    bool Pending { get; }
    bool Speaking { get; }
    void Cancel();
    ValueTask<SpeechSynthesisVoiceWrapper[]> GetVoicesAsync();
    void Pause();
    void Resume();
    void Speak(SpeechSynthesisUtteranceWrapper utterance);
}

public class SpeechSynthesisUtteranceWrapper
{
    [JsonPropertyName("lang")]
    public string Lang { get; set; }

    [JsonPropertyName("pitch")]
    public double Pitch { get; set; }

    [JsonPropertyName("rate")]
    public double Rate { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("voice")]
    public SpeechSynthesisVoiceWrapper? Voice { get; set; }

    [JsonPropertyName("volume")]
    public double Volume { get; set; }
}

public record class SpeechRecognitionErrorEventWrapper(
    [property: JsonPropertyName("error")] string Error,
    [property: JsonPropertyName("message")] string Message);

public class SpeechSynthesisVoiceWrapper
{
    [JsonPropertyName("default")]
    public bool Default { get; set; }

    [JsonPropertyName("lang")]
    public string Lang { get; set; }

    [JsonPropertyName("localService")]
    public bool LocalService { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("voiceURI")]
    public string VoiceURI { get; set; }
}

public interface ISpeechSynthesisServiceExtensions
{
    void Speak(
        ISpeechSynthesisServiceWrapper service,
        SpeechSynthesisUtteranceWrapper utterance,
        Action<double> onUtteranceEnded);

    [JSInvokable(nameof(OnUtteranceEnded))]
    void OnUtteranceEnded(
        ISpeechSynthesisServiceWrapper service,
        string text, double elapsedTimeSpokenInMilliseconds);

    void OnVoicesChanged(
        ISpeechSynthesisServiceWrapper service,
        Func<Task> onVoicesChanged);

    [JSInvokable(nameof(VoicesChangedAsync))]
    Task VoicesChangedAsync(
        ISpeechSynthesisServiceWrapper service,
        string guid);

    void UnsubscribeFromVoicesChanged(
        ISpeechSynthesisServiceWrapper service);
}
