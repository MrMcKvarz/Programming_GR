import java.util.*;
import java.util.function.*;
import java.util.stream.*;

public class StreamPipeline {

    public static int process(List<Integer> data) {
        Predicate<Integer> isEven = x -> x % 2 == 0;
        Function<Integer, Integer> square = x -> x * x;
        Consumer<Integer> logger = x -> {};

        return data.stream()
                .filter(isEven)
                .map(square)
                .peek(logger)
                .reduce(0, Integer::sum);
    }
}