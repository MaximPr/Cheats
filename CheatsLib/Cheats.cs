using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CheatsLib
{
    public class Cheats<T>
    {
        public Regex PatternCheat = new Regex("/( )?(?<message>.+)");
        private Regex argsParser = new Regex(@"(?<name>\w+)(?<args> .*)?");
        private char[] separators = new char[] { ' ' };
        private Dictionary<string, Action<T, string[]>> cheatDic = new Dictionary<string, Action<T, string[]>>();
        private List<KeyValuePair<string, string>> cheatsDescr = new List<KeyValuePair<string, string>>();

        public Cheats()
        {
        }

        public void Clear()
        {
            cheatDic.Clear();
            cheatsDescr.Clear();
        }

        public bool TryRunCheat(string InputMessage, T person)
        {
            var m = PatternCheat.Match(InputMessage);
            if (m.Success)
            {
                string CheatmMessage = m.Groups["message"].Value;
                return RunCheat(CheatmMessage, person);
            }
            return false;
        }

        public bool RunCheat(string message, T p)
        {
            var m = argsParser.Match(message);
            var name = m.Groups["name"].Value;
            name = name.ToLower();
            var argsStr = m.Groups["args"].Value;
            string[] args = argsStr.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            Action<T, string[]> action;
            if (cheatDic.TryGetValue(args.Length + name, out action))
            {
                action(p, args);
                return true;
            }

            return false;
        }

        public string GetCheatDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in cheatsDescr)
            {
                sb.Append(pair.Key);
                if (!string.IsNullOrEmpty(pair.Value))
                    sb.AppendLine(" - " + pair.Value);
            }

            return sb.ToString();
        }

        private void Register(string name, int argsCount, Action<T, string[]> func, string title, string description)
        {
            name = name.ToLower();
            cheatDic.Add(argsCount + name, func);
            cheatsDescr.Add(new KeyValuePair<string, string>(title, description));

            cheatsDescr.Sort((x, y) => string.Compare(x.Key, y.Key));
        }

        public void RegisterCheat(string name, Action<T> f, string descr = "")
        {
            Register(name, 0, (p, x) => f(p), "/" + name, descr);
        }

        public void RegisterCheat(string name, Action<T, string> f, string descr = "")
        {
            Register(name, 1, (p, x) => f(p, x[0]), "/" + name + " string", descr);
        }

        public void RegisterCheat(string name, Action<T, int> f, string descr = "")
        {
            Register(name, 1, (p, x) => f(p, int.Parse(x[0])), "/" + name + " int", descr);
        }

        public void RegisterCheat(string name, Action<T, string, bool> f, string descr = "")
        {
            Register(name, 2, (p, x) => f(p, x[0], bool.Parse(x[1])), "/" + name + " string bool", descr);
        }

        public void RegisterCheat(string name, Action<T, int, int> f, string descr = "")
        {
            Register(name, 2, (p, x) => f(p, int.Parse(x[0]), int.Parse(x[1])), "/" + name + " int int", descr);
        }

        public void RegisterCheat(string name, Action<T, int, string> f, string descr = "")
        {
            Register(name, 2, (p, x) => f(p, int.Parse(x[0]), x[1]), "/" + name + " int string", descr);
        }

        public void RegisterCheat(string name, Action<T, string, int> f, string descr = "")
        {
            Register(name, 2, (p, x) => f(p, x[0], int.Parse(x[1])), "/" + name + " string int", descr);
        }

        public void RegisterCheat(string name, Action<T, string, string> f, string descr = "")
        {
            Register(name, 2, (p, x) => f(p, x[0], x[1]), "/" + name + " string string", descr);
        }

        public void RegisterCheat(string name, Action<T, string, string, string> f, string descr = "")
        {
            Register(name, 3, (p, x) => f(p, x[0], x[1], x[2]), "/" + name + " string string string", descr);
        }

        public void RegisterCheat(string name, Action<T, int, int, int> f, string descr = "")
        {
            Register(name, 3, (p, x) => f(p, int.Parse(x[0]), int.Parse(x[1]), int.Parse(x[2])), "/" + name + " int int int", descr);
        }

        public void RegisterCheat<T1>(string name, Action<T, T1> f, string descr = "")
        {
            var args = f.GetType().GetGenericArguments().ToList();
            args.RemoveAt(0);
            Register(name, args.Count,
                (p, x) => f(p,
                    (T1)Convert.ChangeType(x[0], typeof(T1), CultureInfo.InvariantCulture)
                ),
                "/" + name + " " + string.Join(" ", args.Select(x => x.Name).ToArray()),
                descr
            );
        }

        public void RegisterCheat<T1, T2>(string name, Action<T, T1, T2> f, string descr = "")
        {
            var args = f.GetType().GetGenericArguments().ToList();
            args.RemoveAt(0);
            Register(name, args.Count,
                (p, x) => f(p,
                    (T1)Convert.ChangeType(x[0], typeof(T1), CultureInfo.InvariantCulture),
                    (T2)Convert.ChangeType(x[1], typeof(T2), CultureInfo.InvariantCulture)
                ),
                "/" + name + " " + string.Join(" ", args.Select(x => x.Name).ToArray()),
                descr
            );
        }

        public void RegisterCheat<T1, T2, T3>(string name, Action<T, T1, T2, T3> f, string descr = "")
        {
            var args = f.GetType().GetGenericArguments().ToList();
            args.RemoveAt(0);
            Register(name, args.Count,
                (p, x) => f(p,
                    (T1)Convert.ChangeType(x[0], typeof(T1), CultureInfo.InvariantCulture),
                    (T2)Convert.ChangeType(x[1], typeof(T2), CultureInfo.InvariantCulture),
                    (T3)Convert.ChangeType(x[2], typeof(T3), CultureInfo.InvariantCulture)
                ),
                "/" + name + " " + string.Join(" ", args.Select(x => x.Name).ToArray()),
                descr
            );
        }

        public void RegisterCheat<T1, T2, T3, T4>(string name, Action<T, T1, T2, T3, T4> f, string descr = "")
        {
            var args = f.GetType().GetGenericArguments().ToList();
            args.RemoveAt(0);
            Register(name, args.Count,
                (p, x) => f(p,
                    (T1)Convert.ChangeType(x[0], typeof(T1), CultureInfo.InvariantCulture),
                    (T2)Convert.ChangeType(x[1], typeof(T2), CultureInfo.InvariantCulture),
                    (T3)Convert.ChangeType(x[2], typeof(T3), CultureInfo.InvariantCulture),
                    (T4)Convert.ChangeType(x[3], typeof(T4), CultureInfo.InvariantCulture)
                ),
                "/" + name + " " + string.Join(" ", args.Select(x => x.Name).ToArray()),
                descr
            );
        }
    }
}

