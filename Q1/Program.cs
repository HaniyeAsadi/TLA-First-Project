using System;
using System.Collections.Generic;
using System.Linq;

Dictionary<Tuple<string, char>, List<string>> automata = new Dictionary<Tuple<string, char>, List<string>>();
string[] states = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
string[] alphabet = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
string[] final_states = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
int numberOfEdges =int.Parse(Console.ReadLine());
for (int i = 0; i < numberOfEdges; i++)
{
    string[] edge = Console.ReadLine().Split(',');
    Tuple<string, char> t= new Tuple<string, char>(edge[0], char.Parse(edge[1]));
    List<string> dest = new List<string>(){edge[2]};
    if( automata.ContainsKey(t) ){
        List<string> m = automata[t];
        m.Add(edge[2]);
        automata[t] = m;
    }
    else
        automata.Add(t,dest);
}
char[] check = Console.ReadLine().ToCharArray();

Console.Write(CheckFiniteAutomata(states, alphabet, final_states, automata, check));

static List<string> CheckString(List<string> current_states, Dictionary<Tuple<string, char>, List<string>> automata, char character)
{
    List<string> next_states = new List<string>();
    foreach (var item in current_states)
    {
        if(automata.ContainsKey(new Tuple<string, char>(item,character))){
            var m= automata[new Tuple<string,char>(item,character)];
            foreach (var k in m)
            {
                List<string> lambda = new List<string>();
                lambda = CheckLambda(automata,k); 
                foreach (var l in lambda)
                {
                    if(!next_states.Contains(l)){
                        next_states.Add(l);
                    }
                }
            }
        }
    }
    return next_states;
}

static List<string> CheckLambda(Dictionary<Tuple<string, char>, List<string>> automata, string k)
{
    List<string> states = new List<string>();
    states.Add(k);
    List<string> lambda_trans = new List<string>();
    lambda_trans.Add(k);
    while(states.Count != 0)
    {
        var current_state = states[0];
        Tuple<string, char> cr = new Tuple<string,char>(current_state,'$');
        if(automata.ContainsKey(cr)){
            List<string> end_states = automata[cr];
            foreach (var es in end_states)
            {
                if(!lambda_trans.Contains(es))
                {
                    states.Add(es);
                    lambda_trans.Add(es);
                }
            }
        }
        states.RemoveAt(0);
    }
    return lambda_trans;
}

static string CheckFiniteAutomata(string[] states, string[] alphabet , string[] final_states, Dictionary<Tuple<string, char>, List<string>> automata, char[] check)
{
    string res="Rejected";
    List<string> next_states = CheckLambda(automata, states[0]);
    if(check.Length ==0 || check[0]==' ')
    {
        List<string> empty_string = next_states;
        empty_string.AddRange(CheckLambda(automata, states[0]));                
        foreach (var item in empty_string)
        {
            if(final_states.Contains(item)){
                res = "Accepted";
                break;
            } 
        }
    }
    else{
        foreach (var item in check)
            next_states = CheckString(next_states, automata, item);
        foreach (var item in next_states)
        {
            if(final_states.Contains(item)){
                res = "Accepted";
                break;
            } 
        }
    }
    return res;
}