﻿namespace Contract.Exceptions.Abstractions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}
