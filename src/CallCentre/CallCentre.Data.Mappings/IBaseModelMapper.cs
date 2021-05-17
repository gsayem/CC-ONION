using Microsoft.EntityFrameworkCore;
using System;

namespace CallCentre.Data.Mappings
{
    interface IBaseModelMapper
    {
        void MapEntity(ModelBuilder modelBuilder);
    }
}
