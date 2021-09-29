using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription.Model
{
    public class BaseEntity<TKey> : BaseEntity
    {
        [JsonIgnore]
        [Key]
        public virtual TKey TId { get; set; }

        public BaseEntity()
        {

        }
        public BaseEntity(TKey id)
        {
            TId = id;
        }
    }

    public abstract class BaseEntity
    {
    }
}
