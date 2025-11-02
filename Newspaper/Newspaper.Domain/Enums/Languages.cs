using System.Runtime.Serialization;

namespace Newspaper.Domain.Enums;

public enum Languages
{
    [EnumMember(Value = "ru")]
    Russian = 1,
    [EnumMember(Value = "en")]
    English
}