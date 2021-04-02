using System;

namespace NetGraphQL.Shared.ViewModel
{
    public class ModelBase
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public byte[] Vers { get; set; }
    }
}
