namespace NProtoBufDecoder.Test;

public class ProtoBufDecoderTest {
    [Fact]
    public void DecodeTest0() {
        IEnumerable<ProtoBufNode> enumerable = ProtoBufDecoder.Decode(Convert.FromHexString("409b8ec7f8cad5f5d63d"));
    }

    [Fact]
    public void DecodeTest() {
        IEnumerable<ProtoBufNode> nodes = ProtoBufDecoder.Decode(Convert.FromHexString("09000000000000F83F150000C03F18B99497EEFBFFFFFFFF0120F2B8DCC39A95C594E10128C7EBE8910C308EC7A3BCE5EABAEB9E01388DD7D1A308409B8EC7F8CAD5F5D63D4DC7353AC2518EE3885756EBD69E5D39CAC5BD61721C77A8A91429E1680170007A076461726B72726282010200FF8A017309000000000000F83F150000C03F18B99497EEFBFFFFFFFF0120F2B8DCC39A95C594E10128C7EBE8910C308EC7A3BCE5EABAEB9E01388DD7D1A308409B8EC7F8CAD5F5D63D4DC7353AC2518EE3885756EBD69E5D39CAC5BD61721C77A8A91429E1680170007A076461726B72726282010200FF"));
        {
            Assert.Equal(
                1.5d,
                nodes.N(1).TryAsDouble(out double result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                1.5f,
                nodes.N(2).TryAsFloat(out float result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -1111111111,
                nodes.N(3).TryAsInt32(out int result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -2222222222222222222,
                nodes.N(4).TryAsInt64(out long result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                3258594759,
                nodes.N(5).TryAsUint32(out uint result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                11445594259076998030,
                nodes.N(6).TryAsUint64(out ulong result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -1111111111,
                nodes.N(7).TryAsSint32(out int result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -2222222222222222222,
                nodes.N(8).TryAsSint64(out long result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                3258594759,
                nodes.N(9).TryAsFixed32(out uint result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                11445594259076998030,
                nodes.N(10).TryAsFixed64(out ulong result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -1111111111,
                nodes.N(11).TryAsSfixed32(out int result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                -2222222222222222222,
                nodes.N(12).TryAsSfixed64(out long result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.True(
                nodes.N(13).TryAsBool(out bool result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.False(
                nodes.N(14).TryAsBool(out bool result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                "darkrrb",
                nodes.N(15).TryAsString(out string? result)
                    ? result
                    : throw new Exception()
            );
        }
        {
            Assert.Equal(
                [0, 255],
                nodes.N(16).TryAsBytes(out ReadOnlyMemory<byte> result)
                    ? result.ToArray()
                    : throw new Exception()
            );
        }
        {
            Assert.IsAssignableFrom<IEnumerable<ProtoBufNode>>(
                nodes.N(17).TryAsMessage(out IEnumerable<ProtoBufNode>? result)
                    ? result
                    : throw new Exception()
            );
        }
    }

}
file static class Util {
    public static ProtoBufNode N(this IEnumerable<ProtoBufNode> nodes, ulong number) {
        return nodes.First(n => n.FieldNumber == number);
    }
}

file enum SomeEnum {
    Enum1 = 1,
    Enum2 = 2,
}