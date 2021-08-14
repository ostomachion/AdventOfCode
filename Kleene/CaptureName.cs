using System;
using System.Linq;
using System.Collections.Generic;

namespace Kleene
{
    public class CaptureName
    {
        public IEnumerable<string> Parts { get; }
        public string Head => Parts.First();
        public CaptureName? Tail => Parts.Count() == 1 ? null : new(Parts.Skip(1).ToArray());

        public CaptureName(string dottedName) : this(dottedName.Split('.')) { }

        public CaptureName(params string[] parts)
        {
            if (!parts.Any())
                throw new ArgumentException("CaptureName cannot be empty.", nameof(parts));

            Parts = parts;
        }

        public static implicit operator CaptureName(string value) => new(value);

        public override string ToString() => String.Join('.', Parts);
    }
}
