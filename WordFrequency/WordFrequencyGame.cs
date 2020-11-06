﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFrequency
{
    public class WordFrequencyGame
    {
        private const string SpacePatten = @"\s+";

        public string GetResult(string inputStr)
        {
            //split the input string with 1 to n pieces of spaces
            string[] splitWords = Regex.Split(inputStr, SpacePatten);

            if (splitWords.Length == 1)
            {
                return inputStr + " 1";
            }

            try
            {
                List<Input> inputList = splitWords.Select(word => new Input(word, 1)).ToList();
                //get the map for the next step of sizing the same word
                Dictionary<string, List<Input>> map = GetListMap(inputList);

                List<Input> list = new List<Input>();
                foreach (var entry in map)
                {
                    Input input = new Input(entry.Key, entry.Value.Count);
                    list.Add(input);
                }

                inputList = list;

                inputList.Sort((w1, w2) => w2.WordCount - w1.WordCount);

                List<string> strList = new List<string>();

                //stringJoiner joiner = new stringJoiner("\n");
                foreach (Input w in inputList)
                {
                    string s = w.Value + " " + w.WordCount;
                    strList.Add(s);
                }

                return string.Join("\n", strList.ToArray());
            }
            catch (Exception e)
            {
                return "Calculate Error";
            }
        }

        private Dictionary<string, List<Input>> GetListMap(List<Input> inputList)
        {
            Dictionary<string, List<Input>> map = new Dictionary<string, List<Input>>();
            foreach (var input in inputList)
            {
                //       map.computeIfAbsent(input.getValue(), k -> new ArrayList<>()).add(input);
                if (!map.ContainsKey(input.Value))
                {
                    List<Input> arr = new List<Input>();
                    arr.Add(input);
                    map.Add(input.Value, arr);
                }
                else
                {
                    map[input.Value].Add(input);
                }
            }

            return map;
        }
    }
}