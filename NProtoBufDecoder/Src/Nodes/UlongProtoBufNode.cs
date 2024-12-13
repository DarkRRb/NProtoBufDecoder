using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NProtoBufDecoder;

internal class UlongProtoBufNode(ulong number, WireType type, ulong value) : ProtoBufNode(number, type) {
    private readonly ulong _value = value;

    public override bool TryAsInt32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.VARINT) return false;
        ulong hight = _value >> 32;
        if (hight != 0xFFFFFFFF && hight != 0) return false;

        result = (int)_value;
        return true;
    }

    public override bool TryAsInt64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (long)_value;
        return true;
    }

    public override bool TryAsUint32([NotNullWhen(true)] out uint result) {
        result = default;

        if (WireType != WireType.VARINT) return false;
        if (_value > uint.MaxValue) return false;

        result = (uint)_value;
        return true;
    }

    public override bool TryAsUint64([NotNullWhen(true)] out ulong result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = _value;
        return true;
    }

    public override bool TryAsSint32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (int)(_value >> 1) ^ -(int)(_value & 1);
        return true;
    }

    public override bool TryAsSint64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (long)(_value >> 1) ^ -(long)(_value & 1);
        return true;
    }

    public override bool TryAsBool([NotNullWhen(true)] out bool result) {
        result = default;

        if (WireType != WireType.VARINT) return false;
        if (_value != 0 && _value != 1) return false;

        result = _value == 1;
        return true;
    }

    public override bool TryAsEnum<T>([NotNullWhen(true)] out T result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (T)Enum.ToObject(typeof(T), (int)_value);
        return true;
    }

    public override bool TryAsFixed64([NotNullWhen(true)] out ulong result) {
        result = default;

        if (WireType != WireType.I64) return false;

        result = _value;
        return true;
    }

    public override bool TryAsSfixed64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.I64) return false;


        result = (long)_value;
        return true;
    }

    public override bool TryAsDouble([NotNullWhen(true)] out double result) {
        result = default;

        if (WireType != WireType.I64) return false;


        try {
            result = Unsafe.BitCast<ulong, double>(
                BitConverter.IsLittleEndian ? _value : BinaryPrimitives.ReverseEndianness(_value)
            );
        } catch { return false; }
        return true;
    }

    public override bool TryAsString([NotNullWhen(true)] out string? result) {
        result = default;
        return false;
    }

    public override bool TryAsBytes([NotNullWhen(true)] out ReadOnlyMemory<byte> result) {
        result = default;
        return false;
    }

    public override bool TryAsMessage([NotNullWhen(true)] out IEnumerable<ProtoBufNode>? result) {
        result = default;
        return false;
    }

    public override bool TryAsFixed32([NotNullWhen(true)] out uint result) {
        result = default;

        if (WireType != WireType.I32) return false;

        result = (uint)_value;
        return true;
    }

    public override bool TryAsSfixed32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.I32) return false;

        result = (int)_value;
        return true;
    }

    public override bool TryAsFloat([NotNullWhen(true)] out float result) {
        result = default;

        if (WireType != WireType.I32) return false;

        try {
            result = Unsafe.BitCast<uint, float>(
                BitConverter.IsLittleEndian ? (uint)_value : (uint)BinaryPrimitives.ReverseEndianness(_value)
            );
        } catch { return false; }
        return true;
    }
}