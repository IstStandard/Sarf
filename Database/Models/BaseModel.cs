using System.ComponentModel.DataAnnotations;

namespace Sarf.Database.Models;

public interface IBaseModel
{
    [Key] public Guid Uid { get; init; }
}