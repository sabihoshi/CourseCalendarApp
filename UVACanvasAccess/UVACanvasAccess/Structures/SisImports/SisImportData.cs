using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.SisImports;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.SisImports
{
    [PublicAPI]
    public class SisImportData
    {
        private readonly Api _api;

        internal SisImportData(Api api, SisImportDataModel model)
        {
            _api            = api;
            ImportType      = model.ImportType;
            SuppliedBatches = model.SuppliedBatches;
            Counts          = model.Counts.ConvertIfNotNull(c => new SisImportCounts(api, c));
        }

        public IEnumerable<string> SuppliedBatches { get; set; }

        [CanBeNull] public SisImportCounts Counts { get; set; }

        public string ImportType { get; set; }

        public override string ToString()
            => $"{nameof(ImportType)}: {ImportType}, {nameof(SuppliedBatches)}: {SuppliedBatches}, {nameof(Counts)}: {Counts}";
    }
}