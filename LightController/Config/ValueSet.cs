﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace LightController.Config
{
    // Range of positive values
    public class ValueSet
    {
        private List<Range> ranges;

        public bool Empty => ranges.Count == 0;

        private ValueSet() 
        {
            ranges = new List<Range>();
        }

        public ValueSet(string values)
        {
            string[] strings = values.Split(',');
            ranges = new List<Range>(strings.Length);
            foreach (string s in strings)
            {
                string value = s.Trim();
                if (!string.IsNullOrWhiteSpace(s))
                    ranges.Add(new Range(value));
            }
        }

        public ValueSet(int start, int end)
        {
            ranges = new List<Range>();
            ranges.Add(new Range(start, end));
        }

        public bool GetOverlap(ValueSet other, out ValueSet result)
        {
            result = new ValueSet();

            foreach(Range myRange in ranges)
            {
                foreach(Range otherRange in other.ranges)
                {
                    if (myRange.GetOverlap(otherRange, out Range newRange))
                        result.ranges.Add(newRange);
                }
            }

            return !result.Empty;
        }

        public bool Contains(int value)
        {
            foreach (Range range in ranges)
            {
                if (range.Contains(value))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ranges.Count; i++)
            {
                Range range = ranges[i];
                range.AppendString(sb);
                if (i < ranges.Count - 1)
                    sb.Append(',');
            }    
            return sb.ToString();
        }

        public IEnumerable<int> EnumerateValues()
        {
            foreach(Range range in ranges)
            {
                foreach (int i in range)
                    yield return i;
            }
        }


        private class Range : IEnumerable<int>
        {
            private int start;
            private int end;

            public Range(string range)
            {
                if(range.Contains('-'))
                {
                    string[] split = range.Split(new[] { '-' }, 2);
                    start = int.Parse(split[0].Trim());
                    end = int.Parse(split[1].Trim());
                }
                else
                {
                    start = int.Parse(range);
                    end = start;
                }
            }

            public Range(int start, int end)
            {
                this.start = start;
                this.end = end;
            }

            public bool Contains(int value)
            {
                return value >= start && value <= end;
            }

            public bool GetOverlap(Range other, out Range result)
            {
                result = null;
                if (other.end < this.start)
                    return false;
                if (other.start > this.end)
                    return false;
                int start = Math.Max(this.start, other.start);
                int end = Math.Min(this.end, other.end);
                result = new Range(start, end);
                return true;
            }

            public void AppendString(StringBuilder sb)
            {
                if (start == end)
                    sb.Append(start);
                else
                    sb.Append(start).Append('-').Append(end);
            }

            public override string ToString()
            {
                if (start == end)
                    return start.ToString();
                else
                    return $"{start}-{end}";
            }

            public IEnumerator<int> GetEnumerator()
            {
                return Enumerable.Range(start, (start - end) + 1).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Enumerable.Range(start, (start - end) + 1).GetEnumerator();
            }
        }
    }
}
