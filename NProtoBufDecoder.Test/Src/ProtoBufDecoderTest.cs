namespace NProtoBufDecoder.Test;

public class ProtoBufDecoderTest {
    [Fact]
    public void DecodeTest() {
        IEnumerable<ProtoBufNode> nodes = ProtoBufDecoder.Decode(Convert.FromHexString("09000000000000F83F150000C03F18B99497EEFBFFFFFFFF0120F2B8DCC39A95C594E10128C7EBE8910C308EC7A3BCE5EABAEB9E01388DD7D1A308409B8EC7F8CAD5F5D63D4DC7353AC2518EE3885756EBD69E5D39CAC5BD61721C77A8A91429E1680170007A076461726B72726282010200FF8A017309000000000000F83F150000C03F18B99497EEFBFFFFFFFF0120F2B8DCC39A95C594E10128C7EBE8910C308EC7A3BCE5EABAEB9E01388DD7D1A308409B8EC7F8CAD5F5D63D4DC7353AC2518EE3885756EBD69E5D39CAC5BD61721C77A8A91429E1680170007A076461726B72726282010200FF"));
        Assert.Equal(1.5d, FirstFieldNumber(nodes, 1).AsDouble());
        Assert.Equal(1.5f, FirstFieldNumber(nodes, 2).AsFloat());
        Assert.Equal(-1111111111, FirstFieldNumber(nodes, 3).AsInt32());
        Assert.Equal(-2222222222222222222, FirstFieldNumber(nodes, 4).AsInt64());
        Assert.Equal(3258594759, FirstFieldNumber(nodes, 5).AsUint32());
        Assert.Equal(11445594259076998030, FirstFieldNumber(nodes, 6).AsUint64());
        Assert.Equal(-1111111111, FirstFieldNumber(nodes, 7).AsSint32());
        Assert.Equal(-2222222222222222222, FirstFieldNumber(nodes, 8).AsSint64());
        Assert.Equal(3258594759, FirstFieldNumber(nodes, 9).AsFixed32());
        Assert.Equal(11445594259076998030, FirstFieldNumber(nodes, 10).AsFixed64());
        Assert.Equal(-1111111111, FirstFieldNumber(nodes, 11).AsSfixed32());
        Assert.Equal(-2222222222222222222, FirstFieldNumber(nodes, 12).AsSfixed64());
        Assert.True(FirstFieldNumber(nodes, 13).AsBool());
        Assert.False(FirstFieldNumber(nodes, 14).AsBool());
        Assert.Equal("darkrrb", FirstFieldNumber(nodes, 15).AsString());
        Assert.Equal([0, 255], FirstFieldNumber(nodes, 16).AsBytes().ToArray());
        Assert.IsAssignableFrom<IEnumerable<ProtoBufNode>>(FirstFieldNumber(nodes, 17).AsMessage());
    }

    private static ProtoBufNode FirstFieldNumber(IEnumerable<ProtoBufNode> nodes, ulong number) {
        return nodes.First(n => n.FieldNumber == number);
    }
}

file enum SomeEnum {
    Enum1 = 1,
    Enum2 = 2,
}