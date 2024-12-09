using System;

namespace NProtoBufDecoder;

public class UnknownWireTypeException(WireType unknown) : Exception($"Unknow Wire Type: {unknown}") { }