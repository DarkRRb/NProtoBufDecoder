using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using NProtoBufDecoder;

internal class BytesProtoBufNode(ulong number, WireType type, ReadOnlyMemory<byte> value) : ProtoBufNode(number, type) {
    private readonly ReadOnlyMemory<byte> _value = value;

    public override bool TryAsInt32([NotNullWhen(true)] out int result) {
        result = default;
        return false;
    }

    public override bool TryAsInt64([NotNullWhen(true)] out long result) {
        result = default;
        return false;
    }

    public override bool TryAsUint32([NotNullWhen(true)] out uint result) {
        result = default;
        return false;
    }

    public override bool TryAsUint64([NotNullWhen(true)] out ulong result) {
        result = default;
        return false;
    }

    public override bool TryAsSint32([NotNullWhen(true)] out int result) {
        result = default;
        return false;
    }

    public override bool TryAsSint64([NotNullWhen(true)] out long result) {
        result = default;
        return false;
    }

    public override bool TryAsBool([NotNullWhen(true)] out bool result) {
        result = default;
        return false;
    }

    public override bool TryAsEnum<T>([NotNullWhen(true)] out T result) {
        result = default;
        return false;
    }

    public override bool TryAsFixed64([NotNullWhen(true)] out ulong result) {
        result = default;
        return false;
    }

    public override bool TryAsSfixed64([NotNullWhen(true)] out long result) {
        result = default;
        return false;
    }

    public override bool TryAsDouble([NotNullWhen(true)] out double result) {
        result = default;
        return false;
    }

    public override bool TryAsString([NotNullWhen(true)] out string? result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        result = Encoding.UTF8.GetString(_value.Span);
        return true;
    }

    public override bool TryAsBytes([NotNullWhen(true)] out ReadOnlyMemory<byte> result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        result = _value;
        return true;
    }

    public override bool TryAsMessage([NotNullWhen(true)] out IEnumerable<ProtoBufNode>? result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        try {
            result = ProtoBufDecoder.Decode(_value.Span);
        } catch { return false; }
        return true;
    }

    public override bool TryAsFixed32([NotNullWhen(true)] out uint result) {
        result = default;
        return false;
    }

    public override bool TryAsSfixed32([NotNullWhen(true)] out int result) {
        result = default;
        return false;
    }

    public override bool TryAsFloat([NotNullWhen(true)] out float result) {
        result = default;
        return false;
    }
}