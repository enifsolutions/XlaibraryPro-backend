using XlibraryPro.Domain.Models.MasterData;

namespace XlibraryPro.Application.Features.MasterData.Publisher.Dto;

public class PublisherDto
{
    public long   PublisherId   { get; set; }
    public string PublisherName { get; set; } = string.Empty;

    public static PublisherDto FromModel(PublisherModel m) => new()
    {
        PublisherId   = m.PublisherId,
        PublisherName = m.PublisherName
    };
}
