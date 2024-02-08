using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Exceptions;
using UVACanvasAccess.Model.ToDos;
using UVACanvasAccess.Structures.Assignments;
using UVACanvasAccess.Structures.Quizzes;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.ToDos
{
    [PublicAPI]
    public enum ToDoType : byte
    {
        [ApiRepresentation("grading")] Grading,
        [ApiRepresentation("submitting")] Submitting
    }

    [PublicAPI]
    public abstract class ToDoItem : IPrettyPrint
    {
        private readonly Api _api;

        private protected ToDoItem(Api api, ToDoItemModel model)
        {
            _api = api;
            Type = model.Type.ToApiRepresentedEnum<ToDoType>()
                .Expect(() => new BadApiStateException($"ToDoItem.Type was an unexpected value: {model.Type}"));
            ContextId = model.ContextType.ToLowerInvariant() switch
            {
                "course" => new QualifiedId(model.CourseId.Expect(), ContextType.Course),
                "group"  => new QualifiedId(model.GroupId.Expect(), ContextType.Group),
                _ => throw new BadApiStateException(
                    $"ToDoItemModel.ContextType was an unexpected value: {model.ContextType}")
            };
            IgnoreUrl          = model.IgnoreUrl;
            PermanentIgnoreUrl = model.PermanentIgnoreUrl;
        }

        public QualifiedId ContextId { get; }

        public string IgnoreUrl { get; }

        public string PermanentIgnoreUrl { get; }

        public ToDoType Type { get; }

        public abstract string ToPrettyString();

        /// <summary>
        ///     Hide this item from future requests until it changes.
        /// </summary>
        /// <returns>A Task representing completion of this request.</returns>
        public Task Ignore() => _api.IgnoreToDoItem(this, false);

        /// <summary>
        ///     Hide this item from all future requests.
        /// </summary>
        /// <returns>A Task representing completion of this request.</returns>
        public Task IgnorePermanently() => _api.IgnoreToDoItem(this, true);

        internal static ToDoItem NewToDoItem(Api api, ToDoItemModel model)
        {
            if (model.Assignment != null)
                return new AssignmentToDoItem(api, model);
            return new QuizToDoItem(api, model);
        }
    }

    [PublicAPI]
    public sealed class AssignmentToDoItem : ToDoItem
    {
        internal AssignmentToDoItem(Api api, ToDoItemModel model) : base(api, model)
        {
            Assignment = model.Assignment.ConvertIfNotNull(m => new Assignment(api, m));
        }

        public Assignment Assignment { get; }

        public override string ToPrettyString() => "AssignmentToDoItem {" +
            ($"\n{nameof(Type)}: {Type.GetApiRepresentation()}," +
                $"\n{nameof(IgnoreUrl)}: {IgnoreUrl}," +
                $"\n{nameof(PermanentIgnoreUrl)}: {PermanentIgnoreUrl}," +
                $"\n{nameof(ContextId)}: {ContextId.ToPrettyString()}," +
                $"\n{nameof(Assignment)}: {Assignment.ToPrettyString()}").Indent(4) +
            "\n}";
    }

    [PublicAPI]
    public sealed class QuizToDoItem : ToDoItem
    {
        internal QuizToDoItem(Api api, ToDoItemModel model) : base(api, model)
        {
            Quiz = model.Quiz.ConvertIfNotNull(m => new Quiz(api, m));
        }

        public Quiz Quiz { get; }

        public override string ToPrettyString() => "QuizToDoItem {" +
            ($"\n{nameof(Type)}: {Type.GetApiRepresentation()}," +
                $"\n{nameof(IgnoreUrl)}: {IgnoreUrl}," +
                $"\n{nameof(PermanentIgnoreUrl)}: {PermanentIgnoreUrl}," +
                $"\n{nameof(ContextId)}: {ContextId.ToPrettyString()}," +
                $"\n{nameof(Quiz)}: {Quiz.ToPrettyString()}").Indent(4) +
            "\n}";
    }
}