using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Files;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Files
{
    [PublicAPI]
    public class Folder : IPrettyPrint
    {
        private readonly Api _api;

        internal Folder(Api api, FolderModel model)
        {
            _api           = api;
            ContextType    = model.ContextType;
            ContextId      = model.ContextId;
            FilesCount     = model.FilesCount;
            Position       = model.Position;
            UpdatedAt      = model.UpdatedAt;
            FoldersUrl     = model.FoldersUrl;
            FilesUrl       = model.FilesUrl;
            FullName       = model.FullName;
            LockAt         = model.LockAt;
            Id             = model.Id;
            FoldersCount   = model.FoldersCount;
            Name           = model.Name;
            ParentFolderId = model.ParentFolderId;
            CreatedAt      = model.CreatedAt;
            UnlockAt       = model.UnlockAt;
            Hidden         = model.Hidden;
            HiddenForUser  = model.HiddenForUser;
            Locked         = model.Locked;
            LockedForUser  = model.LockedForUser;
            ForSubmissions = model.ForSubmissions;
        }

        public ulong Id { get; }

        public bool? ForSubmissions { get; }

        public bool? Hidden { get; }

        public bool? HiddenForUser { get; }

        public bool? Locked { get; }

        public bool? LockedForUser { get; }

        public DateTime CreatedAt { get; }

        public DateTime UpdatedAt { get; }

        public DateTime? LockAt { get; }

        public DateTime? UnlockAt { get; }

        public int? Position { get; }

        public string ContextType { get; }

        public string FilesUrl { get; }

        public string FoldersUrl { get; }

        public string FullName { get; }

        public string Name { get; }

        public uint FilesCount { get; }

        public uint FoldersCount { get; }

        public ulong ContextId { get; }

        public ulong? ParentFolderId { get; }

        public string ToPrettyString() => "Folder {" +
            ($"\n{nameof(ContextType)}: {ContextType}," +
                $"\n{nameof(ContextId)}: {ContextId}," +
                $"\n{nameof(FilesCount)}: {FilesCount}," +
                $"\n{nameof(Position)}: {Position}," +
                $"\n{nameof(UpdatedAt)}: {UpdatedAt}," +
                $"\n{nameof(FoldersUrl)}: {FoldersUrl}," +
                $"\n{nameof(FilesUrl)}: {FilesUrl}," +
                $"\n{nameof(FullName)}: {FullName}," +
                $"\n{nameof(LockAt)}: {LockAt}," +
                $"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(FoldersCount)}: {FoldersCount}," +
                $"\n{nameof(Name)}: {Name}," +
                $"\n{nameof(ParentFolderId)}: {ParentFolderId}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}," +
                $"\n{nameof(UnlockAt)}: {UnlockAt}," +
                $"\n{nameof(Hidden)}: {Hidden}," +
                $"\n{nameof(HiddenForUser)}: {HiddenForUser}," +
                $"\n{nameof(Locked)}: {Locked}," +
                $"\n{nameof(LockedForUser)}: {LockedForUser}," +
                $"\n{nameof(ForSubmissions)}: {ForSubmissions}").Indent(4) +
            "\n}";
    }
}