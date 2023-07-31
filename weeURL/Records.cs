using System;
using MongoDB.Bson;

namespace weeURL.Records
{
    public record URL(string shortURL, string longURL);
}

