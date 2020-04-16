namespace SyncGitRepos
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="GitSync" />.
    /// </summary>
    public class GitSync
    {
        /// <summary>
        /// Defines the _quiet.
        /// </summary>
        internal bool _quiet;

        /// <summary>
        /// Defines the _verbose.
        /// </summary>
        internal bool _verbose;

        /// <summary>
        /// Gets or sets a value indicating whether PruneMirrorGit.
        /// </summary>
        public bool PruneMirrorGit { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether Quiet.
        /// </summary>
        public bool Quiet { get => _quiet; set => SetQuiet(value); }

        /// <summary>
        /// Gets or sets a value indicating whether ShowBatchInfo.
        /// </summary>
        public bool ShowBatchInfo { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether Verbose.
        /// </summary>
        public bool Verbose { get => _verbose; set => SetVerbose(value); }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; } = "Marcus Medina";

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; } = "coder@marcusmedina.pro";

        /// <summary>
        /// Gets or sets the MainBranch.
        /// </summary>
        public string MainBranch { get; set; } = "Master";

        /// <summary>
        /// Gets or sets the Folder.
        /// </summary>
        public string Folder { get; set; } = "GitSyncher";

        /// <summary>
        /// Gets or sets a value indicating whether PauseAtEnd.
        /// </summary>
        public static bool PauseAtEnd { get; set; } = false;

        /// <summary>
        /// Gets or sets the MainGit.
        /// </summary>
        public string MainGit { get; set; } = "https://marcusmedina.visualstudio.com/TestGit/_git/HellWorld";

        /// <summary>
        /// Gets or sets the MirrorGit.
        /// </summary>
        public string MirrorGit { get; set; } = "https://github.com/MarcusMedina/Test.git";

        /// <summary>
        /// Gets or sets a value indicating whether OnlyToPrivate.
        /// </summary>
        public bool OnlyToPrivate { get; set; }

        /// <summary>
        /// Gets or sets the main commit branch.
        /// </summary>
        /// <value>
        /// The main commit branch.
        /// </value>
        public string MainCommitBranch { get; set; } = "Merged_with_mirror";
        public bool DontPushWithMirror { get; set; }

        /// <summary>
        /// The ExtraParameters.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        internal string ExtraParameters()
        {
            var retVal = "";
            retVal += Verbose ? " --verbose" : "";
            retVal += Quiet ? " --quiet" : "";
            return retVal;
        }

        /// <summary>
        /// The Prune.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        internal string Prune()
        {
            var retVal = "";
            retVal += PruneMirrorGit ? " --prune" : "";
            return retVal;
        }

        /// <summary>
        /// The DoSync.
        /// </summary>
        internal static void DoSync()
        {
            if (PauseAtEnd)
            {
                BatchScript += "\r\npause";
            }

            File.WriteAllText("cmd.cmd", BatchScript);
            Process.Start("cmd.cmd");
        }

        /// <summary>
        /// The SetQuiet.
        /// </summary>
        /// <param name="value">The value<see cref="bool"/>.</param>
        internal void SetQuiet(bool value)
        {
            _quiet = value;
            if (_quiet)
            {
                _verbose = false;
            }
        }

        /// <summary>
        /// The SetVerbose.
        /// </summary>
        /// <param name="value">The value<see cref="bool"/>.</param>
        internal void SetVerbose(bool value)
        {
            _verbose = value;
            if (_verbose)
            {
                _quiet = false;
            }
        }

        /// <summary>
        /// The QueueSync.
        /// </summary>
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
            sb.AppendLine("git config merge.commit no");
            sb.AppendLine("git config merge.ff no");

            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Fetch                                             -");
                sb.AppendLine("echo -----------------------------------------------------");
            }
            sb.Append("git fetch ").AppendLine();
            sb.Append("git checkout %MainBranch% ").AppendLine();

            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Pull                                              -");
                sb.AppendLine("echo -----------------------------------------------------");
            }
            sb.Append("git pull %MirrorGit% %MainBranch% --allow-unrelated-histories").AppendLine(ExtraParameters());
            sb.Append("git pull %MainGit% %MainBranch% -Xours --allow-unrelated-histories").AppendLine(ExtraParameters());

            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Add new changes                                   -");
                sb.AppendLine("echo -----------------------------------------------------");
            }
            sb.Append("git add .").AppendLine();

            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Commit                                            -");
                sb.AppendLine("echo -----------------------------------------------------");
            }
            sb.Append("git commit -a -m \"Automatic commit ").Append(DateTime.Now.ToString("yyyy-MM-dd h:mm tt")).Append("\"").AppendLine(ExtraParameters());

            if (!OnlyToPrivate)
            {
                if (ShowBatchInfo)
                {
                    sb.AppendLine("echo Pushing to Mirror Git");
                }
                if (DontPushWithMirror)
                {
                    sb.Append("git push %MirrorGit%").Append(ExtraParameters()).AppendLine(Prune());
                }
                else
                {
                    sb.Append("git push --mirror %MirrorGit%").Append(ExtraParameters()).AppendLine(Prune());
                }
            }

            if (ShowBatchInfo)
            {
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo - Push                                              -");
                sb.AppendLine("echo -----------------------------------------------------");
                sb.AppendLine("echo Pushing to Main Git");
            }
            sb.Append("git checkout %MainCommitBranch% ").AppendLine();
            sb.Append("git push --set-upstream %MainGit% %MainCommitBranch% ").AppendLine(ExtraParameters());

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
                Replace("%MainBranch%", MainBranch)
                ;

            BatchScript += script;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public static void Init()
        {
            BatchScript = "";
        }

        /// <summary>
        /// Defines the BatchScript.
        /// </summary>
        private static string BatchScript = "";
    }
}
