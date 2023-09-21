using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ToksozBysNew.Positions
{
    public class Position : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string PositionCode { get; set; }

        [CanBeNull]
        public virtual string PositionName { get; set; }

        public Position()
        {

        }

        public Position(Guid id, string positionCode, string positionName)
        {

            Id = id;
            PositionCode = positionCode;
            PositionName = positionName;
        }

    }
}