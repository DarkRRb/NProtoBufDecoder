using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace NProtoBufDecoder;

public class ProtoBufNode {
    public ulong FieldNumber { get; }

    public WireType WireType { get; }

    private readonly ulong _ulong;

    private readonly ReadOnlyMemory<byte> _bytes;

    internal ProtoBufNode(ulong number, WireType type, ulong value) {
        FieldNumber = number;
        WireType = type;
        _ulong = value;
        _bytes = default!;
    }

    internal ProtoBufNode(ulong number, WireType type, ReadOnlyMemory<byte> value) {
        FieldNumber = number;
        WireType = type;
        _ulong = default;
        _bytes = value;
    }

    public int AsInt32() {
        if (!TryAsInt32(out int result)) throw new InvalidOperationException();
        return result;
    }

    public long AsInt64() {
        if (!TryAsInt64(out long result)) throw new InvalidOperationException();
        return result;
    }

    public uint AsUint32() {
        if (!TryAsUint32(out uint result)) throw new InvalidOperationException();
        return result;
    }

    public ulong AsUint64() {
        if (!TryAsUint64(out ulong result)) throw new InvalidOperationException();
        return result;
    }

    public int AsSint32() {
        if (!TryAsSint32(out int result)) throw new InvalidOperationException();
        return result;
    }

    public long AsSint64() {
        if (!TryAsSint64(out long result)) throw new InvalidOperationException();
        return result;
    }

    public bool AsBool() {
        if (!TryAsBool(out bool result)) throw new InvalidOperationException();
        return result;
    }

    public T AsEnum<T>() where T : struct, Enum {
        if (!TryAsEnum(out T result)) throw new InvalidOperationException();
        return result;
    }

    public ulong AsFixed64() {
        if (!TryAsFixed64(out ulong result)) throw new InvalidOperationException();
        return result;
    }

    public long AsSfixed64() {
        if (!TryAsSfixed64(out long result)) throw new InvalidOperationException();
        return result;
    }

    public double AsDouble() {
        if (!TryAsDouble(out double result)) throw new InvalidOperationException();
        return result;
    }

    public string AsString() {
        if (!TryAsString(out string? result)) throw new InvalidOperationException();
        return result;
    }

    public ReadOnlyMemory<byte> AsBytes() {
        if (!TryAsBytes(out ReadOnlyMemory<byte> result)) throw new InvalidOperationException();
        return result;
    }

    public IEnumerable<ProtoBufNode> AsMessage() {
        if (!TryAsMessage(out IEnumerable<ProtoBufNode>? result)) throw new InvalidOperationException();
        return result;
    }

    public uint AsFixed32() {
        if (!TryAsFixed32(out uint result)) throw new InvalidOperationException();
        return result;
    }

    public int AsSfixed32() {
        if (!TryAsSfixed32(out int result)) throw new InvalidOperationException();
        return result;
    }

    public float AsFloat() {
        if (!TryAsFloat(out float result)) throw new InvalidOperationException();
        return result;
    }

    public bool TryAsInt32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (int)_ulong;
        return true;
    }

    public bool TryAsInt64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (long)_ulong;
        return true;
    }

    public bool TryAsUint32([NotNullWhen(true)] out uint result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (uint)_ulong;
        return true;
    }

    public bool TryAsUint64([NotNullWhen(true)] out ulong result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = _ulong;
        return true;
    }

    public bool TryAsSint32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (int)(_ulong >> 1) ^ -(int)(_ulong & 1);
        return true;
    }

    public bool TryAsSint64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (long)(_ulong >> 1) ^ -(long)(_ulong & 1);
        return true;
    }

    public bool TryAsBool([NotNullWhen(true)] out bool result) {
        result = default;

        if (WireType != WireType.VARINT) return false;
        if (_ulong != 0 && _ulong != 1) return false;

        result = _ulong == 1;
        return true;
    }

    public bool TryAsEnum<T>([NotNullWhen(true)] out T result) where T : struct, Enum {
        result = default;

        if (WireType != WireType.VARINT) return false;

        result = (T)Enum.ToObject(typeof(T), (int)_ulong);
        return true;
    }

    public bool TryAsFixed64([NotNullWhen(true)] out ulong result) {
        result = default;

        if (WireType != WireType.I64) return false;

        result = _ulong;
        return true;
    }

    public bool TryAsSfixed64([NotNullWhen(true)] out long result) {
        result = default;

        if (WireType != WireType.I64) return false;


        result = (long)_ulong;
        return true;
    }

    public bool TryAsDouble([NotNullWhen(true)] out double result) {
        result = default;

        if (WireType != WireType.I64) return false;


        try {
            result = Unsafe.BitCast<ulong, double>(
                BitConverter.IsLittleEndian ? _ulong : BinaryPrimitives.ReverseEndianness(_ulong)
            );
        } catch { return false; }
        return true;
    }

    public bool TryAsString([NotNullWhen(true)] out string? result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        result = Encoding.UTF8.GetString(_bytes.Span);
        return true;
    }

    public bool TryAsBytes([NotNullWhen(true)] out ReadOnlyMemory<byte> result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        result = _bytes;
        return true;
    }

    public bool TryAsMessage([NotNullWhen(true)] out IEnumerable<ProtoBufNode>? result) {
        result = default;

        if (WireType != WireType.LEN) return false;

        try {
            result = ProtoBufDecoder.Decode(_bytes.Span);
        } catch { return false; }
        return true;
    }

    public bool TryAsFixed32([NotNullWhen(true)] out uint result) {
        result = default;

        if (WireType != WireType.I32) return false;

        result = (uint)_ulong;
        return true;
    }

    public bool TryAsSfixed32([NotNullWhen(true)] out int result) {
        result = default;

        if (WireType != WireType.I32) return false;

        result = (int)_ulong;
        return true;
    }

    public bool TryAsFloat([NotNullWhen(true)] out float result) {
        result = default;

        if (WireType != WireType.I32) return false;

        try {
            result = Unsafe.BitCast<uint, float>(
                BitConverter.IsLittleEndian ? (uint)_ulong : (uint)BinaryPrimitives.ReverseEndianness(_ulong)
            );
        } catch { return false; }
        return true;
    }
}