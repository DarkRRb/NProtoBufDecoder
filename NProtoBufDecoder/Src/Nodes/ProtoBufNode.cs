using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NProtoBufDecoder;

public abstract class ProtoBufNode(ulong number, WireType type) {
    public ulong FieldNumber { get; } = number;

    public WireType WireType { get; } = type;

    public abstract bool TryAsInt32([NotNullWhen(true)] out int result);

    public abstract bool TryAsInt64([NotNullWhen(true)] out long result);

    public abstract bool TryAsUint32([NotNullWhen(true)] out uint result);

    public abstract bool TryAsUint64([NotNullWhen(true)] out ulong result);

    public abstract bool TryAsSint32([NotNullWhen(true)] out int result);

    public abstract bool TryAsSint64([NotNullWhen(true)] out long result);

    public abstract bool TryAsBool([NotNullWhen(true)] out bool result);

    public abstract bool TryAsEnum<T>([NotNullWhen(true)] out T result) where T : struct, Enum;

    public abstract bool TryAsFixed64([NotNullWhen(true)] out ulong result);

    public abstract bool TryAsSfixed64([NotNullWhen(true)] out long result);

    public abstract bool TryAsDouble([NotNullWhen(true)] out double result);

    public abstract bool TryAsString([NotNullWhen(true)] out string? result);

    public abstract bool TryAsBytes([NotNullWhen(true)] out ReadOnlyMemory<byte> result);

    public abstract bool TryAsMessage([NotNullWhen(true)] out IEnumerable<ProtoBufNode>? result);

    public abstract bool TryAsFixed32([NotNullWhen(true)] out uint result);

    public abstract bool TryAsSfixed32([NotNullWhen(true)] out int result);

    public abstract bool TryAsFloat([NotNullWhen(true)] out float result);
}
