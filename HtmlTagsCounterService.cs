using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HtmlTagsAPI
{
    public class HtmlTagsCounterService : IHtmlTagsCounterService
    {
        public HtmlTagsCounter Get(string siteName)
        {
            HttpClient client = new HttpClient();
            List<KeyValuePair<string, int>> tags = new List<KeyValuePair<string, int>>();
            List<string> tagList = new List<string>();
            string source;
            string pattern = @"(?<=</?)([^ >/]+)";

            using (HttpResponseMessage response = client.GetAsync(siteName).Result)
            {
                using (HttpContent content = response.Content)
                {
                    source = content.ReadAsStringAsync().Result;
                }
            }

            MatchCollection matches = Regex.Matches(source, pattern);
            int count = matches.Count;

            for (int i = 0; i < matches.Count; i++)
            {
                tagList.Add(matches[i].ToString());
            }

            var distinctTags = tagList.GroupBy(i => i);

            foreach (var grp in distinctTags)
            {
                tags.Add(new KeyValuePair<string, int>(grp.Key, grp.Count()));
            }


            return new HtmlTagsCounter
            {
                HtmlTagsWithCount = tags
            };
            
        }
    }
}
