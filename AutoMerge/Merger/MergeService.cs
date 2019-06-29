using System;
using System.Collections.Generic;
using System.Linq;
using AutoMerge.Extensions;

namespace AutoMerge.Merger
{
    
    public static class MergeService
    {
        public static string[] ThreeWayMerge(string[] source, string[] changeA, string[] changeB)
        {
            var mappingA = MapChanges(source, changeA);
            var mappingB = MapChanges(source, changeB);

            var result = MergeMappings(mappingA, mappingB);
            
            return result;
        }

        private static List<LineMap> MapChanges(string[] source, string[] change)
        {
            var mapping = new List<LineMap>();
            var maxLength = MaxLength(source.ToList(), change.ToList());
            var changeShift = 0;
            var sourceShift = 0;

            for (var i = 0; i < maxLength; i++)
            {
                var leftOver = GetLeftovers(source.ToList(), change.ToList(), i, sourceShift, changeShift);

                if (leftOver != null)
                {
                    mapping.AddRange(leftOver.Select(item => new LineMap {Change = Change.Add, Body = item}));
                    break;
                }

                if (source[i + sourceShift].CleanString() == change[i + changeShift].CleanString())
                {
                    mapping.Add(new LineMap
                    {
                        Change = Change.None,
                        Body = source[i + sourceShift]
                    });
                }
                else
                {
                    if (IsLineDeleted(source, change, i, sourceShift, changeShift))
                    {
                        mapping.Add(new LineMap
                        {
                            Change = Change.Delete,
                            Body = source[i + sourceShift]
                        });
                        changeShift--;
                    }
                    else
                    {
                        if (IsLineMoved(source, change, i, sourceShift))
                        {
                            mapping.Add(new LineMap
                            {
                                Change = Change.Add,
                                Body = change[i + changeShift]
                            });
                            sourceShift--;
                        }
                        else
                        {
                            mapping.Add(new LineMap
                            {
                                Change = Change.Edit,
                                Body = change[i + changeShift]
                            });
                        }
                        
                    }
                }
            }
            return mapping;
        }

        private static string[] MergeMappings(List<LineMap> changeA, List<LineMap> changeB)
        {
            var result = new List<string>();
            var maxLength = MaxLength(changeA, changeB);
            var changeAShift = 0;
            var changeBShift = 0;
            for (var i = 0; i < maxLength; i++)
            {
                var a = i + changeAShift;
                var b = i + changeBShift;
                var leftOver = GetLeftovers(changeA, changeB, i, changeAShift, changeBShift);

                if (leftOver != null)
                {
                    result.AddRange(leftOver.Select(item => item.Body));
                    break;
                }
                if (changeA[a].Change == Change.None && changeB[b].Change == Change.None)
                {
                    result.Add(changeA[a].Body);
                    continue;
                }

                if (changeA[a].Change == Change.Edit && changeB[b].Change == Change.Edit)
                {
                    result.Add("-- Conflict A --" + changeA[a].Body);
                    result.Add("-- Conflict B --" + changeB[b].Body);
                    continue;
                }
                
                if (changeA[a].Change == Change.None && changeB[b].Change == Change.Edit)
                {
                    result.Add(changeB[b].Body);
                    continue;
                }
                
                if (changeA[a].Change == Change.Edit && changeB[b].Change == Change.None)
                {
                    result.Add(changeA[a].Body);
                    continue;
                }
                
                if (changeA[a].Change == Change.Add)
                {
                    var iterator = a;
                    while (iterator < changeA.Count && changeA[iterator].Change == Change.Add)
                    {
                        result.Add(changeA[iterator].Body);
                        iterator++;
                        changeAShift++; 
                    }
                    changeAShift--;
                    changeBShift--;
                    continue;
                }
                
                if (changeB[b].Change == Change.Add)
                {
                    var iterator = b;
                    while (iterator < changeB.Count && changeB[iterator].Change == Change.Add)
                    {
                        result.Add(changeB[iterator].Body);
                        iterator++;
                        changeBShift++; 
                    }
                    changeAShift--;
                    changeBShift--;
                    continue;
                }
                
                if (changeA[a].Change == Change.None && changeB[b].Change == Change.Delete
                    || changeA[a].Change == Change.Delete && changeB[b].Change == Change.None
                    || changeA[a].Change == Change.Delete && changeB[b].Change == Change.Delete)
                {
                    continue;
                }
                
                
            }

            return result.ToArray();
        }

        private static bool IsLineMoved(string[] source, string[] change, int i, int sourceShift)
        {
            var sourceLineICount = source.Count(line => line == source[i + sourceShift]);
            var changeLineICount = change.Count(line => line == source[i + sourceShift]);
            return sourceLineICount == changeLineICount;
        }
        
        private static bool IsLineDeleted(string[] source, string[] change, int i, int sourceShift, int changeShift)
        {
            
            return source.Length > i + 1 + sourceShift && source[i + 1 + sourceShift] == change[i + changeShift];
        }


        private static List<T> GetLeftovers<T>(List<T> first, List<T> second, int i, int firstShift, int secondShift)
        {
            if (first.Count() == i + firstShift)
            {
                return second.Where(item => second.IndexOf(item) >= i + secondShift).ToList();
            }
            if (second.Count() == i + secondShift)
            {
                return first.Where(item => first.IndexOf(item) >= i + firstShift).ToList();
            }
            return null;
        }

        private static int MaxLength<T>(List<T> first, List<T> second)
        {
            return first.Count > second.Count ? first.Count : second.Count;
        }
    }
}