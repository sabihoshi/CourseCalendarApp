using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Model.Groups;
using UVACanvasAccess.Model.Users;

namespace UVACanvasAccess.Model.Calendar
{
    /*
     * This class combines the fields of normal, reservation, time-slot, and assignment calendar events.
     * Concrete structure classes will specialize to these types and inherit from a common base.
     */

    internal class CalendarEventModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [CanBeNull]
        [JsonProperty("assignment")]
        public AssignmentModel Assignment { get; set; }

        [JsonProperty("all_day")] public bool AllDay { get; set; }

        [JsonProperty("hidden")] public bool Hidden { get; set; }

        [JsonProperty("can_manage_appointment_group")]
        public bool? CanManageAppointmentGroup { get; set; } // undocumented

        [JsonProperty("own_reservation")] public bool? OwnReservation { get; set; }

        [JsonProperty("reserved")] public bool? Reserved { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("end_at")] public DateTime EndAt { get; set; }

        [JsonProperty("start_at")] public DateTime StartAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }

        [JsonProperty("all_day_date")] public DateTime? AllDayDate { get; set; }

        [CanBeNull] [JsonProperty("group")] public GroupModel Group { get; set; }

        [CanBeNull]
        [JsonProperty("assignment_overrides")]
        public IEnumerable<AssignmentOverrideModel> AssignmentOverrides { get; set; }

        [CanBeNull]
        [JsonProperty("child_events")]
        public IEnumerable<CalendarEventModel> ChildEvents { get; set; }

        [JsonProperty("all_context_codes")] public string AllContextCodes { get; set; } // comma separated

        [JsonProperty("appointment_group_url")]
        public string AppointmentGroupUrl { get; set; }

        [JsonProperty("context_code")] public string ContextCode { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [CanBeNull]
        [JsonProperty("effective_context_code")]
        public string EffectiveContextCode { get; set; }

        [JsonProperty("html_url")] public string HtmlUrl { get; set; }

        [JsonProperty("location_address")] public string LocationAddress { get; set; }

        [JsonProperty("location_name")] public string LocationName { get; set; }

        [CanBeNull]
        [JsonProperty("parent_event_id")]
        public string ParentEventId { get; set; }

        [JsonProperty("participant_type")] public string ParticipantType { get; set; } // User|Group

        [CanBeNull]
        [JsonProperty("reserve_url")]
        public string ReserveUrl { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("workflow_state")] public string WorkflowState { get; set; }

        [JsonProperty("available_slots")] public uint? AvailableSlots { get; set; }

        [JsonProperty("child_events_count")] public uint? ChildEventsCount { get; set; }

        [JsonProperty("participant_limit")] public uint? ParticipantLimit { get; set; }

        [JsonProperty("participants_per_appointment")]
        public uint? ParticipantsPerAppointment { get; set; }

        [JsonProperty("appointment_group_id")] public ulong AppointmentGroupId { get; set; }

        [CanBeNull] [JsonProperty("user")] public UserModel User { get; set; }
    }
}