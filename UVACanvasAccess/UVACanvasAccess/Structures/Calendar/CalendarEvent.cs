using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Calendar;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Calendar
{
    [PublicAPI]
    public abstract class CalendarEvent : IPrettyPrint
    {
        private protected readonly Api Api;

        private protected CalendarEvent(Api api, CalendarEventModel model)
        {
            Api                  = api;
            Id                   = model.Id;
            Title                = model.Title;
            StartAt              = model.StartAt;
            EndAt                = model.EndAt;
            Type                 = model.Type;
            Description          = model.Description;
            ContextCode          = model.ContextCode;
            EffectiveContextCode = model.EffectiveContextCode;
            AllContextCodes      = model.AllContextCodes.Split(',');
            WorkflowState        = model.WorkflowState;
            Hidden               = model.Hidden;
            ParentEventId        = model.ParentEventId;
            ChildEvents          = model.ChildEvents.SelectNotNull(child => FromModel(api, child));
            ChildEventsCount     = model.ChildEventsCount;
            Url                  = model.Url;
            HtmlUrl              = model.HtmlUrl;
            AllDayDate           = model.AllDayDate;
            AllDay               = model.AllDay;
            CreatedAt            = model.CreatedAt;
            UpdatedAt            = model.UpdatedAt;
        }

        public ulong Id { get; }

        public bool AllDay { get; }

        public bool Hidden { get; }

        public DateTime CreatedAt { get; }

        public DateTime EndAt { get; }

        public DateTime StartAt { get; }

        public DateTime UpdatedAt { get; }

        public DateTime? AllDayDate { get; }

        [CanBeNull] public IEnumerable<CalendarEvent> ChildEvents { get; }

        public IEnumerable<string> AllContextCodes { get; }

        public string ContextCode { get; }

        public string Description { get; }

        [CanBeNull] public string EffectiveContextCode { get; }

        public string HtmlUrl { get; }

        [CanBeNull] public string ParentEventId { get; }

        public string Title { get; }

        public string Type { get; }

        public string Url { get; }

        public string WorkflowState { get; }

        public uint? ChildEventsCount { get; }

        public virtual string ToPrettyString() => "CalendarEvent {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Title)}: {Title}," +
                $"\n{nameof(StartAt)}: {StartAt}," +
                $"\n{nameof(EndAt)}: {EndAt}," +
                $"\n{nameof(Type)}: {Type}," +
                $"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(ContextCode)}: {ContextCode}," +
                $"\n{nameof(EffectiveContextCode)}: {EffectiveContextCode}," +
                $"\n{nameof(AllContextCodes)}: {AllContextCodes.ToPrettyString()}," +
                $"\n{nameof(WorkflowState)}: {WorkflowState}," +
                $"\n{nameof(Hidden)}: {Hidden}," +
                $"\n{nameof(ParentEventId)}: {ParentEventId}," +
                $"\n{nameof(ChildEventsCount)}: {ChildEventsCount}," +
                $"\n{nameof(ChildEvents)}: {ChildEvents?.ToPrettyString()}," +
                $"\n{nameof(Url)}: {Url}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}," +
                $"\n{nameof(AllDayDate)}: {AllDayDate}," +
                $"\n{nameof(AllDay)}: {AllDay}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}," +
                $"\n{nameof(UpdatedAt)}: {UpdatedAt}").Indent(4) +
            "\n}";

        internal static CalendarEvent FromModel(Api api, CalendarEventModel model)
        {
            if (model.ReserveUrl != null) return new TimeSlotCalendarEvent(api, model);
            if (model.User != null) return new UserReservationCalendarEvent(api, model);
            if (model.Type == "event") return new BasicCalendarEvent(api, model);

            throw new NotImplementedException("CalendarEvent::FromModel didn't recognize model");
        }
    }
}