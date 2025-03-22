from calculator import calculate
from fractions import Fraction
import pytest

@pytest.mark.parametrize('operation, expected_result', [
    ('3 + 4', 7),
    ('(3) + (4)', 7),
    ('4 + 3', 7),
    ('0 + 3', 3),
    ('(4 + 3) + 10', 17),
    ('10 + (4 + 3)', 17),
    ('10 + (4 + 3) + 10', 27),
    ('10 + 4 + 3 + 10', 27),
])
def test_sum(operation: str, expected_result):
    assert calculate(operation) == expected_result 

@pytest.mark.parametrize('operation, expected_result', [
    ('2 * 3', 6),
    ('(3) * (2)', 6),
    ('2 * 3', 6),
    ('0 * 3', 0),
    ('(2 * 1) * 10', 20),
    ('10 * (2 * 3)', 60),
    ('10 * (5 * 2) * 2', 200),
    ('10 * 5 * 2 * 2', 200),
])
def test_mul(operation: str, expected_result):
    assert calculate(operation) == expected_result 


@pytest.mark.parametrize('operation, expected_result', [
    ('2 - 3', -1),
    ('2 - (3 - 10)', 9),
    ('-(-3)', 3),
    ('-3', -3),
])
def test_sub(operation: str, expected_result):
    assert calculate(operation) == expected_result 


@pytest.mark.parametrize('operation, expected_result', [
    ('2 / 3', Fraction(2,3)),
    ('2 / (3 + 2)', Fraction(2,5)),
    ('(3 + 2) / (3 + 2)', 1),
])
def test_div(operation: str, expected_result):
    assert calculate(operation) == expected_result 

