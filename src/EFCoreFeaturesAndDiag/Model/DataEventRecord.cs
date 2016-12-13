using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EFCoreFeaturesAndDiag.Model
{
    using System.ComponentModel.DataAnnotations;

    public class DataEventRecord
    {
        private string _description;

        [Key]
        public long DataEventRecordId { get; set; }
        public string Name { get; set; }
        public string MadDescription {
            get { return _description; }
            set { _description = value;  }
        }

        public DateTime Timestamp { get; set; }
        [ForeignKey("SourceInfoId")]
        public SourceInfo SourceInfo { get; set; }
        public long SourceInfoId { get; set; }
    }
}
