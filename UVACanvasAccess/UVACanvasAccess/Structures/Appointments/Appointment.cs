using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Appointments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Appointments
{
    [PublicAPI]
    public class Appointment : IPrettyPrint
    {
        private readonly Api _api;

        internal Appointment(Api api, AppointmentModel model)
        {
            _api    = api;
            Id      = model.Id;
            StartAt = model.StartAt;
            EndAt   = model.EndAt;
        }

        public ulong Id { get; }

        public DateTime EndAt { get; }

        public DateTime StartAt { get; }

        public string ToPrettyString() => "Appointment {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(StartAt)}: {StartAt}," +
                $"\n{nameof(EndAt)}: {EndAt}").Indent(4) +
            "\n}";
    }
}