using LingChat.Enum;
using LingChat.Struct;
using LingChat.Struct.LingChat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace LingChat.Manager
{
    internal static class SettingManager
    {
        public static string ConfigFilePath => Path.Combine(AppContext.BaseDirectory, "Config.json");

        public static Setting LoadSettingByFile()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    Setting defaultSetting = CreateDefaultSetting();
                    SaveSetting(defaultSetting);
                    return defaultSetting;
                }

                Setting? setting = JsonConvert.DeserializeObject<Setting>(File.ReadAllText(ConfigFilePath));
                return NormalizeSetting(setting);
            }
            catch
            {
                return CreateDefaultSetting();
            }
        }

        public static void SaveSetting(Setting setting)
        {
            Setting normalized = NormalizeSetting(setting);
            string json = JsonConvert.SerializeObject(normalized, Formatting.Indented);
            File.WriteAllText(ConfigFilePath, json);
        }

        public static LingChatSetting ImportLingChatSettingFromEnv(string envFilePath)
        {
            if (!File.Exists(envFilePath))
            {
                throw new FileNotFoundException("Env file was not found.", envFilePath);
            }

            LingChatSetting setting = new();
            Dictionary<string, string> values = ParseEnvFile(envFilePath);

            if (values.TryGetValue("LLM_PROVIDER", out string? llmProvider))
            {
                setting.LlmProvider = ParseLlmProvider(llmProvider);
            }

            ApplyString(values, "CHAT_API_KEY", value => setting.ChatApiKey = value);
            ApplyString(values, "VD_API_KEY", value => setting.VdApiKey = value);
            ApplyString(values, "CHAT_BASE_URL", value => setting.ChatBaseUrl = value);
            ApplyString(values, "MODEL_TYPE", value => setting.ModelType = value);
            ApplyString(values, "VD_BASE_URL", value => setting.VdBaseUrl = value);
            ApplyString(values, "VD_MODEL", value => setting.VdModel = value);
            ApplyDouble(values, "TEMPERATURE", value => setting.Temperature = value);
            ApplyDouble(values, "TOP_P", value => setting.TopP = value);
            ApplyString(values, "OLLAMA_BASE_URL", value => setting.OllamaBaseUrl = value);
            ApplyString(values, "OLLAMA_MODEL", value => setting.OllamaModel = value);
            ApplyString(values, "LMSTUDIO_MODEL_TYPE", value => setting.LmStudioModelType = value);
            ApplyString(values, "LMSTUDIO_BASE_URL", value => setting.LmStudioBaseUrl = value);
            ApplyString(values, "LMSTUDIO_API_KEY", value => setting.LmStudioApiKey = value);
            ApplyString(values, "TRANSLATE_LLM_PROVIDER", value => setting.TranslateLlmProvider = value);
            ApplyString(values, "TRANSLATE_API_KEY", value => setting.TranslateApiKey = value);
            ApplyString(values, "GEMINI_API_KEY", value => setting.GeminiApiKey = value);
            ApplyString(values, "GEMINI_MODEL_TYPE", value => setting.GeminiModelType = value);

            ApplyBool(values, "USE_RAG", value => setting.UseRag = value);
            ApplyBool(values, "USE_TIME_SENSE", value => setting.UseTimeSense = value);
            ApplyBool(values, "ENABLE_TRANSLATE", value => setting.EnableTranslate = value);
            ApplyBool(values, "LLM_OUTPUT_SEC_LANG", value => setting.LlmOutputSecLang = value);

            ApplyBool(values, "USE_PERSISTENT_MEMORY", value => setting.UsePersistentMemory = value);
            ApplyInt(values, "MEMORY_UPDATE_INTERVAL", value => setting.MemoryUpdateInterval = value);
            ApplyInt(values, "MEMORY_RECENT_WINDOW", value => setting.MemoryRecentWindow = value);

            ApplyString(values, "BACKEND_LOG_DIR", value => setting.BackendLogDir = value);
            ApplyString(values, "APP_LOG_DIR", value => setting.AppLogDir = value);
            ApplyString(values, "TEMP_VOICE_DIR", value => setting.TempVoiceDir = value);
            ApplyBool(values, "ENABLE_FILE_LOGGING", value => setting.EnableFileLogging = value);
            ApplyString(values, "LOG_FILE_DIRECTORY", value => setting.LogFileDirectory = value);
            ApplyBool(values, "ENABLE_FRONTEND_LOG_FORWARDING", value => setting.EnableFrontendLogForwarding = value);
            ApplyString(values, "LOG_LEVEL", value => setting.LogLevel = value);
            ApplyBool(values, "PRINT_CONTEXT", value => setting.PrintContext = value);

            ApplyString(values, "BACKEND_BIND_ADDR", value => setting.BackendBindAddr = value);
            ApplyInt(values, "BACKEND_PORT", value => setting.BackendPort = value);
            ApplyString(values, "FRONTEND_BIND_ADDR", value => setting.FrontendBindAddr = value);
            ApplyString(values, "EMOTION_BIND_ADDR", value => setting.EmotionBindAddr = value);
            ApplyInt(values, "EMOTION_PORT", value => setting.EmotionPort = value);

            ApplyString(values, "EMOTION_MODEL_PATH", value => setting.EmotionModelPath = value);

            ApplyString(values, "SIMPLE_VITS_API_URL", value => setting.SimpleVitsApiUrl = value);
            ApplyString(values, "STYLE_BERT_VITS2_URL", value => setting.StyleBertVits2Url = value);
            ApplyString(values, "SBV2API_API_URL", value => setting.Sbv2ApiApiUrl = value);
            ApplyString(values, "GPT_SOVITS_API_URL", value => setting.GptSovitsApiUrl = value);
            ApplyString(values, "GPT_SOVITS_REF_AUDIO", value => setting.GptSovitsRefAudio = value);
            ApplyString(values, "GPT_SOVITS_PROMPT_TEXT", value => setting.GptSovitsPromptText = value);
            ApplyString(values, "GPT_SOVITS_GPT_MODEL", value => setting.GptSovitsGptModel = value);
            ApplyString(values, "GPT_SOVITS_SOVITS_MODEL", value => setting.GptSovitsSovitsModel = value);
            ApplyString(values, "AIVIS_API_KRY", value => setting.AivisApiKry = value);
            ApplyString(values, "VOICE_FORMAT", value => setting.VoiceFormat = value);

            ApplyBool(values, "OPEN_FRONTEND_APP", value => setting.OpenFrontendApp = value);
            ApplyInt(values, "MAX_PROACTIVE_TIMES", value => setting.MaxProactiveTimes = value);
            ApplyBool(values, "ENABLE_SCHEDULE_REMINDER", value => setting.EnableScheduleReminder = value);
            ApplyBool(values, "ENABLE_PROACTIVE_SYSTEM", value => setting.EnableProactiveSystem = value);
            ApplyBool(values, "ENABLE_VISUAL_PRECEPTION", value => setting.EnableVisualPreception = value);
            ApplyBool(values, "ENABLE_TOPIC_CREATER", value => setting.EnableTopicCreater = value);
            ApplyBool(values, "ENABLE_TODO_PRECEPTION", value => setting.EnableTodoPreception = value);
            ApplyBool(values, "ENABLE_IMPORTANT_DAY_REMINDER", value => setting.EnableImportantDayReminder = value);
            ApplyInt(values, "TODO_WEIGHT", value => setting.TodoWeight = value);
            ApplyInt(values, "TOPIC_WEIGHT", value => setting.TopicWeight = value);
            ApplyInt(values, "SCREEN_WEIGHT", value => setting.ScreenWeight = value);

            return setting;
        }

        private static Setting CreateDefaultSetting()
        {
            return new Setting();
        }

        private static Setting NormalizeSetting(Setting? setting)
        {
            Setting normalized = setting ?? CreateDefaultSetting();
            normalized.GlobalSetting ??= new GlobalSetting();
            normalized.LingChatSetting ??= new LingChatSetting();

            if (string.IsNullOrWhiteSpace(normalized.LingChatInstallPath))
            {
                normalized.LingChatInstallPath = "./LingChatInstall";
            }

            if (string.IsNullOrWhiteSpace(normalized.TTSServicesInstallPath))
            {
                normalized.TTSServicesInstallPath = "./TTSServicesInstall";
            }

            if (string.IsNullOrWhiteSpace(normalized.GlobalSetting.LingChatInstallPath))
            {
                normalized.GlobalSetting.LingChatInstallPath = normalized.LingChatInstallPath;
            }
            else
            {
                normalized.LingChatInstallPath = normalized.GlobalSetting.LingChatInstallPath;
            }

            return normalized;
        }

        private static Dictionary<string, string> ParseEnvFile(string envFilePath)
        {
            Dictionary<string, string> values = new(StringComparer.OrdinalIgnoreCase);
            foreach (string rawLine in File.ReadAllLines(envFilePath))
            {
                string line = rawLine.Trim();
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                {
                    continue;
                }

                int commentIndex = line.IndexOf(" #", StringComparison.Ordinal);
                if (commentIndex >= 0)
                {
                    line = line[..commentIndex].TrimEnd();
                }

                int separatorIndex = line.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                string key = line[..separatorIndex].Trim();
                string value = line[(separatorIndex + 1)..].Trim().Trim('"');
                values[key] = value;
            }

            return values;
        }

        private static LingChatSettingEnums.LLM_PROVIDER ParseLlmProvider(string value)
        {
            return value.Trim().ToLowerInvariant() switch
            {
                "ollama" => LingChatSettingEnums.LLM_PROVIDER.Ollama,
                "lmstudio" => LingChatSettingEnums.LLM_PROVIDER.LmStudio,
                _ => LingChatSettingEnums.LLM_PROVIDER.WebLLM
            };
        }

        private static void ApplyString(Dictionary<string, string> values, string key, Action<string> setter)
        {
            if (values.TryGetValue(key, out string? value))
            {
                setter(value);
            }
        }

        private static void ApplyBool(Dictionary<string, string> values, string key, Action<bool> setter)
        {
            if (values.TryGetValue(key, out string? value) && bool.TryParse(value, out bool parsed))
            {
                setter(parsed);
            }
        }

        private static void ApplyInt(Dictionary<string, string> values, string key, Action<int> setter)
        {
            if (values.TryGetValue(key, out string? value) && int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed))
            {
                setter(parsed);
            }
        }

        private static void ApplyDouble(Dictionary<string, string> values, string key, Action<double> setter)
        {
            if (values.TryGetValue(key, out string? value) && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsed))
            {
                setter(parsed);
            }
        }
    }
}
