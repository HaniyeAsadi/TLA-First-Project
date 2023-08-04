using System;
using System.Collections.Generic;
using System.Linq;

Dictionary<Tuple<string, char>, List<string>> NFA = new Dictionary<Tuple<string, char>, List<string>>();            
string[] states = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
string[] alphabet = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
string[] final_states = Console.ReadLine().
                    Split(new char[] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
int numberofedges =int.Parse(Console.ReadLine());
for (int i = 0; i < numberofedges; i++)
{
    string[] edge = Console.ReadLine().Split(',');
    Tuple<string, char> t= new Tuple<string, char>(edge[0], char.Parse(edge[1]));
    List<string> dest = new List<string>(){edge[2]};
    if( NFA.ContainsKey(t) ){
        List<string> m = NFA[t];
        m.Add(edge[2]);
        NFA[t] = m;
    }
    else
        NFA.Add(t,dest);
}
ChangeAutomata(states, alphabet, states[0], final_states, NFA);

static List<string> CheckLambda(Dictionary<Tuple<string, char>, List<string>> NFA, string k)
{
    List<string> states = new List<string>();
    states.Add(k);
    List<string> lambda_trans = new List<string>();
    lambda_trans.Add(k);
    while(states.Count != 0)
    {
        var current_state = states[0];
        Tuple<string, char> cr = new Tuple<string,char>(current_state,'$');
        if(NFA.ContainsKey(cr)){
            List<string> end_states = NFA[cr];
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
static void ChangeAutomata(string[] states, string[] alphabet, string initial_state, string[] final_states, Dictionary<Tuple<string, char>, List<string>> NFA)
{
    List<List<string>> dfa_states = new();
    List<string> dfa_states_name = new();
    dfa_states.Add(new List<string>(){initial_state});
    dfa_states_name.Add(initial_state);
    for(int d=0; d< dfa_states.Count; d++){
        foreach(var alp in alphabet)
        {
            string start_states = String.Join(",", dfa_states[d]);
            List<string> next_states = new List<string>();
            foreach(var st in dfa_states[d]){
                Tuple<string, char> t = new Tuple<string, char>(start_states, char.Parse(alp));
                List<string> lambda_trans = CheckLambda(NFA, st);
                foreach (var la in lambda_trans)
                {
                    if(NFA.ContainsKey(new Tuple<string, char>(la, char.Parse(alp))))
                        foreach (var item in NFA[new Tuple<string, char>(la, char.Parse(alp))])
                        {
                            if(!next_states.Contains(item))
                                next_states.Add(item);
                        }
                }
                int i = 0 ;
                while(i< next_states.Count){
                    if(NFA.ContainsKey(new Tuple<string, char>(next_states[i], '$'))){
                        foreach(var item in NFA[new Tuple<string, char>(next_states[i], '$')]){
                            if(!next_states.Contains(item))
                                next_states.Add(item);
                        }
                    }
                    i++;
                }
                next_states.Sort();
                if(next_states.Count == 0){
                    if(!dfa_states_name.Contains("trap"))
                        dfa_states_name.Add("trap");
                }
            }
            if(next_states.Count != 0){
                string s = String.Join(",", next_states.ToArray());
                if(!dfa_states_name.Contains(s)){
                    dfa_states.Add(next_states);
                    dfa_states_name.Add(s);
                }
            }

        }
    }

    
    System.Console.WriteLine(dfa_states_name.Count.ToString());
}
