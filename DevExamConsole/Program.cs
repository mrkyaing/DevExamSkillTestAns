Console.WriteLine("Answer :Exercise Number 1");
int total = 0;
total=Enumerable.Range(0, 1000).Where(n => n % 3 == 0 || n % 5 == 0).Sum();
Console.WriteLine($"The sum of all the multiples of 3 or 5 below 1000 :{total}");
Console.WriteLine("=======xxxxxxxxxxxxx=======");

Console.WriteLine("Answer :Exercise Number 2");
int sumResult=FibonacciSequenace(400);
Console.WriteLine($"The sum of the even-valued terms {sumResult}");
Console.WriteLine("=======xxxxxxxxxxxxx=======");
static int FibonacciSequenace(int length) {
    if (length < 2)
        return 0;
    long f1 = 0, f2 = 2;
    long theSumResult = f1 + f2;

    while (f2 <= length) {
        long fn = 4 * f2 + f1;
        if (fn > length)
            break;
        f1 = f2;
        f2 = fn;
        theSumResult += f2;
    }
    return (int)theSumResult;
}