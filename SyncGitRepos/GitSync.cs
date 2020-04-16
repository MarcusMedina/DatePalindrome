using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SyncGitRepos
{
    public class GitSync
    {
        bool _quiet;
        bool _verbose;
        public bool PruneMirrorGit { get; set; } = false;

        public bool Quiet
        {
            get => _quiet;
            set => SetQuiet(value);
        }

        public bool ShowBatchInfo { get; set; } = true;

        public bool Verbose
        {
            get => _verbose;
            set => SetVerbose(value);
        }

        public string Name { get; set; } = "Marcus Medina";
        public string Email { get; set; } = "coder@marcusmedina.pro";
        public string MainBranch { get; set; } = "Master";
        public string MirrorBranch { get; set; } = "Master";

        public string Folder { get; set; } = "GitSync";

        public bool PauseAtEnd { get; set; } = false;

        public string MainGit { get; set; } = "https://marcusmedina.visualstudio.com/TestGit/_git/HellWorld";

        public string MirrorGit { get; set; } = "https://github.com/MarcusMedina/Test.git";
        public bool OnlyToPrivate { get; set; }

        string ExtraParameters()
        {
            var retVal = "";
            retVal += Verbose ? " --verbose" : "";
            retVal += Quiet ? " --quiet" : "";
            return retVal;
        }

        string Prune()
        {
            var retVal = "";
            retVal += PruneMirrorGit ? " --prune" : "";
            return retVal;
        }

        internal void DoSync()
        {
            if (PauseAtEnd)
            {
                BatchScript += "\r\npause";
            }

            File.WriteAllText("cmd.cmd", BatchScript);
            Process.Start("cmd.cmd");
        }

        void SetQuiet(bool value)
        {
            _quiet = value;
            if (_quiet)
            {
                _verbose = false;
            }
        }

        void SetVerbose(bool value)
        {
            _verbose = value;
            if (_verbose)
            {
                _quiet = false;
            }
        }

        public void QueueSync()
        {
            // BuildMyString.com generated code. Please enjoy your string responsibly.

            var sb = new StringBuilder();

            sb.AppendLine("@echo off");
            sb.AppendLine("mkdir %Folder%");
            sb.AppendLine("cd %Folder%");
            sb.AppendLine("git init");
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.Append("echo - Config ").AppendLine(Email);
                sb.AppendLine("echo -----------------------------------------------------");
            }

            sb.Append("git config user.email \"").Append(Email).AppendLine("\"");
            sb.Append("git config user.name \"").Append(Name).AppendLine("\"");
            sb.AppendLine("git remote add origin %MainGit%");
            sb.AppendLine("git remote add upstream %MirrorGit%");
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Pull                                              -");
                sb.AppendLine("echo -----------------------------------------------------");
            }

            sb.Append("git pull %MirrorGit% %MirrorBranch% --allow-unrelated-histories").AppendLine(ExtraParameters());
            sb.Append("git pull %MainGit% %MainBranch% --allow-unrelated-histories").AppendLine(ExtraParameters());
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Commit                                            -");
                sb.AppendLine("echo -----------------------------------------------------");
            }

            sb.Append("git checkout %MainBranch% ").AppendLine();
            sb.Append("git commit -a -m \"Automatic commit ").Append(DateTime.Now.ToString("yyyy-MM-dd h:mm tt")).Append("\"").AppendLine(ExtraParameters());
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Push                                              -");
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo Pushing to Main Git");
            }

            sb.Append("git push --set-upstream %MainGit% %MainBranch%").AppendLine(ExtraParameters());

            if (!OnlyToPrivate)
            {
                if (ShowBatchInfo)
                {
                    sb.AppendLine("echo Pushing to Mirror Git");
                }
                sb.Append("git push --mirror %MirrorGit%").Append(ExtraParameters()).AppendLine(Prune());
            }
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Done                                              -");
                sb.AppendLine("echo -----------------------------------------------------");
            }

            sb.AppendLine("cd ..");
            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Cleaning up folder                                -");
                sb.AppendLine("echo -----------------------------------------------------");
            }
            sb.AppendLine("rmdir %Folder% /Q /S");
            sb.AppendLine();

            var script = sb.ToString().
                Replace("%Folder%", Folder).
                Replace("%MainGit%", MainGit).
                Replace("%MirrorGit%", MirrorGit).
                Replace("%MainBranch%", MainBranch).
                Replace("%MirrorBranch%", MirrorBranch)
                ;

            BatchScript += script;
        }

        public void Init()
        {
            BatchScript = "";
        }

        private static string BatchScript = "";
    }
}