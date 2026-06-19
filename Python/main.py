from Logger import Logger

class InsufficientFundsError(Exception):

    def __init__(self, balance: float, requested: float, account_number: str):
        self.balance = balance
        self.requested = requested
        self.account_number = account_number
        super().__init__(
            f"Недостатньо коштів на рахунку {account_number}. "
            f"Баланс: {balance}, Запитувана сума: {requested}"
        )

class BankAccount:
    def __init__(self, account_number: str, initial_balance: float = 0.0):
        self.account_number = account_number
        self.balance = initial_balance
        self._logger = Logger()  # Отримуємо єдиний екземпляр логера
        self._logger.log(f"Створено рахунок {self.account_number} з балансом {self.balance}")

    def deposit(self, amount: float) -> None:
        if amount <= 0:
            raise ValueError("Сума поповнення має бути більшою за нуль.")

        self.balance += amount
        self._logger.log(
            f"Рахунок {self.account_number}: успішне поповнення на {amount}. "
            f"Новий баланс: {self.balance}"
        )

    def withdraw(self, amount: float) -> None:
        if amount <= 0:
            raise ValueError("Сума зняття має бути більшою за нуль.")

        if amount > self.balance:
            # Логуємо невдалу спробу перед тим, як кинути виняток
            self._logger.log(
                f"ПОМИЛКА: Спроба зняття {amount} з рахунку {self.account_number} відхилена. "
                f"Поточний баланс: {self.balance}"
            )
            raise InsufficientFundsError(self.balance, amount, self.account_number)

        self.balance -= amount
        self._logger.log(
            f"Рахунок {self.account_number}: успішне зняття {amount}. "
            f"Новий баланс: {self.balance}"
        )

def main():
    account = BankAccount(account_number="UA12345", initial_balance=500.0)

    account.deposit(250.0)
    account.withdraw(100.0)

    try:
        account.withdraw(1000.0)  # Перевищує ліміт (зараз на балансі 650.0)
    except InsufficientFundsError as e:
        print(f"Перехоплено виняток: {e}")

    another_logger_reference = Logger()
    original_logger = Logger()

    is_same = another_logger_reference is original_logger
    print(f"Чи є екземпляри логера ідентичними? {is_same}")
    print(f"Всього записів у глобальному логу: {len(original_logger.logs)}")


if __name__ == "__main__":
    main()