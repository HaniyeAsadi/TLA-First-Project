# TLA-First-Project
In this repository, I have 3 different projects that are implemented by C#.
Format of input finite automata in all questions is as follows:
1. At the first line, a set of states is defined, where the first state is the initial state of the automata.
2. At the second line, alphabet of automata is represented.
3. At the third line, final states of automata are represented.
4. At the fourth line, an integer of n is represented that shows number of transition rules of the automata.
5. At the next n lines,  each transition of the automata is represented. Each transition defines as follows:
qs,a,qd
It demonstrates that from qs state by reading the a alphabet, next state would be qd.
$ is demonstrates λ.

In the first question (Q1 file), we check that a string is accepted or rejected by the finite automata.
The input will be a finite automata, and a string to check. Then the output will be "Accpeted" or "Rejected" depending on the string and the automata.
One input and output example is represented:
input:
{A,B}
{a,b}
{B}
4
A,b,A
A,a,B
B,a,B
B,b,B
bbb
output:
Rejected

In the second question (Q2 file), we try converting an NFA to a DFA.
The input will be just a finite automata. And the output will be an integer representing number of states of DFA.
input:
{q0,q1,q2,q3,q4}
{a,b}
{q1,q3}
6
q0,a,q1
q1,b,q2
q1,$,q3
q3,b,q4
q2,a,q3
q4,a,q2
output:
8

In the third question (Q3 file), we try minimizing the DFA to the simplest possible.
The input will be just a fininite automata. And the output will be an integer that shows number of minimized DFA states.
input:
{q0,q1,q2,q3,q4}
{0,1}
{q4}
10
q0,0,q1
q0,1,q3
q1,0,q2
q1,1,q4
q2,0,q1
q2,1,q4
q3,1,q4
q3,0,q2
q4,0,q4
q4,1,q4
output:
3