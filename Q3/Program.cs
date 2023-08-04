using System;
using System.Collections.Generic;
using System.Linq;

Dictionary<Tuple<string, char>, string> DFA = new Dictionary<Tuple<string, char>, string>();            
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
    string dest = edge[2];
    DFA.Add(t,dest);
}
MinimizeDFA(states, alphabet, states[0], final_states, DFA);

static List<string> Check_IsReachable(string initial_state, string[] states ,string[] alphabet, Dictionary<Tuple<string, char>, string> DFA)
{
    List<string> rechable_stataes = new List<string>();
    rechable_stataes.Add(initial_state);
    int i=0;
    while(i < rechable_stataes.Count){
        foreach (var alp in alphabet){
            Tuple<string, char> t = new Tuple<string, char>(rechable_stataes[i], char.Parse(alp));
            if(!rechable_stataes.Contains(DFA[t]))
                rechable_stataes.Add(DFA[t]);
            if(rechable_stataes.Count == states.Length)
                break;
        }
        i++;
    }
    rechable_stataes.Sort();
    return rechable_stataes;
}
static List<List<string>> inSameList(List<List<string>> collect, string[] alphabet, Dictionary<Tuple<string, char>, string> DFA)
{
    List<List<string>> newSameStates = new List<List<string>>();
    List<List<string>> copy =new List<List<string>>();
    foreach(var y in collect){
        List<string> m = new List<string>();
        foreach(var x in y){
            m.Add(x);
        }
        copy.Add(m);
    }
    foreach(var item in copy){
        while(item.Count !=0){
            List<string> n = new List<string>();
            n.Add(item[0]);
            item.Remove(item[0]);
            string EqualState= null;
            int i = 0;
            List<string> c = new List<string>();
            foreach(var p in item){
                c.Add(p);
            }
            while(i< c.Count){
                bool inOneList = true;
                foreach(var alp in alphabet){
                    Tuple<string, char> t1 = Tuple.Create(n[0], char.Parse(alp));
                    Tuple<string, char> t2 = Tuple.Create(c[i], char.Parse(alp));
                    foreach(var col in collect){
                        if(col.Contains(DFA[t1])){
                            if(col.Contains(DFA[t2])){
                                EqualState = c[i];
                            }
                            else{
                                inOneList=false;
                            }
                        }
                    }
                }
                if(inOneList){
                    n.Add(EqualState);
                    item.Remove(EqualState);
                }
                i++;
            }
            newSameStates.Add(n);
        }
    }
    return newSameStates;
}
static int Classify(List<List<string>> collect,string[] alphabet ,Dictionary<Tuple<string, char>, string> DFA)
{
    List<List<string>> copy =new List<List<string>>();
    bool isequal = false;
    while(!isequal){
        foreach(var y in collect){
            List<string> m = new List<string>();
            foreach(var x in y){
                m.Add(x);
            }
            copy.Add(m);
        }
        List<List<string>> new_classify =  inSameList(copy, alphabet, DFA);
        if(new_classify.Count == collect.Count)
            isequal = true;
        else{
            collect.Clear();
            copy.Clear();
            foreach(var y in new_classify){
                List<string> m = new List<string>();
                foreach(var x in y){
                    m.Add(x);
                }
                collect.Add(m);
            }
        }
        
    }
    return collect.Count;
}
static void MinimizeDFA(string[] states, string[] alphabet, string initial_state, string[] final_states, Dictionary<Tuple<string, char>, string> DFA)
{
    List<string> reachable = Check_IsReachable(initial_state, states,alphabet, DFA);
    foreach(var st in states){
        if(!reachable.Contains(st)){
            foreach(var alp in alphabet){
                Tuple<string, char> t = new Tuple<string, char>(st, char.Parse(alp));
                DFA.Remove(t);
            }
        }
    }
    string[] new_states = new string[reachable.Count];
    List<string> final_states_list = final_states.ToList();
    for(int i=0; i< reachable.Count; i++){
        new_states[i] = reachable[i];
    }
    List<List<string>> collect = new List<List<string>>();
    List<string> nonfinal = new List<string>();
    foreach(var item in new_states){
        if(!final_states_list.Contains(item))    
            nonfinal.Add(item);
    }
    collect.Add(nonfinal);
    List<string> final = new List<string>();
    foreach(var item1 in new_states){
        if(final_states_list.Contains(item1))
            final.Add(item1);
    }
    collect.Add(final);
    int u = Classify(collect, alphabet, DFA);
    Console.WriteLine(u.ToString());
}