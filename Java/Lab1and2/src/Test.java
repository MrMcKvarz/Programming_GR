import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;
import java.util.*;
import java.util.function.Function;

class PipelineTest {

    @Test
    void testPipelineResult() {
        List<Integer> data = List.of(1, 2, 3, 4);
        assertEquals(20, StreamPipeline.process(data));
    }

    @Test
    void testAndThen() {
        Function<Integer, Integer> f = x -> x * 2;
        Function<Integer, Integer> g = x -> x + 1;
        assertEquals(7, f.andThen(g).apply(3));
    }

    @Test
    void testCompose() {
        Function<Integer, Integer> f = x -> x * 2;
        Function<Integer, Integer> g = x -> x + 1;
        assertEquals(8, f.compose(g).apply(3));
    }

    @Test
    void testIdentityTransformer() {
        TInterface<Integer, Integer> id = TInterface.identity();
        assertEquals(5, id.apply(5));
    }

    @Test
    void testEmptyList() {
        assertEquals(0, StreamPipeline.process(List.of()));
    }
}