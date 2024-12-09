using System;
using System.Collections.Generic;

namespace NProtoBufDecoder;

public static class ProtoBufDecoder {
    public static IEnumerable<ProtoBufNode> Decode(ReadOnlySpan<byte> bytes) {
        List<ProtoBufNode> result = [];

        ProtoBufReader reader = new(bytes);
        while (!reader.IsEof) {
            ulong tag = reader.ReadVarint();
            ulong number = tag >> 3;
            WireType type = (WireType)(tag & 0b_0000_0111);

            result.Add(type switch {
                WireType.VARINT => new ProtoBufNode(number, type, reader.ReadVarint()),
                WireType.I64 => new ProtoBufNode(number, type, reader.ReadI64()),
                WireType.LEN => new ProtoBufNode(number, type, reader.ReadLen()),
                WireType.I32 => new ProtoBufNode(number, type, reader.ReadI32()),
                _ => throw new UnknownWireTypeException(type),
            });
        }

        return result;
    }
}