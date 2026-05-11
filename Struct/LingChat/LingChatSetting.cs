using LingChat.Enum;

namespace LingChat.Struct.LingChat
{
    public class LingChatSetting
    {
        // API and model settings
        public LingChatSettingEnums.LLM_PROVIDER LlmProvider { get; set; } = LingChatSettingEnums.LLM_PROVIDER.WebLLM;
        public string ChatApiKey { get; set; } = "";
        public string VdApiKey { get; set; } = "";
        public string ChatBaseUrl { get; set; } = "https://api.deepseek.com";
        public string ModelType { get; set; } = "deepseek-v4-flash";
        public string VdBaseUrl { get; set; } = "https://dashscope.aliyuncs.com/compatible-mode/v1";
        public string VdModel { get; set; } = "qwen3.5-flash";
        public double Temperature { get; set; } = 1.0d;
        public double TopP { get; set; } = 0.9d;
        public string OllamaBaseUrl { get; set; } = "http://localhost:11434";
        public string OllamaModel { get; set; } = "llama3";
        public string LmStudioModelType { get; set; } = "unknow";
        public string LmStudioBaseUrl { get; set; } = "http://localhost:1234/v1";
        public string LmStudioApiKey { get; set; } = "lm-studio";
        public string TranslateLlmProvider { get; set; } = "qwen-translate";
        public string TranslateApiKey { get; set; } = "sk-114514";
        public string GeminiApiKey { get; set; } = "sk-114516";
        public string GeminiModelType { get; set; } = "gemini-pro";

        // Conversation settings
        public bool UseRag { get; set; }
        public bool UseTimeSense { get; set; } = true;
        public bool EnableTranslate { get; set; } = true;
        public bool LlmOutputSecLang { get; set; } = true;

        // Persistent memory settings
        public bool UsePersistentMemory { get; set; } = true;
        public int MemoryUpdateInterval { get; set; } = 250;
        public int MemoryRecentWindow { get; set; } = 30;

        // Storage and logging settings
        public string BackendLogDir { get; set; } = "data/logs";
        public string AppLogDir { get; set; } = "data/log";
        public string TempVoiceDir { get; set; } = "ling_chat/data/temp_voice";
        public bool EnableFileLogging { get; set; }
        public string LogFileDirectory { get; set; } = "data/run_logs";
        public bool EnableFrontendLogForwarding { get; set; }
        public string LogLevel { get; set; } = "INFO";
        public bool PrintContext { get; set; } = true;

        // Service endpoints
        public string BackendBindAddr { get; set; } = "0.0.0.0";
        public int BackendPort { get; set; } = 8765;
        public string FrontendBindAddr { get; set; } = "localhost";
        public string EmotionBindAddr { get; set; } = "0.0.0.0";
        public int EmotionPort { get; set; } = 8000;

        // Model paths
        public string EmotionModelPath { get; set; } = @"ling_chat\third_party\emotion_model_19emo";

        // Speech synthesis settings
        public string SimpleVitsApiUrl { get; set; } = "http://localhost:23456";
        public string StyleBertVits2Url { get; set; } = "http://localhost:5000";
        public string Sbv2ApiApiUrl { get; set; } = "http://localhost:3000";
        public string GptSovitsApiUrl { get; set; } = "http://127.0.0.1:9880";
        public string GptSovitsRefAudio { get; set; } = string.Empty;
        public string GptSovitsPromptText { get; set; } = string.Empty;
        public string GptSovitsGptModel { get; set; } = string.Empty;
        public string GptSovitsSovitsModel { get; set; } = string.Empty;
        public string AivisApiKry { get; set; } = string.Empty;
        public string VoiceFormat { get; set; } = "wav";

        // Experimental features
        public bool OpenFrontendApp { get; set; } = true;
        public int MaxProactiveTimes { get; set; } = 1;
        public bool EnableScheduleReminder { get; set; } = true;
        public bool EnableProactiveSystem { get; set; }
        public bool EnableVisualPreception { get; set; } = true;
        public bool EnableTopicCreater { get; set; } = true;
        public bool EnableTodoPreception { get; set; } = true;
        public bool EnableImportantDayReminder { get; set; } = true;
        public int TodoWeight { get; set; } = -1;
        public int TopicWeight { get; set; } = -1;
        public int ScreenWeight { get; set; } = -1;
    }
}
