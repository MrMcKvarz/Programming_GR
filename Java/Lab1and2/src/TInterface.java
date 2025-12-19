@FunctionalInterface
public interface TInterface<T, R> {

    R apply(T value);

    // метод за замовчуванням
    default <V> TInterface<T, V> andThen(TInterface<R, V> after) {
        return v -> after.apply(this.apply(v));
    }

    // статичний метод
    static <T> TInterface<T, T> identity() {
        return t -> t;
    }
}
