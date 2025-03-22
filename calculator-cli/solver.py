from lark import Lark
from lark import Transformer
from fractions import Fraction

'''Operation parser. It parses operation that uses add, division, multiplication
and substraction functions.'''
operation_parser = Lark(r"""

    ?start: sum 

    ?sum: product 
       | sum "+" atom       -> add
       | sum "-" atom       -> sub 

    ?product: atom
       | product "*" atom   -> mul
       | product "/" atom   -> div

    ?atom: INT              -> num 
        | "-" atom          -> neg
        | "(" sum ")"

    %import common.INT
    %import common.WS_INLINE

    %ignore WS_INLINE

    """)


class RationalMathSolver(Transformer):
    '''Solver for operations that use rational numbers.
    A Transformer works depth-first, so it evaluates the base case first.'''

    def add(self, children):
        right, left = children
        return right + left

    def sub(self, children):
        right, left = children
        return right - left

    def mul(self, children):
        right, left = children
        return right * left

    def div(self, children):
        right, left = children
        return Fraction(right, left)

    def neg(self, n):
        n, = n
        return (-1) * n

    def num(self, n):
        # Base case 
        (n, ) = n
        return Fraction(int(n), 1)


def solve(operation: str):
    tree = operation_parser.parse(operation)
    solver = RationalMathSolver()
    return solver.transform(tree)
