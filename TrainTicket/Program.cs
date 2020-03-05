using System;

// https://atcoder.jp/contests/abc079/tasks/abc079_c
namespace TrainTicket
{
    class Program
    {
        private class Node
        {
            public enum OpType
            {
                None,
                Add,
                Sub,
            }

            const int ExpectedValue = 7;
            int m_value;
            Node m_plus;
            Node m_minus;
            OpType m_op;

            public Node(int value, OpType op = OpType.None)
            {
                m_value = value;
                m_op = op;
            }

            public void Insert(int value)
            {
                if (m_plus == null)
                {
                    m_plus = new Node(value, OpType.Add);
                }
                else
                {
                    m_plus.Insert(value);
                }

                if (m_minus == null)
                {
                    m_minus = new Node(value, OpType.Sub);
                }
                else
                {
                    m_minus.Insert(value);
                }
            }

            public bool Exec()
            {
                return Eval(0, "");
            }

            private bool HasNext()
            {
                if (m_plus != null) return true;
                if (m_minus != null) return true;
                return false;
            }

            private bool Eval(int value, string s, int count = 0)
            {
                var result = 0;
                var expr = s;
                var found = false;

                if (count > 0)
                {
                    if (m_op == OpType.Add)
                    {
                        result = value + m_value;
                        expr += $"+{m_value}";
                    }
                    else if (m_op == OpType.Sub)
                    {
                        result = value - m_value;
                        expr += $"-{m_value}";
                    }
                }
                else
                {
                    result = m_value;
                    expr = m_value.ToString();
                }

                if (m_plus != null && m_plus.Eval(result, expr, count + 1)) found = true;
                if (m_minus != null && m_minus.Eval(result, expr, count + 1)) found = true;
                if (result == ExpectedValue && !HasNext())
                {
                    Console.WriteLine($"{expr}={result}");
                    found = true;
                }
                return found;
            }
        }

        static void Main(string[] args)
        {
            bool TryParse(string s, out int value)
            {
                if (!int.TryParse(s, out value) || !(value >= 0 && value <= 9))
                {
                    Console.WriteLine($"0以上9以下の値を入れてください 入力された値: '{s}'");
                    return false;
                }
                return true;
            }

            var s = Console.ReadLine();
            if (s.Length != 4)
            {
                Console.WriteLine("文字数が一致しません。ABCDの形式で4文字入力してください");
                return;
            }

            var A = 0;
            var B = 0;
            var C = 0;
            var D = 0;
            if (!TryParse(s[0].ToString(), out A)) { return; }
            if (!TryParse(s[1].ToString(), out B)) { return; }
            if (!TryParse(s[2].ToString(), out C)) { return; }
            if (!TryParse(s[3].ToString(), out D)) { return; }

            var node = new Node(A);
            node.Insert(B);
            node.Insert(C);
            node.Insert(D);
            if (!node.Exec())
            {
                Console.WriteLine("答えが存在しない値です");
            }
        }
    }
}
