using Business.Models.DTO;
using Business.Models.RegForms;
using Data.Entities;

namespace Business.Factories;

public static class StatusFactory
{
    public static StatusRegForm Create() => new();
    public static StatusEntity Create(StatusRegForm formData) => new()
    {

        StatusBool = formData.StatusBool
    };
    public static Status Create(StatusEntity entity)
    {
        return new Status
        {
            StatusId = entity.StatusId,
            StatusBool = entity.StatusBool
        };
    }
}
