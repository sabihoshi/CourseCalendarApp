using System.IO;
using JetBrains.Annotations;
using NLog;
using Tomlyn.Syntax;
using static System.Environment;

namespace AppUtils
{
    [PublicAPI]
    public class AppHome
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public AppHome(string ns)
        {
            Ns = ns;

            ShareDir = GetFolderPath(SpecialFolder.LocalApplicationData, SpecialFolderOption.Create);
            NsDir    = Path.Combine(ShareDir, ns);

            if (!Directory.Exists(NsDir)) Directory.CreateDirectory(NsDir);

            ConfigPath = Path.Combine(NsDir, "config.toml");
        }

        public string ConfigPath { get; }

        public string Ns { get; }

        public string NsDir { get; }

        public string ShareDir { get; }

        [CanBeNull]
        public AppConfig GetConfig()
        {
            var result = AppConfig.FromConfigPath(ConfigPath);

            return result.Match(cfg => cfg,
                bag =>
                {
                    foreach (var diag in bag)
                    {
                        Logger.Error(diag.ToString);
                    }

                    return null;
                });
        }

        public bool ConfigPresent() => File.Exists(ConfigPath);

        public void CreateConfig(DocumentSyntax initialConfig = null)
        {
            File.WriteAllText(ConfigPath, (initialConfig?.ToString() ?? "") + "\n");
            Logger.Warn($"Created new config at {ConfigPath}.");
        }
    }
}