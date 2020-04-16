namespace SyncGitRepos
{
    static class Program
    {
        static void Main()
        {
            var sync = new GitSync
            {
                MainGit = "https://kvackstudio@dev.azure.com/kvackstudio/kvackstudio/_git/kvackstudio",
                MainBranch = "Develop",
                MirrorGit = "https://gitlab.com/kvack/kvackstudio.git",
                MirrorBranch = "develop",
                Folder = "GitSync_Kvack",
                Verbose = true,
                PauseAtEnd = true,
                PruneMirrorGit = true,
                Quiet = false,
                ShowBatchInfo = true,
                OnlyToPrivate = false
            };
            sync.Init();
            sync.QueueSync();

            QuickSyncGithub("TopWinPrio");
            QuickSyncGithub("TypedMath");

            //QuickSyncVSTS("CodeGenerator");
            //QuickSyncVSTS("CodeGenerator2");
            //QuickSyncVSTS("TimedWebcamShots");
            //QuickSyncVSTS("ConsoleFaceEmotion_test");
            //QuickSyncVSTS("PeopleClassificator");
            //QuickSyncVSTS("ClassRefAndClone");

            sync.DoSync();
        }

        private static void QuickSyncGithub(string project, string branch = "master", bool onlyToPrivate = false)
        {
            var sync = new GitSync
            {
                MainBranch = branch,
                MirrorBranch = branch,
                MainGit = "https://marcusmedina.visualstudio.com/" + project + "/_git/" + project,
                MirrorGit = "https://github.com/MarcusMedina/" + project + ".git/",
                Folder = "GitSync_" + project,
                Verbose = true,
                PauseAtEnd = false,
                PruneMirrorGit = false,
                Quiet = false,
                ShowBatchInfo = true,
                OnlyToPrivate = onlyToPrivate
            };
            sync.QueueSync();
        }
    }
}

