using System.IO;
using JetBrains.Annotations;
using OneOf;
using Tomlyn;
using Tomlyn.Model;
using Tomlyn.Syntax;

namespace AppUtils
{
    [PublicAPI]
    public class AppConfig
    {
        private TomlTable _config;

        private AppConfig(DocumentSyntax validatedSyntaxTree) { _config = validatedSyntaxTree.ToModel(); }

        public static OneOf<AppConfig, DiagnosticsBag> FromConfigPath(string configPath)
        {
            var syntaxTree = Toml.Parse(File.ReadAllText(configPath));

            if (syntaxTree.HasErrors) return syntaxTree.Diagnostics;

            return new AppConfig(syntaxTree);
        }

        public TomlTable GetTable(string name) => (TomlTable) _config[name];
    }
}