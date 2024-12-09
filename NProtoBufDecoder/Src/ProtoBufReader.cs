using System;
using System.Buffers.Binary;

namespace NProtoBufDecoder;

internal ref struct ProtoBufReader(ReadOnlySpan<byte> bytes) {
    private readonly ReadOnlySpan<byte> _bytes = bytes;

    private int _offset = 0;

    public readonly bool IsEof => _offset >= _bytes.Length;

    public ulong ReadVarint() {
        (ulong result, int length) = PeekVarint();
        _offset += length;
        return result;
    }

    public readonly (ulong result, int length) PeekVarint() {
        ulong result = 0;
        for (int i = _offset; i < _bytes.Length; i++) {
            byte current = _bytes[i];
            result += ((ulong)current & 0b_0111_1111) << 7 * (i - _offset);
            if (current >> 7 == 0) return (result, i - _offset + 1);
        }
        throw new IndexOutOfRangeException();
    }

    public ulong ReadI64() {
        ulong result = BinaryPrimitives.ReadUInt64LittleEndian(_bytes[_offset..(_offset + 8)]);
        _offset += 8;
        return result;
    }

    public byte[] ReadLen() {
        (ulong lenLength, int length) = PeekVarint();
        byte[] result = _bytes[(_offset + length)..(_offset + length + (int)lenLength)].ToArray();
        _offset += length + (int)lenLength;
        return result;
    }

    public uint ReadI32() {
        uint result = BinaryPrimitives.ReadUInt32LittleEndian(_bytes[_offset..(_offset + 4)]);
        _offset += 4;
        return result;
    }
}