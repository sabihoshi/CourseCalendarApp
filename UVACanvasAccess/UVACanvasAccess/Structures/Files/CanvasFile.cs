using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Files;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Files
{
    [PublicAPI]
    public class CanvasFile : IPrettyPrint
    {
        private readonly Api _api;

        internal CanvasFile(Api api, CanvasFileModel model)
        {
            _api            = api;
            Id              = model.Id;
            Uuid            = model.Uuid;
            FolderId        = model.FolderId;
            DisplayName     = model.DisplayName;
            Filename        = model.Filename;
            ContentType     = model.ContentType;
            Url             = model.Url;
            Size            = model.Size;
            CreatedAt       = model.CreatedAt;
            UpdatedAt       = model.UpdatedAt;
            UnlockAt        = model.UnlockAt;
            Locked          = model.Locked;
            Hidden          = model.Hidden;
            LockAt          = model.LockAt;
            HiddenForUser   = model.HiddenForUser;
            ThumbnailUrl    = model.ThumbnailUrl;
            ModifiedAt      = model.ModifiedAt;
            MimeClass       = model.MimeClass;
            MediaEntryId    = model.MediaEntryId;
            LockedForUser   = model.LockedForUser;
            LockInfo        = model.LockInfo;
            LockExplanation = model.LockExplanation;
            PreviewUrl      = model.PreviewUrl;
        }

        public ulong Id { get; }

        public bool Hidden { get; }

        public bool HiddenForUser { get; }

        public bool Locked { get; }

        public bool LockedForUser { get; }

        public object LockInfo { get; }

        public string ContentType { get; }

        public string CreatedAt { get; }

        public string DisplayName { get; }

        public string Filename { get; }

        public string LockAt { get; }

        public string LockExplanation { get; }

        public string MediaEntryId { get; }

        public string MimeClass { get; }

        public string ModifiedAt { get; }

        public string PreviewUrl { get; }

        public string ThumbnailUrl { get; }

        public string UnlockAt { get; }

        public string UpdatedAt { get; }

        public string Url { get; }

        public string Uuid { get; }

        public ulong FolderId { get; private set; }

        public ulong Size { get; }

        public string ToPrettyString() => "CanvasFile { " +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Uuid)}: {Uuid}," +
                $"\n{nameof(FolderId)}: {FolderId}," +
                $"\n{nameof(DisplayName)}: {DisplayName}," +
                $"\n{nameof(Filename)}: {Filename}," +
                $"\n{nameof(ContentType)}: {ContentType}," +
                $"\n{nameof(Url)}: {Url}," +
                $"\n{nameof(Size)}: {Size}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}," +
                $"\n{nameof(UpdatedAt)}: {UpdatedAt}," +
                $"\n{nameof(UnlockAt)}: {UnlockAt}," +
                $"\n{nameof(Locked)}: {Locked}," +
                $"\n{nameof(Hidden)}: {Hidden}," +
                $"\n{nameof(LockAt)}: {LockAt}," +
                $"\n{nameof(HiddenForUser)}: {HiddenForUser}," +
                $"\n{nameof(ThumbnailUrl)}: {ThumbnailUrl}," +
                $"\n{nameof(ModifiedAt)}: {ModifiedAt}," +
                $"\n{nameof(MimeClass)}: {MimeClass}," +
                $"\n{nameof(MediaEntryId)}: {MediaEntryId}," +
                $"\n{nameof(LockedForUser)}: {LockedForUser}," +
                $"\n{nameof(LockInfo)}: {LockInfo}," +
                $"\n{nameof(LockExplanation)}: {LockExplanation}," +
                $"\n{nameof(PreviewUrl)}: {PreviewUrl}").Indent(4) +
            "\n}";

        public async Task<bool> MoveTo(ulong folderId, Api.OnDuplicate onDuplicate)
        {
            var r = await _api.MoveFile(Id, onDuplicate, folderId: folderId);

            if (r.Id != Id) return false;

            FolderId = r.FolderId;
            return true;
        }

        public Task<bool> MoveTo(Folder folder, Api.OnDuplicate onDuplicate) => MoveTo(folder.Id, onDuplicate);

        public async Task<bool> Rename(string newName, Api.OnDuplicate onDuplicate)
        {
            var r = await _api.MoveFile(Id, onDuplicate, newName);

            if (r.Id != Id) return false;

            FolderId = r.FolderId;
            return true;
        }

        public Task<byte[]> Download() => _api.DownloadPersonalFile(this);

        public async Task<CanvasFile> CopyTo(ulong folderId, Api.OnDuplicate onDuplicate)
        {
            var r = await _api.CopyFile(Id, folderId, onDuplicate);

            return r.Id == Id
                ? r
                : null;
        }

        public Task<CanvasFile> CopyTo(Folder folder, Api.OnDuplicate onDuplicate) => CopyTo(folder.Id, onDuplicate);
    }
}