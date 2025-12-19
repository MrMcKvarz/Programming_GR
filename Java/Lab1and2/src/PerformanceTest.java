import java.util.stream.IntStream;

public class PerformanceTest {

    public static void main(String[] args) {
        int[] array = IntStream.range(0, 10_000).toArray();

        long t1 = System.nanoTime();
        int sum1 = 0;
        for (int x : array) {
            if (x % 2 == 0) sum1 += x * x;
        }
        long t2 = System.nanoTime();

        long t3 = System.nanoTime();
        int sum2 = IntStream.of(array)
                .filter(x -> x % 2 == 0)
                .map(x -> x * x)
                .sum();
        long t4 = System.nanoTime();

        System.out.println("Loop time:   " + (t2 - t1));
        System.out.println("Stream time: " + (t4 - t3));
    }
}