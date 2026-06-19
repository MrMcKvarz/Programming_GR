from abc import ABC, abstractmethod

class Document(ABC):

    @abstractmethod
    def print(self) -> None:

        pass

    def prepare_for_printing(self) -> None:

        print(f"--- Підготовка до друку [{self.__class__.__name__}] ---")
        print("Перевірка форматування та черги принтера...")

        self.print()
        print("Друк успішно завершено.\n")


class PDFDocument(Document):
    def print(self) -> None:
        print("Рендеринг векторних шрифтів та друк PDF-документа.")


class WordDocument(Document):
    def print(self) -> None:
        print("Розмітка тексту, перевірка полів та друк документа Word (.docx).")


class ExcelDocument(Document):
    def print(self) -> None:
        print("Форматування сітки, обчислення формул та друк таблиці Excel (.xlsx).")

class DocumentFactory:


    @staticmethod
    def create_document(doc_type: str) -> Document:

        target_type = doc_type.strip().lower()

        if target_type == "pdf":
            return PDFDocument()
        elif target_type in ("word", "docx"):
            return WordDocument()
        elif target_type in ("excel", "xlsx"):
            return ExcelDocument()
        else:
            raise ValueError(f"Помилка: Невідомий тип документа '{doc_type}'")


if __name__ == "__main__":
    requested_documents = ["pdf", "word", "excel"]

    for doc_name in requested_documents:
        try:
            document_obj = DocumentFactory.create_document(doc_name)

            document_obj.prepare_for_printing()

        except ValueError as e:
            print(e)

    try:
        invalid_doc = DocumentFactory.create_document("powerpoint")
    except ValueError as e:
        print(f"Перехоплено виняток: {e}")