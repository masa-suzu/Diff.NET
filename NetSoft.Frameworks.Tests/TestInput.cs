﻿using System.Collections.Generic;
using System.Linq;

namespace NetSoft.Frameworks.Tests
{
    public class TestInput
    {
        public static IEnumerable<object[]> Strings()
        {
            yield return new object[]{
                    new string[]{"h", "e" },
                    new string[]{"s","h", "e" },
                    new EditScript<string>(){
                        new Edit<string>(){Change = 0, Value = "e" },
                        new Edit<string>(){Change = 0, Value = "h" },
                        new Edit<string>(){Change = 1, Value = "s" },
                    }
                };
            yield return new object[]{
                    new string[]{"s","h", "e" },
                    new string[]{   "h", "e" },
                    new EditScript<string>(){
                        new Edit<string>(){Change = 0, Value =   "e" },
                        new Edit<string>(){Change = 0, Value =   "h" },
                        new Edit<string>(){Change = -1, Value =  "s" },
                    }
                };
            yield return new object[]{
                    new string[]{      "x=", "1*", "3", ";"},
                    new string[]{"let","x=", "1*",       ";"},
                    new EditScript<string>(){
                        new Edit<string>(){Change = 0, Value =  ";" },
                        new Edit<string>(){Change = -1, Value =  "3" },
                        new Edit<string>(){Change = 0, Value =  "1*" },
                        new Edit<string>(){Change = 0, Value =  "x=" },
                        new Edit<string>(){Change = 1, Value =  "let" },
                    }
                };
            yield return new object[]{
                    new string[]{"a","b", "c" },
                    new string[]{"a","x","c","d" },

                    new EditScript<string>(){
                        new Edit<string>(){Change = 1, Value =  "d" },
                        new Edit<string>(){Change = 0, Value =  "c" },
                        new Edit<string>(){Change = 1, Value =  "x" },
                        new Edit<string>(){Change = -1, Value =  "b" },
                        new Edit<string>(){Change = 0, Value =  "a" },

                    }
                };
            yield return new object[]{
                    new string[]{"k","i","t","t","e","n" },
                    new string[]{"s","i","t","t","i","n","g" },

                    new EditScript<string>(){
                        new Edit<string>(){Change = 1, Value =  "g" },
                        new Edit<string>(){Change = 0, Value =  "n" },
                        new Edit<string>(){Change = 1, Value =  "i" },
                        new Edit<string>(){Change = -1, Value =  "e" },
                        new Edit<string>(){Change = 0, Value =  "t" },
                        new Edit<string>(){Change = 0, Value =  "t" },
                        new Edit<string>(){Change = 0, Value =  "i" },
                        new Edit<string>(){Change = 1, Value =  "s" },
                        new Edit<string>(){Change = -1, Value =  "k" },
                    }
            };
        }

        public static IEnumerable<object[]> Integers()
        {
            yield return new object[]{
                    new int[]{ 1,2,3 },
                    new int[]{ 2,3,6},
                    new EditScript<int>(){
                        new Edit<int>(){Change = 1, Value =  6 },
                        new Edit<int>(){Change =  0, Value =  3 },
                        new Edit<int>(){Change =  0, Value =  2 },
                        new Edit<int>(){Change = -1, Value =  1 },
                    }
            };
        }
        public static IEnumerable<object[]> StringWithSurrogatePair()
        {
            yield return new object[]{
                    "xあz",
                    "x𩸽z",
                    new EditScript<string>(){
                        new Edit<string>(){Change = 0, Value = "z" },
                        new Edit<string>(){Change =  1, Value =  "𩸽" },
                        new Edit<string>(){Change =  -1, Value =  "あ" },
                        new Edit<string>(){Change = 0, Value =  "x" },
                    }
            };
        }
        public static IEnumerable<object[]> Nulls()
        {
            yield return new object[]{
                null,
                new string[] {"null" },
            };
            yield return new object[]{
                new string[] {"null" },
                null,
            };
        }
        public static IEnumerable<object[]> ContainsNull()
        {
            yield return new object[]{
                new string[] {"notnull",null,"notnull" },
                new string[] {"notnull","notnull" },
                new EditScript<string>(){
                    new Edit<string>(){Change =  0, Value = "notnull" },
                    new Edit<string>(){Change = -1, Value = null },
                    new Edit<string>(){Change =  0, Value = "notnull" },
                }
            };
            yield return new object[]{
                new string[] {"notnull","notnull" },
                new string[] {"notnull",null,"notnull" },
                new EditScript<string>(){
                    new Edit<string>(){Change =  0, Value = "notnull" },
                    new Edit<string>(){Change = 1, Value = null },
                    new Edit<string>(){Change =  0, Value = "notnull" },
                }
            };
            yield return new object[]{
                new string[] { null },
                new string[] { null },
                new EditScript<string>(){
                    new Edit<string>(){Change =  0, Value = null },
                }
            };

        }
    }

    public static class BenchmarkInput
    {
        public static IEnumerable<object[]> x500()
        {
            yield return new object[]{
                    Enumerable.Range(0,500).ToArray(),
                    Enumerable.Range(0,500).ToArray(),
                    Enumerable
                    .Range(0,500)
                    .Select(i =>new Edit<int>(){Change = 0, Value = i })
                    .To()
            };
        }
        public static IEnumerable<object[]> x1000()
        {
            yield return new object[]{
                    Enumerable.Range(0,1000).ToArray(),
                    Enumerable.Range(0,1000).ToArray(),
                    Enumerable
                    .Range(0,1000)
                    .Select(i =>new Edit<int>(){Change = 0, Value = i })
                    .To()
            };
        }
        public static IEnumerable<object[]> x2000()
        {
            yield return new object[]{
                    Enumerable.Range(0,2000).ToArray(),
                    Enumerable.Range(0,2000).ToArray(),
                    Enumerable
                    .Range(0,2000)
                    .Select(i =>new Edit<int>(){Change = 0, Value = i })
                    .To()
            };
        }

        private static EditScript<int> To(this IEnumerable<Edit<int>> from)
        {
            var to = new EditScript<int>();
            foreach (var item in from.Reverse())
            {
                to.Push(item);
            }
            return to;
        }
    }

}