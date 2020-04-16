namespace SyncGitRepos
{
    static class Program
    {
        static void Main()
        {
            GitSync.Init();
            GitSync.PauseAtEnd = true;

            SyncKvackStudioWithGitlab();
            QuickSyncGithub("TopWinPrio");
            QuickSyncGithub("TypedMath");
            QuickSyncGithub("DatePalindrome");

            //QuickSyncVSTS("CodeGenerator");
            //QuickSyncVSTS("CodeGenerator2");
            //QuickSyncVSTS("TimedWebcamShots");
            //QuickSyncVSTS("ConsoleFaceEmotion_test");
            //QuickSyncVSTS("PeopleClassificator");
            //QuickSyncVSTS("ClassRefAndClone");

            GitSync.DoSync();
        }

        private static void SyncKvackStudioWithGitlab()
        {
            var sync = new GitSync
            {
                MainGit = "https://kvackstudio@dev.azure.com/kvackstudio/kvackstudio/_git/kvackstudio",
                MainBranch = "develop",
                MainCommitBranch = "Develop_merged_with_GitLab",
                MirrorGit = "https://gitlab.com/kvack/kvackstudio.git",
                Folder = "GitSync_Kvack",
                Verbose = true,
                PruneMirrorGit = true,
                Quiet = false,
                ShowBatchInfo = true,
                OnlyToPrivate = false,
                DontPushWithMirror = true,
            };
            sync.QueueSync();

        }

        private static void QuickSyncGithub(string project, string branch = "master", bool onlyToPrivate = false)
        {
            var sync = new GitSync
            {
                MainBranch = branch,
                MainGit = "https://marcusmedina.visualstudio.com/" + project + "/_git/" + project,
                MirrorGit = "https://github.com/MarcusMedina/" + project + ".git/",
                Folder = "GitSync_" + project,
                Verbose = true,
                PruneMirrorGit = false,
                Quiet = false,
                ShowBatchInfo = true,
                OnlyToPrivate = onlyToPrivate,
                DontPushWithMirror = false
            };
            sync.QueueSync();
        }
    }
}

