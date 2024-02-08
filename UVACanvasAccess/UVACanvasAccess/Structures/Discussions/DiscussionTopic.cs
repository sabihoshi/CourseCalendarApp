using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Discussions;
using UVACanvasAccess.Structures.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Discussions
{
    [PublicAPI]
    public class DiscussionTopic : IPrettyPrint
    {
        public enum DiscussionHome : byte
        {
            Course,
            Group
        }

        private readonly Api _api;

        internal DiscussionTopic(Api api, DiscussionTopicModel model, string home, ulong homeId)
            : this(api, model, home == "courses" ? DiscussionHome.Course : DiscussionHome.Group, homeId) { }

        internal DiscussionTopic(Api api, DiscussionTopicModel model, DiscussionHome home, ulong homeId)
        {
            _api                    = api;
            Home                    = home;
            HomeId                  = homeId;
            Id                      = model.Id;
            Title                   = model.Title;
            Message                 = model.Message;
            HtmlUrl                 = model.HtmlUrl;
            PostedAt                = model.PostedAt;
            LastReplyAt             = model.LastReplyAt;
            RequireInitialPost      = model.RequireInitialPost;
            UserCanSeePosts         = model.UserCanSeePosts;
            DiscussionSubentryCount = model.DiscussionSubentryCount;
            ReadState               = model.ReadState;
            UnreadCount             = model.UnreadCount;
            Subscribed              = model.Subscribed;
            SubscriptionHold        = model.SubscriptionHold;
            AssignmentId            = model.AssignmentId;
            DelayedPostAt           = model.DelayedPostAt;
            Published               = model.Published;
            LockAt                  = model.LockAt;
            Locked                  = model.Locked;
            Pinned                  = model.Pinned;
            LockedForUser           = model.LockedForUser;
            LockInfo                = model.LockInfo;
            LockExplanation         = model.LockExplanation;
            UserName                = model.UserName;
            TopicChildren           = model.TopicChildren;
            GroupTopicChildren      = model.GroupTopicChildren;
            RootTopicId             = model.RootTopicId;
            PodcastUrl              = model.PodcastUrl;
            DiscussionType          = model.DiscussionType;
            GroupCategoryId         = model.GroupCategoryId;
            Attachments = from a in model.Attachments
                select new FileAttachment(api, a);
            Permissions        = model.Permissions;
            AllowRating        = model.AllowRating;
            OnlyGradersCanRate = model.OnlyGradersCanRate;
            SortByRating       = model.SortByRating;
            Author             = model.Author.ConvertIfNotNull(m => new UserDisplay(api, m));
        }

        public ulong Id { get; }

        public bool? AllowRating { get; }

        public bool? Locked { get; }

        public bool? LockedForUser { get; }

        public bool? OnlyGradersCanRate { get; }

        public bool? Pinned { get; }

        public bool? Published { get; }

        public bool? RequireInitialPost { get; }

        public bool? SortByRating { get; }

        public bool? Subscribed { get; }

        public bool? UserCanSeePosts { get; }

        public DateTime? DelayedPostAt { get; }

        public DateTime? LastReplyAt { get; }

        public DateTime? LockAt { get; }

        public DateTime? PostedAt { get; }

        public Dictionary<string, bool> Permissions { get; }

        public DiscussionHome Home { get; }

        public IEnumerable<FileAttachment> Attachments { get; }

        public IEnumerable<uint> TopicChildren { get; }

        public object GroupTopicChildren { get; }

        public object LockInfo { get; }

        public string AssignmentId { get; }

        public string DiscussionType { get; }

        public string HtmlUrl { get; }

        public string LockExplanation { get; }

        public string Message { get; }

        public string PodcastUrl { get; }

        public string ReadState { get; }

        public string SubscriptionHold { get; }

        public string Title { get; }

        public string UserName { get; }

        public uint? DiscussionSubentryCount { get; }

        public uint? UnreadCount { get; }

        public ulong HomeId { get; }

        public ulong? GroupCategoryId { get; }

        public ulong? RootTopicId { get; }

        public UserDisplay Author { get; }

        public string ToPrettyString() => "DiscussionTopic {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Title)}: {Title}," +
                $"\n{nameof(Author)}: {Author}," +
                $"\n{nameof(Message)}: {Message}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}," +
                $"\n{nameof(PostedAt)}: {PostedAt}," +
                $"\n{nameof(LastReplyAt)}: {LastReplyAt}," +
                $"\n{nameof(RequireInitialPost)}: {RequireInitialPost}," +
                $"\n{nameof(UserCanSeePosts)}: {UserCanSeePosts}," +
                $"\n{nameof(DiscussionSubentryCount)}: {DiscussionSubentryCount}," +
                $"\n{nameof(ReadState)}: {ReadState}," +
                $"\n{nameof(UnreadCount)}: {UnreadCount}," +
                $"\n{nameof(Subscribed)}: {Subscribed}," +
                $"\n{nameof(SubscriptionHold)}: {SubscriptionHold}," +
                $"\n{nameof(AssignmentId)}: {AssignmentId}," +
                $"\n{nameof(DelayedPostAt)}: {DelayedPostAt}," +
                $"\n{nameof(Published)}: {Published}," +
                $"\n{nameof(LockAt)}: {LockAt}," +
                $"\n{nameof(Locked)}: {Locked}," +
                $"\n{nameof(Pinned)}: {Pinned}," +
                $"\n{nameof(LockedForUser)}: {LockedForUser}," +
                $"\n{nameof(LockInfo)}: {LockInfo}," +
                $"\n{nameof(LockExplanation)}: {LockExplanation}," +
                $"\n{nameof(UserName)}: {UserName}," +
                $"\n{nameof(TopicChildren)}: {TopicChildren}," +
                $"\n{nameof(GroupTopicChildren)}: {GroupTopicChildren}," +
                $"\n{nameof(RootTopicId)}: {RootTopicId}," +
                $"\n{nameof(PodcastUrl)}: {PodcastUrl}," +
                $"\n{nameof(DiscussionType)}: {DiscussionType}," +
                $"\n{nameof(GroupCategoryId)}: {GroupCategoryId}," +
                $"\n{nameof(Attachments)}: {Attachments.ToPrettyString()}," +
                $"\n{nameof(Permissions)}: {Permissions.ToPrettyString()}," +
                $"\n{nameof(AllowRating)}: {AllowRating}," +
                $"\n{nameof(OnlyGradersCanRate)}: {OnlyGradersCanRate}," +
                $"\n{nameof(SortByRating)}: {SortByRating}").Indent(4) +
            "\n}";

        /// <summary>
        ///     Gets the list of entries for this topic.
        /// </summary>
        /// <returns>The list of entries.</returns>
        public Task<IEnumerable<TopicEntry>> GetEntries()
        {
            return Home switch
            {
                DiscussionHome.Course => _api.ListCourseDiscussionTopicEntries(HomeId, Id),
                DiscussionHome.Group  => _api.ListGroupDiscussionTopicEntries(HomeId, Id),
                _                     => throw new ArgumentOutOfRangeException()
            };
        }
    }
}