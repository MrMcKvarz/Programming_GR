public class Main {

    public static int square(int x) {
        return x * x;
    }

    public int increment(int x) {
        return x + 1;
    }

    public static void main(String[] args) {
        TInterface<Integer, Integer> lambda = x -> x * 2;
        TInterface<Integer, Integer> staticRef = Main::square;

        Main demo = new Main();
        TInterface<Integer, Integer> instanceRef = demo::increment;

        System.out.println(lambda.apply(5));
        System.out.println(staticRef.apply(5));
        System.out.println(instanceRef.apply(5));
    }
}