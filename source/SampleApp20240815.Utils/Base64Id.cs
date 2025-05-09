using System;
using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace SampleApp20240815.Utils;

public static class Base64Id
{
    #region GuidToB64Id

    public const byte BytePlus = (byte)'+';
    public const byte ByteSlash = (byte)'/';

    [Obsolete("Use FromGuidToB64IdStd(Guid id)")]
    public static string GuidToB64IdManualNotOptimized(Guid id)
    {
        return Convert.ToBase64String(id.ToByteArray())
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
    }

    [Obsolete("Use FromGuidToB64IdStd(Guid id)")]
    public static string GuidToB64IdManualOptimized(Guid id)
    {
        Span<byte> bytes = stackalloc byte[16];
        MemoryMarshal.TryWrite(bytes, in id);

        Span<byte> utf8 = stackalloc byte[24];
        Base64.EncodeToUtf8(bytes, utf8, out _, out _);

        return string.Create(22, utf8, (dest, src) =>
        {
            for (var i = 0; i < 22; i++)
            {
                dest[i] = src[i] switch
                {
                    BytePlus => '-',
                    ByteSlash => '_',
                    _ => (char)src[i],
                };
            }
        });
    }

    public static string GuidToB64IdStd(Guid id)
    {
        Span<byte> bytes = stackalloc byte[16];
        MemoryMarshal.Write(bytes, in id);

        Span<char> chars = stackalloc char[24];
        Base64Url.EncodeToChars(bytes, chars, out _, out _);

        return string.Create(22, chars[..22], (dest, src) => src.CopyTo(dest));
    }

    #endregion

    #region B64IdToGuid

    [Obsolete("Use FromB64IdToGuidStd(ReadOnlySpan<char> base64Id)")]
    public static Guid B64IdToGuidManualNotOptimized(string base64Id)
    {
        return new Guid(Convert.FromBase64String(base64Id.Replace('-', '+').Replace('_', '/') + "=="));
    }

    [Obsolete("Use FromB64IdToGuidStd(ReadOnlySpan<char> base64Id)")]
    public static Guid B64IdToGuidManualOptimized(ReadOnlySpan<char> base64Id)
    {
        Span<char> chars = stackalloc char[24];
        for (var i = 0; i < 22; i++)
        {
            chars[i] = base64Id[i] switch
            {
                '-' => '+',
                '_' => '/',
                _ => base64Id[i],
            };
        }

        chars[22] = chars[23] = '=';

        Span<byte> bytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(chars, bytes, out _);

        return new Guid(bytes);
    }

    public static Guid B64IdToGuidStd(ReadOnlySpan<char> base64Id)
    {
        Span<char> chars = stackalloc char[24];
        base64Id.CopyTo(chars);
        chars[22] = chars[23] = '=';

        Span<byte> bytes = stackalloc byte[16];
        Base64Url.DecodeFromChars(chars, bytes, out _, out _);

        return new Guid(bytes);
    }

    #endregion
}
