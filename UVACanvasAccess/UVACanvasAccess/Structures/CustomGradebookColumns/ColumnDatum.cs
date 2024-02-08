using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.CustomGradebookColumns;

namespace UVACanvasAccess.Structures.CustomGradebookColumns
{
    [PublicAPI]
    public class ColumnDatum
    {
        private readonly Api _api;

        internal ColumnDatum(Api api, ColumnDatumModel model)
        {
            _api    = api;
            Content = model.Content;
            UserId  = model.UserId;
        }

        public string Content { get; }

        public ulong UserId { get; }
    }
}