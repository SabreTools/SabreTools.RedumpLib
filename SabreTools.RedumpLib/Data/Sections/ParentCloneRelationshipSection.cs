using System;
using Newtonsoft.Json;

namespace SabreTools.RedumpLib.Data.Sections
{
    /// <summary>
    /// Parent/Clone relationship section of New Disc form
    /// </summary>
    public class ParentCloneRelationshipSection : ICloneable
    {
        [JsonProperty(PropertyName = "d_parent_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? ParentID { get; set; }

        [JsonProperty(PropertyName = "d_is_regional_parent", NullValueHandling = NullValueHandling.Ignore)]
        public bool RegionalParent { get; set; }

        public object Clone()
        {
            return new ParentCloneRelationshipSection
            {
                ParentID = this.ParentID,
                RegionalParent = this.RegionalParent,
            };
        }
    }
}
